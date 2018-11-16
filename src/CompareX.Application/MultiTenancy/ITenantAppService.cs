using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CompareX.MultiTenancy.Dto;

namespace CompareX.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
