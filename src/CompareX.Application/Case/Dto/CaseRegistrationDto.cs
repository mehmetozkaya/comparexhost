using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.Case.Dto
{
    [AutoMapFrom(typeof(CaseRegistration))]
    public class CaseRegistrationDto : CreationAuditedEntityDto
    {
        public virtual Guid CaseId { get; protected set; }

        public virtual long UserId { get; protected set; }

        public virtual string UserName { get; protected set; }

        public virtual string UserSurname { get; protected set; }
    }
}
