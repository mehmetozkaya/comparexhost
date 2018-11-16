using System.Threading.Tasks;
using Abp.Application.Services;
using CompareX.Authorization.Accounts.Dto;

namespace CompareX.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
