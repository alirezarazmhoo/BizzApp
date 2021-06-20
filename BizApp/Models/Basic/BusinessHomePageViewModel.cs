using BizzAppInfrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Models.Basic
{

	#region Main
	public class BusinessHomePageViewModel
	{
		public Guid BusinessId { get; set; }
		public string BusinessName { get; set; }
		public bool FavoritConditation { get; set; }
		public BusinessHomePage_SliderViewModel businessHomePage_SliderViewModel { get; set; }
		public BusinessHomePage_SummaryViewModel businessHomePage_SummaryViewModel { get; set;  }
		public BusinessHomePage_FeatureViewModel businessHomePage_FeatureViewModel { get; set; }
		public BusinessHomePage_DescriptionViewModel businessHomePage_DescriptionViewModel { get; set; }
		public BusinessHomePage_RightPageBusinessInfoViewModel businessHomePage_RightPageBusinessInfoViewModel { get; set; }
		public BusinessHomePage_HoursAndLocationViewModel businessHomePage_HoursAndLocationViewModel { get; set; }
		public ICollection< BusinessHomePage_ReviewsViewModel> businessHomePage_ReviewsViewModel { get; set; }
		public ICollection<BusinessHomePage_FaqViewModel> businessHomePage_FaqViewModels { get; set; }
		public ICollection<BusinessHomePage_NearSponseredViewModel> businessHomePage_NearSponseredViewModel { get; set;  }
		public ICollection<BusinessHomePage_RelatedBusinessViewModel> businessHomePage_RelatedBusinessViewModels { get; set; }
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
		public Guid BusinessId { get; set; }
		public string Title { get; set; }
		public int Rate { get; set; }
		public int Reviews { get; set; }
		public bool IsClaimed { get; set; }
		public int TotalPhotos { get; set;  }
		public string Description { get; set; }
		public string Date { get; set;  }
	}
	#endregion
	#region BusinessFeatures
	public class BusinessHomePage_FeatureViewModel
	{
		public string BoldFeature { get; set; }
		public List<BusinessFeatureItem> Features { get; set; }
	}

	public class BusinessFeatureItem
	{
		public string Name { get; set; }
		public string Icon { get; set; }
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
		public string DistricName { get; set; }
	}
	#endregion
	#region Description 
	public class BusinessHomePage_DescriptionViewModel
	{
		public string BusinessName { get; set; }
		public string Descripton { get; set; }
	}
	#endregion
	#region RightPageBusinessInfo
	public class BusinessHomePage_RightPageBusinessInfoViewModel
	{
		public string WebSiteUrl { get; set; }
		public string PhoneNumber { get; set;  }
		public string Address { get; set;  }
		public byte[] QrCode { get; set; }

	}
	#endregion
	#region Reviews
	public class BusinessHomePage_ReviewsViewModel
	{
		public string Id { get; set;  }
		public string FullName { get; set; }
		public string UserName { get; set; }
		public string UserPicture { get; set; }
		public string DistricName { get; set; }
		public int TotalPictures { get; set; }
		public int TotalReviews { get; set; }
		public int ReviewTotalPictures { get; set; }
		public int Rate { get; set;  }
		public string Date { get; set;  }
		public string Text { get; set;  }
		public int Cool { get; set; }
		public int UseFull { get; set; }
		public int Funny { get; set; }
		public Guid ReviewId { get; set;  }
		public ICollection<string> ReviewPictures { get; set; }

	}

	#endregion
	#region BusinessFaq
	public class BusinessHomePage_FaqViewModel
	{
		public Guid Id { get; set; }
		public Guid BusinessId { get; set; }
		public  int AnswersCount { get; set;  }
		public string Question { get; set; }
		public string Date { get; set; }
		public ICollection<string> Answers { get; set; }
	}
	#endregion
	#region RelatedBusiness
	public class BusinessHomePage_RelatedBusinessViewModel
	{
		public string Image { get; set; }
		public string Name { get; set;  }
		public string Description { get; set;  }
		public int Rate { get; set;  }
		public int Review { get; set;  }
	}
	#endregion
	#region HoursAndLocations
	public class BusinessHomePage_HoursAndLocationViewModel
	{
		public string Address { get; set;  }
		public double Lon { get; set;  }
		public double Lat { get; set;  }
		public List<LocationHours> LocationHours { get; set;  }
	}
	#endregion




}
