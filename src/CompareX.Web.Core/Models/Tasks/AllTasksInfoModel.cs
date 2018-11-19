using Abp.AutoMapper;
using CompareX.Tasks;
using CompareX.Tasks.Dto;
using System;

namespace CompareX.Models.Tasks
{
    [AutoMapFrom(typeof(TaskListDto))]
    public class AllTasksInfoModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public TaskState State { get; set; }

        public string StateDesc
        {
            get
            {
                switch (State)
                {
                    case TaskState.Open:
                        return "Open";
                    case TaskState.Completed:
                        return "Completed";
                    default:
                        return string.Empty;
                }
            }
        }

    }   
}
