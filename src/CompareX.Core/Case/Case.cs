using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompareX.Case
{
    [Table("AppCases")]
    public class Case : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        public const int MaxTitleLength = 128;
        public const int MaxDescriptionLength = 2048;

        public virtual int TenantId { get; set; }

        [Required]
        [StringLength(MaxTitleLength)]
        public virtual string Title { get; protected set; }

        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; protected set; }

        public virtual DateTime Date { get; protected set; }

        public virtual bool IsCancelled { get; protected set; }

        [Range(0, int.MaxValue)]
        public virtual int MaxRegistrationCount { get; protected set; }

        [ForeignKey("CaseId")]
        public virtual ICollection<CaseRegistration> Registrations { get; protected set; }

        protected Case()
        {
        }

        public static Case Create(int tenantId, string title, DateTime date, string description = null, int maxRegistrationCount = 0)
        {
            var newCase = new Case
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Title = title,
                Description = description,
                MaxRegistrationCount = maxRegistrationCount
            };

            newCase.SetDate(date);
            newCase.Registrations = new Collection<CaseRegistration>();
            return newCase;
        }

        public bool IsInPast()
        {
            return Date < Clock.Now;
        }

        public bool IsAllowedCancellationTimeEnded()
        {
            return Date.Subtract(Clock.Now).TotalHours <= 2.0; //2 hours can be defined as Event property and determined per event
        }

        public void ChangeDate(DateTime date)
        {
            if (date == Date)
            {
                return;
            }

            SetDate(date);

            // DomainEvents.EventBus.Trigger(new CaseDateChangedEvent(this));
        }

        internal void Cancel()
        {
            AssertNotInPast();
            IsCancelled = true;
        }

        private void SetDate(DateTime date)
        {
            AssertNotCancelled();

            if (date < Clock.Now)
            {
                throw new UserFriendlyException("Can not set an event's date in the past!");
            }

            if (date <= Clock.Now.AddHours(3)) //3 can be configurable per tenant
            {
                throw new UserFriendlyException("Should set an event's date 3 hours before at least!");
            }

            Date = date;

            // DomainEvents.EventBus.Trigger(new EventDateChangedEvent(this));
        }

        private void AssertNotInPast()
        {
            if (IsInPast())
            {
                throw new UserFriendlyException("This event was in the past");
            }
        }

        private void AssertNotCancelled()
        {
            if (IsCancelled)
            {
                throw new UserFriendlyException("This event is canceled!");
            }
        }      
    }
}
