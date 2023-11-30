using Microsoft.AspNetCore.Mvc;
using ToDo.Web.Mvc;
using ToDo.Web.Shared;

namespace ToDo.Web.Controllers
{
    public class BaseController : Controller
    {
        private const string OperationResultKey = "operation-result";
        private const string DeleteConfirmationViewPath = "~/Views/Shared/_DeleteConfirmation.cshtml";
        private const string ActiveConfirmationViewPath = "~/Views/Shared/_ActivateConfirmation.cshtml";
        private const string DeactiveConfirmationViewPath = "~/Views/Shared/_DeactivateConfirmation.cshtml";

        protected OperationResult GetOperationResult()
        {
            return TempData.Get<OperationResult>(OperationResultKey);
        }

        protected void SetOperationResult(OperationResult model)
        {
            TempData.Put(OperationResultKey, model);
        }

        protected void SetOperationResult(bool isSuccess, string message)
        {
            var operationResult = new OperationResult(isSuccess, message);
            SetOperationResult(operationResult);
        }

        protected IActionResult DeleteConfirmationView(string entityName, string itemName, int entityId, string controllerName)
        {
            var model = new DeleteConfirmationViewModel(entityName, itemName, entityId, controllerName);
            return View(DeleteConfirmationViewPath, model);
        }

        protected IActionResult ActiveConfirmationView(string entityName, string itemName, int entityId, string controllerName)
        {
            var model = new ActiveConfirmationViewModel(entityName, itemName, entityId, controllerName);
            return View(ActiveConfirmationViewPath, model);
        }

        protected IActionResult DeactiveConfirmationView(string entityName, string itemName, int entityId, string controllerName)
        {
            var model = new DeactiveConfirmationViewModel(entityName, itemName, entityId, controllerName);
            return View(DeactiveConfirmationViewPath, model);
        }
    }
}
