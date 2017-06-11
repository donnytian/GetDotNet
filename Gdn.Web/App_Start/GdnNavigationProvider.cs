using System;
using System.IO;
using System.Web.Hosting;
using Abp.Application.Navigation;
using Abp.Localization;
using Newtonsoft.Json;

namespace Gdn.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml or _MainSidebar.cshtml to know how to render menu.
    /// </summary>
    public class GdnNavigationProvider : NavigationProvider
    {
        /// <summary>
        /// Max menu level depth. The depth of top level is 0.
        /// </summary>
        public const int MaxMemuDepth = 3;

        /// <summary>
        /// The separator that should be used in menu item name to indicate the levels.
        /// </summary>
        public const char MenuLevelSeparator = '.';

        private const string NavigationJsonFilePath = "~/Views/Navigation.json";

        public override void SetNavigation(INavigationProviderContext context)
        {
            var path = HostingEnvironment.MapPath(NavigationJsonFilePath)
                ?? throw new FileNotFoundException($"The {NavigationJsonFilePath} file was not found!");

            var navigationString = File.ReadAllText(path);
            dynamic navigation = JsonConvert.DeserializeObject(navigationString);

            foreach (var menuItem in navigation.MainMenu)
            {
                AddMenuItem(context.Manager.MainMenu, menuItem, 0);
            }
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, GdnConsts.LocalizationSourceName);
        }

        private static void AddMenuItem(IHasMenuItemDefinitions parentItem, dynamic jsonMenuItem, int depth)
        {
            if (depth > MaxMemuDepth)
            {
                throw new InvalidOperationException("Menu level is too deep!");
            }

            var requireAuth = jsonMenuItem.RequiresAuthentication != null &&
                              (bool)jsonMenuItem.RequiresAuthentication.Value;

            var menuItem = new MenuItemDefinition(
                jsonMenuItem.Name.Value,
                L(jsonMenuItem.L10nKey.Value),
                url: jsonMenuItem.Url?.Value,
                icon: jsonMenuItem.Icon?.Value,
                requiredPermissionName: jsonMenuItem.RequiredPermissionName?.Value,
                requiresAuthentication: requireAuth
            );

            parentItem.Items.Add(menuItem);

            if (jsonMenuItem.Items == null)
            {
                return;
            }

            foreach (var subItem in jsonMenuItem.Items)
            {
                AddMenuItem(menuItem, subItem, depth + 1);
            }
        }
    }
}
