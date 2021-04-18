using System;

namespace DomainClass
{
	public class UserActivity
	{
		public Guid Id { get; set; }
		public string UserId { get; set; }
		public string IpAddress { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Table { get; set; }
		public string TableId { get; set; }

	}
}
