using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace BizApp.Extensions
{
	public static class HtmlHelperExtensions
	{
		public static string IsActive(this IHtmlHelper htmlHelper, string controller, string action)
		{
			var routeData = htmlHelper.ViewContext.RouteData;

			var routeAction = routeData.Values["action"].ToString();
			var routeController = routeData.Values["controller"].ToString().ToLower();

			var returnActive = (controller.Equals(routeController, StringComparison.OrdinalIgnoreCase) && action.Equals(routeAction, StringComparison.OrdinalIgnoreCase));

			return returnActive ? "active_menu" : "";
		}
	}
}
