using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using CompareX.Configuration.Dto;

namespace CompareX.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : CompareXAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
