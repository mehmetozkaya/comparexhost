using Abp.Events.Bus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.Case
{
    public class CaseDateChangedEvent : EntityEventData<Case>
    {
        public CaseDateChangedEvent(Case entity)
            :base(entity)
        {

        }        

    }
}
