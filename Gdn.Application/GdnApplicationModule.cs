using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace Gdn
{
    [DependsOn(typeof(GdnCoreModule), typeof(AbpAutoMapperModule), typeof(GdnCommonModule))]
    public class GdnApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                //Add your custom AutoMapper mappings here...
                //mapper.CreateMap<,>()
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
