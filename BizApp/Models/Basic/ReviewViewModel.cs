using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Models.Basic
{
	#region Main
	public class ReviewViewModel
	{
		public Guid BussinessId { get; set;}
		public int CurrentRate { get; set;  }
		public string BusinessName { get; set; }
		public ICollection<Review_ReviewListViewModel> review_ReviewListViewModels { get; set;  }
	}
	#endregion
	#region ReviewList
	public class Review_ReviewListViewModel
	{
		public Guid Id { get; set;  }
		public string Image { get; set;} 
		public string FullName  { get; set;  }
		public int Rate { get; set;}
		public string Date { get; set;}
		public string Text { get; set;}
		public int TotalReviewPicture { get; set; }
		public int TotalReview { get; set; }
		public int TotalBusinessMediaPicture { get; set;  }

	}

	#endregion



}
