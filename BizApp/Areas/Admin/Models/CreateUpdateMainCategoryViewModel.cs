using System.ComponentModel.DataAnnotations;

namespace BizApp.Areas.Admin.Models
{
	public class CreateUpdateMainCategoryViewModel
	{
		public int CategoryId { get; set; }
		[Required]
		public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Icon { get; set; }
        public string IconWeb { get; set; }
		[Range(0, 10, ErrorMessage = "ترتیب باید عددی بین 1 تا 10 باشد")]
		public int? Order { get; set; }
		public bool ChangedPngIcon { get; set; }
		public bool ChangedFeatureImage { get; set; }
	}
}
