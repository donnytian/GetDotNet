using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.AutoMapper;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Zero.Configuration;
using Abp.Modules;
using Abp.ObjectMapping;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using Gdn.Api;
using Gdn.WeChat.Dto;
using Hangfire;
using WeChat.Core;

namespace Gdn.Web
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(GdnDataModule),
        typeof(GdnApplicationModule),
        typeof(GdnWebApiModule),
        typeof(AbpWebSignalRModule),
        //typeof(AbpHangfireModule), - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
        typeof(AbpWebMvcModule))]
    public class GdnWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Enable database based localization
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<GdnNavigationProvider>();

            //Configure Hangfire - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
            //Configuration.BackgroundJobs.UseHangfire(configuration =>
            //{
            //    configuration.GlobalConfiguration.UseSqlServerStorage("Default");
            //});
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Custom object mappings.

            var mapperConfigs = Configuration.Modules.AbpAutoMapper().Configurators;
            mapperConfigs.Add(config =>
                {
                    config.CreateMap<TokenResponse, TokenDto>()
                        .ForMember(t => t.ExpiresAtUtc, o => o.ResolveUsing((r, d) => r.ExpiresAt?.ToUniversalTime()));
                }
            );
            mapperConfigs.Add(config =>
            {
                config.CreateMap<TokenDto, TokenResponse>()
                    .ForMember(r => r.ExpiresAt, o => o.ResolveUsing((d, r) => d.ExpiresAtUtc?.ToLocalTime()));
            });
        }
    }
}
