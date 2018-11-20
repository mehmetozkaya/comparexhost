using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CompareX.People;
using CompareX.Tasks.Dto;
using Microsoft.EntityFrameworkCore;


namespace CompareX.Tasks
{
    public class TaskAppService : CompareXAppServiceBase, ITaskAppService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IRepository<Person, Guid> _personRepository;

        public TaskAppService(ITaskRepository taskRepository, IRepository<Person, Guid> personRepository)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }

        public async Task<ListResultDto<TaskListDto>> GetAll(GetAllTasksInput input)
        {
            var tasks = await _taskRepository
                .GetAll()
                .Include(t => t.AssignedPerson)
                .WhereIf(input.State.HasValue, t => t.State == input.State.Value)
                .OrderByDescending(t => t.CreationTime)
                .ToListAsync();

            return new ListResultDto<TaskListDto>(
                ObjectMapper.Map<List<TaskListDto>>(tasks)
                );
        }

        public async System.Threading.Tasks.Task Create(CreateTaskInput createTaskInput)
        {
            var task = ObjectMapper.Map<Task>(createTaskInput);
            await _taskRepository.InsertAsync(task);
        }

        public GetTasksOutput GetTasks(GetTaskInput input)
        {
            var tasks = _taskRepository.GetAllWithPeople(input.AssignedPersonId, input.State);

            return new GetTasksOutput
            {
                Tasks = ObjectMapper.Map<List<TaskListDto>>(tasks)
            };
        }

        public void UpdateTask(UpdateTaskInput input)
        {
            Logger.Info("Updating a task for input: " + input);

            var task = _taskRepository.Get(input.TaskId);

            if (input.State.HasValue)
            {
                task.State = input.State.Value;
            }

            if (input.AssignedPersonId.HasValue)
            {
                task.AssignedPerson = _personRepository.Load(input.AssignedPersonId.Value);
            }

            //_taskRepository.Update(task);
            //We even do not call Update method of the repository.
            //Because an application service method is a 'unit of work' scope as default.
            //ABP automatically saves all changes when a 'unit of work' scope ends (without any exception).
        }

        public void CreateTask(CreateTaskInput input)
        {
            Logger.Info("Creating a task for input: " + input);

            var task = new Task { Description = input.Description };

            if (input.AssignedPersonId.HasValue)
            {
                task.AssignedPersonId = input.AssignedPersonId.Value;
            }

            //Saving entity with standard Insert method of repositories.
            _taskRepository.Insert(task);
        }
    }
}
