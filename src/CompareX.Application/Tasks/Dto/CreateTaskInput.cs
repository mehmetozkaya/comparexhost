using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CompareX.Tasks.Dto
{
    [AutoMapTo(typeof(Task))]
    public class CreateTaskInput
    {
        [Required]
        [StringLength(Task.MaxTitleLength)]
        public string Title { get; set; }

        [StringLength(Task.MaxDescriptionLength)]
        public string Description { get; set; }

        public Guid? AssignedPersonId { get; set; }
    }
}
