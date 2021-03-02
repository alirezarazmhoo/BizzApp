using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass
{
	public class CategoryTerm
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int CategoryId { get; set; }
		[Required]
		[Column(TypeName = "varchar(50)")]
		public string Key { get; set; }
		[Required]
		[Column(TypeName = "nvarchar(MAX)")]
		public string Value { get; set; }

		public virtual Category Category { get; set; }
	}
}
