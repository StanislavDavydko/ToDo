using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using ToDo.Web.Models.TaskViewModel;
using ToDo.Web.Models;
using ToDo.DomainModel.Enums;
using ToDo.DomainModel.Services;
using System.Threading.Tasks;
using System.Net;

namespace ToDo.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ITaskService _taskService;

        public HomeController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.GetTasks();

            var result = new TaskListViewModel()
            {
                Tasks = tasks,
                OperationResult = GetOperationResult()
            };

            return View(result);
        }

        [HttpGet("[action]")]
        public IActionResult Add()
        {
            var task = new DomainModel.Task()
            {
                UpdatedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                Active = true,
                TaskType = TaskType.ToDo
            };

            var model = new AddEditTaskViewModel(task);

            return View(model);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add(AddEditTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _taskService.Add(
                    model.Active,
                    model.TaskType,
                    model.Name,
                    model.Description,
                    model.CreatedDate,
                    model.UpdatedDate);

                SetOperationResult(true, "Task has been created successfully");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskService.GetTask(id);

            if (task == null)
            {
                return NotFound();
            }

            var model = new AddEditTaskViewModel(task);

            return View(model);
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Edit(AddEditTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var status = await _taskService.Edit(
                    model.Id,
                    model.Active,
                    model.TaskType,
                    model.Name,
                    model.Description,
                    model.UpdatedDate);

                if (status == HttpStatusCode.OK)
                {
                    SetOperationResult(true, "Task has been updated successfully");

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }
            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskService.GetTask(id);

            if (task == null)
            {
                return NotFound();
            }

            return DeleteConfirmationView("Tasks", task.Name, task.Id, nameof(HomeController));
        }

        [HttpPost("[action]/{id}"), ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var status = await _taskService.Delete(id);

            if (status == HttpStatusCode.OK)
            {
                SetOperationResult(true, "Task has been removed successfully");

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Active(int id)
        {
            var task = await _taskService.GetTask(id);

            if (task == null)
            {
                return NotFound();
            }

            return ActiveConfirmationView("Tasks", task.Name, task.Id, nameof(HomeController));
        }

        [HttpPost("[action]/{id}"), ActionName(nameof(Active))]
        public async Task<IActionResult> ActiveConfirmed(int id)
        {
            await _taskService.ActiveConfirmed(id);

            SetOperationResult(true, "Task has been actived successfully");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Deactive(int id)
        {
            var task = await _taskService.GetTask(id);

            if (task == null)
            {
                return NotFound();
            }

            return DeactiveConfirmationView("Tasks", task.Name, task.Id, nameof(HomeController));
        }

        [HttpPost("[action]/{id}"), ActionName(nameof(Deactive))]
        public async Task<IActionResult> DeactiveConfirmed(int id)
        {
            await _taskService.DeactiveConfirmed(id);

            SetOperationResult(true, "Task has been deactived successfully");

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
