using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass
{
	public class City
	{
		public City()
		{
		}

		public City(string name, int provinceId)
		{
			Name = name;
			ProvinceId = provinceId;
		}

		#region Properties
		[Key]
		public int Id { get; private set; }

		[Required]
		[Column(TypeName ="nvarchar(100)")]
		public string Name { get; set; }

		[Required]
		public int ProvinceId { get; set; }


		public virtual Province Province { get; set; }
		public virtual ICollection<District> Districts { get; set; }
		public virtual ICollection<BizAppUser> Users { get; set; }
		
		#endregion
	}
}
