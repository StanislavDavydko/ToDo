﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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

        public async Task<List<DomainModel.Task>> GetTasks()
        {
            var task = await _repository.GetTasks();

            return task;
        }

        public async Task<DomainModel.Task> GetTask(int id)
        {
            var task = await _repository.GetTask(id);

            return task;
        }

        public async System.Threading.Tasks.Task Add(
            bool active,
            TaskType taskType,
            string name,
            string description,
            DateTime createdDate,
            DateTime updatedDate)
        {
            var task = new DomainModel.Task
            {
                Active = active,
                TaskType = taskType,
                Name = name,
                Description = description,
                CreatedDate = createdDate,
                UpdatedDate = updatedDate
            };

            _repository.Add(task);
            await _repository.SaveChangesAsync();
        }

        public async Task<HttpStatusCode> Edit(
            int id,
            bool active,
            TaskType taskType,
            string name,
            string description,
            DateTime updatedDate)
        {
            var dbEntity = await _repository.GetTask(id);

            if (dbEntity == null)
            {
                return HttpStatusCode.NotFound;
            }

            dbEntity.Active = active;
            dbEntity.TaskType = taskType;
            dbEntity.Name = name;
            dbEntity.Description = description;
            dbEntity.UpdatedDate = updatedDate;

            await _repository.SaveChangesAsync();

            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> Delete(int id)
        {
            var task = await _repository.GetTask(id);

            if (task == null)
            {
                return HttpStatusCode.NotFound;
            }

            _repository.Delete(task);
            await _repository.SaveChangesAsync();

            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> ActiveConfirmed(int id)
        {
            var dbEntity = await _repository.GetTask(id);

            if (dbEntity == null)
            {
                return HttpStatusCode.NotFound;
            }

            dbEntity.Active = true;

            await _repository.SaveChangesAsync();

            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> DeactiveConfirmed(int id)
        {
            var dbEntity = await _repository.GetTask(id);

            if (dbEntity == null)
            {
                return HttpStatusCode.NotFound;
            }

            dbEntity.Active = false;

            await _repository.SaveChangesAsync();

            return HttpStatusCode.OK;
        }
    }
}
