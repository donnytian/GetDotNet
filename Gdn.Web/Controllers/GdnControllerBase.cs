using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Gdn.Web.Filters;
using Microsoft.AspNet.Identity;

namespace Gdn.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    [NonDefaultViewRedirectActionFilter]
    public abstract class GdnControllerBase : AbpController
    {
        protected GdnControllerBase()
        {
            LocalizationSourceName = GdnConsts.LocalizationSourceName;
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}