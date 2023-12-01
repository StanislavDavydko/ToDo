using ToDo.Web.Mvc;

namespace ToDo.Web.Shared
{
    public class ActiveConfirmationViewModel
    {
        public string ItemName { get; set; }

        public string EntityName { get; set; }

        public int EntityId { get; set; }

        public string ControllerName { get; set; }

        public string ActiveActionName { get; set; }

        public ActiveConfirmationViewModel(string entityName, string itemName, int entityId, string controllerName)
        {
            ItemName = itemName;
            EntityName = entityName;
            EntityId = entityId;
            ControllerName = UrlHelper.NormalizeControllerName(controllerName);
            ActiveActionName = "Active";
        }

        public ActiveConfirmationViewModel(
            string entityName,
            string itemName,
            int entityId,
            string controllerName,
            string actionName)
            : this(entityName, itemName, entityId, controllerName)
        {
            ActiveActionName = actionName;
        }
    }
}
