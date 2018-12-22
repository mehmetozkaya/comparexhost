using Abp.Domain.Services;
using CompareX.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompareX.Courses.Manager
{
    public interface ICourseManager : IDomainService
    {
        Task<Course> GetAsync(int id);
        Task CreateAsync(Course newCase);
        void Cancel(Course cancelCase);
        Task<CourseRegistration> RegisterAsync(Course registerCase, User user);
        Task CancelRegistrationAsync(Course cancelRegisterCase, User user);
        Task<IReadOnlyList<User>> GetRegisteredUsersAsync(Course registeredCase);
    }
}
