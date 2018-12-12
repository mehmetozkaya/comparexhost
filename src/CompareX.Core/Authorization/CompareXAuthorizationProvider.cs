using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace CompareX.Authorization
{
    public class CompareXAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Cases, L("Cases"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            var phoneBookPermission = context.CreatePermission(PermissionNames.Pages_Tenant_PhoneBook, L("PhoneBook"), multiTenancySides: MultiTenancySides.Tenant);
            phoneBookPermission.CreateChildPermission(PermissionNames.Pages_Tenant_PhoneBook_CreatePerson, L("CreateNewPerson"), multiTenancySides: MultiTenancySides.Tenant);
            phoneBookPermission.CreateChildPermission(PermissionNames.Pages_Tenant_PhoneBook_DeletePerson, L("DeletePerson"), multiTenancySides: MultiTenancySides.Tenant);

            

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CompareXConsts.LocalizationSourceName);
        }
    }
}
