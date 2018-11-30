using Abp.Events.Bus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.Case
{
    public class CaseCancelledEvent : EntityEventData<Case>
    {
        public CaseCancelledEvent(Case entity) : base(entity)
        {
        }
    }
}
