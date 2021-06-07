using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.BusinessApplication.Models
{
	public class BusinessApplicationDto
	{
	}
	public class BusinessApplicationSlider
	{
		public string BusinessName { get; set;  }
		public string City { get; set; }
		public string District { get; set; }
		public int Rate { get; set; }
		public int TotalReview { get; set; }
		public string PhoneNumber { get; set;  }
		public string BusinessFeatureImage { get; set; }


	}
	public class BusinessApplicationInformation
	{
       public double Longitude { get; set; }
		public double Latitude { get; set; }
		public string Address { get; set; }
		public string CallNumber { get; set;  }
		public string WebSiteUrl { get;set; }
	}

	public class BusinessApplicationFeatures
	{
		public int Id { get; set;  }
		public string Name { get; set;  }
		public string Icon { get; set;  }
		public bool IsInFeatrue { get; set;  }
	}
	public class BusinessApplicationChangeInformation
	{
		public Guid Id { get; set;  }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
		public string Address { get; set; }
		public long CallNumber { get; set; }
		public string WebSiteUrl { get; set; }
	}

}
