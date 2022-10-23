using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Infrastructure.HtmlHelpers
{
    public static class HtmlHelpers
    {
        public static string IsSelected(this IHtmlHelper html, string controller = null, string cssClass = null)
        {
            if (string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = "active";
            }

            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (string.IsNullOrWhiteSpace(controller))
            {
                controller = currentController;
            }

            return controller.Equals(currentController, StringComparison.OrdinalIgnoreCase) ? cssClass : string.Empty;
        }

        public static string PageClass(this IHtmlHelper htmplHelper)
        {
            string currentAction = (string)htmplHelper.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static string IsSelected(this IHtmlHelper html, string action = null)
        {
            var cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];

            if (string.IsNullOrWhiteSpace(action))
            {
                action = currentAction;
            }

            return action.Equals(currentAction, StringComparison.OrdinalIgnoreCase) ? cssClass : string.Empty;
        }

        public static string IsActive(this IHtmlHelper html, bool active)
        {
            var cssActiveClass = "bg-success";
            var cssInActiveClass = "bg-danger";

            return active ? cssActiveClass : cssInActiveClass;
        }
    }
}