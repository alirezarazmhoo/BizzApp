using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Models.Basic
{
	#region Main
	public class AskTheCommunityViewModel
	{
		public AskTheCommunity_NavbarViewModel askTheCommunity_NavbarViewModel { get; set; }
		public ICollection<AskTheCommunity_QuestionListViewModel> askTheCommunity_QuestionListViewModels { get; set; }
		public AskTheCommunity_BusinessViewModel askTheCommunity_BusinessViewModel { get; set; }
	}
	#endregion
	#region Navbar
	public class AskTheCommunity_NavbarViewModel
	{
		public string Subject { get; set;  }
	}
	#endregion
	#region QuestionList
	public class AskTheCommunity_QuestionListViewModel
	{
		public Guid Id { get; set;  }
		public string Subject { get; set; }
		public string UserId { get; set; }
		public string UserName { get; set; }
		public string UserImage { get; set;  }
		public string Answer { get; set; }
		public int AnswersCount { get; set; }
		public string Date { get; set; }
	}
	#endregion
	#region Business
	public class AskTheCommunity_BusinessViewModel
	{
		public Guid Id { get; set;  }
		public string Image { get; set; }
		public int Rate { get; set; }
		public int Review { get; set; }
	}
	#endregion

}
