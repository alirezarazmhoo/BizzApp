using BizApp.Models.Basic;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass.Businesses;
using DomainClass.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
namespace BizApp.Controllers
{
	public class AskTheCommunityController : Controller
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public AskTheCommunityController(IUnitOfWorkRepo unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			_UnitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor;

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
				string UserPhoto = string.Empty; 
				if (item.BusinessFaqAnswers.FirstOrDefault() != null)
				{
					UserPhoto = item.BusinessFaqAnswers.FirstOrDefault().BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage).FirstOrDefault() == null ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : item.BusinessFaqAnswers.FirstOrDefault().BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage).FirstOrDefault().UploadedPhoto;
				}
				else
				{
					UserPhoto = "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg"; 
				}
				
				var Date = item.Date == DateTime.MinValue ? string.Empty : DateChanger.ToPersianDateString(item.Date);
				askTheCommunity_QuestionListViewModels.Add(new AskTheCommunity_QuestionListViewModel() { Subject = item.Question , Answer = item.BusinessFaqAnswers.FirstOrDefault() == null ? "بدون پاسخ"  : item.BusinessFaqAnswers.FirstOrDefault().Text,  UserImage = UserPhoto , AnswersCount = item.BusinessFaqAnswers.Where(s=>s.StatusEnum ==  DomainClass.Enums.StatusEnum.Accepted).Count() , Date = Date, UserId = item.BizAppUserId , UserName =await _UnitOfWork.UserRepo.GetFullName(item.BizAppUserId) , Id = item.Id});
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
		[Authorize]
		public IActionResult IndexAuthorize(Guid Id ,Guid BusinessFaqId, int redirectType)
		{
			switch (redirectType)
			{
				case 1: 
			return RedirectToAction(nameof(Index), "AskTheCommunity", new { Id = Id });
				case 2: 
			return RedirectToAction(nameof(GetFaqsAnswers), "AskTheCommunity", new { Id = Id , BusinessFaqId = BusinessFaqId });
				default:
		    return RedirectToAction(nameof(Index), "AskTheCommunity", new { Id = Id });

			}
		} 
		[HttpPost]
		public async Task<IActionResult> Add(BusinessFaq model)
		{
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction(nameof(IndexAuthorize), "AskTheCommunity", new { Id = model.BusinessId  , redirectType  =1});

			}
			try
			{
				await _UnitOfWork.AskTheCommunityRepo.AddBusinessFaq(model);
				await _UnitOfWork.SaveAsync();
				return RedirectToAction(nameof(GetFaqsAnswers)  , "AskTheCommunity" , new { Id = model.BusinessId, BusinessFaqId = model.Id });
			}
			catch (Exception)
			{
				return Json(new { success = false });
			}
		}
		public async Task<IActionResult> GetFaqsAnswers(Guid Id , Guid BusinessFaqId)
		{
			var BusinessId = Id;
			var BusinessFaqIId = BusinessFaqId;
			#region Objects
			AnswerAskTheCommunityViewModel answerAskTheCommunityViewModel = new AnswerAskTheCommunityViewModel();
			AnswerAskTheCommunity_NavbarViewModel answerAskTheCommunity_NavbarViewModel = new AnswerAskTheCommunity_NavbarViewModel();
			AnswerAskTheCommunity_AnswersCountViewModel answerAskTheCommunity_AnswersCountViewModel = new AnswerAskTheCommunity_AnswersCountViewModel();
			List<AnswerAskTheCommunity_AnswersViewModel> answerAskTheCommunity_AnswersViewModels = new List<AnswerAskTheCommunity_AnswersViewModel>();
			List<AskTheCommunity_QuestionListViewModel> askTheCommunity_QuestionListViewModels = new List<AskTheCommunity_QuestionListViewModel>();  
			#endregion
			#region Resource
			var BusinessItem = await _UnitOfWork.BusinessRepo.GetById(BusinessId);
			var BusinessFaqItem = await _UnitOfWork.AskTheCommunityRepo.GetBusinessFaqById(BusinessFaqIId);
			var Answers = await _UnitOfWork.AskTheCommunityRepo.GetBusinessFaqAnswers(BusinessFaqIId);
			var OtherQuestions = await _UnitOfWork.AskTheCommunityRepo.GetBusinessFaq(BusinessId);
			#endregion
			#region Navbar
			answerAskTheCommunity_NavbarViewModel.BusinessId = BusinessItem.Id;
			answerAskTheCommunity_NavbarViewModel.BusinessFaqId = BusinessFaqIId; 
			answerAskTheCommunity_NavbarViewModel.BusinessName = BusinessItem.Name;
			answerAskTheCommunity_NavbarViewModel.BusinessDistricName = BusinessItem.District.Name;
			answerAskTheCommunity_NavbarViewModel.BusinessCity = BusinessItem.District.City.Name;
			answerAskTheCommunity_NavbarViewModel.BusinessRate = BusinessItem.Rate == 0 ? 1 : BusinessItem.Rate;
			answerAskTheCommunity_NavbarViewModel.BusinessTotalReview = await _UnitOfWork.ReviewRepo.GetBusinessTotalReview(BusinessId);
			answerAskTheCommunity_NavbarViewModel.BusinessImage = string.IsNullOrEmpty(BusinessItem.FeatureImage) == false ? "/Upload/DefaultPicutres/Bussiness/business-strategy-success-target-goals_1421-33.jpg" : BusinessItem.FeatureImage; 
			answerAskTheCommunity_NavbarViewModel.Date = BusinessFaqItem.Date.ToPersianDateString();
			answerAskTheCommunity_NavbarViewModel.QuestionSubject = BusinessFaqItem.Question;
			answerAskTheCommunity_NavbarViewModel.UserName =await _UnitOfWork.UserRepo.GetUserName(BusinessFaqItem.BizAppUserId);
			#endregion
			#region AnswerCount
			var AnswerCount = await _UnitOfWork.AskTheCommunityRepo.AnswerCount(BusinessFaqIId);
			answerAskTheCommunity_AnswersCountViewModel.Count = AnswerCount;
			#endregion
			#region Answers
			foreach (var item in Answers)
			{
				var UserPhoto = item.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage).FirstOrDefault() == null ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : item.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage).FirstOrDefault().UploadedPhoto;
				answerAskTheCommunity_AnswersViewModels.Add(new AnswerAskTheCommunity_AnswersViewModel() { HelpFullCount = item.HelpFullCount, NotHelpFullCount = item.NotHelpFullCount, Text = item.Text, UserName = item.BizAppUser.FullName, UserPicture = UserPhoto ,  Date = item.Date.ToPersianDateString() , Id  = item.Id  });
			}
			#endregion
			#region OtherQuestions
			foreach (var item in OtherQuestions.Where(s=>s.Id != BusinessFaqId))
			{

				var Date = item.Date == DateTime.MinValue ? string.Empty : DateChanger.ToPersianDateString(item.Date);
				askTheCommunity_QuestionListViewModels.Add(new AskTheCommunity_QuestionListViewModel() { Subject = item.Question, UserImage = string.Empty, AnswersCount = item.BusinessFaqAnswers.Count, Date = Date, Id = item.Id });
			}
			#endregion
			#region FinalResualts
			answerAskTheCommunityViewModel.answerAskTheCommunity_AnswersCountViewModel = answerAskTheCommunity_AnswersCountViewModel;
			answerAskTheCommunityViewModel.answerAskTheCommunity_NavbarViewModel = answerAskTheCommunity_NavbarViewModel;
			answerAskTheCommunityViewModel.answerAskTheCommunity_AnswersViewModels = answerAskTheCommunity_AnswersViewModels;
			answerAskTheCommunityViewModel.askTheCommunity_QuestionListViewModels = askTheCommunity_QuestionListViewModels; 
			#endregion
			return View(answerAskTheCommunityViewModel);
		}
		[HttpPost]
		public async Task<IActionResult> AddFaqsAnswers(BusinessFaqAnswer model ,Guid BusinessId)
		{
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction(nameof(IndexAuthorize), "AskTheCommunity", new { Id = BusinessId , BusinessFaqId = model.BusinessFaqId , redirectType =2 });
			}
			try
			{
			model.BizAppUserId = GetUserId(); 
			await _UnitOfWork.AskTheCommunityRepo.AddBusinessFaqAnswers(model);
			await _UnitOfWork.SaveAsync();
			return RedirectToAction(nameof(PageAddFaqsAnswer), "AskTheCommunity", new { BusinessFaqId = model.BusinessFaqId, Id = model.Id , BusinessId = BusinessId , CurrentUserId = model.BizAppUserId });
			}
			catch (Exception)
			{
				return Json(new { success = false });
			}
		}
		public async Task<IActionResult> PageAddFaqsAnswer(Guid BusinessFaqId ,Guid Id, Guid BusinessId , string CurrentUserId)
		{
			AnswerAskTheCommunityViewModel answerAskTheCommunityViewModel = new AnswerAskTheCommunityViewModel();
			AnswerAskTheCommunity_NavbarViewModel answerAskTheCommunity_NavbarViewModel = new AnswerAskTheCommunity_NavbarViewModel();
			AnswerAskTheCommunity_AnswersCountViewModel answerAskTheCommunity_AnswersCountViewModel = new AnswerAskTheCommunity_AnswersCountViewModel();
			AnswerAskTheCommunity_AnswersViewModel singleAnswerAskTheCommunity_AnswersViewModels = new AnswerAskTheCommunity_AnswersViewModel();
			List<AskTheCommunity_QuestionListViewModel> askTheCommunity_QuestionListViewModels = new List<AskTheCommunity_QuestionListViewModel>();
			var BusinessItem = await _UnitOfWork.BusinessRepo.GetById(BusinessId);
			var BusinessFaqItem = await _UnitOfWork.AskTheCommunityRepo.GetBusinessFaqById(BusinessFaqId);
			var CurrentUser = await _UnitOfWork.UserRepo.GetById(CurrentUserId);
			var BusinessFaqAnswer = await _UnitOfWork.AskTheCommunityRepo.GetBusinessFaqAnswerById(Id); 
			answerAskTheCommunity_NavbarViewModel.BusinessId = BusinessItem.Id;
			answerAskTheCommunity_NavbarViewModel.BusinessFaqId = BusinessFaqId;
			answerAskTheCommunity_NavbarViewModel.BusinessName = BusinessItem.Name;
			answerAskTheCommunity_NavbarViewModel.BusinessDistricName = BusinessItem.District.Name;
			answerAskTheCommunity_NavbarViewModel.BusinessCity = BusinessItem.District.City.Name;
			answerAskTheCommunity_NavbarViewModel.BusinessRate = BusinessItem.Rate == 0 ? 1 : BusinessItem.Rate;
			answerAskTheCommunity_NavbarViewModel.BusinessTotalReview = await _UnitOfWork.ReviewRepo.GetBusinessTotalReview(BusinessId);
			answerAskTheCommunity_NavbarViewModel.BusinessImage = string.IsNullOrEmpty(BusinessItem.FeatureImage) == false ? "/Upload/DefaultPicutres/Bussiness/business-strategy-success-target-goals_1421-33.jpg" : BusinessItem.FeatureImage;
			answerAskTheCommunity_NavbarViewModel.Date = BusinessFaqItem.Date.ToPersianDateString();
			answerAskTheCommunity_NavbarViewModel.QuestionSubject = BusinessFaqItem.Question;
			answerAskTheCommunity_NavbarViewModel.UserName = await _UnitOfWork.UserRepo.GetUserName(BusinessFaqItem.BizAppUserId);
			singleAnswerAskTheCommunity_AnswersViewModels.Date = DateTime.Now.ToPersianDateString();
			singleAnswerAskTheCommunity_AnswersViewModels.Id = Id;
			singleAnswerAskTheCommunity_AnswersViewModels.Text = BusinessFaqAnswer.Text;
			singleAnswerAskTheCommunity_AnswersViewModels.UserName = CurrentUser.UserName;
			singleAnswerAskTheCommunity_AnswersViewModels.TotalBusinessImage = await _UnitOfWork.ReviewRepo.GetUserTotalBusinessMedia(GetUserId());
			singleAnswerAskTheCommunity_AnswersViewModels.TotalFriend = await _UnitOfWork.UserRepo.GetUserFriendsCount(GetUserId());
			singleAnswerAskTheCommunity_AnswersViewModels.TotalReview = await _UnitOfWork.ReviewRepo.GetUserTotalReview(GetUserId());
			singleAnswerAskTheCommunity_AnswersViewModels.UserPicture = CurrentUser.ApplicationUserMedias.Where(s => s.IsMainImage).FirstOrDefault() == null ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : CurrentUser.ApplicationUserMedias.Where(s => s.IsMainImage).FirstOrDefault().UploadedPhoto;
			#region FinalResualts
			answerAskTheCommunityViewModel.answerAskTheCommunity_NavbarViewModel = answerAskTheCommunity_NavbarViewModel;
			answerAskTheCommunityViewModel.singleAnswerAskTheCommunity_AnswersViewModels = singleAnswerAskTheCommunity_AnswersViewModels;
			#endregion
			return View(answerAskTheCommunityViewModel);
		}
		[HttpPost]
		public async Task<IActionResult> AddHelpfullAnswers(Guid Id )
		{		
			if (!User.Identity.IsAuthenticated)
			{
				return Json(new { success = true, type = "authorize" });
			}
			CommunityVoteType voteType =  await _UnitOfWork.AskTheCommunityRepo.AddHelpFull(Id, GetUserId());
			await _UnitOfWork.SaveAsync();
			if(voteType == CommunityVoteType.AddHelpFullCount)
			{
				return Json(new { success = true , type = "add" });
			}
			else if (voteType == CommunityVoteType.RemoveHelpFullCount)
			{
				return Json(new { success = true, type = "remove" });
			}
			else
			{
				return Json(new { success = true, type = "addbyremove" });
			}
		}
		[HttpPost]
		public async Task<IActionResult> AddNotHelpfullAnswers(Guid Id)
		{
			CommunityVoteType voteType = await _UnitOfWork.AskTheCommunityRepo.AddNotHelpFull(Id, GetUserId());
			await _UnitOfWork.SaveAsync();
			if (!User.Identity.IsAuthenticated)
			{
				return Json(new { success = true, type = "authorize" });
			}
			if (voteType == CommunityVoteType.AddNotHelpFullCount)
			{
				return Json(new { success = true, type = "add" });
			}
			else if(voteType == CommunityVoteType.RemoveNotHelpFullCount)
			{
				return Json(new { success = true, type = "remove" });
			}
			else
			{
				return Json(new { success = true, type = "addbyremove" });
			}
		}
		[HttpPost]
		public async Task<IActionResult> RemoveFaqsAnswer(Guid BusinessFaqAnswerId, Guid BusinessId ,Guid BusinessFaqId)
		{
			try
			{
			await _UnitOfWork.AskTheCommunityRepo.RemoveFaqAnswer(BusinessFaqAnswerId);
			await _UnitOfWork.SaveAsync();
			return RedirectToAction(nameof(GetFaqsAnswers), "AskTheCommunity", new { Id = BusinessId, BusinessFaqId = BusinessFaqId });
			}
			catch (Exception)
			{
				return Json(new { success = false });
			}
		}
		[HttpPost]
		public async Task<IActionResult> EditFactAnswer(BusinessFaqAnswer model)
		{
			try
			{
				await _UnitOfWork.AskTheCommunityRepo.EditFactAnswer(model);
		
				return Json(new { success = true });
			}
			catch (Exception)
			{
				return Json(new { success = false });

			}
		}
		private string GetUserId()
		{
			return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
		}
	}
}
