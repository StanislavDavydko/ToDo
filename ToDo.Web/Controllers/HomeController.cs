using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using ToDo.Web.Models.TaskViewModel;
using ToDo.Services.DataAccess;
using ToDo.Web.Models;
using ToDo.DomainModel.Enums;
using ToDo.DomainModel.Services.DataAccess;
using ToDo.DomainModel;

namespace ToDo.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ITaskRepository _repository;
        private readonly ILogger<HomeController> _logger;
        private readonly ITaskService _taskService;

        public HomeController(
            ITaskRepository repository,
            ILogger<HomeController> logger,
            ITaskService taskService)
        {
            _repository = repository;
            _logger = logger;
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var task = _repository.GetTasks();

            var result = new TaskListViewModel()
            {
                Tasks = task,
                OperationResult = GetOperationResult()
            };

            return View(result);
        }

        [HttpGet("[action]")]
        public IActionResult Add()
        {
            var task = new Task()
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
        public IActionResult Add(AddEditTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var task = _taskService.Add(model);

                _repository.Add(task);
                _repository.SaveChanges();

                SetOperationResult(true, "Task has been created successfully");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Edit(int id)
        {
            var task = _repository.GetTask(id);

            if (task == null)
            {
                return NotFound();
            }

            var model = new AddEditTaskViewModel(task);

            return View(model);
        }

        [HttpPost("[action]/{id}")]
        public IActionResult Edit(AddEditTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                _taskService.Edit(model);

                _repository.SaveChanges();

                SetOperationResult(true, "Task has been updated successfully");

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Delete(int id)
        {
            var task = _repository.GetTask(id);

            if (task == null)
            {
                return NotFound();
            }

            return DeleteConfirmationView("Tasks", task.Name, task.Id, nameof(HomeController));
        }

        [HttpPost("[action]/{id}"), ActionName(nameof(Delete))]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _taskService.Delete(id);

            _repository.Delete(task);
            _repository.SaveChanges();

            SetOperationResult(true, "Task has been removed successfully");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Active(int id)
        {
            var task = _repository.GetTask(id);

            if (task == null)
            {
                return NotFound();
            }

            return ActiveConfirmationView("Tasks", task.Name, task.Id, nameof(HomeController));
        }

        [HttpPost("[action]/{id}"), ActionName(nameof(Active))]
        public IActionResult ActiveConfirmed(int id)
        {
            var dbEntity = _repository.GetTask(id);

            if (dbEntity == null)
            {
                return NotFound();
            }

            dbEntity.Active = true;

            _repository.SaveChanges();

            SetOperationResult(true, "Task has been actived successfully");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Deactive(int id)
        {
            var task = _repository.GetTask(id);

            if (task == null)
            {
                return NotFound();
            }

            return DeactiveConfirmationView("Tasks", task.Name, task.Id, nameof(HomeController));
        }

        [HttpPost("[action]/{id}"), ActionName(nameof(Deactive))]
        public IActionResult DeactiveConfirmed(int id)
        {
            var dbEntity = _repository.GetTask(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            dbEntity.Active = false;

            _repository.SaveChanges();

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
