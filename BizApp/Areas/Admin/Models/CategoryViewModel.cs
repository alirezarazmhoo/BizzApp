using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.Admin.Models
{
	public class CategoryViewModel
	{
		public int CategoryId { get; set; }
		[Required]
		public string Name { get; set; }
		public int? ParentCategoryId { get; set; }

		public bool HasChild { get; set; }
		public int ChildCount { get; set; }
	}
}
