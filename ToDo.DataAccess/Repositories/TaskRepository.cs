using System.Collections.Generic;
using System.Linq;
using ToDo.DomainModel;
using ToDo.DomainModel.DataAccess;

namespace ToDo.DataAccess.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Task> GetTasks()
        {
            return _context.Task
                .OrderByDescending(a => a.Active)
                .ThenBy(a => a.CreatedDate)
                .ToList();
        }

        public Task GetTask(int id)
        {
            return _context.Task
                .FirstOrDefault(a => a.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Add(Task task)
        {
            _context.Task.Add(task);
        }

        public void Delete(Task task)
        {
            _context.Task.Remove(task);
        }
    }
}
