using System;

namespace BizApp.Areas.Profile.Models.UserActivities
{
	public class UserActivityViewModel<T>
	{
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public T Data { get; set; }
	}
}
