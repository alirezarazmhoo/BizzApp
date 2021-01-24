using BizApp.Utility;
using DomainClass;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BizApp.Areas.Admin.Models
{
	public class UserViewModel
	{
		public PaginatedList<BizAppUser> UserList{ get; set; }
		public BizAppUser User{ get; set; }
		public Guid Id { get; set; }
		[Required(ErrorMessage = "نام و نام خانوادگی را وارد کنید")]
		[MaxLength(150, ErrorMessage = "نام و نام خانوادگی بسیار طولانی است")]
		[MinLength(3, ErrorMessage = "نام و نام خانوادگی حداقل باید دارای 3 کاراکتر باشد")]
		[DisplayName("نام و نام خانوادگی")]
		public string FullName { get; set; }
		[EmailAddress(ErrorMessage = "ایمیل وارد شده نامعتبر است")]
		[DisplayName("ایمیل")]
		public string Email { get; set; }
		[Required(ErrorMessage = "نام کاربری راوارد کنید")]
		[MaxLength(100, ErrorMessage = "نام کاربری بسیار طولانی است")]
		[MinLength(1, ErrorMessage = "نام کاربری بسیار کوتاه است")]
		[DisplayName("نام کاربری")]
		public string Username { get; set; }
		[Required(ErrorMessage = "رمز راوارد کنید")]
		[MaxLength(100, ErrorMessage = "رمز عبور بسیار طولانی است")]
		[MinLength(6, ErrorMessage = "نام عبور حداقل باید دارای 6 کاراکتر باشد")]
		[DisplayName("رمز عبور")]
		public string Password { get; set; }
		[Required(ErrorMessage = "شماره موبایل را وارد کنید")]
		[RegularExpression(@"^(\d{10})$", ErrorMessage = "شماره موبایل را وارد کنید")]
		[DisplayName("شماره موبایل")]
		public long Mobile { get; set; }
		[MaxLength(100, ErrorMessage = "نشانی بسیار طولانی است")]
		[MinLength(6, ErrorMessage = "نشانی حداقل باید دارای 6 کاراکتر باشد")]
		[DisplayName("نشانی")]
		public string Address { get; set; }
		public string RoleName { get; set; }

	}
}
