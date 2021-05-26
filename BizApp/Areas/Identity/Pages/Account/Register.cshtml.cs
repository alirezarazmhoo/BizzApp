using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using DomainClass;
using BizApp.Extensions;
using BizApp.Utility;
using System.Transactions;
using DomainClass.Infrastructure;
using DataLayer.Infrastructure;

namespace BizApp.Areas.Identity.Pages.Account
{
	[AllowAnonymous]
	public class RegisterModel : PageModel
	{
		private readonly SignInManager<BizAppUser> _signInManager;
		private readonly UserManager<BizAppUser> _userManager;
		private readonly ILogger<RegisterModel> _logger;
		private readonly IEmailSender _emailSender;
		private readonly IUnitOfWorkRepo _unitOfWork;

		public RegisterModel(
			UserManager<BizAppUser> userManager,
			SignInManager<BizAppUser> signInManager,
			ILogger<RegisterModel> logger,
			IEmailSender emailSender,
			IUnitOfWorkRepo unitOfWork)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
			_emailSender = emailSender;
			_unitOfWork = unitOfWork;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public string ReturnUrl { get; set; }

		public IList<AuthenticationScheme> ExternalLogins { get; set; }

		public class InputModel
		{
			[Required(ErrorMessage = "نام و نام خانوادگی خود را وارد کنید")]
			[Display(Name = "نام و نام خانوادگی", Prompt = "نام و نام خانوادگی")]
			public string FullName { get; set; }

			[Required(ErrorMessage = "نام کاربری را وارد کنید")]
			[Display(Name = "نام کاربری", Prompt = "نام کاربری")]
			[UniqueUserName]
			[RegularExpression(@"^([a-zA-Z]+)([a-zA-Z0-9]+)$", ErrorMessage = "نام کاربری تنها باید تلفیقی از حروف و اعداد باشد")]
			public string UserName { get; set; }

			[Required(ErrorMessage = "موبایل خود را وارد کنید")]
			[Display(Name = "موبایل", Prompt = "موبایل")]
			[Mobile(ErrorMessage = "شماره موبایل نامعتبر است")]
			[UniqueMemberMobile]
			public long Mobile { get; set; }

			[EmailAddress]
			[Display(Name = "ایمیل", Prompt = "ایمیل")]
			public string Email { get; set; }

			[Required(ErrorMessage = "رمز عبور را وارد کنید")]
			//[StringLength(100, ErrorMessage = "رمز عبور باید حداقل دارای {0 کاراکتر باشد}", MinimumLength = 6)]
			[DataType(DataType.Password)]
			[Display(Name = "رمز عبور", Prompt = "رمز عبور")]
			public string Password { get; set; }

			[PostalCode(ErrorMessage = "کد پستی نامعتبر است")]
			[Display(Name = "کد پستی", Prompt = "کد پستی")]
			public string PostalCode { get; set; }

			// birth date fields
			public int? Year { get; set; }
			public int? Month { get; set; }
			public int? Day { get; set; }

			public int? CityId { get; set; }

			//[DataType(DataType.Password)]
			//[Display(Name = "Confirm password")]
			//[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
			//public string ConfirmPassword { get; set; }
		}

		public async Task OnGetAsync(string returnUrl = null)
		{
			if (User.Identity.IsAuthenticated) Response.Redirect("/");

			ReturnUrl = returnUrl;
			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
		}

		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			returnUrl = returnUrl ?? Url.Content("~/");
			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

			if (ModelState.IsValid)
			{
				var user = new BizAppUser
				{
					FullName = Input.FullName,
					Mobile = Input.Mobile,
					UserName = Input.UserName,
					Email = Input.Email,
					PostalCode = Input.PostalCode,
					CityId = Input.CityId ,
					SecurityStamp = Guid.NewGuid().ToString()
				};

				// validate birth date
				if (Input.Year > 0 && (Input.Month > 0 && Input.Month < 13) && (Input.Day > 0 && Input.Day < 32))
				{
					// convert selected date to geo date
					var persianDate = $"{Input.Year}/{Input.Month:00}/{Input.Day:00}";
					var geoBirthDate = persianDate.ToGeorgianDateTime();

					// add birth date
					user.BirthDate = geoBirthDate;
				}

				// Create New User
				using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
				var result = await _userManager.CreateAsync(user, Input.Password);
				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(user, UserConfiguration.MemberRoleName);

					_logger.LogInformation("یک حساب کاربری جدید با موفقیت ثبت شد");

					scope.Complete();
					scope.Dispose();

					//var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					//code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
					//var callbackUrl = Url.Page(
					//    "/Account/ConfirmEmail",
					//    pageHandler: null,
					//    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
					//    protocol: Request.Scheme);

					//await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
					//    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

					//if (_userManager.Options.SignIn.RequireConfirmedAccount)
					//{
					//    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
					//}
					//else
					//{

					await _signInManager.SignInAsync(user, isPersistent: false);
					return LocalRedirect(returnUrl);
					//}
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}

			}

			// If we got this far, something failed, redisplay form
			return Page();
		}
	}
}
