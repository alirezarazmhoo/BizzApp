using AutoMapper;
using BizApp.Areas.Profile.Models.Account;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using BizApp.Utility;
using System;
using DomainClass.Commands;
using DomainClass;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;

// change for commit

namespace BizApp.Areas.Profile.Controllers
{
	[Area("profile")]
	[Authorize]
	public class AccountController : ProfileController
	{
		private readonly UserManager<BizAppUser> _userManager;
		private readonly SignInManager<BizAppUser> _signInManager;

		public AccountController(IUnitOfWorkRepo unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<BizAppUser> userManager, SignInManager<BizAppUser> signInManager) : base(unitOfWork, httpContextAccessor, mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public async Task<IActionResult> Edit()
		{
			var userId = CurrentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
			var userDetail = await UnitOfWork.UserRepo.GetById(userId);

			var model = Mapper.Map<EditAcountViewModel>(userDetail);
			var persianDate = userDetail.BirthDate.ToPersianDateString();

			// set birth date
			if (!string.IsNullOrEmpty(persianDate))
			{
				var dateParts = persianDate.Split('/');
				model.Day = int.Parse(dateParts[2]);
				model.Month = int.Parse(dateParts[1]);
				model.Year = int.Parse(dateParts[0]);
			}

			// set main photo image
			model.MainPhoto = await UnitOfWork.UserRepo.GetMainPhoto(userId);

			// set city name
			if (model.CityId != null) {
				var cityWithProvince = await UnitOfWork.CityRepo.GetWithProvince((int) model.CityId);
				model.CityName = cityWithProvince.ListName;
			}

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Update(EditAcountViewModel model)
		{
			//ModelState.Remove("Id");
			if (!ModelState.IsValid)
			{
				return View("edit", model);
			}

			try
			{
				var command = Mapper.Map<EditAcountCommand>(model);

				// convert persian birth date to geo
				if (model.Year > 0 && model.Month > 0 && model.Day > 0)
				{
					var persianDate = $"{model.Year}/{model.Month}/{model.Day}";
					var geoDate = persianDate.ToGeorgianDateTime();
					command.BirthDate = geoDate;
				}
				else
				{
					command.BirthDate = null;
				}

				await UnitOfWork.UserRepo.UpdateProfile(command);

				TempData["message"] = "تغییرات ارسالی ذخیره شد";

				return RedirectToAction("edit");
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpGet]
		public IActionResult ChangePassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model) 
		{
			// check current password
			if (!ModelState.IsValid) return View(model);

			var user = await _userManager.GetUserAsync(User);

			// change password
			var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					if (error.Code.Equals("PasswordMismatch", StringComparison.OrdinalIgnoreCase))
					{
						ModelState.AddModelError(string.Empty, "رمز عبور فعلی اشتباه است");
					}
				}
				return View();
			}

			await _signInManager.RefreshSignInAsync(user);
			// show message
			TempData["message"] = "رمز عبور شما با موفقیت تغییر کرد";

			return View();

		}
	}
}
