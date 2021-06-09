using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Models.Basic
{
	#region Main
	public class MainPageViewModel
	{
		public MainPage_SliderViewModel  MainPage_SliderViewModel { get; set;  }
		public MainPage_ReviewAwaitsViewModel   MainPage_ReviewAwaitsViewModel {get;set;}
		public ICollection<MainPage_RecentActivity>  MainPage_RecentActivity { get; set; }
		//public List<MainPage_BusinessesByCategory> MainPage_BusinessesByCategory { get; set;  }
		//public MainPage_BusinessesByCategoryMoreCategories  MainPage_BusinessesByCategoryMoreCategories { get; set; }
		public MainPage_BusinessesByCategoryMain  MainPage_BusinessesByCategoryMain { get; set; }

	}
	#endregion
	#region Slider
	public class MainPage_SliderViewModel
	{
		public string Image { get; set; }
		public string Title { get; set; }
		public IList< string> UserRoles {get;set;}
		//public MainPage_PhotoBusinessOwner MainPage_PhotoBusinessOwner { get; set;  }
		//public MainPage_PhotoCreator  MainPage_PhotoCreator { get; set; }
		public List<MainPage_Category>  MainPage_Category { get; set; }
	}
	public class MainPage_PhotoBusinessOwner
	{
		public int Id { get; set;  }
		public string Image { get; set; }
		public string Name { get; set;  }
		public int Stars { get; set; }
		public int TotalReviews { get; set; }
	}
	public class MainPage_PhotoCreator
	{
		public string Id { get; set; }
		public string Name { get; set; }
	}
	public class MainPage_Category
	{
		public int Id { get; set; }
		public string Image { get; set; }
		public string Name { get; set; }
		public string PngIcon { get; set; }
		public List<Tuple<string , string , int >> MoreCategories { get; set; }
		public Dictionary<int, string>  CategoryChilds { get; set; }
	}
	#endregion
	#region ReviewAwaits
	public class MainPage_ReviewAwaitsViewModel
	{
		public ICollection<MainPage_ReviewAwaitsItem>  MainPage_ReviewAwaitsItems {get;set;}
	}
	public class MainPage_ReviewAwaitsItem
	{
		public string Image { get; set;  }
		public string Name { get; set; }
		public int Rate { get; set; }
	}

	#endregion
	#region RecentActivity
	public class MainPage_RecentActivity
	{
		public ActivityType ActivityType { get; set; }
		public ICollection<MainPage_RecentActivityUserMediaBusiness> MainPage_RecentActivityUserMediaBusinesses { get; set;  }
		public MainPage_RecentActivityCreator MainPage_RecentActivityCreator { get; set;  }
		public MainPage_RecentActivityContent  MainPage_RecentActivityContent { get; set; }
	}
	public class MainPage_RecentActivityUserMediaBusiness
	{
		public Guid Id { get; set;  }
		public string Description { get; set; }
		public long LikeCount { get; set; }
		public string Image { get; set; }
		public string UsersName { get; set; }

	}
	public class MainPage_RecentActivityCreator
	{
		public string Id { get; set;  }
		public string Name { get; set; }
		public string Image { get; set;  }
	}
	public class MainPage_RecentActivityContent
	{
		public Guid Id { get; set; }
		public string Name { get; set;}
		public string Image { get; set; }
		public int Likes { get; set; }
		public string Text { get; set; }
		public int Rate { get; set;  }
		public int UseFulCount { get; set; }
		public int FunnyCount { get; set;  }
		public int CoolCount { get; set; }
		public Guid BusinessId { get; set; }
	}
	public enum ActivityType
	{
		AddPhoto ,
		WriteReview
	}
	#endregion
	#region BusinessesByCategory
	public class MainPage_BusinessesByCategoryMain
	{
		public ICollection<MainPage_BusinessesByCategory>  MainPage_BusinessesByCategories { get; set; }
		public MainPage_BusinessesByCategoryMoreCategories MainPage_BusinessesByCategoryMoreCategories { get; set; }
	}
	public class MainPage_BusinessesByCategory
	{
		public int Id { get; set; }
		public string Image { get; set; }
		public string Name { get; set; }
		public string PngIcon { get; set; }

	}
	public class MainPage_BusinessesByCategoryMoreCategories
	{
	public List<Tuple<string, string, int>> MoreCategories { get; set; }
	}
		#endregion
	}
