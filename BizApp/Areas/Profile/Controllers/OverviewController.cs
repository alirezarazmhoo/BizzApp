using AutoMapper;
using BizApp.Areas.Profile.Models.UserActivities;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BizApp.Areas.Profile.Controllers
{
	[Area("profile")]
	public class OverviewController : Controller
	{
		private readonly IUnitOfWorkRepo _unitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<BizAppUser> _userManager;

		public OverviewController(IUnitOfWorkRepo unitOfWork, UserManager<BizAppUser> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}
		public async System.Threading.Tasks.Task<IActionResult> IndexAsync(string userName = null, int page = 1)
		{
			var currentUserName = _httpContextAccessor.HttpContext.User.Identity.Name;
			userName = string.IsNullOrEmpty(userName) ? currentUserName : userName;

			if (string.IsNullOrEmpty(userName))
				return Redirect("/identity/account/login");

			var model = new UserActivityViewModel();
			// check user seen others profile or self profile
			if (!userName.Equals(currentUserName, StringComparison.OrdinalIgnoreCase))
			{
				// in this state just take user reviews
				model.Reviews = await _unitOfWork.ReviewRepo.GetUseReviews(userName, 1);
			}
			else
			{
				// if current user wants seen self activities
				var currentUser = await _userManager.GetUserAsync(HttpContext.User);
				var activities = _unitOfWork.UserActivityRepo.GetAllActivities(currentUser.Id, page);

				
			}

			// get activities
			//var activities = _unitOfWork.UserActivityRepo.GetAllActivities();

			return View();
		}
	}
}
