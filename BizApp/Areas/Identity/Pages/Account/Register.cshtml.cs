﻿using System;
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

namespace BizApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<BizAppUser> _signInManager;
        private readonly UserManager<BizAppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<BizAppUser> userManager,
            SignInManager<BizAppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
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

            [Required(ErrorMessage = "موبایل خود را وارد کنید")]
            [Display(Name = "موبایل", Prompt = "موبایل")]
            [Mobile(ErrorMessage = "شماره موبایل نامعتبر است")]
            public long Mobile { get; set; }

			[EmailAddress]
            [Display(Name = "ایمیل", Prompt = "ایمیل")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "رمز عبور باید حداقل دارای {0 کاراکتر باشد}", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "رمز عبور", Prompt = "رمز عبور")]
            public string Password { get; set; }

            [PostalCode(ErrorMessage = "کد پستی نامعتبر است")]
            [Display(Name = "کد پستی", Prompt = "کد پستی")]
            public string PostalCode { get; set; }

			public int Year { get; set; }
			public int Month { get; set; }
			public int Day { get; set; }

			//[DataType(DataType.Password)]
			//[Display(Name = "Confirm password")]
			//[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
			//public string ConfirmPassword { get; set; }
		}

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new BizAppUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
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
