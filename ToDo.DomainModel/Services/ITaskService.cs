using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.DomainModel.Enums;

namespace ToDo.DomainModel.Services
{
    public interface ITaskService
    {
        Task<List<Task>> GetTasks();

        Task<Task> GetTask(int id);

        System.Threading.Tasks.Task Add(
            bool active,
            TaskType taskType,
            string name,
            string description,
            DateTime createdDate,
            DateTime updatedDate);

        System.Threading.Tasks.Task Edit(
            int id,
            bool active,
            TaskType taskType,
            string name,
            string description,
            DateTime updatedDate);

        System.Threading.Tasks.Task Delete(int id);

        System.Threading.Tasks.Task ActiveConfirmed(int id);

        System.Threading.Tasks.Task DeactiveConfirmed(int id);
    }
}
