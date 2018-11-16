using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace CompareX.EntityFrameworkCore
{
    public static class CompareXDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<CompareXDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<CompareXDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
