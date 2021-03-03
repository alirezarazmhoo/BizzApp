using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Review
{
public	class ReviewMedia
	{ 
		public Guid Id { get; set;  }
		public Guid ReviewId { get; set; }
		public virtual Review Review { get; set; }
		public string Image { get; set;  }
		public int LikeCount { get; set;  }
	}
}
