using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainClass.Businesses
{
	public class BusinessQoute
	{
		[Key]
		public int Id { get; set; }
		public int CategoryId { get; set; }
		[Required]
		[Column(TypeName = "NVARCHAR(500)")]
		public string Ask { get; set; }
		[Required]
		public string Answer { get; set; }
		public bool IsSelectedAnswer { get; set; }

		public virtual Category Category { get; set; }
		public virtual ICollection<BusinessQouteUser> Qoutes { get; set; }
	}
}
