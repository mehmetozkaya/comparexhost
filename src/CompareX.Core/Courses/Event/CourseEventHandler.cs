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
    }
}
