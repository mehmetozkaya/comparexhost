using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Castle.Core.Logging;
using CompareX.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.Case.Notifications
{
    public class CaseUserEmailer : IEventHandler<EntityCreatedEventData<Case>>,
        IEventHandler<CaseDateChangedEvent>,
        IEventHandler<CaseCancelledEvent>,
        ITransientDependency
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
    }
}
