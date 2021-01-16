using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass.Businesses
{
	public class BusinessCallNumber
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public Guid BusinessId { get; set; }
		[Required]
		[Column(TypeName = "varchar(12)")]
		public string Number { get; set; }

		public virtual Business Business { get; set; }
	}
}
