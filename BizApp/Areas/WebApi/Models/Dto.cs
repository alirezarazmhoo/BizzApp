using DomainClass.Businesses;
using DomainClass.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.Models
{
	public class CategoryDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Icon { get; set; }
		public string Image { get; set; }
	}
	public class InputCategroy
	{
		public double longitude { get; set; }
		public double latitude { get; set; }
		public string id { get; set; }
	}
	public class BusinessOnMap
	{
		public string name { get; set; }
		public string website { get; set; }
		public string boldfeature { get; set; }
		public string phonenumber { get; set; }
		public string category { get; set; }
		public string featureimage { get; set; }
		public string address { get; set; }
		public string districtname { get; set; }
		public string description { get; set; }
		public List<string> images { get; set; }
		public int totalreview { get; set; }
		public int rate { get; set; }
		public double longitude { get; set; }
		public double latitude { get; set; }
		public Guid id { get; set; }
	}
	public class BusinessItem
	{
		public Guid id { get; set; }
		public double longitude { get; set; }
		public double latitude { get; set; }
		public string website { get; set; }
		public string boldfeature { get; set; }
		public string phonenumber { get; set; }
		public string category { get; set; }

		public string name { get; set; }
		public int rate { get; set; }
		public int totalreview { get; set; }
		public string address { get; set; }
		public string districname { get; set; }
		public string description { get; set; }
		public string featureimage { get; set; }
		public List<string> images { get; set; }
		public List<Review> reviews { get; set; }
	}
	public class Review
	{
		public Guid Id { get; set; }
		public string Image { get; set; }
		public string FullName { get; set; }
		public string UserId { get; set; }
		public string BusinessImage { get; set;  }
		public int Rate { get; set; }
		public string Date { get; set; }
		public string Text { get; set; }
		public int TotalReviewPicture { get; set; }
		public int TotalReview { get; set; }
		public int TotalBusinessMediaPicture { get; set; }
		public int UseFull { get; set; }
		public int Cool { get; set; }
		public int Funny { get; set; }
		public ICollection<ReviewMedias> ReviewMedias { get; set; }
	}
	public class ReviewMedias
	{
		public Guid Id { get; set; }
		public string Url { get; set; }
		public string MediaType { get; set; }
		public string Caption { get; set; }
	}
	public class BusinessGallery
	{
		public Guid Id { get; set; }
		public string Url { get; set; }
		public string Description { get; set; }
		public string MediaType { get; set; }
		public int UserTotalFriends { get; set; }
		public int UserTotalPictures { get; set; }
		public int UserTotalReview { get; set; }
		public string UserName { get; set; }
		public string UserPicture { get; set; }
		public string UserId { get; set; }
		public string Date { get; set;   }
		public Guid BusinessId { get; set; }
	}
	public class BusinessTimeAndFeature
	{
		public string Description { get; set; }
		public ICollection<BusinessTime> BusinessTimes { get; set; }
		public ICollection<BusinessFeature> BusinessFeatures { get; set; }

	}
	public class BusinessTime
	{
		public WeekDaysEnum Day { get; set; }
		public string DayName { get; set; }
		public TimeSpan? FromTime { get; set; }
		public TimeSpan? ToTime { get; set; }


	}
	public class BusinessFeature
	{
		public string Title { get; set; }
		public string Icon { get; set; }
	}
	public class UserProfile
	{
		public string Id { get; set;  }
		public string UserName { get; set; }
		public string Address { get; set; }
		public int TotalReviewPicture { get; set; }
		public int TotalReview { get; set; }
		public int TotalBusinessMediaPicture { get; set; }
		public int TotalFriends { get; set; }
		public string Image { get; set; }
		public string City { get; set;  }
	}
	public class Activity : UserProfile
	{
		public int Type { get; set; }
		public string BusinessImage { get; set; }
		public int BusinessRate { get; set; }
		public string BusinessName { get; set; }
		public int BusinessTotalReview { get; set; }
		public Guid BusinessId { get; set; }
		public int ReviewRate { get; set; }
		public string Text { get; set; }
		public int CoolCount { get; set; }
		public int UseFullCount { get; set; }
		public int FunnyCount { get; set; }
		public string Date { get; set; }
		public Dictionary<Guid, string> Pictures { get; set; }
		public List<(Guid Id, string Image, string Description)> ReviewMedias { get; set; }

		public string UserId { get; set;  }
	}
	public class ReviewProfile
	{
		public Guid Id { get; set; }
		public string Image { get; set; }
		public string BusinessName { get; set;  }
		public int TotalImages { get; set;  }
		public string Text { get; set; }
		public int Rate { get; set; }
		public int UseFull { get; set; }
		public int Cool { get; set; }
		public int Funny { get; set; }
		public List<(Guid Id, string Image, string Description)> ReviewMedias { get; set; }
		public string UserName { get; set; }
		public string UserId { get; set;   }
		public string UserPicture { get; set;  }
		public int UserTotalReview { get; set;  }
		public int UserTotalFriend { get; set; }
		public int UserTotalBusinessMedia { get; set;  }
		public string Date { get; set;  }

	}
	public class AbutUserProfile
	{
		public string HomeTown { get; set;  }
		public string MyFavoritMovie { get; set; }
		public string WhyYouShouldReadMyReviews { get; set; }
		public string WhenImNotYelping { get; set;  }

	}
	public class BookMark 
	{
		public Guid id { get; set; }
		public string name { get; set; }
		public int rate { get; set;  }
		public string businessImage { get; set; }
		
	
	}


}
