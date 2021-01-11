using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BizApp.Areas.Admin.Models
{
	public class DistrictViewModel
	{
		public int DistrictId { get; set; }
		[Required(ErrorMessage = "نام راوارد کنید")]
		[MaxLength(100, ErrorMessage = "نام بسیار طولانی است")]
		[MinLength(1, ErrorMessage = "نام بسیار کوتاه است")]
		[DisplayName("نام")]
		public string Name { get; set; }
		[Required(ErrorMessage = "استان را انتخاب کنید")]
		public int CityId { get; set; }
		public string CityName { get; set; }
	}
}
