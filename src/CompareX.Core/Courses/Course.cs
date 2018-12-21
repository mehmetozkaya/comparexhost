using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompareX.Courses
{
    [Table("AppCourses")]
    public class Course : FullAuditedEntity, IMustHaveTenant
    {
        public int TenantId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
