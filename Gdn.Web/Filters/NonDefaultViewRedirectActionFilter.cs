using System;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace Gdn.Web.Filters
{
    /// <summary>
    /// Since LobiAdmin is using URL hash and AJAX to get pages, we need to redirect all non-AJAX and non-default requests to the default page,
    /// which is the only page has a layout page.
    /// If we don't do this, the requests are fine but that might cause a strange RUL like "/Users/Index#/Home/Index".
    /// Once we do the redirection, the URL will be always like either "/#/xxx/yyy" or "/home/index/#xxx/yyy".
    /// </summary>
    public class NonDefaultViewRedirectActionFilter : ActionFilterAttribute
    {
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var isAjax = filterContext.HttpContext?.Request?.IsAjaxRequest() == true;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            var disabled = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(DisableNonDefaultViewRedirectAttribute), false).Any();
            disabled |= filterContext.ActionDescriptor.GetCustomAttributes(typeof(DisableNonDefaultViewRedirectAttribute), false).Any();

            // DO NOT redirect for child action, AJAX request, default page and those explicitly disabled.
            if (
                !disabled
                && !filterContext.IsChildAction
                && !isAjax
                && !(
                    RouteConfig.DefaultMvcController.Equals(controllerName, StringComparison.InvariantCultureIgnoreCase)
                    && RouteConfig.DefaultMvcAction.Equals(actionName, StringComparison.InvariantCultureIgnoreCase)
                    )
                )
            {
                filterContext.Result = new RedirectResult("/", true);
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}