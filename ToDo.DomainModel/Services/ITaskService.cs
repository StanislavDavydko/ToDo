using System;
using ToDo.DomainModel.Enums;

namespace ToDo.DomainModel.Services
{
    public interface ITaskService
    {
        Task Add(
            bool active,
            TaskType taskType,
            string name,
            string description,
            DateTime createdDate,
            DateTime updateDate);

        Task Edit(
            int id,
            bool active,
            TaskType taskType,
            string name,
            string description,
            DateTime createdDate,
            DateTime updateDate);

        Task Delete(int id);
    }
}
