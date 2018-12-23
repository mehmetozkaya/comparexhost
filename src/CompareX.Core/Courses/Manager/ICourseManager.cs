using Abp.Domain.Services;
using CompareX.Authorization.Users;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace CompareX.Courses
{
    public interface ICourseManager : IDomainService
    {
        Task<Course> GetAsync(int id);
        Task CreateAsync(Course newCourse);
        void Cancel(Course cancelCourse);
        Task<CourseRegistration> RegisterAsync(Course registerCourse, User user);
        Task CancelRegistrationAsync(Course cancelRegisterCourse, User user);
        Task<IReadOnlyList<User>> GetRegisteredUsersAsync(Course registeredCourse);
    }

    public interface ICourseManager2 : IDomainService
    {
        Task<Course> GetAsync(int id);
    }
}
