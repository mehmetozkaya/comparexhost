using Abp.Domain.Services;
using CompareX.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompareX.Courses
{
    public interface ICourseRegistrationPolicy : IDomainService
    {
        Task CheckRegistrationAttemptAsync(Course checkCourse, User user);
    }
}
