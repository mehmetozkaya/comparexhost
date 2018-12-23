using Abp.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareX.EntityFrameworkCore.Seed.Host
{
    public class InitialCaseCreator
    {
        private readonly CompareXDbContext _context;

        public InitialCaseCreator(CompareXDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create()
        {
            var brokenCar = _context.Cases.FirstOrDefault(c => c.Title == "Broken Car");
            if(brokenCar == null)
            {
                _context.Cases.Add(
                        Case.Case.Create(1, "Broken Car", Clock.Now.AddDays(10), "There is a car broken insurance need", 8)       
                    );
            }

            var internetSpeed = _context.Cases.FirstOrDefault(c => c.Title == "Internet Speed");
            if (internetSpeed == null)
            {
                _context.Cases.Add(
                        Case.Case.Create(1, "Internet Speed", Clock.Now.AddDays(10), "Internet Speed so slow", 8)
                    );
            }
        }

    }
}
