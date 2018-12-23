using Abp.Timing;
using CompareX.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareX.EntityFrameworkCore.Seed.Host
{
    public class InitialCourseCreator
    {
        private readonly CompareXDbContext _context;

        public InitialCourseCreator(CompareXDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create()
        {
            var aspnet = _context.Courses.FirstOrDefault(c => c.Title == "Asp.Net Core");
            if(aspnet == null)
            {
                _context.Courses.Add(
                    Course.Create(1, "Asp.Net Core", Clock.Now.AddDays(10), "Basic of Asp.Net Core and EF.Core", 10)
                    );
            }

            var angular = _context.Courses.FirstOrDefault(c => c.Title == "Angular");
            if (angular == null)
            {
                _context.Courses.Add(
                    Course.Create(1, "Angular", Clock.Now.AddDays(10), "Basic of Angular", 10)
                    );
            }

            var javascript = _context.Courses.FirstOrDefault(c => c.Title == "Javascript");
            if (javascript == null)
            {
                _context.Courses.Add(
                    Course.Create(1, "Javascript", Clock.Now.AddDays(10), "Basic of Javascript", 10)
                    );
            }

            var react = _context.Courses.FirstOrDefault(c => c.Title == "React");
            if (react == null)
            {
                _context.Courses.Add(
                    Course.Create(1, "React", Clock.Now.AddDays(10), "Basic of React", 10)
                    );
            }
            
            // Tenant 1 : Angular - Javascript - React  = Web Development
            // Tenant 2 : Digital Marketing - Investing - Leadership - Agile - Business Strategy

        }

    }
}
