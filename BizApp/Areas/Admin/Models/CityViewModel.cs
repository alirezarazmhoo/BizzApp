using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BizApp.Areas.Admin.Models
{
	public class CityViewModel
	{
		public int CityId { get; set; }
		[Required(ErrorMessage = "نام راوارد کنید")]
		[MaxLength(100, ErrorMessage = "نام بسیار طولانی است")]
		[MinLength(1, ErrorMessage = "نام بسیار کوتاه است")]
		[DisplayName("نام")]
		public string Name { get; set; }
		[Required(ErrorMessage ="استان را انتخاب کنید")]
		public int ProvinceId { get; set; }
		public string ProvinceName { get; set; }
	}
}
