using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDo.DomainModel.DataAccess
{
    public interface ITaskRepository
    {
        Task<List<DomainModel.Task>> GetTasks();

        Task<Task> GetTask(int id);

        System.Threading.Tasks.Task SaveChangesAsync();

        void Add(Task task);

        void Delete(Task task);
    }
}
