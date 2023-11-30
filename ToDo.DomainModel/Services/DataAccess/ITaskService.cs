using ToDo.Web.Models.TaskViewModel;

namespace ToDo.DomainModel.Services.DataAccess
{
    public interface ITaskService
    {
        Task Add(AddEditTaskViewModel model);

        Task Edit(AddEditTaskViewModel model);

        Task Delete(int id);
    }
}
