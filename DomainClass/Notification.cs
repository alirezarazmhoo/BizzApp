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
		public string UserId { get; set; }
		[Required]
		[MaxLength(1000)]
		public string Description { get; set; }
		[Required]
		public NotificationStatus Status { get; set; }
		[Required]
		public NotificationModel Model { get; set; }
		[Required]
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		public virtual BizAppUser User { get; set; }

	}
}
