using ToDo.Web.Mvc;

namespace ToDo.Web.Shared
{
    public class DeactiveConfirmationViewModel
    {
        public string ItemName { get; set; }

        public string EntityName { get; set; }

        public int EntityId { get; set; }

        public string ControllerName { get; set; }

        public string DeactiveActionName { get; set; }

        public DeactiveConfirmationViewModel(string entityName, string itemName, int entityId, string controllerName)
        {
            ItemName = itemName;
            EntityName = entityName;
            EntityId = entityId;
            ControllerName = UrlHelper.NormalizeControllerName(controllerName);
            DeactiveActionName = "Deactive";
        }

        public DeactiveConfirmationViewModel(
            string entityName,
            string itemName,
            int entityId,
            string controllerName,
            string actionName)
            : this(entityName, itemName, entityId, controllerName)
        {
            DeactiveActionName = actionName;
        }
    }
}
