using System.Reflection;
using Abp;
using Abp.Modules;
using Gdn.Configuration;

namespace Gdn
{
    //[DependsOn(typeof(AbpZeroCoreModule))]
    public class GdnCommonModule : AbpModule
    {
        /// <inheritdoc />
        public override void PreInitialize()
        {
            Configuration.Settings.Providers.Add<GdnSettingProvider>();
        }

        /// <inheritdoc />
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
