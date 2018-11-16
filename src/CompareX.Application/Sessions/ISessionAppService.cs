using System.Threading.Tasks;
using Abp.Application.Services;
using CompareX.Sessions.Dto;

namespace CompareX.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
