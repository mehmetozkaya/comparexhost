using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.UI;
using CompareX.Authorization.Users;
using Microsoft.EntityFrameworkCore;

namespace CompareX.Case
{
    public class CaseManager : ICaseManager
    {
        public IEventBus EventBus { get; set; }

        private readonly ICaseRegistrationPolicy _registrationPolicy;
        private readonly IRepository<CaseRegistration> _caseRegistrationRepository;
        private readonly IRepository<Case, Guid> _caseRepository;

        public CaseManager(ICaseRegistrationPolicy registrationPolicy, IRepository<CaseRegistration> caseRegistrationRepository, IRepository<Case, Guid> caseRepository)
        {            
            _registrationPolicy = registrationPolicy ?? throw new ArgumentNullException(nameof(registrationPolicy));
            _caseRegistrationRepository = caseRegistrationRepository ?? throw new ArgumentNullException(nameof(caseRegistrationRepository));
            _caseRepository = caseRepository ?? throw new ArgumentNullException(nameof(caseRepository));

            EventBus = NullEventBus.Instance;
        }

        public async Task<Case> GetAsync(Guid id)
        {
            var getCase = await _caseRepository.FirstOrDefaultAsync(id);
            if (getCase == null)
            {
                throw new UserFriendlyException("Could not found the event, maybe it's deleted!");
            }

            return getCase;
        }        

        public async Task CreateAsync(Case newCase)
        {
            await _caseRepository.InsertAsync(newCase);
        }

        public void Cancel(Case cancelCase)
        {
            cancelCase.Cancel();
            EventBus.Trigger(new CaseCancelledEvent(cancelCase));
        }

        public async Task<CaseRegistration> RegisterAsync(Case registerCase, User user)
        {
            return await _caseRegistrationRepository.InsertAsync(
                    await CaseRegistration.CreateAsync(registerCase, user, _registrationPolicy)
                );
        }

        public async Task CancelRegistrationAsync(Case cancelRegisterCase, User user)
        {
            var registration = await _caseRegistrationRepository.FirstOrDefaultAsync(r => r.CaseId == cancelRegisterCase.Id && r.UserId == user.Id);
            if(registration == null)
            {
                //No need to cancel since there is no such a registration
                return;
            }

            await registration.CancelAsync(_caseRegistrationRepository);
        }

        public async Task<IReadOnlyList<User>> GetRegisteredUsersAsync(Case registeredCase)
        {
            return await _caseRegistrationRepository
                .GetAll()
                .Include(registration => registration.User)
                .Where(registration => registration.CaseId == registeredCase.Id)
                .Select(registration => registration.User)
                .ToListAsync();
        }
        

    }
}
