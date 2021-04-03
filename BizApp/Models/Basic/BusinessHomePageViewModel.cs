using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Models.Basic
{

	#region Main
	public class BusinessHomePageViewModel
	{
		public BusinessHomePage_SliderViewModel businessHomePage_SliderViewModel { get; set; }
		public BusinessHomePage_SummaryViewModel businessHomePage_SummaryViewModel { get; set;  }
		public BusinessHomePage_FeatureViewModel businessHomePage_FeatureViewModel { get; set; }
		public ICollection<BusinessHomePage_NearSponseredViewModel> businessHomePage_NearSponseredViewModel { get; set;  }

	}
	#endregion
	#region Slider
	public class BusinessHomePage_SliderViewModel
	{
		public IEnumerable<string> Images { get; set;  }
	}
	#endregion
	#region BusinessSummary
	public class BusinessHomePage_SummaryViewModel
	{
		public string Title { get; set; }
		public int Rate { get; set; }
		public int Reviews { get; set; }
		public bool IsClaimed { get; set; }
		public int TotalPhotos { get; set;  }
	}
	#endregion
	#region BusinessFeatures
	public class BusinessHomePage_FeatureViewModel
	{
		public string BoldFeature { get; set; }
		public List<string> Features { get; set; }
	}
	#endregion
	#region NearSponsered
	public class BusinessHomePage_NearSponseredViewModel
	{
		public Guid Id { get; set; }
		public string Image { get; set; }
		public string Name { get; set;  }
		public int Rate { get; set;  }
		public int Review { get; set; }
        public string Descripton { get; set; }
	}
	#endregion
}
