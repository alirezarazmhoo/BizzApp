using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Queries
{
	public  class UserImpactDto
	{
		public int ReviewVotedAllTime { get; set; }
		public int ReviewVotedUseFull { get; set; }
		public int CustomerBusinessPictureLikedAllTime { get; set;  }
		public int CustomerBusinessPictureLikedInPreviousDays { get; set;  }

	}
}
