using DomainClass.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Review
{
	public class UsersInReviewVotes
	{
		public Guid Id { get; set; }
		public Guid ReviewId { get; set; }
		public VotesType VotesType { get; set; }
		public string BizAppUserId { get; set; }
		public virtual Review  Review { get; set; }
		public virtual BizAppUser BizAppUser { get; set; }
	}
}
