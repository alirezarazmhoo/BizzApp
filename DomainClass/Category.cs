using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DomainClass
{
	public class Category
	{
		public int Id { get; set; }
		[DisplayName("دسته بندی")]
		[Required(ErrorMessage ="نام{0} را وارد کنید ")]
		[MaxLength(50 , ErrorMessage ="طول نام بیش از حد زیاد است ")]
		public string Name { get; set; }
		public int? ParentCategoryId { get; set; }
		public int? Order { get; set; }

		public virtual Category ParentCategory { get; set; }
		public virtual ICollection<CategoryFeature> CategoryFeatures { get; set; }
		public virtual ICollection<CategoryTerm> Terms { get; set; }
	}
}
