using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Businesses
{
public class BusinessRecentlyViewed
	{
		public Guid Id { get; set;  }
		public string BizAppUserId { get; set;  }
		public Guid BusinessId { get; set; }
		public DateTime Date { get; set; } = DateTime.Now; 
		public virtual BizAppUser  BizAppUser { get; set; }
		public virtual Business Business { get; set; }
	}
}
