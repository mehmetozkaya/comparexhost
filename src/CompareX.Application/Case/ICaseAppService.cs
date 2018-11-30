using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CompareX.Case.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompareX.Case
{
    public interface ICaseAppService : IApplicationService
    {
        Task<ListResultDto<CaseListDto>> GetListAsync(GetCaseListInput input);

        Task<CaseDetailOutput> GetDetailAsync(EntityDto<Guid> input);

        Task CreateAsync(CreateCaseInput input);

        Task CancelAsync(EntityDto<Guid> input);

        Task<CaseRegisterOutput> RegisterAsync(EntityDto<Guid> input);

        Task CancelRegistrationAsync(EntityDto<Guid> input);
    }
}
