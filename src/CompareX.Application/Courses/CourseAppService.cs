using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using CompareX.Courses.Dto;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using CompareX.Authorization.Users;
using CompareX.Case;
using Abp.Runtime.Session;

namespace CompareX.Courses
{
    public class CourseAppService : CompareXAppServiceBase, ICourseAppService
    {
        private readonly ICourseManager _courseManager;
        private readonly IRepository<Course> _courseRepository;

        public CourseAppService(ICourseManager courseManager, IRepository<Course> courseRepository)
        {
            _courseManager = courseManager ?? throw new ArgumentNullException(nameof(courseManager));
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        }

        public async Task<ListResultDto<CourseDto>> GetListAsync(GetCourseListInput input)
        {
            var courses = await _courseRepository
                .GetAll()
                .Include(e => e.Registrations)
                .WhereIf(!input.IncludeCanceledEvents, e => !e.IsCancelled)
                .OrderByDescending(e => e.CreationTime)
                .Take(64)
                .ToListAsync();

            var courseList = ObjectMapper.Map<List<CourseDto>>(courses);
            return new ListResultDto<CourseDto>(courseList);

            // return new ListResultDto<CourseDto>(courses.MapTo<List<CourseDto>>());            
        }

        public async Task<CourseDetailOutput> GetDetailAsync(EntityDto input)
        {
            var detailedCourse = await _courseRepository
                .GetAll()
                .Include(e => e.Registrations)
                .ThenInclude(r => r.User)
                .Where(e => e.Id == input.Id)
                //.WhereIf(AbpSession.TenantId.HasValue, e => e.TenantId == AbpSession.TenantId.Value)
                .FirstOrDefaultAsync();

            if (detailedCourse == null)
            {
                throw new UserFriendlyException("Could not found the event, maybe it's deleted.");
            }

            var courseDetail = ObjectMapper.Map<CourseDetailOutput>(detailedCourse);
            //return courseDetail;
            return detailedCourse.MapTo<CourseDetailOutput>();
        }

        public async Task CreateAsync(CreateCourseInput input)
        {
            var tenantId = 1;
            if (AbpSession.TenantId.HasValue)
                tenantId = AbpSession.TenantId.Value;

            var newCourse = Course.Create(tenantId, input.Title, input.Date, input.Description, input.MaxRegistrationCount);
            await _courseManager.CreateAsync(newCourse);
        }

        public async Task CancelAsync(EntityDto input)
        {
            var cancelCourse = await _courseManager.GetAsync(input.Id);
            _courseManager.Cancel(cancelCourse);
        }

        public async Task<CourseRegisterOutput> RegisterAsync(EntityDto input)
        {
            var registration = await RegisterAndSaveAsync(await _courseManager.GetAsync(input.Id), await GetCurrentUserAsync());

            return new CourseRegisterOutput
            {
                RegistrationId = registration.Id
            };
        }

        private async Task<CourseRegistration> RegisterAndSaveAsync(Course course, User user)
        {
            var registration = await _courseManager.RegisterAsync(course, user);
            await CurrentUnitOfWork.SaveChangesAsync();
            return registration;
        }

        public async Task CancelRegistrationAsync(EntityDto input)
        {
            await _courseManager.CancelRegistrationAsync(
                await _courseManager.GetAsync(input.Id),
                await GetCurrentUserAsync()
                );
        }

    }
}
