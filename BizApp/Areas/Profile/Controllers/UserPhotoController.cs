using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

		[HttpGet, ActionName("index")]
		public async Task<IActionResult> Index(string userName) 
		{
			// get user photos 
			var user = await _unitOfWork.UserRepo.GetByUserName(userName);

			// check if user not exists
			if (user == null) return NotFound();

			// get user photos
			var photos = await _unitOfWork.UserPhotoRepo.GetAll(user.Id);

			// return result
			return View(photos);
		}

		[HttpGet, ActionName("upload")]
		[Authorize]
		public async Task<IActionResult> CreateNew() 
		{
			// get user full name
			var userId = _userManager.GetUserId(HttpContext.User);
			// check user image
			var user = await _unitOfWork.UserRepo.GetById(userId);
			var userName = !string.IsNullOrEmpty(user.FullName) ? user.FullName : "بدون نام";

			ViewBag.UserName = userName;

			return View();
		}

		[HttpPost, ActionName("upload")]
		[Authorize]
		public async Task<IActionResult> HandleUpload(IFormFile[] file)
		{
			// get user id
			var userId = _userManager.GetUserId(HttpContext.User);

			try
			{
				// upload user photso by repository
				var result = await _unitOfWork.UserPhotoRepo.UploadPhotos(userId, file);
				
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
