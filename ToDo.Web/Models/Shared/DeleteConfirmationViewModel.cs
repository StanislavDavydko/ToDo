using ToDo.Web.Mvc;

namespace ToDo.Web.Shared
{
    public class DeleteConfirmationViewModel
    {
        public string ItemName { get; set; }

        public string EntityName { get; set; }

        public int EntityId { get; set; }

        public string ControllerName { get; set; }

        public string DeleteActionName { get; set; }

        public DeleteConfirmationViewModel(string entityName, string itemName, int entityId, string controllerName)
        {
            ItemName = itemName;
            EntityName = entityName;
            EntityId = entityId;
            ControllerName = UrlHelper.NormalizeControllerName(controllerName);
            DeleteActionName = "Delete";
        }

        public DeleteConfirmationViewModel(
            string entityName,
            string itemName,
            int entityId,
            string controllerName,
            string deleteActionName)
            : this(entityName, itemName, entityId, controllerName)
        {
            DeleteActionName = deleteActionName;
        }
    }
}
