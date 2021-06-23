using BizApp.Extensions;
using DomainClass.Businesses;
using DomainClass.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
		public bool isOpen { get; set; }
		public int categoryId { get; set; }
		public double distance { get; set; }
		public DateTime date { get; set; }
		public List<int> featuresId { get; set; }
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
		public bool isOpenNow { get; set; }
		public List<string> images { get; set; }
		public List<Review> reviews { get; set; }
	}
	public class Review
	{
		public Guid Id { get; set; }
		public string Image { get; set; }
		public string FullName { get; set; }
		public string UserId { get; set; }
		public string BusinessImage { get; set; }
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
		public string Date { get; set; }
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
		public int id { get; set; }

		public string Title { get; set; }
		public string Icon { get; set; }
	}
	public class UserProfile
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public string Address { get; set; }
		public int TotalReviewPicture { get; set; }
		public int TotalReview { get; set; }
		public int TotalBusinessMediaPicture { get; set; }
		public int TotalFriends { get; set; }
		public int TotalLikes { get; set; }
		public int TotalVotes { get; set; }

		public string Image { get; set; }
		public string City { get; set; }
		public List<Tuple<Guid, string, string>> UserProfileImages { get; set; }

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

		public string UserId { get; set; }
	}
	public class ReviewProfile
	{
		public Guid Id { get; set; }
		public string Image { get; set; }
		public string BusinessName { get; set; }
		public int TotalImages { get; set; }
		public string Text { get; set; }
		public int Rate { get; set; }
		public int UseFull { get; set; }
		public int Cool { get; set; }
		public int Funny { get; set; }
		public List<(Guid Id, string Image, string Description)> ReviewMedias { get; set; }
		public string UserName { get; set; }
		public string UserId { get; set; }
		public string UserPicture { get; set; }
		public int UserTotalReview { get; set; }
		public int UserTotalFriend { get; set; }
		public int UserTotalBusinessMedia { get; set; }
		public string Date { get; set; }

	}
	public class AbutUserProfile
	{
		public string HomeTown { get; set; }
		public string MyFavoritMovie { get; set; }
		public string WhyYouShouldReadMyReviews { get; set; }
		public string WhenImNotYelping { get; set; }

	}
	public class BookMark
	{
		public Guid id { get; set; }
		public string name { get; set; }
		public int rate { get; set; }
		public string businessImage { get; set; }
	}
	public class UserInputModel
	{
		[Required(ErrorMessage = "نام و نام خانوادگی خود را وارد کنید")]
		[Display(Name = "نام و نام خانوادگی", Prompt = "نام و نام خانوادگی")]
		public string FullName { get; set; }
		[Required(ErrorMessage = "نام کاربری را وارد کنید")]
		[Display(Name = "نام کاربری", Prompt = "نام کاربری")]
		[UniqueUserName]
		[RegularExpression(@"^([a-zA-Z]+)([a-zA-Z0-9]+)$", ErrorMessage = "نام کاربری تنها باید تلفیقی از حروف و اعداد باشد")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "موبایل خود را وارد کنید")]
		[Display(Name = "موبایل", Prompt = "موبایل")]
		[Mobile(ErrorMessage = "شماره موبایل نامعتبر است")]
		[UniqueMemberMobile]
		public long Mobile { get; set; }


		[Display(Name = "ایمیل", Prompt = "ایمیل")]
		public string Email { get; set; }

		[Required(ErrorMessage = "رمز عبور را وارد کنید")]
		[DataType(DataType.Password)]
		[Display(Name = "رمز عبور", Prompt = "رمز عبور")]
		public string Password { get; set; }


		[Display(Name = "کد پستی", Prompt = "کد پستی")]
		public string PostalCode { get; set; }
		// birth date fields
		public int? Year { get; set; }
		public int? Month { get; set; }
		public int? Day { get; set; }
		public int? CityId { get; set; }
	}
	public class UserLoginModel
	{
		[Required(ErrorMessage = "نام کاربری را وارد کنید")]
		[Display(Name = "نام کاربری", Prompt = "نام کاربری")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "رمز عبور را وارد کنید")]
		[DataType(DataType.Password)]
		[Display(Name = "رمز عبور", Prompt = "رمز عبور")]
		public string Password { get; set; }
	}
	public class UserBusinessRecentlyViewed
	{
		public Guid recentlyviewedid { get; set; }
		public Guid id { get; set; }
		public string name { get; set; }
		public string featureimage { get; set; }
		public string districtname { get; set; }


	}
	public class BusinessShortModel
	{
		public Guid id { get; set; }
		public string name { get; set; }
		public string districtname { get; set; }
		public string featureimage { get; set; }
	}

	public class RecentActivityModel
	{

		public List<RecentActivityReviewModel> RecentActivityReviewModels { get; set; }
		public List<RecentActivityUserBusinessMediaPicture>  RecentActivityUserBusinessMediaPictures { get; set; }

	}
	public class RecentActivityReviewModel 
	{
		public Guid ReviewId { get; set;  }
		public string UserId { get; set; }
		public string UserName { get; set; }
		public string UserImage { get; set; }
		public int UserTotalReviews { get; set; }
		public int UserTotalFriends { get; set; }
		public int UserTotalBusinessImage { get; set; }
		public Guid BusinessId { get; set; }
		public string BusinessName { get; set; }
		public string BusinessImage { get; set; }
		public int BusinessRate { get; set; }
		public int BusinessTotalReview { get; set;  }
		public string BusinessAddress { get; set;  }


		public List<(Guid Id, string Image, string Description)> ReviewMedias { get; set; }
		public string ReviewText { get; set; }
		public int ReviewRate { get; set; }
		public int ReviewUseFull { get; set; }
		public int ReviewCool { get; set; }
		public int ReviewFunny { get; set; }
	}

	public class RecentActivityUserBusinessMediaPicture
	{

		public string UserId { get; set; }
		public string UserName { get; set; }
		public string UserImage { get; set; }
		public int UserTotalReviews { get; set; }
		public int UserTotalFriends { get; set; }
		public int UserTotalBusinessImage { get; set; }
		public Guid BusinessId { get; set; }
		public string BusinessName { get; set; }
		public string BusinessImage { get; set; }
		public int BusinessRate { get; set; }
		public int BusinessTotalReview { get; set; }
		public string BusinessAddress { get; set; }

		public long LikeCount { get; set; }
		public List<(Guid Id, string Image, string Description)> Medias { get; set; }


	}



}
