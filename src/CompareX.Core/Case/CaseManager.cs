using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.UI;
using CompareX.Authorization.Users;

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
            var @event = await _caseRepository.FirstOrDefaultAsync(id);
            if (@event == null)
            {
                throw new UserFriendlyException("Could not found the event, maybe it's deleted!");
            }

            return @event;
        }

        // TODO : here

        public Task CreateAsync(Case newCase)
        {
            throw new NotImplementedException();
        }

        public void Cancel(Case cancelCase)
        {
            throw new NotImplementedException();
        }

        public Task<CaseRegistration> RegisterAsync(Case registerCase, User user)
        {
            throw new NotImplementedException();
        }

        public Task CancelRegistrationAsync(Case cancelRegisterCase, User user)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<User>> GetRegisteredUsersAsync(Case registeredCase)
        {
            throw new NotImplementedException();
        }

        

    }
}
