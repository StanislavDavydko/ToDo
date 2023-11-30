using System;
using ToDo.DomainModel;
using ToDo.DomainModel.DataAccess;
using ToDo.DomainModel.Enums;
using ToDo.DomainModel.Services;

namespace ToDo.DomainServices
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public Task Add(
            bool active, 
            TaskType taskType, 
            string name, 
            string description, 
            DateTime createdDate, 
            DateTime updatedDate)
        {
            var task = new Task
            {
                Active = active,
                TaskType = taskType,
                Name = name,
                Description = description,
                CreatedDate = createdDate,
                UpdatedDate = updatedDate
            };

            return task;
        }

        public Task Edit(
            int id,
            bool active, 
            TaskType taskType, 
            string name, 
            string description, 
            DateTime createdDate, 
            DateTime updatedDate)
        {
            var dbEntity = _repository.GetTask(id);
            
            if (dbEntity == null)
            {
                return dbEntity;
            }

            dbEntity.Active = active;
            dbEntity.TaskType = taskType;
            dbEntity.Name = name;
            dbEntity.Description = description;
            dbEntity.UpdatedDate = updatedDate;

            return dbEntity;
        }

        public Task Delete(int id)
        {
            var task = _repository.GetTask(id);

            return task;
        }
    }
}
