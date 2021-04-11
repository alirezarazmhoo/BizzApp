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
		public AnswerAskTheCommunity_AnswersCountViewModel answerAskTheCommunity_AnswersCountViewModel { get; set; }
	}
	#endregion
	#region Navbar
	public class AnswerAskTheCommunity_NavbarViewModel
	{
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
		public string UserName { get; set; }
		public string UserPicture { get; set; }
		public string Text { get; set; }
		public int HelpFullCount { get; set; }
		public int NotHelpFullCount { get; set; }
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
