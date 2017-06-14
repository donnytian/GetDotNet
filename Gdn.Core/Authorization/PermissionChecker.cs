using Abp.Authorization;
using Gdn.Authorization.Roles;
using Gdn.MultiTenancy;
using Gdn.Users;

namespace Gdn.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
