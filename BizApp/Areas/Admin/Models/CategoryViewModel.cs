using System.ComponentModel.DataAnnotations;

namespace BizApp.Areas.Admin.Models
{
	public class CategoryViewModel
	{
		public int CategoryId { get; set; }
		[Required]
		public string Name { get; set; }
		public int? ParentCategoryId { get; set; }
		public int? Order { get; set; }
		public bool HasChild { get; set; }
		public int ChildCount { get; set; }
	}
}
