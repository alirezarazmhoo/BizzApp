using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Extensions
{
	public class ActivityActionFilterAttribute : ActionFilterAttribute
	{
		public ActivityActionFilterAttribute()
		{

		}

		public override void OnActionExecuted(ActionExecutedContext context)
		{
			var result = context.Result;
			// Do something with Result.
			if (context.Canceled == true)
			{
				// Action execution was short-circuited by another filter.
			}

			if (context.Exception != null)
			{
				// Exception thrown by action or action filter.
				// Set to null to handle the exception.
				context.Exception = null;
			}

			base.OnActionExecuted(context);
		}
	}
}
