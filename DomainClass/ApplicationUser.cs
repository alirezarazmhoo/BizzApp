using System;
using Microsoft.AspNetCore.Identity;


namespace DomainClass
{
	public class ApplicationUser : IdentityUser
	{
		public string FullName { get; set; }
		public long Mobile { get; set; }
		public string Address { get; set; }
		public string NationalCode { get; set; }
		public string ApiToken { get; set; }
		public string Url { get; set; }
		public string Password { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.Now;
		//public UserType UserType { get; set; }
	}
}
