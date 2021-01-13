using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass
{
	public class CategoryFeature
	{
		public CategoryFeature()
		{
		}

		public CategoryFeature(string name)
		{
			Name = name;
		}

		[Key]
		public int Id { get; set; }
		[Required]
		[Column(TypeName = "nvarchar(100)")]
		public string Name { get; set; }
		[Required]
		public int CategoryId { get; set; }

		public virtual Category Category { get; set; }
	}
}
