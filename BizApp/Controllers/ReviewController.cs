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
		public ReviewController(IUnitOfWorkRepo unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor;
		}

		[Authorize]
		public async Task<IActionResult> Index(Guid Id, int? star)
		{
			var businessId = Id;

			var business = await _unitOfWork.BusinessRepo.GetById(businessId);
			if (business == null) return NotFound();

			#region Objects
			var reviewViewModel = new ReviewViewModel();
			List<Review_ReviewListViewModel> review_ReviewListViewModel = new List<Review_ReviewListViewModel>();

			var Reviews = await _unitOfWork.ReviewRepo.GetBusinessReviews(businessId);
			#endregion
			foreach (var item in Reviews)
			{
				var UserPhoto = item.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage).FirstOrDefault() == null ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : item.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage).FirstOrDefault().UploadedPhoto;
				review_ReviewListViewModel.Add(new Review_ReviewListViewModel() { Date = item.Date.ToPersianDateString(), FullName = item.BizAppUser.FullName, Image = UserPhoto, Rate = item.Rate, Text = item.Description, TotalReview = await _unitOfWork.ReviewRepo.GetUserTotalReview(item.BizAppUserId), TotalBusinessMediaPicture = await _unitOfWork.ReviewRepo.GetUserTotalReviewMedia(item.BizAppUserId), TotalReviewPicture = await _unitOfWork.ReviewRepo.GetUserTotalBusinessMedia(item.BizAppUserId) });
			}

			#region FinalResult
			reviewViewModel.BusinessName = await _unitOfWork.BusinessRepo.GetBusinessName(businessId);
			reviewViewModel.review_ReviewListViewModels = review_ReviewListViewModel;
			reviewViewModel.BussinessId = businessId;
			reviewViewModel.CurrentRate = star.HasValue ? star.Value : 1;
			#endregion

			return View(reviewViewModel);
		}
		public async Task<IActionResult> GuessReivew()
		{
				string UserId = string.Empty;
			List<int> Districts = new List<int>();
			int District = 0; 
			#region Objects
			GuessReviewViewModel guessReviewViewModel = new GuessReviewViewModel();
			List<GuessReview_BusinessListViewModel> guessReview_BusinessListViewModels = new List<GuessReview_BusinessListViewModel>();
			#endregion
			#region Resource
			if (User.Identity.IsAuthenticated)
			{
				UserId = GetUserId();
			}
			if (HttpContext.Session.GetInt32("districId").HasValue)
			{
				District = HttpContext.Session.GetInt32("districId").Value;
			}
			if (!User.Identity.IsAuthenticated || HttpContext.Session.GetString("districId") == null)
			{
				Districts.AddRange(await _unitOfWork.DistrictRepo.GetDeafults());
			}
			var items = await _unitOfWork.ReviewRepo.GuessReview(Districts, District, UserId, null , 0 , 0 );
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
		public async Task<JsonResult> GetMoreGuessReivew(int? page , int cityId , int categoryId)
		{
			bool HasNext = true;
			string UserId = string.Empty;
			List<int> Districts = new List<int>();
			int District = 0;
			#region Objects
			GuessReviewViewModel guessReviewViewModel = new GuessReviewViewModel();
			List<GuessReview_BusinessListViewModel> guessReview_BusinessListViewModels = new List<GuessReview_BusinessListViewModel>();
			#endregion
			#region Resource
			if (User.Identity.IsAuthenticated)
			{
				UserId = GetUserId();
			}
			if (HttpContext.Session.GetInt32("districId").HasValue)
			{
				District = HttpContext.Session.GetInt32("districId").Value;
			}
			if (!User.Identity.IsAuthenticated || HttpContext.Session.GetString("districId") == null)
			{
				Districts.AddRange(await _unitOfWork.DistrictRepo.GetDeafults());
			}
			var items = await _unitOfWork.ReviewRepo.GuessReview(Districts, District, UserId, page , cityId , categoryId);
			#endregion
			#region ListBusiness
			foreach (var item in items)
			{
				guessReview_BusinessListViewModels.Add(new GuessReview_BusinessListViewModel() { Id = item.Id, Image = string.IsNullOrEmpty(item.FeatureImage) == true ? "/Upload/DefaultPicutres/Bussiness/Business.jpg" : item.FeatureImage, Name = item.Name });
			}
			int CurrentPage;
			if (HasNext)
			{
				CurrentPage = (page.HasValue ? page.Value : 1) + 1;
			}
			else
			{
				CurrentPage = page.Value;
			}
			#endregion
			#region FinalResualt
			guessReviewViewModel.guessReview_BusinessListViewModels = guessReview_BusinessListViewModels;
			#endregion
			return Json(new
			{
				success = true,
				items = guessReviewViewModel.guessReview_BusinessListViewModels,
				currentpage = CurrentPage,
				hasnext = HasNext = guessReview_BusinessListViewModels.Count > 0 ? true : false
			});
		}
		[HttpPost]
		public async Task<JsonResult> ChangeUseFullCount(Guid Id)
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
				if (await _unitOfWork.ReviewRepo.ChangeHelpFull(Id, UserId) == DomainClass.Enums.VotesAction.Add)
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
				var UserName = await _unitOfWork.UserRepo.GetFullName(UserId);
				if (await _unitOfWork.ReviewRepo.ChangeLikeCount(Id, UserId) == DomainClass.Enums.VotesAction.Add)
				{
					type = "add";
				}
				else
				{
					type = "submission";

				}
				await _unitOfWork.SaveAsync();
				return Json(new { success = true, type, username = UserName });
			}
			catch (Exception)
			{
				return Json(new { success = false });

			}
		}
		[HttpPost]
		public async Task<ActionResult> PostReview(Review model, IFormFile[] files)
		{
			model.BizAppUserId = GetUserId();
			await _unitOfWork.ReviewRepo.PostReview(model, files);
			return RedirectToAction(nameof(GuessReivew));
		}
		private string GetUserId()
		{
			return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
		}
	}
}
