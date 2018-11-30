using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.Case
{
    public interface ICaseAppService : IApplicationService
    {
        Task<ListResultDto<EventListDto>> GetListAsync(GetEventListInput input);

        Task<EventDetailOutput> GetDetailAsync(EntityDto<Guid> input);

        Task CreateAsync(CreateEventInput input);

        Task CancelAsync(EntityDto<Guid> input);

        Task<EventRegisterOutput> RegisterAsync(EntityDto<Guid> input);

        Task CancelRegistrationAsync(EntityDto<Guid> input);
    }
}
