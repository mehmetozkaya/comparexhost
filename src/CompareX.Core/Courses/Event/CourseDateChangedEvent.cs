using Abp.Events.Bus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.Courses
{
    public class CourseDateChangedEvent : EntityEventData<Course>
    {
        public CourseDateChangedEvent(Course entity)
            : base(entity)
        {

        }
    }
}
