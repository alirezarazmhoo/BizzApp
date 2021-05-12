using DomainClass.Enums;
using System;

namespace BizApp.Areas.Profile.Models
{
	public class NotificationViewModel
	{
		public string Title { get; set; }
		public string Link { get; set; }
		public NotificationModel Model { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
