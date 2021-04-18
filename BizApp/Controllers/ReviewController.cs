using BizApp.Models.Basic;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass.Review;
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
	public class ReviewController : Controller
	{
		private readonly IUnitOfWorkRepo _unitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public ReviewController(IUnitOfWorkRepo unitOfWork , IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<IActionResult> Index(Guid Id)
		{
			var BusinessId =Id;
			#region Objects
			ReviewViewModel reviewViewModel = new ReviewViewModel();
			List<Review_ReviewListViewModel> review_ReviewListViewModel = new List<Review_ReviewListViewModel>();
			#endregion
			#region Resource
			var Reviews = await _unitOfWork.ReviewRepo.GetBusinessReviews(BusinessId);
			#endregion
			foreach (var item in Reviews)
			{
				var UserPhoto = item.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage).FirstOrDefault() == null ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : item.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage).FirstOrDefault().UploadedPhoto;
				review_ReviewListViewModel.Add(new Review_ReviewListViewModel() { Date = item.Date.ToPersianDateString(), FullName = item.BizAppUser.FullName, Image = UserPhoto, Rate = item.Rate, Text = item.Description , TotalReview = await _unitOfWork.ReviewRepo.GetUserTotalReview(item.BizAppUserId) , TotalBusinessMediaPicture = await _unitOfWork.ReviewRepo.GetUserTotalReviewMedia(item.BizAppUserId) , TotalReviewPicture =await _unitOfWork.ReviewRepo.GetUserTotalBusinessMedia(item.BizAppUserId) });
			}
			#region FinalResult
			reviewViewModel.BusinessName = await _unitOfWork.BusinessRepo.GetBusinessName(BusinessId);
			reviewViewModel.review_ReviewListViewModels = review_ReviewListViewModel;
			#endregion
			return View(reviewViewModel);
		}
		public async Task<IActionResult> GuessReivew(string Id)
		{
			#region Objects
			GuessReviewViewModel guessReviewViewModel = new GuessReviewViewModel();
			List<GuessReview_BusinessListViewModel> guessReview_BusinessListViewModels = new List<GuessReview_BusinessListViewModel>();
			#endregion
			#region Resource
			var items = await _unitOfWork.ReviewRepo.GuessReview(Id, null);
			#endregion
			#region ListBusiness
			foreach (var item in items)
			{
				guessReview_BusinessListViewModels.Add(new GuessReview_BusinessListViewModel() { Id = item.Id, Image = string.IsNullOrEmpty(item.FeatureImage) == true ? "/Upload/DefaultPicutres/Bussiness/Business.jpg" : item.FeatureImage, Name = item.Name }); 
			}
			#endregion
			#region FinalResualt
			guessReviewViewModel.guessReview_BusinessListViewModels = guessReview_BusinessListViewModels; 
			#endregion
			return View(guessReviewViewModel);
		}
		[HttpPost]
		public async Task<JsonResult> ChangeUseFullCount(Guid Id)
		{
		
			try {
				string type;
				if (!User.Identity.IsAuthenticated)
				{
				
					type = "authorize";
					return Json(new { success = true, type });

				}
			    var UserId = GetUserId();
				if (await _unitOfWork.ReviewRepo.ChangeHelpFull(Id, UserId) == DomainClass.Enums.VotesAction.Add)
				{
					type = "add";
				}
				else
				{
					type = "submission";

				}
				await _unitOfWork.SaveAsync();
			    return Json(new { success = true , type });
			}
			catch (Exception)
			{
				return Json(new { success = false });

			}

		}
		[HttpPost]
		public async Task<JsonResult> ChangeFunnyCount(Guid Id)
		{
			try
			{
			
				string type;
				if (!User.Identity.IsAuthenticated)
				{
				
					type = "authorize";
					return Json(new { success = true, type });

				}
			var UserId = GetUserId();
				if (await _unitOfWork.ReviewRepo.ChangeFunnyCount(Id, UserId) == DomainClass.Enums.VotesAction.Add)
				{
					type = "add";
				}
				else
				{
					type = "submission";

				}
				await _unitOfWork.SaveAsync();
				return Json(new { success = true, type });
			}
			catch (Exception)
			{
				return Json(new { success = false });

			}

		}
		[HttpPost]
		public async Task<JsonResult> ChangeCoolCount(Guid Id)
		{
			
			try
			{
				string type;
				if (!User.Identity.IsAuthenticated)
				{
					
					type = "authorize";
					return Json(new { success = true, type });

				}
				var UserId = GetUserId();

				if (await _unitOfWork.ReviewRepo.ChangeCoolCount(Id, UserId) == DomainClass.Enums.VotesAction.Add)
				{
					type = "add";
				}
				else
				{
					type = "submission";

				}
				await _unitOfWork.SaveAsync();
				return Json(new { success = true, type });
			}
			catch (Exception)
			{
				return Json(new { success = false });

			}

		}
		[HttpPost]
		public async Task<JsonResult> ChangeLike(Guid Id)
		{

			try
			{
				string type;
				if (!User.Identity.IsAuthenticated)
				{
					//return Redirect("/Identity/Account/Login");
					type = "authorize";
					return Json(new { success = true, type });

				}
				var UserId = GetUserId();
			   var UserName = await _unitOfWork.UserRepo.GetUserName(UserId);
				if (await _unitOfWork.ReviewRepo.ChangeLikeCount(Id, UserId) == DomainClass.Enums.VotesAction.Add)
				{
					type = "add";
				}
				else
				{
					type = "submission";

				}
				await _unitOfWork.SaveAsync();
				return Json(new { success = true, type , username = UserName });
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
