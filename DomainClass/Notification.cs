using DomainClass.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DomainClass
{
	public class Notification
	{
		[Key]
		public Guid Id { get; set; }
		[Required]
		// The user receive notification
		public string UserId { get; set; }
		[Required]
		public NotificationModel Model { get; set; }
		[Required]
		public string ModelId { get; set; }
		// The user create notification->use for friendship feature
		public string CreatorUserId { get; set; }
		[Required]
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		public virtual BizAppUser User { get; set; }
		public virtual BizAppUser Creator { get; set; }

	}
}
