using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BizApp.Areas.Admin.Models
{
	public class ProvinceViewModel
	{
		public int ProvinceId { get; set; }
		[Required(ErrorMessage = "نام راوارد کنید")]
		[MaxLength(50, ErrorMessage = "نام بسیار طولانی است")]
		[MinLength(1, ErrorMessage = "نام بسیار کوتاه است")]
		[DisplayName("نام")]
		public string Name { get; set; }
	}
}
