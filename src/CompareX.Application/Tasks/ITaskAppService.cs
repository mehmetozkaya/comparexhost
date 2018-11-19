using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CompareX.Tasks.Dto;

namespace CompareX.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        System.Threading.Tasks.Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input);
        System.Threading.Tasks.Task Create(CreateTaskInput createTaskInput);
    }
}
