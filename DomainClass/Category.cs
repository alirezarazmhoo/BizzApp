using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
		[Column(TypeName = "varchar(50)")]
		public string Icon { get; set; }
		public virtual Category ParentCategory { get; set; }
		public virtual ICollection<CategoryFeature> CategoryFeatures { get; set; }
	}
}
