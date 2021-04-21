using DataLayer.Infrastructure;
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
	public class OverviewController : Controller
	{
		private readonly IUnitOfWorkRepo _unitOfWork;
		private readonly System.Security.Principal.IIdentity _currentUser;

		public OverviewController(IUnitOfWorkRepo unitOfWork, IHttpContextAccessor httpContextAccessor)
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

			foreach (var activity in activities)
			{

			}
			//var activities = _unitOfWork.UserActivityRepo.GetAllActivities();

			return View("index");
		}
	}
}
