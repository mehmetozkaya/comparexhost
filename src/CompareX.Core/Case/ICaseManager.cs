using Abp.Domain.Services;
using CompareX.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompareX.Case
{
    public interface ICaseManager : IDomainService
    {
        Task<Case> GetAsync(Guid id);
        Task CreateAsync(Case newCase);
        void Cancel(Case cancelCase);
        Task<CaseRegistration> RegisterAsync(Case registerCase, User user);
        Task CancelRegistrationAsync(Case cancelRegisterCase, User user);
        Task<IReadOnlyList<User>> GetRegisteredUsersAsync(Case registeredCase);
    }
}
