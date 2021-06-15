using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.BusinessProfile.Models
{
	public class BusinessAccountBusinessInformationDto
	{
		public Guid Id { get; set;  }
		[Required(ErrorMessage ="نام کسب و کار خود را وارد نمایید")]
		public string Name { get; set; }
		public double Longitude { get; set;  }
		public double Latitude { get; set; }
		public int TotalReview { get; set;  }
		public string Address { get; set; }
		public string WebSiteUrl { get; set; }
		public long CallNumber { get; set; }
		public string Biography { get; set; }
		public string DistricyName { get; set;  }
		public string CategoryName { get; set; }
		public string Email { get; set; }
		public string Description { get; set; }
		public string PostalCode { get; set; }
		public int DistrictId { get; set;  }
		public List<(int FeatureId, string FeatureName, bool IsInFeatrue, DomainClass.Enums.BusinessFeatureType , string Value)> BusinessFeatrues{ get; set; }
		public string MainImage { get; set;  }
		public int CategoryId { get; set;  }
		public string SelectedFeatures { get; set; }
		public IFormFile file { get; set;  }

	}
}
