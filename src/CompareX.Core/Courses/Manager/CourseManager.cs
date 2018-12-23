using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Events.Bus;
using Abp.UI;
using CompareX.Authorization.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareX.Courses
{
    public class CourseManager2 : DomainService, ICourseManager2
    {
        private readonly ICourseRegistrationPolicy _registrationPolicy;

        private readonly IRepository<CourseRegistration> _courseRegistrationRepository;
        private readonly IRepository<Course> _courseRepository;


        public CourseManager2(IRepository<CourseRegistration> courseRegistrationRepository, IRepository<Course> courseRepository)
        {
            // _registrationPolicy = registrationPolicy ?? throw new ArgumentNullException(nameof(registrationPolicy));

            _courseRegistrationRepository = courseRegistrationRepository ?? throw new ArgumentNullException(nameof(courseRegistrationRepository));
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        }

        public Task<Course> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
    }

    public class CourseManager : DomainService, ICourseManager
    {
        public IEventBus EventBus { get; set; }

        private readonly ICourseRegistrationPolicy _registrationPolicy;
        private readonly IRepository<CourseRegistration> _courseRegistrationRepository;
        private readonly IRepository<Course> _courseRepository;

        public CourseManager(ICourseRegistrationPolicy registrationPolicy, IRepository<CourseRegistration> courseRegistrationRepository, IRepository<Course> courseRepository)
        {
            _registrationPolicy = registrationPolicy ?? throw new ArgumentNullException(nameof(registrationPolicy));
            _courseRegistrationRepository = courseRegistrationRepository ?? throw new ArgumentNullException(nameof(courseRegistrationRepository));
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));

            EventBus = NullEventBus.Instance;
        }



        public async Task<Course> GetAsync(int id)
        {
            var getCourse = await _courseRepository.FirstOrDefaultAsync(id);
            if (getCourse == null)
            {
                throw new UserFriendlyException("Could not found the event, maybe it's deleted!");
            }

            return getCourse;
        }

        public async Task CreateAsync(Course newCourse)
        {
            await _courseRepository.InsertAsync(newCourse);
        }

        public void Cancel(Course cancelCourse)
        {
            cancelCourse.Cancel();
            EventBus.Trigger(new CourseCancelledEvent(cancelCourse));
        }

        public async Task<CourseRegistration> RegisterAsync(Course registerCourse, User user)
        {
            return await _courseRegistrationRepository.InsertAsync(
                    await CourseRegistration.CreateAsync(registerCourse, user, _registrationPolicy)
                );
        }

        public async Task CancelRegistrationAsync(Course cancelRegisterCourse, User user)
        {
            var registration = await _courseRegistrationRepository.FirstOrDefaultAsync(r => r.CourseId == cancelRegisterCourse.Id && r.UserId == user.Id);
            if (registration == null)
            {
                //No need to cancel since there is no such a registration
                return;
            }

            await registration.CancelAsync(_courseRegistrationRepository);
        }

        public async Task<IReadOnlyList<User>> GetRegisteredUsersAsync(Course registeredCourse)
        {
            return await _courseRegistrationRepository
                .GetAll()
                .Include(registration => registration.User)
                .Where(registration => registration.CourseId == registeredCourse.Id)
                .Select(registration => registration.User)
                .ToListAsync();
        }

    }
}
