using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using CompareX.Configuration;
using CompareX.Web;

namespace CompareX.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class CompareXDbContextFactory : IDesignTimeDbContextFactory<CompareXDbContext>
    {
        public CompareXDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CompareXDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            CompareXDbContextConfigurer.Configure(builder, configuration.GetConnectionString(CompareXConsts.ConnectionStringName));

            return new CompareXDbContext(builder.Options);
        }
    }
}
