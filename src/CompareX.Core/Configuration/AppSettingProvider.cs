using System.Collections.Generic;
using Abp.Configuration;

namespace CompareX.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "black", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.MaxAllowedEventRegistrationCountInLast30DaysPerUser, "10", scopes: SettingScopes.Tenant)
            };
        }
    }
}
