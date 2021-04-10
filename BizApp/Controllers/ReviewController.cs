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

		[HttpPost]
		public async Task<IActionResult> AddFaqsAnswers(Review model, IFormFile[] files)
		{
			try
			{
				await _unitOfWork.ReviewRepo.AddReview(model, files);
				await _unitOfWork.SaveAsync();
				return RedirectToAction(nameof(Index));
			}
			catch (Exception )
			{
				return RedirectToAction(nameof(Index));
			}
		}

		public async Task<IActionResult> AddMediaForBusiness(CustomerBusinessMedia model, IFormFile[] files)
		{
			try
			{
				await _unitOfWork.ReviewRepo.AddBusinessMedia(model,files);
				await _unitOfWork.SaveAsync();
				return Content("");
			}
			catch (Exception )
			{
				return Content("");

			}
		}





	}
}
