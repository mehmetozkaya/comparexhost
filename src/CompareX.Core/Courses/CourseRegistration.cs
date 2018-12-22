using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CompareX.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompareX.Courses
{
    [Table("AppCourseRegistrations")]
    public class CourseRegistration : FullAuditedEntity, IMustHaveTenant
    {
        public virtual int TenantId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; protected set; }
        public virtual int CourseId { get; protected set; }

        [ForeignKey("UserId")]
        public virtual User User { get; protected set; }
        public virtual long UserId { get; protected set; }

    }
}
