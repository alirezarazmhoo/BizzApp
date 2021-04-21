using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass
{
	public class District
	{
		public District()
		{
		}

		public District(string name, int cityId)
		{
			Name = name;
			CityId = cityId;
		}

		[Key]
		public int Id { get; private set; }

		[Required]
		[Column(TypeName ="nvarchar(100)")]
		public string Name { get; set; }
		
		[Required]
		public int CityId { get; set; }
		public virtual City City { get; set; }

		public bool IsDefault { get; set; }


	}
}
