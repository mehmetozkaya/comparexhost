using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using CompareX.Authorization.Roles;
using CompareX.Authorization.Users;
using CompareX.MultiTenancy;

namespace CompareX.EntityFrameworkCore
{
    public class CompareXDbContext : AbpZeroDbContext<Tenant, Role, User, CompareXDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public CompareXDbContext(DbContextOptions<CompareXDbContext> options)
            : base(options)
        {
        }
    }
}
