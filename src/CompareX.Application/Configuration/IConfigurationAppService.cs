using System.Threading.Tasks;
using CompareX.Configuration.Dto;

namespace CompareX.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
