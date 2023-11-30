using System;

namespace ToDo.Web.Mvc
{
    public static class UrlHelper
    {
        public static ReadOnlySpan<char> NormalizeControllerName(ReadOnlySpan<char> controller)
        {
            var controllerWordPosition = controller.LastIndexOf("Controller");
            return controllerWordPosition > 0 ? controller.Slice(0, controllerWordPosition) : controller;
        }

        public static string NormalizeControllerName(this string controller)
        {
            var normalizedControllerName = NormalizeControllerName(controller.AsSpan());
            var normalizedControllerNameString = normalizedControllerName.ToString();
            return normalizedControllerNameString;
        }
    }
}
