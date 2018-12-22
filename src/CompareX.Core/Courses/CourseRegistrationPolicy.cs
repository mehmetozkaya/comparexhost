using System;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.UI;
using CompareX.Authorization.Users;

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

        public Task CheckRegistrationAttemptAsync(Course checkCourse, User user)
        {
            if (checkCourse == null) { throw new ArgumentNullException("checkCourse"); }
            if (user == null) { throw new ArgumentNullException("user"); }

            CheckCourseDate(checkCourse);
            await CheckCaseRegistrationFrequencyAsync(user);

        }

        private static void CheckCourseDate(Course checkCourse)
        {
            if (checkCourse.IsInPast())
            {
                throw new UserFriendlyException("Can not register event in the past!");
            }
        }
    }
}
