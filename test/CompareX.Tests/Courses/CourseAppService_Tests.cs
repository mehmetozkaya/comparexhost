using CompareX.Courses;
using CompareX.Courses.Dto;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CompareX.Tests.Courses
{
    public class CourseAppService_Tests : CompareXTestBase
    {
        private readonly ICourseAppService _courseAppService;

        public CourseAppService_Tests()
        {
            _courseAppService = Resolve<ICourseAppService>();
        }

        [Fact]
        public async Task Should_Get_Test_Courses()
        {
            var output = await _courseAppService.GetListAsync(new GetCourseListInput());
            output.Items.Count.ShouldBe(4);
        }
    }    
}
