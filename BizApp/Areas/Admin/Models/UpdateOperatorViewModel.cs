using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BizApp.Areas.Admin.Models
{
	public class UpdateOperatorViewModel
	{
		public string Id { get; set; }
		[Required(ErrorMessage = "نام و نام خانوادگی را وارد کنید")]
		[MaxLength(150, ErrorMessage = "نام و نام خانوادگی بسیار طولانی است")]
		[MinLength(3, ErrorMessage = "نام و نام خانوادگی حداقل باید دارای 3 کاراکتر باشد")]
		[DisplayName("نام و نام خانوادگی")]
		public string FullName { get; set; }
		[EmailAddress(ErrorMessage = "ایمیل وارد شده نامعتبر است")]
		[DisplayName("ایمیل")]
		public string Email { get; set; }
		
		[Required(ErrorMessage = "شماره موبایل را وارد کنید")]
		[RegularExpression(@"^(\d{10})$", ErrorMessage = "شماره موبایل را وارد کنید")]
		[DisplayName("شماره موبایل")]
		public long Mobile { get; set; }
		[MaxLength(100, ErrorMessage = "نشانی بسیار طولانی است")]
		[MinLength(4, ErrorMessage = "نشانی حداقل باید دارای 6 کاراکتر باشد")]
		[DisplayName("نشانی")]
		public string Address { get; set; }
		public bool IsEnabled { get; set; }
	}
}
