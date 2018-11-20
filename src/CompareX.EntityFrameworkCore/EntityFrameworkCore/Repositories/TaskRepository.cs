using Abp.EntityFrameworkCore;
using CompareX.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareX.EntityFrameworkCore.Repositories
{
    public class TaskRepository : CompareXRepositoryBase<Task>, ITaskRepository
    {
        public TaskRepository(IDbContextProvider<CompareXDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public List<Task> GetAllWithPeople(Guid? assignedPersonId, TaskState? state)
        {
            var query = GetAll();

            if (assignedPersonId.HasValue)
                query = query.Where(task => task.AssignedPersonId == assignedPersonId);

            if (state.HasValue)
                query = query.Where(task => task.State == state);

            return query
                .OrderByDescending(task => task.CreationTime)
                .Include(task => task.AssignedPerson)
                .ToList();
        }
    }
}
