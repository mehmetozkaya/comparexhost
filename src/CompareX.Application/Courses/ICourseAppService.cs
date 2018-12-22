using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompareX.Courses
{
    public interface ICourseAppService : IApplicationService
    {
        Task<ListResultDto<CourseDto>> GetListAsync(GetCourseInput input);

        Task<CourseDetailOutput> GetDetailAsync(EntityDto input);

        Task CreateAsync(CreateCourseInput input);

        Task CancelAsync(EntityDto input);

        Task<CourseRegisterOutput> RegisterAsync(EntityDto input);

        Task CancelRegistrationAsync(EntityDto input);
    }
}
