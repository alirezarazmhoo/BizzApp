using DomainClass.Enums;
using System;

namespace DomainClass.Review
{
	public class ReviewMedia
	{
		public Guid Id { get; set; }
		public Guid ReviewId { get; set; }
		public string Image { get; set; }
		public int LikeCount { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Description { get; set; }
		public virtual Review Review { get; set; }
	}
}
