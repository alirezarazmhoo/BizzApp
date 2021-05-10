using System.ComponentModel.DataAnnotations;

namespace BizApp.Areas.Profile.Models.Account
{
	public class ChangePasswordViewModel
	{
		[Required(ErrorMessage = "رمز فعلی اجباری است")]
		[DataType(DataType.Password)]
		public string CurrentPassword { get; set; }
		[Required(ErrorMessage = "رمز جدید احباری است")]
		[MinLength(8, ErrorMessage = "طول رمز عبور حداقل باید 8 کاراکتر باشد")]
		[MaxLength(50, ErrorMessage = "طول رمز عبور بیش از حد مجاز است")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }
		[Required(ErrorMessage = "تکرار رمز جدید اجباری است")]
		[MinLength(8, ErrorMessage = "طول رمز عبور حداقل باید 8 کاراکتر باشد")]
		[MaxLength(50, ErrorMessage = "طول رمز عبور بیش از حد مجاز است")]
		[Compare("NewPassword", ErrorMessage = "رمز عبور جدید با تکرار آن تطبیق ندارد")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
