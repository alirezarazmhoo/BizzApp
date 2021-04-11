using BizApp.Models.Basic;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass.Review;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Controllers
{
	public class ReviewController : Controller
	{
		private readonly IUnitOfWorkRepo _unitOfWork;
		public ReviewController(IUnitOfWorkRepo unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IActionResult> Index()
		{
			var BusinessId = new Guid("4e9b06be-2a73-4c40-fea1-08d8e04ff1b3");

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
				review_ReviewListViewModel.Add(new Review_ReviewListViewModel() { Date = item.Date.ToPersianDateString(), FullName = item.BizAppUser.FullName, Image = UserPhoto, Rate = item.Rate, Text = item.Description });
			}
			#region FinalResult
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
			var UserId = "8ad1f65a-c47d-4f1a-a601-5c64c186c09b";
			try {
				string type;
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
			var UserId = "8ad1f65a-c47d-4f1a-a601-5c64c186c09b";
			try
			{
				string type;
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
			var UserId = "8ad1f65a-c47d-4f1a-a601-5c64c186c09b";
			try
			{
				string type;
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
	}
}
