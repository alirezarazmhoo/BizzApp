using DomainClass.Businesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.BusinessApplication.Models
{
	public class BusinessUpdateTimeDto
	{
		public Guid BusinessId { get; set;  }
		public List<BizApp.Areas.WebApi.Models.BusinessTime> BusinessTimes { get; set; }
	}
}
