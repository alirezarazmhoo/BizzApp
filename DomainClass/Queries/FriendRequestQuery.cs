using System;

namespace DomainClass.Queries
{
	public class FriendRequestQuery
	{
		public Guid Id { get; set; }
		public string Message { get; set; }
		public DateTime CreatedAt { get; set; }

		public UserProfileDetailQuery UserDetail { get; set; }
	}
}
