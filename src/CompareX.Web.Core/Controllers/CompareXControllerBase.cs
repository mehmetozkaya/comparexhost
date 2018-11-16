using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace CompareX.Controllers
{
    public abstract class CompareXControllerBase: AbpController
    {
        protected CompareXControllerBase()
        {
            LocalizationSourceName = CompareXConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
