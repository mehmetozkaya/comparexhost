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

namespace CompareX.Case.Notifications
{
    public class CaseUserEmailer : IEventHandler<CaseDateChangedEvent>, IEventHandler<CaseCancelledEvent>, ITransientDependency
    {
        public ILogger Logger { get; set; }

        private readonly ICaseManager _caseManager;
        private readonly UserManager _userManager;

        public CaseUserEmailer(ICaseManager caseManager, UserManager userManager)
        {
            _caseManager = caseManager ?? throw new ArgumentNullException(nameof(caseManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

            Logger = NullLogger.Instance;            
        }

        //[UnitOfWork]
        //public virtual void HandleEvent(EntityCreatedEventData<Case> caseData)
        //{
        //    //TODO: Send email to all tenant users as a notification
        //    var users = _userManager
        //      .Users
        //      .Where(u => u.TenantId == caseData.Entity.TenantId)
        //      .ToList();

        //    foreach (var user in users)
        //    {
        //        var message = string.Format("Hey! There is a new event '{0}' on {1}! Want to register?", caseData.Entity.Title, caseData.Entity.Date);
        //        Logger.Debug(string.Format("TODO: Send email to {0} -> {1}", user.EmailAddress, message));
        //    }
        //}

        public void HandleEvent(CaseDateChangedEvent eventData)
        {
            //TODO: Send email to all registered users!
            var registeredUsers = AsyncHelper.RunSync(() => _caseManager.GetRegisteredUsersAsync(eventData.Entity));
            foreach (var user in registeredUsers)
            {
                var message = eventData.Entity.Title + " event's date is changed! New date is: " + eventData.Entity.Date;
                Logger.Debug(string.Format("TODO: Send email to {0} -> {1}", user.EmailAddress, message));
            }
        }

        public void HandleEvent(CaseCancelledEvent eventData)
        {
            //TODO: Send email to all registered users!
            var registeredUsers = AsyncHelper.RunSync(() => _caseManager.GetRegisteredUsersAsync(eventData.Entity));
            foreach (var user in registeredUsers)
            {
                var message = eventData.Entity.Title + " event is canceled!";
                Logger.Debug(string.Format("TODO: Send email to {0} -> {1}", user.EmailAddress, message));
            }
        }
    }
}
