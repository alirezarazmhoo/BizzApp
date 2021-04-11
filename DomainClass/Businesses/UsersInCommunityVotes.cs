using DomainClass.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Businesses
{
	public class UsersInCommunityVotes
	{
		public Guid Id { get; set;  }
		public  Guid BusinessFaqAnswerId { get; set; }
		public VotesType  VotesType { get; set;  }
		public string  BizAppUserId  { get; set; }
		public virtual BusinessFaqAnswer BusinessFaqAnswer { get; set; }
		public virtual BizAppUser BizAppUser { get; set; }

	}
}
