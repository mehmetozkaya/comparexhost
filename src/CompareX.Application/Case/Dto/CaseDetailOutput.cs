using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.Case.Dto
{
    public class CaseDetailOutput : FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsCancelled { get; set; }

        public virtual int MaxRegistrationCount { get; protected set; }

        public int RegistrationsCount { get; set; }

        public ICollection<CaseRegistrationDto> Registrations { get; set; }
    }
}
