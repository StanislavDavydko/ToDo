using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using ToDo.DomainModel.Legal.Enums;
using ToDo.Web.Models.TaskViewModel;
using ToDo.Services.DataAccess;
using ToDo.Web.Models;

namespace ToDo.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ITaskRepository _repository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ITaskRepository repository,
            ILogger<HomeController> logger)
        {
            _repository = repository;
            _logger = logger;
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
                var task = new Task
                {
                    Active = model.Active,
                    TaskType = model.TaskType,
                    Name = model.Name,
                    Description = model.Description,
                    CreatedDate = model.CreatedDate,
                    UpdatedDate = model.UpdatedDate
                };

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
                var dbEntity = _repository.GetTask(model.Id);
                if (dbEntity == null)
                {
                    return NotFound();
                }

                dbEntity.Active = model.Active;
                dbEntity.TaskType = model.TaskType;
                dbEntity.Name = model.Name;
                dbEntity.Description = model.Description;
                dbEntity.UpdatedDate = model.UpdatedDate;

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
            var task = _repository.GetTask(id);

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
