﻿using DomainClass;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace BizApp.Areas.Profile.Models
{
	public class ProfileViewModel
	{
		public string Id { get; set; }
		public string FullName { get; set; }
		public DateTime RegisterDate { get; set; }
		public int ReviewCount { get; set; }
		public int UploadedPhotoCount { get; set; }
		public string ProvinceName { get; set; }
		public string CityName { get; set; }
		public IList<string> Photos { get; set; }

		//public ClaimsIdentity GenerateUserIdentityAsync(UserManager<BizAppUser> manager)
		//{
		//	// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
		//	var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

		//	// Add custom user claims here
		//	identity.AddClaim(new Claim("FullName", this.FullName));
		//	// ... add other claims as you like.

		//	return identity;
		//}
	}
}
