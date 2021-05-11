using BizApp.Models.Basic;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass.Businesses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BizApp.Controllers
{
	public class AskTheCommunityController : Controller
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public AskTheCommunityController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}
		public async Task<IActionResult>  Index(Guid Id)
		{
			var BusinessId = Id; 
			#region Objects 
			AskTheCommunityViewModel askTheCommunityViewModel = new AskTheCommunityViewModel();
			AskTheCommunity_NavbarViewModel askTheCommunity_NavbarViewModel = new AskTheCommunity_NavbarViewModel();
			List<AskTheCommunity_QuestionListViewModel> askTheCommunity_QuestionListViewModels = new List<AskTheCommunity_QuestionListViewModel>();
			AskTheCommunity_BusinessViewModel askTheCommunity_BusinessViewModel = new AskTheCommunity_BusinessViewModel();
			#endregion
			#region Resource
			var NavbarItem = await _UnitOfWork.AskTheCommunityRepo.GetBusinessFaq(BusinessId);
			var BusinessNameItem = await _UnitOfWork.BusinessRepo.GetBusinessName(BusinessId);
			var BusinessItem = await _UnitOfWork.BusinessRepo.GetById(BusinessId);
			int BusinessReviewCount = await _UnitOfWork.ReviewRepo.BusinessReviewCount(BusinessId);
			#endregion
			#region Navbar
			askTheCommunity_NavbarViewModel.Subject = BusinessNameItem;
			#endregion
			#region QuestionItems
			foreach (var item in NavbarItem)
			{
				var UserPhoto = item.BusinessFaqAnswers.FirstOrDefault().BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage).FirstOrDefault() == null ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : item.BusinessFaqAnswers.FirstOrDefault().BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage).FirstOrDefault().UploadedPhoto;
				var Date = item.Date == DateTime.MinValue ? string.Empty : DateChanger.ToPersianDateString(item.Date);
				askTheCommunity_QuestionListViewModels.Add(new AskTheCommunity_QuestionListViewModel() { Subject = item.Question , Answer = item.BusinessFaqAnswers.FirstOrDefault().Text ,  UserImage = UserPhoto , AnswersCount = item.BusinessFaqAnswers.Count , Date = Date, UserId = item.BizAppUserId , UserName =await _UnitOfWork.UserRepo.GetFullName(item.BizAppUserId)});
			}
			#endregion
			#region BusinessItem 
			askTheCommunity_BusinessViewModel.Id = BusinessItem.Id;
			askTheCommunity_BusinessViewModel.Image = string.IsNullOrEmpty(BusinessItem.FeatureImage) == true ? "/Upload/DefaultPicutres/Bussiness/Business.jpg" : BusinessItem.FeatureImage;
			askTheCommunity_BusinessViewModel.Rate = BusinessItem.Rate == 0 ? 1 : BusinessItem.Rate;
			askTheCommunity_BusinessViewModel.Review = BusinessReviewCount;
			#endregion
			#region FinalResults
			askTheCommunityViewModel.askTheCommunity_NavbarViewModel = askTheCommunity_NavbarViewModel;
			askTheCommunityViewModel.askTheCommunity_QuestionListViewModels = askTheCommunity_QuestionListViewModels;
			askTheCommunityViewModel.askTheCommunity_BusinessViewModel = askTheCommunity_BusinessViewModel;
			#endregion
			return View(askTheCommunityViewModel);
		}
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Add(BusinessFaq model)
		{
			try
			{
				await _UnitOfWork.AskTheCommunityRepo.AddBusinessFaq(model);
				await _UnitOfWork.SaveAsync();
				return Json(new { success = true });
			}
			catch (Exception)
			{
				return Json(new { success = false });
			}
		}
		public async Task<IActionResult> GetFaqsAnswers(Guid Id , Guid BusinessFaqId)
		{
			var BusinessId = new Guid("4e9b06be-2a73-4c40-fea1-08d8e04ff1b3");
			var BusinessFaqIId = new Guid("e9301ca0-8705-41b9-5fb2-08d8f8cc9de8");
			#region Objects
			AnswerAskTheCommunityViewModel answerAskTheCommunityViewModel = new AnswerAskTheCommunityViewModel();
			AnswerAskTheCommunity_NavbarViewModel answerAskTheCommunity_NavbarViewModel = new AnswerAskTheCommunity_NavbarViewModel();
			AnswerAskTheCommunity_AnswersCountViewModel answerAskTheCommunity_AnswersCountViewModel = new AnswerAskTheCommunity_AnswersCountViewModel();
			List<AnswerAskTheCommunity_AnswersViewModel> answerAskTheCommunity_AnswersViewModels = new List<AnswerAskTheCommunity_AnswersViewModel>();
			#endregion
			#region Resource
			var BusinessNameItem = await _UnitOfWork.BusinessRepo.GetBusinessName(BusinessId);
			var BusinessFaqItem = await _UnitOfWork.AskTheCommunityRepo.GetBusinessFaqById(BusinessFaqIId);
			var Answers = await _UnitOfWork.AskTheCommunityRepo.GetBusinessFaqAnswers(BusinessFaqIId);
			#endregion
			#region Navbar
			answerAskTheCommunity_NavbarViewModel.BusinessName = BusinessNameItem;
			answerAskTheCommunity_NavbarViewModel.Date = BusinessFaqItem.Date.ToPersianDateString();
			#endregion
			#region AnswerCount
			var AnswerCount = await _UnitOfWork.AskTheCommunityRepo.AnswerCount(BusinessFaqIId);
			answerAskTheCommunity_AnswersCountViewModel.Count = AnswerCount;
			#endregion
			#region Answers
			foreach (var item in Answers)
			{
				var UserPhoto = item.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage).FirstOrDefault() == null ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : item.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage).FirstOrDefault().UploadedPhoto;
				answerAskTheCommunity_AnswersViewModels.Add(new AnswerAskTheCommunity_AnswersViewModel() { HelpFullCount = item.HelpFullCount, NotHelpFullCount = item.NotHelpFullCount, Text = item.Text, UserName = item.BizAppUser.FullName, UserPicture = UserPhoto });
			}
			#endregion
			#region FinalResualts
			answerAskTheCommunityViewModel.answerAskTheCommunity_AnswersCountViewModel = answerAskTheCommunity_AnswersCountViewModel;
			answerAskTheCommunityViewModel.answerAskTheCommunity_NavbarViewModel = answerAskTheCommunity_NavbarViewModel;
			answerAskTheCommunityViewModel.answerAskTheCommunity_AnswersViewModels = answerAskTheCommunity_AnswersViewModels;
			#endregion
			return View(answerAskTheCommunityViewModel);
		}
		[HttpPost]
		public async Task<IActionResult> AddFaqsAnswers(BusinessFaqAnswer model)
		{
			await _UnitOfWork.AskTheCommunityRepo.AddBusinessFaqAnswers(model);
			await _UnitOfWork.SaveAsync();
			return Json(new { success = true });
		}
		[HttpPost]
		public async Task<IActionResult> AddHelpfullAnswers(Guid Id, string UserId)
		{
			await _UnitOfWork.AskTheCommunityRepo.AddHelpFull(Id, UserId);
			await _UnitOfWork.SaveAsync();
			return Json(new { success = true });
		}
		[HttpPost]
		public async Task<IActionResult> AddNotHelpfullAnswers(Guid Id, string UserId)
		{
			await _UnitOfWork.AskTheCommunityRepo.AddNotHelpFull(Id, UserId);
			await _UnitOfWork.SaveAsync();
			return Json(new { success = true });
		}
	}
}
