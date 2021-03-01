using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BizApp.Areas.Admin.Models
{
	public class UserViewModel : UpdateOperatorViewModel
	{
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
		public string roleId { get; set; }

	}
}
