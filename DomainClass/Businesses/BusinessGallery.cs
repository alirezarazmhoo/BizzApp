using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass.Businesses
{
	public class BusinessGallery
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public Guid BusinessId { get; set; }
		[Required]
		[Column(TypeName = "nvarchar(250)")]
		public string FileAddress { get; set; }

		public virtual Business Business { get; set; }
	}
}
