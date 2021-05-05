using DomainClass.Businesses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass
{
	public class UserFavorits
	{
		public Guid Id { get; set;  }
		public string BizAppUserId { get; set; }
		public Guid BusinessId { get; set;  }
		public DateTime Date { get; set;  }
		public virtual BizAppUser BizAppUser { get; set; }
		public virtual Business Business { get; set; }
	}
}
