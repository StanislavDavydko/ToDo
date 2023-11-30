using System.Collections.Generic;

namespace ToDo.DomainModel.DataAccess
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
