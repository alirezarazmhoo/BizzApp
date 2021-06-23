using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Queries
{
	public class CalculateUserProfileCompleted
	{
		public bool HasBookMark { get; set;  }
		public bool HasUseFullVoteForAReview { get; set; }
		public bool DidLikeACustomerBusinessPicture { get; set;  }
		public bool HasAReview { get; set;  }
		public bool DidSaveAPictureForBusiness { get; set; }
	}
}
