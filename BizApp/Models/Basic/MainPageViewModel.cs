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
		public ICollection<MainPage_BusinessesByCategoryViewModel> MainPage_BusinessesByCategoryViewModels { get; set;  }
	}
	#endregion
	#region Slider
	public class MainPage_SliderViewModel
	{
		public string Image { get; set; }
		public MainPage_PhotoBusinessOwner MainPage_PhotoBusinessOwner { get; set;  }
		public MainPage_PhotoCreator  MainPage_PhotoCreator { get; set; }
		public MainPage_Category MainPage_Category { get; set; }
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
		public string Image { get; set; }
		public string Name { get; set; }
		public ICollection<Dictionary<int, string>>  CategoryChilds { get; set; }
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
		public MainPage_RecentActivityCreator MainPage_RecentActivityCreator { get; set;  }
		public MainPage_RecentActivityContent  MainPage_RecentActivityContent { get; set; }
	}
	public class MainPage_RecentActivityCreator
	{
		public string Id { get; set;  }
		public string Name { get; set; }
		public string Image { get; set;  }
		public ActivityType ActivityType { get; set;  }
	}
	public class MainPage_RecentActivityContent
	{
		public string Id { get; set; }
		public string Name { get; set;}
		public string Image { get; set; }
		public int Likes { get; set; }
		public string Text { get; set; }
		public int Rate { get; set;  }
		public int UseFulCount { get; set; }
		public int FunnyCount { get; set;  }
		public int CoolCount { get; set; }

	}
	public enum ActivityType
	{
		AddPhoto ,
		WriteReview
	}
	#endregion
	#region BusinessesByCategory
	public class MainPage_BusinessesByCategoryViewModel
	{
		public int Id { get; set; }
		public string Image { get; set; }
		public string Name { get; set; }
	}
	#endregion
}
