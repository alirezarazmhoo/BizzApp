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
		public int Order { get; set; }
	}
}
