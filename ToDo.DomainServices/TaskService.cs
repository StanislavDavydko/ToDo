using ToDo.DomainModel;
using ToDo.DomainModel.Services.DataAccess;
using ToDo.Services.DataAccess;
using ToDo.Web.Models.TaskViewModel;

namespace ToDo.DomainServices
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public Task Add(AddEditTaskViewModel model)
        {
            var task = new Task
            {
                Active = model.Active,
                TaskType = model.TaskType,
                Name = model.Name,
                Description = model.Description,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate
            };

            return task;
        }

        public Task Edit(AddEditTaskViewModel model)
        {
            var dbEntity = _repository.GetTask(model.Id);
            
            if (dbEntity == null)
            {
                return dbEntity;
            }

            dbEntity.Active = model.Active;
            dbEntity.TaskType = model.TaskType;
            dbEntity.Name = model.Name;
            dbEntity.Description = model.Description;
            dbEntity.UpdatedDate = model.UpdatedDate;

            return dbEntity;
        }

        public Task Delete(int id)
        {
            var task = _repository.GetTask(id);

            return task;
        }
    }
}
