using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Models.Basic
{
	#region Main
	public class ReviewViewModel
	{
		public ICollection<Review_ReviewListViewModel> review_ReviewListViewModels { get; set;  }
	}
	#endregion
	#region ReviewList
	public class Review_ReviewListViewModel
	{
		public Guid Id { get; set;  }
		public string Image { get; set;} 
		public string FullName  { get; set;  }
		public int Rate { get; set;  }
		public string Date { get; set;  }
		public string Text { get; set;  }

	}

	#endregion



}
