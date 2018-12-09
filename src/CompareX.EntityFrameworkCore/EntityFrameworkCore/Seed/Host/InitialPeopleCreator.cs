using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareX.EntityFrameworkCore.Seed.Host
{
    public class InitialPeopleCreator
    {
        private readonly CompareXDbContext _context;

        public InitialPeopleCreator(CompareXDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create()
        {
            var douglas = _context.People.FirstOrDefault(p => p.EmailAddress == "douglas.adams@fortytwo.com");
            if(douglas == null)
            {
                _context.People.Add(
                    new People.Person
                    {
                        Name = "Douglas",
                        Surname = "Adams",
                        EmailAddress = "douglas.adams@fortytwo.com"
                    });
            }

            var asimov = _context.People.FirstOrDefault(p => p.EmailAddress == "isaac.asimov@foundation.org");
            if (asimov == null)
            {
                _context.People.Add(
                    new People.Person
                    {
                        Name = "Isaac",
                        Surname = "Asimov",
                        EmailAddress = "isaac.asimov@foundation.org"
                    });
            }
        }
    }
}
