using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass.Businesses
{
	public class Business
	{
		[Key]
		public Guid Id { get; set; }
		[Required]
		[Column(TypeName = "nvarchar(50)")]
		public string Name { get; set; }
		[Column(TypeName = "nvarchar(255)")]
		public string Description { get; set; }
		[Column(TypeName = "nvarchar(200)")]
		public string Address { get; set; }
		[Column(TypeName = "nvarchar(100)")]
		public string WebsiteUrl { get; set; }
		[Required]
		public int DistrictId { get; set; }
		public int? CategoryId { get; set; }
		[Column(TypeName = "nvarchar(255)")]
		public string FeatureImage { get; set; }

		public virtual District District { get; set; }
		public virtual Category Category { get; set; }

		public virtual ICollection<BusinessCallNumber> CallNumbers { get; set; }
		public virtual ICollection<BusinessGallery> Galleries { get; set; }
		public virtual ICollection<BusinessFeature> Features { get; set; }
		public virtual ICollection<BusinessTime> BusinessTimes{ get; set; }


	}
}
