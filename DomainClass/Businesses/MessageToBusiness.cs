using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Businesses
{
	public class MessageToBusiness
	{
		public Guid Id { get; set; }
		public DateTime Date { get; set;  }
		public string Title { get; set; }
		public string Message { get; set; }
		public string Mobile { get; set;  }
		public string FullName { get; set;  }
		public Guid BusinessId { get; set; }
		public virtual Business Business { get; set; }
		public string BizAppUserId { get; set; }
	}
}
