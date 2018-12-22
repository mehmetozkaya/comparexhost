using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.Courses.Dto
{
    public class CourseDtoCollection
    {
        [AutoMapFrom(typeof(Course))]
        public class CourseDto : FullAuditedEntityDto
        {
            public string Title { get; set; }

            public string Description { get; set; }

            public DateTime Date { get; set; }

            public bool IsCancelled { get; set; }

            public virtual int MaxRegistrationCount { get; protected set; }

            public int RegistrationsCount { get; set; }
        }
    }
}
