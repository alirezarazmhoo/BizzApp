using System.Collections.Generic;

namespace BizApp.Areas.Profile.Models.UserActivities
{
	public class BasicUserActivityViewModel
	{
		public SharedProfileDetailViewModel UserDetail { get; set; }
		public IList<ActivityViewModel> Activities { get; set; }
		public IList<NotificationViewModel> Notifications { get; set; }
	}
}
