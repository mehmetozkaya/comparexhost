using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using Abp.UI;
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

        public static async Task<CaseRegistration> CreateAsync(Case newCase, User user, IEventRegistrationPolicy registrationPolicy)
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

        public async Task CancelAsync(IRepository<CaseRegistration> repository)
        {
            if (repository == null) { throw new ArgumentNullException("repository"); }

            if (Case.IsInPast())
            {
                throw new UserFriendlyException("Can not cancel event which is in the past!");
            }

            if (Case.IsAllowedCancellationTimeEnded())
            {
                throw new UserFriendlyException("It's too late to cancel your registration!");
            }

            await repository.DeleteAsync(this);
        }

    }

    // for temporary purpose
    public interface IEventRegistrationPolicy
    {
        Task CheckRegistrationAttemptAsync(Case newCase, User user);
    }
}
