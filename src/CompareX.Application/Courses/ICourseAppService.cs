using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using CompareX.Courses.Dto;

namespace CompareX.Courses
{
    public interface ICourseAppService : IApplicationService
    {
        Task<ListResultDto<CourseDto>> GetListAsync(GetCourseListInput input);

        Task<CourseDetailOutput> GetDetailAsync(EntityDto input);

        Task CreateAsync(CreateCourseInput input);

        Task CancelAsync(EntityDto input);

        Task<CourseRegisterOutput> RegisterAsync(EntityDto input);

        Task CancelRegistrationAsync(EntityDto input);
    }   
}
