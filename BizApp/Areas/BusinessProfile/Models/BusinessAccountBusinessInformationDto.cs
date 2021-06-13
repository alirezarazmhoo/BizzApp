using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.BusinessProfile.Models
{
	public class BusinessAccountBusinessInformationDto
	{
		public Guid Id { get; set;  }
		public string Name { get; set; }
		public double Longitude { get; set;  }
		public double Latitude { get; set; }
		public int TotalReview { get; set;  }
		public string Address { get; set; }
		public string WebSiteUrl { get; set; }
		public long CallNumber { get; set; }


	}
}
