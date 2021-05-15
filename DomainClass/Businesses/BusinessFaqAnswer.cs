using DomainClass.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Businesses
{
	public  class BusinessFaqAnswer
	{
		public Guid Id { get; set; }
		public string Text { get; set;  }
		public Guid BusinessFaqId { get; set;  }
		public virtual BusinessFaq  BusinessFaq { get; set;  }
		public int HelpFullCount { get; set; }
		public int NotHelpFullCount { get; set; }
		public StatusEnum StatusEnum { get; set; }
		public DateTime Date { get; set;  }
		public string BizAppUserId { get; set;  }
		public virtual BizAppUser BizAppUser { get; set; }
	}

}
