using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.BusinessProfile.Models
{
	public class BusinessProfileInputModelDto
	{
		[Required(ErrorMessage = "نام کاربری را وارد کنید")]
		public string Username { get; set; }
		[Required(ErrorMessage = "رمز عبور را وارد کنید")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Display(Name = "مرا به خاطر بسپار")]
		public bool RememberMe { get; set; }
	}
}
