using System;
using System.Collections.Generic;
using System.Net;
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

        Task<HttpStatusCode> Edit(
            int id,
            bool active,
            TaskType taskType,
            string name,
            string description,
            DateTime updatedDate);

        Task<HttpStatusCode> Delete(int id);

        Task<HttpStatusCode> ActiveConfirmed(int id);

        Task<HttpStatusCode> DeactiveConfirmed(int id);
    }
}
