using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.Tasks.Dto
{
    public class GetTaskInput
    {
        public Guid? AssignedPersonId { get; set; }
        public TaskState? State { get; set; }
    }
}
