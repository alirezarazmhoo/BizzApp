using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Businesses
{
	public class BusinessFaq
	{ 
		public Guid Id {get;set;}
		public string Question {get;set;}
		public string Answer {get;set;}
		public Guid BusinessId {get; set;}
		public virtual Business Business {get;set;}
	}
}
