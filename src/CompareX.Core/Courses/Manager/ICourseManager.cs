using Abp.Domain.Services;
using CompareX.Authorization.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompareX.Courses
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
