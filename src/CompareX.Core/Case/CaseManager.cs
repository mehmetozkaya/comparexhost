using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using CompareX.Authorization.Users;

namespace CompareX.Case
{
    public class CaseManager : ICaseManager
    {
        public IEventBus EventBus { get; set; }

        private readonly ICaseRegistrationPolicy _registrationPolicy;
        private readonly IRepository<CaseRegistration> _eventRegistrationRepository;
        private readonly IRepository<Case, Guid> _eventRepository;


        public void Cancel(Case cancelCase)
        {
            throw new NotImplementedException();
        }

        public Task CancelRegistrationAsync(Case cancelRegisterCase, User user)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Case newCase)
        {
            throw new NotImplementedException();
        }

        public Task<Case> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<User>> GetRegisteredUsersAsync(Case registeredCase)
        {
            throw new NotImplementedException();
        }

        public Task<CaseRegistration> RegisterAsync(Case registerCase, User user)
        {
            throw new NotImplementedException();
        }
    }
}
