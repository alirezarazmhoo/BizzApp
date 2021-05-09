using DomainClass.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Businesses
{
	public class BusinessFaq
	{ 
		public Guid Id {get;set;}
		public DateTime Date { get; set;  }
		public string Question {get;set;}
		public Guid BusinessId {get; set;}
		public virtual Business Business {get;set;}
		public StatusEnum StatusEnum { get; set; }
		public ICollection<BusinessFaqAnswer> BusinessFaqAnswers { get; set;  }
		public string BizAppUserId { get; set; }
		public virtual BizAppUser BizAppUser { get; set; }
	}
}
