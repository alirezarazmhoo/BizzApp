using BizApp.Areas.Profile.Models.UserActivities;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Review.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.Profile.Controllers
{
	[Area("profile")]
	public class OverviewController : ProfileController
	{
		private readonly IUnitOfWorkRepo _unitOfWork;
		private readonly System.Security.Principal.IIdentity _currentUser;

		public OverviewController(IUnitOfWorkRepo unitOfWork, IHttpContextAccessor httpContextAccessor)
			: base(unitOfWork, httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_currentUser = httpContextAccessor.HttpContext.User.Identity;
		}

		[HttpGet]
		public async Task<IActionResult> Index(string userName, int page = 1)
		{
			if (string.IsNullOrEmpty(userName) || userName.Equals(_currentUser.Name, StringComparison.OrdinalIgnoreCase))
				return await Index(page);

			try
			{
				// get user reviews
				var reviews = await _unitOfWork.ReviewRepo.GetUseReviews(userName, 1);
				var paginatedLisModel = new PagedList<UserReviewPaginateQuery>(reviews.AsQueryable(), 1, 10);

				return View("guest", paginatedLisModel);
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}
		}

		private async Task<IActionResult> Index(int page)
		{
			if (string.IsNullOrEmpty(_currentUser.Name)) return Redirect("/identity/account/login");

			// get activities
			var activities = await _unitOfWork.UserActivityRepo.GetAllActivities(_currentUser.Name, page);

			//var userDetail = await GetUserDetailWithMainImage();
			var model = new BasicUserActivityViewModel
			{
				Activities = new List<ActivityViewModel>(),
				UserDetail = new Models.SharedProfileDetailViewModel
				{
					FullName = "بدون نام",
					Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
					UserName = _currentUser.Name,
					MainPhotoPath = null
				}
			};
			
			ActivityViewModel item;

			foreach (var activity in activities)
			{
				item = new ActivityViewModel(activity.CreatedAt, activity.Description);

				switch (activity.TableName)
				{
					case TableName.Reviews:
						item.Data = await _unitOfWork.ReviewRepo.GetUseReview(new Guid(activity.TableKey));
						model.Activities.Add(item);
						
						break;
					case TableName.UserPhotos:
						var imagePath = await _unitOfWork.UserPhotoRepo.GetPathById(new Guid(activity.TableKey));
						item.Data = new AddedUserPhotoViewModel(imagePath);
						model.Activities.Add(item);

						break;
				}
			}

			return View("index", model);
		}
	}
}
