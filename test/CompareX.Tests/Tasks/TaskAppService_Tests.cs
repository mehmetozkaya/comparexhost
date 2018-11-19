using CompareX.Tasks;
using CompareX.Tasks.Dto;
using Shouldly;
using Xunit;

namespace CompareX.Tests.Tasks
{
    public class TaskAppService_Tests : CompareXTestBase
    {
        private readonly ITaskAppService _taskAppService;

        public TaskAppService_Tests()
        {
            _taskAppService = Resolve<ITaskAppService>();
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Get_All_Tasks()
        {
            // Act
            var output = await _taskAppService.GetAll(new GetAllTasksInput());

            // Assert
            output.Items.Count.ShouldBe(2);             
        }

    }
}
