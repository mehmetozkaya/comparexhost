using Abp.Events.Bus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.Courses
{
    public class CourseCancelledEvent : EntityEventData<Course>
    {
        public CourseCancelledEvent(Course entity) : base(entity)
        {
        }
    }
}
