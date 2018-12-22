using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Repositories;
using Abp.UI;
using CompareX.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace CompareX.Courses
{
    [Table("CourseRegistrations")]
    public class CourseRegistration : FullAuditedEntity, IMustHaveTenant
    {
        public virtual int TenantId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; protected set; }
        public virtual int CourseId { get; protected set; }

        [ForeignKey("UserId")]
        public virtual User User { get; protected set; }
        public virtual long UserId { get; protected set; }

        protected CourseRegistration()
        {        
        }

        public static async Task<CourseRegistration> CreateAsync(Course newCourse, User user, ICourseRegistrationPolicy registrationPolicy)
        {
            await registrationPolicy.CheckRegistrationAttemptAsync(newCourse, user);

            return new CourseRegistration
            {
                TenantId = newCourse.TenantId,
                CourseId = newCourse.Id,
                Course = newCourse,
                UserId = user.Id,
                User = user
            };
        }

        public async Task CancelAsync(IRepository<CourseRegistration> repository)
        {
            if (repository == null) { throw new ArgumentNullException("repository"); }

            if (Course.IsInPast())
            {
                throw new UserFriendlyException("Can not cancel event which is in the past!");
            }

            if (Course.IsAllowedCancellationTimeEnded())
            {
                throw new UserFriendlyException("It's too late to cancel your registration!");
            }

            await repository.DeleteAsync(this);
        }

    }
}
