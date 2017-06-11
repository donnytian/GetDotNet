using Abp.Application.Navigation;
using Gdn.Sessions.Dto;

namespace Gdn.Web.Models.Layout
{
    public class MainMenuViewModel
    {
        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }

        public bool IsMultiTenancyEnabled { get; set; }

        public UserMenu MainMenu { get; set; }

        public string ActiveMenuItemName { get; set; }

        public virtual string GetShownLoginName()
        {
            var fullName = LoginInformations.User.FullName;

            if (!IsMultiTenancyEnabled)
            {
                return fullName;
            }

            return LoginInformations.Tenant == null
                ? fullName
                : fullName + " - " + LoginInformations.Tenant.TenancyName;
        }

        public virtual string GetLastLoggedInTime()
        {
            return LoginInformations.User.LastLoginTime?.ToLocalTime().ToLongDateString() ?? string.Empty;
        }
    }
}