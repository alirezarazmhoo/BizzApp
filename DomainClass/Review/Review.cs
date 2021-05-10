using DomainClass.Businesses;
using DomainClass.Enums;
using System;
using System.Collections.Generic;

namespace DomainClass.Review
{
	public class Review
	{
		public Guid Id { get; set; }
		public StatusEnum StatusEnum { get; set; }
		public string Description { get; set; }
		public int Rate { get; set; }
		public int UsefulCount { get; set; }
		public int FunnyCount { get; set; }
		public int CoolCount { get; set; }
		public DateTime Date { get; set; }
		public Guid BusinessId { get; set; }
		public string BizAppUserId { get; set; }

		public virtual Business Business { get; set; }
		public BizAppUser BizAppUser { get; set; }
		public virtual ICollection<ReviewMedia> ReviewMedias { get; set; }
		public virtual ICollection<UsersInReviewLike> UsersInReviewLikes { get; set; }
		[System.ComponentModel.DataAnnotations.Schema.NotMapped]
		public string[] caption { get; set; }
	}
}
