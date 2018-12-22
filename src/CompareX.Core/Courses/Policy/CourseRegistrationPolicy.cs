using System;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Timing;
using Abp.UI;
using CompareX.Authorization.Users;
using CompareX.Configuration;

namespace CompareX.Courses
{
    public class CourseRegistrationPolicy : ICourseRegistrationPolicy
    {
        private readonly IRepository<CourseRegistration> _courseRegistrationRepository;
        private readonly ISettingManager _settingManager;

        public CourseRegistrationPolicy(IRepository<CourseRegistration> courseRegistrationRepository, ISettingManager settingManager)
        {
            _courseRegistrationRepository = courseRegistrationRepository ?? throw new ArgumentNullException(nameof(courseRegistrationRepository));
            _settingManager = settingManager ?? throw new ArgumentNullException(nameof(settingManager));
        }

        public async Task CheckRegistrationAttemptAsync(Course checkCourse, User user)
        {
            if (checkCourse == null) { throw new ArgumentNullException("checkCourse"); }
            if (user == null) { throw new ArgumentNullException("user"); }

            CheckCourseDate(checkCourse);
            await CheckCourseRegistrationFrequencyAsync(user);
        }

        private static void CheckCourseDate(Course checkCourse)
        {
            if (checkCourse.IsInPast())
            {
                throw new UserFriendlyException("Can not register event in the past!");
            }
        }

        private async Task CheckCourseRegistrationFrequencyAsync(User user)
        {
            var oneMonthAgo = Clock.Now.AddDays(-30);
            var maxAllowedEventRegistrationCountInLast30DaysPerUser = await _settingManager.GetSettingValueAsync<int>(AppSettingNames.MaxAllowedEventRegistrationCountInLast30DaysPerUser);
            if (maxAllowedEventRegistrationCountInLast30DaysPerUser > 0)
            {
                var registrationCountInLast30Days = await _courseRegistrationRepository.CountAsync(r => r.UserId == user.Id && r.CreationTime >= oneMonthAgo);
                if (registrationCountInLast30Days > maxAllowedEventRegistrationCountInLast30DaysPerUser)
                {
                    throw new UserFriendlyException(string.Format("Can not register to more than {0}", maxAllowedEventRegistrationCountInLast30DaysPerUser));
                }
            }
        }
    }
}
