namespace CompareX.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly CompareXDbContext _context;

        public InitialHostDbBuilder(CompareXDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
            new InitialPeopleAndPhoneCreator(_context).Create();
            new InitialCaseCreator(_context).Create();
            new InitialCourseCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
