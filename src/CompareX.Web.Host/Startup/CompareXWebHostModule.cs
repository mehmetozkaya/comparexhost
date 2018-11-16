using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CompareX.Configuration;

namespace CompareX.Web.Host.Startup
{
    [DependsOn(
       typeof(CompareXWebCoreModule))]
    public class CompareXWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public CompareXWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CompareXWebHostModule).GetAssembly());
        }
    }
}
