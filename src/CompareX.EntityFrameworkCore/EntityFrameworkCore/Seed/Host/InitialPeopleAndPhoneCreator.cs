using CompareX.PhoneNumber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareX.EntityFrameworkCore.Seed.Host
{
    public class InitialPeopleAndPhoneCreator
    {
        private readonly CompareXDbContext _context;

        public InitialPeopleAndPhoneCreator(CompareXDbContext context)
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
                        EmailAddress = "douglas.adams@fortytwo.com",
                        Phones = new List<Phone>
                        {
                            new Phone { Type = PhoneType.Home, Number = "1112242" },
                            new Phone { Type = PhoneType.Mobile, Number = "2223342" }
                        }
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
                        EmailAddress = "isaac.asimov@foundation.org",
                        Phones = new List<Phone>
                                {
                                    new Phone { Type = PhoneType.Home, Number = "8889977" }
                                }
                    });
            }
        }
    }
}
