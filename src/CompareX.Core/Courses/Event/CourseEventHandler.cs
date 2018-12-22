using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.Courses
{
    public class CourseEventHandler : IEventHandler<EntityCreatedEventData<Course>>, IEventHandler<CourseDateChangedEvent>, IEventHandler<CourseCancelledEvent>, ITransientDependency
    {
        public void HandleEvent(EntityCreatedEventData<Course> eventData)
        {
            throw new NotImplementedException();
        }

        public void HandleEvent(CourseDateChangedEvent eventData)
        {
            throw new NotImplementedException();
        }

        public void HandleEvent(CourseCancelledEvent eventData)
        {
            throw new NotImplementedException();
        }
    }
}
