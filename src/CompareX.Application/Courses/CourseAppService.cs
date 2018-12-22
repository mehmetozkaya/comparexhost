using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CompareX.Courses.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompareX.Courses
{
    public class CourseAppService : CompareXAppServiceBase, ICourseAppService
    {
        private readonly ICourseManager _courseManager;
        private readonly IRepository<Course> _caseRepository;

        public CourseAppService(ICourseManager courseManager, IRepository<Course> caseRepository)
        {
            _courseManager = courseManager ?? throw new ArgumentNullException(nameof(courseManager));
            _caseRepository = caseRepository ?? throw new ArgumentNullException(nameof(caseRepository));
        }

        public async Task<ListResultDto<CourseDto>> GetListAsync(GetCourseListInput input)
        {
            var cases = await _caseRepository
                .GetAll()
                .Include(e => e.Registrations)
                .WhereIf(!input.IncludeCanceledEvents, e => !e.IsCancelled)
                .OrderByDescending(e => e.CreationTime)
                .Take(64)
                .ToListAsync();

            return new ListResultDto<CaseListDto>(cases.MapTo<List<CaseListDto>>());
        }

    }
}
