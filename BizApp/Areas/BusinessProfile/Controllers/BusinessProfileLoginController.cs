using BizApp.Areas.BusinessProfile.Models;
using DataLayer.Data;
using DomainClass;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.BusinessProfile.Controllers
{
	[Area("BusinessProfile")]

	public class BusinessProfileLoginController : Controller
	{
		private readonly UserManager<BizAppUser> _userManager;
		private readonly SignInManager<BizAppUser> _signInManager;
		private readonly ApplicationDbContext _DbContext;
		public BusinessProfileLoginController(SignInManager<BizAppUser> signInManager,
		UserManager<BizAppUser> userManager, ApplicationDbContext DbContext)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_DbContext = DbContext;
		}
		public IActionResult Index()
		{
			return View();
		}

		//public BusinessProfileInputModelDto Input { get; set; }
		public async Task<IActionResult> ConfirmLogin(BusinessProfileInputModelDto Input)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(Input.Username);
				if (user == null)
				{
					ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه است");
					return RedirectToAction(nameof(Index));
				}

				if (!user.IsEnabled)
				{
					ModelState.AddModelError(string.Empty, "حساب کاربری شما غیرفعال است");
					return RedirectToAction(nameof(Index));

				}

				var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.RememberMe, lockoutOnFailure: false);

				if (result.Succeeded)
				{			
					if (User.IsInRole("Owner"))
					{
						return RedirectToAction("index", "home", new { area = "admin" });
					}
					return RedirectToAction(nameof(Index));
				}
			}
					return RedirectToAction(nameof(Index));
		}
	}
}
