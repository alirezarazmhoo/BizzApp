using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Review
{
	public class UsersInReviewLike
	{ 
		public Guid Id { get; set;  }
		public Guid  ReviewId { get; set; }
		public virtual Review Review { get; set; }
		public string BizAppUserId { get; set;  }
		public BizAppUser BizAppUser { get; set;  }
	}
}
