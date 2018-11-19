using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.Tasks
{
    public interface ITaskRepository : IRepository<Task>
    {
        List<Task> GetAllWithPeople(Guid? assignedPersonId, TaskState? state);
    }
}
