using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gdn.Web.Filters
{
    /// <summary>
    /// Use this attribute to disable action-redirection.
    /// See more details at <see cref="NonDefaultViewRedirectActionFilter"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DisableNonDefaultViewRedirectAttribute : Attribute
    {
    }
}