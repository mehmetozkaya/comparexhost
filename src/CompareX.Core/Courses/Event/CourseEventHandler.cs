using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Threading;
using Castle.Core.Logging;
using CompareX.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareX.Courses
{
    public class CourseEventHandler : IEventHandler<EntityCreatedEventData<Course>>, IEventHandler<CourseDateChangedEvent>, IEventHandler<CourseCancelledEvent>, ITransientDependency
    {
        public ILogger Logger { get; set; }

        private readonly ICourseManager _courseManager;
        private readonly UserManager _userManager;

        public CourseEventHandler(ICourseManager courseManager, UserManager userManager)
        {            
            _courseManager = courseManager ?? throw new ArgumentNullException(nameof(courseManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

            Logger = NullLogger.Instance;
        }

        [UnitOfWork]
        public void HandleEvent(EntityCreatedEventData<Course> courseData)
        {
            //TODO: Send email to all tenant users as a notification
            var users = _userManager
              .Users
              .Where(u => u.TenantId == courseData.Entity.TenantId)
              .ToList();

            foreach (var user in users)
            {
                var message = string.Format("Hey! There is a new event '{0}' on {1}! Want to register?", courseData.Entity.Title, courseData.Entity.Date);
                Logger.Debug(string.Format("TODO: Send email to {0} -> {1}", user.EmailAddress, message));
            }            
        }

        public void HandleEvent(CourseDateChangedEvent eventData)
        {
            //TODO: Send email to all registered users!
            var registeredUsers = AsyncHelper.RunSync(() => _courseManager.GetRegisteredUsersAsync(eventData.Entity));
            foreach (var user in registeredUsers)
            {
                var message = eventData.Entity.Title + " event's date is changed! New date is: " + eventData.Entity.Date;
                Logger.Debug(string.Format("TODO: Send email to {0} -> {1}", user.EmailAddress, message));
            }            
        }

        public void HandleEvent(CourseCancelledEvent eventData)
        {
            throw new NotImplementedException();
        }
    }
}
