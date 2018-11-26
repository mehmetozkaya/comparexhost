using Abp.Domain.Services;
using CompareX.Authorization.Users;
using System.Threading.Tasks;

namespace CompareX.Case
{
    public interface ICaseRegistrationPolicy : IDomainService
    {
        Task CheckRegistrationAttemptAsync(Case checkCase, User user);
    }
}
