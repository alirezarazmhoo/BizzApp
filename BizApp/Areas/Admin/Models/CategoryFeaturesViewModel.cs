using System.ComponentModel.DataAnnotations;

namespace BizApp.Areas.Admin.Models
{
	public class CategoryFeaturesViewModel
	{
		public int CategoryFeatureId { get; set; }
		[Required]
		[MaxLength(99, ErrorMessage = "نام بسیار طولانی است")]
		[MinLength(2, ErrorMessage = "نام بسیار کوتاه است")]
		public string Name { get; set; }
		public int CategoryId { get; set; }
	}
}
