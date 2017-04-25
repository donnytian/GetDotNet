using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Gdn.EntityFramework;

namespace Gdn.Migrator
{
    [DependsOn(typeof(GdnDataModule))]
    public class GdnMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<GdnDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}