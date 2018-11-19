using CompareX.Models.Tasks;
using CompareX.Tasks;
using CompareX.Tasks.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TasksController : CompareXControllerBase
    {
        private readonly ITaskAppService _taskAppService;

        public TasksController(ITaskAppService taskAppService)
        {
            _taskAppService = taskAppService ?? throw new ArgumentNullException(nameof(taskAppService));
        }

        [HttpPost]
        public List<AllTasksInfoModel> GetAll([FromBody] GetAllTasksInput input)
        {
            var output = _taskAppService.GetAll(input);
            return ObjectMapper.Map<List<AllTasksInfoModel>>(output.Result.Items);
        }


        [HttpPost]
        public void Create([FromBody] CreateTaskInput input)
        {
            var output = _taskAppService.Create(input);
        }

    }
}
