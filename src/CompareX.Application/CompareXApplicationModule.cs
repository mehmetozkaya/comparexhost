using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CompareX.Authorization;
using CompareX.Features;

namespace CompareX
{
    [DependsOn(
        typeof(CompareXCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class CompareXApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<CompareXAuthorizationProvider>();
            Configuration.Features.Providers.Add<CompareXFeatureProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(CompareXApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
