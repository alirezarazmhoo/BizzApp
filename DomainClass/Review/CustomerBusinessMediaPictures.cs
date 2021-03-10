using DomainClass.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Review
{
	public class CustomerBusinessMediaPictures
	{
		public Guid Id { get; set; }
		public string Image { get; set; }
		public string Description { get; set;  }
		public long LikeCount { get; set;  }
		public StatusEnum StatusEnum { get; set; }
		public Guid CustomerBusinessMediaId { get; set;  }
		public CustomerBusinessMedia CustomerBusinessMedia { get; set; }
	}
}
