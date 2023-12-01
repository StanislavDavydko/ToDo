using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<DomainModel.Task>> GetTasks()
        {
            return await _context.Task
                .OrderByDescending(a => a.Active)
                .ThenBy(a => a.CreatedDate)
                .ToListAsync();
        }

        public async Task<DomainModel.Task> GetTask(int id)
        {
            return await _context.Task
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Add(DomainModel.Task task)
        {
            _context.Task.Add(task);
        }

        public void Delete(DomainModel.Task task)
        {
            _context.Task.Remove(task);
        }
    }
}
