using System.ComponentModel.DataAnnotations;

namespace BizApp.Areas.Admin.Models
{
	public class FeatureViewModel
	{
		public int FeatureId { get; set; }
		[Required(ErrorMessage = "نام راوارد کنید")]
		[MaxLength(99, ErrorMessage = "نام بسیار طولانی است")]
		[MinLength(1, ErrorMessage = "نام بسیار کوتاه است")]
		public string Name { get; set; }
		public int FeatureType { get; set;  }
	}
}
