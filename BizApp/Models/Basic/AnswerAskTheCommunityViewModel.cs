using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Models.Basic
{
	#region Main
	public class AnswerAskTheCommunityViewModel
	{
		public AnswerAskTheCommunity_NavbarViewModel answerAskTheCommunity_NavbarViewModel { get; set; }
		public ICollection<AnswerAskTheCommunity_AnswersViewModel> answerAskTheCommunity_AnswersViewModels { get; set; }
		public AnswerAskTheCommunity_AnswersViewModel singleAnswerAskTheCommunity_AnswersViewModels { get; set; }

		public AnswerAskTheCommunity_AnswersCountViewModel answerAskTheCommunity_AnswersCountViewModel { get; set; }
		public ICollection<AskTheCommunity_QuestionListViewModel> askTheCommunity_QuestionListViewModels { get; set; }
	}
	#endregion
	#region Navbar
	public class AnswerAskTheCommunity_NavbarViewModel
	{
		public Guid BusinessFaqId { get; set;  }
		public Guid BusinessId { get; set; }
		public string BusinessImage { get; set; }
		public string BusinessCity { get; set; }
		public int BusinessRate { get; set; } 
		public string BusinessDistricName { get; set;  }
		public int BusinessTotalReview { get; set; }
		public string BusinessName { get; set; }
		public string QuestionSubject { get; set; }
		public string Subject { get; set; }
		public string UserName { get; set; }
		public string UserId { get; set; }
		public string Date { get; set; }
	}
	#endregion
	#region AnswerCount
	public class AnswerAskTheCommunity_AnswersCountViewModel
	{
		public int Count { get; set; }
	}

	#endregion

	#region Answers
	public class AnswerAskTheCommunity_AnswersViewModel
	{
		public Guid Id { get; set;  }
		public string UserName { get; set; }
		public string UserPicture { get; set; }
		public string Text { get; set; }
		public int TotalReview { get; set;  }
		public int TotalBusinessImage { get; set; }
		public int TotalFriend { get; set;   }
		public int HelpFullCount { get; set; }
		public int NotHelpFullCount { get; set; }
		public string Date { get; set;  }
	}
	#endregion
	#region Business
	public class AnswerAskTheCommunity_BusinessViewModel
	{
		public Guid Id { get; set; }
		public string Image { get; set; }
		public int Rate { get; set; }
		public int Review { get; set; }

	}
	#endregion





}
