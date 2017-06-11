using System;
using Abp.Castle.Logging.Log4Net;
using Abp.Timing;
using Abp.Web;
using Castle.Facilities.Logging;

namespace Gdn.Web
{
    public class MvcApplication : AbpWebApplication<GdnWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig("log4net.config")
            );

            Clock.Provider = ClockProviders.Utc;

            base.Application_Start(sender, e);
        }
    }
}
