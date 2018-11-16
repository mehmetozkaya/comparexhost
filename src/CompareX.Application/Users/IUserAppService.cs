using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CompareX.Roles.Dto;
using CompareX.Users.Dto;

namespace CompareX.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
