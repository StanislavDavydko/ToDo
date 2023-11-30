using System.Collections.Generic;
using ToDo.DomainModel;
using ToDo.Web.Shared;

namespace ToDo.Web.Models.TaskViewModel
{
    public class TaskListViewModel : OperationResultViewModel
    {
        public List<Task>? Tasks { get; set; }
    }
}
