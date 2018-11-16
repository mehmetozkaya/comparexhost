using Abp.Authorization;
using CompareX.Authorization.Roles;
using CompareX.Authorization.Users;

namespace CompareX.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
