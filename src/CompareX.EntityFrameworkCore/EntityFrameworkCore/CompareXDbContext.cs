using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using CompareX.Authorization.Roles;
using CompareX.Authorization.Users;
using CompareX.MultiTenancy;
using CompareX.Tasks;
using CompareX.People;
using CompareX.PhoneNumber;
using CompareX.Courses;

namespace CompareX.EntityFrameworkCore
{
    public class CompareXDbContext : AbpZeroDbContext<Tenant, Role, User, CompareXDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<CompareX.Case.Case> Cases { get; set; }
        public DbSet<CompareX.Case.CaseRegistration> CaseRegistrations { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<Course> Courses { get; set; }


        public CompareXDbContext(DbContextOptions<CompareXDbContext> options)
            : base(options)
        {
        }
    }
}
