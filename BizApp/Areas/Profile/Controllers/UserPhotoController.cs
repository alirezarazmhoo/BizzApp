using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System;
using System.Threading.Tasks;

namespace BizApp.Areas.Profile.Controllers
{
	[Area("profile")]
	public class UserPhotoController : Controller
	{
		private readonly UserManager<BizAppUser> _userManager;
		private readonly IUnitOfWorkRepo _unitOfWork;

		public UserPhotoController(IUnitOfWorkRepo unitOfWork, UserManager<BizAppUser> userManager)
		{
			_userManager = userManager;
			_unitOfWork = unitOfWork;
		}

		private async Task<BizAppUser> GetUserDetail(string userName = null)
		{
			BizAppUser user;
			if (userName == null)
			{
				var userId = _userManager.GetUserId(HttpContext.User);
				user = await _unitOfWork.UserRepo.GetById(userId);
			}
			else
			{
				user = await _unitOfWork.UserRepo.GetByUserName(userName);
			}

			if (user != null) user.FullName = !string.IsNullOrEmpty(user.FullName) ? user.FullName : "بدون نام";

			return user;
		}

		[HttpGet, ActionName("index")]
		public async Task<IActionResult> Index(string userName, int page = 1)
		{
			// get user photos 
			var user = await GetUserDetail(userName);

			// check if user not exists
			if (user == null) return NotFound();

			ViewBag.FullName = user.FullName;

			// get user photos
			var photos = await _unitOfWork.UserPhotoRepo.GetAll(user.Id);

			// pagination data
			var result = new PagedList<ApplicationUserMedia>(photos, page, 10);

			// return result
			return View(result);
		}

		[HttpGet, ActionName("upload")]
		[Authorize]
		public async Task<IActionResult> CreateNew()
		{
			// get user full name
			var user = await GetUserDetail();

			ViewBag.FullName = user.FullName;

			return View();
		}

		[HttpPost, ActionName("upload")]
		[Authorize]
		public async Task<IActionResult> HandleUpload(IFormFile file)
		{
			// get user id
			var userId = _userManager.GetUserId(HttpContext.User);

			try
			{
				// upload user photso by repository
				var result = await _unitOfWork.UserPhotoRepo.UploadPhoto(userId, file);

				if (result == UploadResult.Succeed)
					return Ok("Uploaded");

				return Problem(result.ToString());
			}
			catch (Exception ex)
			{
				return Problem(ex.Message, "UserPhoto", 500);
			}

		}
	}
}
