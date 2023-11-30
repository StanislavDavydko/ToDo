using System.Collections.Generic;
using ToDo.DomainModel;

namespace ToDo.Services.DataAccess
{
    public interface ITaskRepository
    {
        List<Task> GetTasks();

        Task GetTask(int id);

        void SaveChanges();

        void Add(Task task);

        void Delete(Task task);
    }
}
