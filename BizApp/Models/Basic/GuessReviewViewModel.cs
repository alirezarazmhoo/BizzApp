using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Models.Basic
{
	#region Main
	public class GuessReviewViewModel
	{
		public ICollection<GuessReview_BusinessListViewModel> guessReview_BusinessListViewModels; 
	}
	#endregion

	#region List
	public class GuessReview_BusinessListViewModel
	{
		public Guid Id { get; set; }
		public string Image { get; set; }
		public string Name { get; set;  }


	}
	#endregion

}
