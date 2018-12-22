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

namespace CompareX.Courses
{
    public class CourseAppService : CompareXAppServiceBase, ICourseAppService
    {
        private readonly ICourseManager _courseManager;
        private readonly IRepository<Course> _courseRepository;

        public CourseAppService(ICourseManager courseManager, IRepository<Course> caseRepository)
        {
            _courseManager = courseManager ?? throw new ArgumentNullException(nameof(courseManager));
            _courseRepository = caseRepository ?? throw new ArgumentNullException(nameof(caseRepository));
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
        }

        public async Task<CourseDetailOutput> GetDetailAsync(EntityDto input)
        {
            var detailedCourse = await _courseRepository
                .GetAll()
                .Include(e => e.Registrations)
                .ThenInclude(r => r.User)
                .Where(e => e.Id == input.Id)
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
            var newCourse = Course.Create(1, input.Title, input.Date, input.Description, input.MaxRegistrationCount);
            await _courseManager.CreateAsync(newCourse);
        }


    }
}
