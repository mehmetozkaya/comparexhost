using CompareX.EntityFrameworkCore;
using CompareX.MultiTenancy;
using CompareX.People;
using CompareX.Tasks;
using CompareX.Case;
using System.Linq;
using Abp.Timing;

namespace CompareX.Tests.TestDatas
{
    public class TestDataBuilder
    {
        public const string TestCaseTitle = "Test case title";
        private readonly CompareXDbContext _context;

        public TestDataBuilder(CompareXDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            var neo = new Person("Neo");
            _context.People.Add(neo);
            _context.SaveChanges();

            _context.Tasks.AddRange(
                new Task("Follow the white rabbit", "Follow the white rabbit in order to know the reality.", neo.Id),
                new Task("Clean your room") { State = TaskState.Completed }
                );

            var defaultTenant = _context.Tenants.Single(t => t.TenancyName == Tenant.DefaultTenantName);
            _context.Cases.Add(CompareX.Case.Case.Create(defaultTenant.Id, TestCaseTitle, Clock.Now.AddDays(1)));
            _context.SaveChanges();
        }

    }
}
