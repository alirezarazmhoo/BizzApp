using AutoMapper;
using BizApp.Areas.Profile.Models;
using BizApp.Extensions;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.Profile.Controllers
{
	[Area("profile")]
	public class UserPhotoController : Controller
	{
		private readonly UserManager<BizAppUser> _userManager;
		private readonly IUnitOfWorkRepo _unitOfWork;
		private readonly IMapper _mapper;

		public UserPhotoController(IUnitOfWorkRepo unitOfWork, UserManager<BizAppUser> userManager, IMapper mapper)
		{
			_userManager = userManager;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		private async Task<string> GetCurrentUserId()
		{
			// get current user id
			var user = await _userManager.GetUserAsync(HttpContext.User);
			if (user == null) return null;

			return user.Id;
		}

		private async Task<SharedProfileDetailViewModel> GetUserDetail(string userName = null)
		{
			if (userName == null)
			{
				userName = _userManager.GetUserName(HttpContext.User);
			}

			var user = await _unitOfWork.UserProfileRepo.GetSharedUserDetail(userName);

			var result = _mapper.Map<SharedProfileDetailViewModel>(user);

			return result;
		}

		[HttpGet, ActionName("index")]
		[ActivityActionFilter]
		public async Task<IActionResult> Index(string userName, int page = 1)
		{
			// get user photos 
			var user = await GetUserDetail(userName);

			// check if user not exists
			if (user == null) return NotFound();

			ViewBag.FullName = user.FullName;

			// get user photos
			var photos = await _unitOfWork.UserPhotoRepo.GetAll(user.Id);
			// cast to view model
			var photosViewModel = _mapper.Map<IEnumerable<UserPhotosViewModel>>(photos);
			// pagination photos
			var paginatePhotos = new PagedList<UserPhotosViewModel>(photosViewModel.AsQueryable(), page, 20);

			var model = new UserPhotosWithProfileDetailViewModel
			{
				ProfileDetail = user,
				UserPhotos = paginatePhotos
			};

			// return result
			return View(model);
		}

		[HttpGet, ActionName("upload")]
		[Authorize]
		public async Task<IActionResult> CreateNew()
		{
			// get user full name
			var user = await GetUserDetail();

			ViewBag.FullName = user.FullName;
			ViewBag.UserName = user.UserName;

			return View();
		}

		[HttpPost, ActionName("upload")]
		[Authorize]
		[ActivityActionFilter]
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

		[HttpPost, ActionName("setasprimary")]
		[Authorize]
		public async Task<IActionResult> SetAsPrimary(Guid id)
		{
			// get current user id
			var userId = await GetCurrentUserId();
			if (userId == null) return Unauthorized();

			try
			{
				await _unitOfWork.UserPhotoRepo.SetAsPrimary(id, userId);
				TempData["message"] = "تصویر پروفایل شما تغییر کرد";

				return RedirectToAction("index");
			}
			catch (UnauthorizedAccessException)
			{
				return Unauthorized();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet, ActionName("confirmDelete")]
		[Authorize]
		public async Task<IActionResult> ConfirmDelete(Guid id)
		{
			try
			{
				var data = await _unitOfWork.UserPhotoRepo.GetById(id);
				var model = _mapper.Map<RemoveUserPhotoViewModel>(data);

				return View(model);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex);
			}
		}

		[HttpPost, ActionName("delete")]
		[Authorize]
		public async Task<IActionResult> DeletePhoto(Guid id)
		{
			// get current user id
			var userId = await GetCurrentUserId();
			if (userId == null) return Unauthorized();

			try
			{
				await _unitOfWork.UserPhotoRepo.DeletePhoto(id, userId);
				TempData["message"] = "تصویر پروفایل حذف شد";

				return RedirectToAction("index");
			}
			catch (UnauthorizedAccessException)
			{
				return StatusCode(StatusCodes.Status503ServiceUnavailable);
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

	}
}
