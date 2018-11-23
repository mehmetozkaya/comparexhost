using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CompareX.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace CompareX.Case
{
    [Table("AppCaseRegistrations")]
    public class CaseRegistration : CreationAuditedEntity, IMustHaveTenant
    {
        public virtual int TenantId { get; set; }

        [ForeignKey("CaseId")]
        public virtual Case Case { get; protected set; }
        public virtual Guid CaseId { get; protected set; }

        [ForeignKey("UserId")]
        public virtual User User { get; protected set; }
        public virtual long UserId { get; protected set; }

        protected CaseRegistration()
        {
        }

        public async Task<CaseRegistration> CreateAsync(Case newCase, User user, IEventRegistrationPolicy registrationPolicy)
        {
            await registrationPolicy.CheckRegistrationAttemptAsync(newCase, user);

            return new CaseRegistration
            {
                TenantId = newCase.TenantId,
                CaseId = newCase.Id,
                Case = newCase,
                UserId = user.Id,
                User = user
            };
        }


    }
}
