using CompareX.EntityFrameworkCore;
using CompareX.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly CompareXDbContext _context;

        public TestDataBuilder(CompareXDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            //var neo = new Person("Neo");
            //_context.People.Add(neo);
            //_context.SaveChanges();

            _context.Tasks.AddRange(
                new Task("Follow the white rabbit", "Follow the white rabbit in order to know the reality."),
                new Task("Clean your room") { State = TaskState.Completed }
                );
        }

    }
}
