using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CompareX.Authorization;

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
