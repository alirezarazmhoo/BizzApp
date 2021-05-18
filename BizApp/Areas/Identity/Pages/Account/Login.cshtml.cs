using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using DomainClass;
using DataLayer.Data;

namespace BizApp.Areas.Identity.Pages.Account
{
	[AllowAnonymous]
	public class LoginModel : PageModel
	{
		private readonly UserManager<BizAppUser> _userManager;
		private readonly SignInManager<BizAppUser> _signInManager;
		private readonly ILogger<LoginModel> _logger;
		private readonly ApplicationDbContext _DbContext;

		public LoginModel(SignInManager<BizAppUser> signInManager,
			ILogger<LoginModel> logger,
			UserManager<BizAppUser> userManager, ApplicationDbContext DbContext)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
			_DbContext = DbContext;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public IList<AuthenticationScheme> ExternalLogins { get; set; }

		public string ReturnUrl { get; set; }

		[TempData]
		public string ErrorMessage { get; set; }

		public class InputModel
		{
			[Required(ErrorMessage = "نام کاربری را وارد کنید")]
			public string Username { get; set; }

			[Required(ErrorMessage = "رمز عبور را وارد کنید")]
			[DataType(DataType.Password)]
			public string Password { get; set; }

			[Display(Name = "مرا به خاطر بسپار")]
			public bool RememberMe { get; set; }
		}

		public async Task OnGetAsync(string returnUrl = null)
		{
			if (User.Identity.IsAuthenticated) Response.Redirect("/");

			if (!string.IsNullOrEmpty(ErrorMessage))
			{
				ModelState.AddModelError(string.Empty, ErrorMessage);
			}

			returnUrl ??= Url.Content("~/");

			// Clear the existing external cookie to ensure a clean login process
			await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

			ReturnUrl = returnUrl;
		}

		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			returnUrl ??= Url.Content("~/");

			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(Input.Username);
				if (user == null) 
				{
					ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه است");
					return Page();
				}

				if (!user.IsEnabled)
				{
					ModelState.AddModelError(string.Empty, "حساب کاربری شما غیرفعال است");
					return Page();
				}

				var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.RememberMe, lockoutOnFailure: false);
				
				if (result.Succeeded)
				{
					_logger.LogInformation("User logged in.");

					if (User.IsInRole("admin") || User.IsInRole("operator")) return RedirectToAction("index", "home", new { area = "admin" });

					return LocalRedirect(returnUrl);
				}
				if (result.RequiresTwoFactor)
				{
					return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
				}
				if (result.IsLockedOut)
				{
					_logger.LogWarning("User account locked out.");
					return RedirectToPage("./Lockout");
				}
				else
				{
					ModelState.AddModelError(string.Empty, "نام کاربری یا رمز عبور اشتباه است");
					return Page();
				}
			}

			// If we got this far, something failed, redisplay form
			return Page();
		}
	}
}
