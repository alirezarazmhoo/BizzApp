using AutoMapper;
using BizApp.Areas.Profile.Models;
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
using System.Security.Claims;
using System.Threading.Tasks;

namespace BizApp.Areas.Profile.Controllers
{
	[Area("profile")]
	public class OverviewController : ProfileController
	{
		public OverviewController(IUnitOfWorkRepo unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
			: base(unitOfWork, httpContextAccessor, mapper)
		{
		}

		[HttpGet]
		public async Task<IActionResult> Index(string userName, int page = 1)
		{
			if (string.IsNullOrEmpty(userName) || userName.Equals(CurrentUser.Identity.Name, StringComparison.OrdinalIgnoreCase))
				return await Index(page);

			try
			{
				// get user reviews
				var reviews = await UnitOfWork.ReviewRepo.GetUseReviews(userName, 1);
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
			SharedProfileDetailViewModel userDetail;
			try
			{
				userDetail = await GetUserDetailWithMainImage();
			}
			catch (UnauthorizedAccessException)
			{
				return Redirect("/identity/account/login");
			}
			catch (Exception)
			{
				return StatusCode(500);
			}

			// get activities
			var activities = await UnitOfWork.UserActivityRepo.GetAllActivities(CurrentUser.FindFirst(ClaimTypes.NameIdentifier).Value, page);

			var model = new BasicUserActivityViewModel
			{
				Activities = new List<ActivityViewModel>(),
				UserDetail = userDetail
			};
			
			ActivityViewModel item;
			foreach (var activity in activities)
			{
				item = new ActivityViewModel(activity.CreatedAt, activity.Description);

				switch (activity.TableName)
				{
					case TableName.Reviews:
						item.Data = await UnitOfWork.ReviewRepo.GetUseReview(new Guid(activity.TableKey));
						model.Activities.Add(item);
						
						break;
					case TableName.UserPhotos:
						var imagePath = await UnitOfWork.UserPhotoRepo.GetPathById(new Guid(activity.TableKey));
						item.Data = new AddedUserPhotoViewModel(imagePath);
						model.Activities.Add(item);

						break;
				}
			}

			return View("index", model);
		}
	}
}
