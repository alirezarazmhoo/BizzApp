using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass
{
	public class Province
	{
		public Province()
		{
		}

		public Province(string name)
		{
			Name = name;
		}

		#region Properties
		[Key]
		public int Id { get; set; }
		[Required]
		[Column(TypeName = "nvarchar(50)")]
		public string Name { get; set; }

		public virtual ICollection<City> Cities{ get; set; }
		#endregion
	}
}
