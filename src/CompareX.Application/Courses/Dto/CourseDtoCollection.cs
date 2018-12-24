using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CompareX.Courses.Dto
{   
    [AutoMapFrom(typeof(Course))]
    public class CourseDto : FullAuditedEntityDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        // TODOX
        //public string Location { get; set; }

        public DateTime Date { get; set; }

        public bool IsCancelled { get; set; }

        public virtual int MaxRegistrationCount { get; set; }

        public int RegistrationsCount { get; set; }
    }

    public class GetCourseListInput
    {
        public bool IncludeCanceledEvents { get; set; }
    }
        
    public class CourseDetailOutput : FullAuditedEntityDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        // TODOX
        //public string Location { get; set; }

        public DateTime Date { get; set; }

        public bool IsCancelled { get; set; }

        public virtual int MaxRegistrationCount { get; set; }

        public int RegistrationsCount { get; set; }

        public ICollection<CourseRegistrationDto> Registrations { get; set; }
    }

    [AutoMapFrom(typeof(CourseRegistration))]
    public class CourseRegistrationDto : CreationAuditedEntityDto
    {
        public virtual int CourseId { get; protected set; }

        public virtual long UserId { get; protected set; }

        public virtual string UserName { get; protected set; }

        public virtual string UserSurname { get; protected set; }
    }

    public class CreateCourseInput
    {
        [Required]
        [StringLength(Course.MaxTitleLength)]
        public string Title { get; set; }

        [StringLength(Course.MaxDescriptionLength)]
        public string Description { get; set; }

        // TODOX
        //public string Location { get; set; }

        public DateTime Date { get; set; }

        [Range(0, int.MaxValue)]
        public int MaxRegistrationCount { get; set; }
    }

    public class CourseRegisterOutput
    {
        public int RegistrationId { get; set; }
    }

}
