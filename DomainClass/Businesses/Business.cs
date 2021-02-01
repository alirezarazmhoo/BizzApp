using DomainClass.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DomainClass.Businesses
{
	public class Business : ICreator
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
		[Column(TypeName = "nvarchar(11)")]
		public string PostalCode { get; set; }
		[Column(TypeName = "nvarchar(100)")]
		public string WebsiteUrl { get; set; }
		[Column(TypeName = "nvarchar(50)")]
		public string Email { get; set; }
		[Column(TypeName = "nvarchar(100)")]
		public string Biography { get; set; }
		[Required]
		public int DistrictId { get; set; }
		public int? CategoryId { get; set; }
		[Column(TypeName = "nvarchar(255)")]
		public string FeatureImage { get; set; }

		public virtual District District { get; set; }
		public virtual Category Category { get; set; }

		[AllowNull]
		public string UserCreatorId { get; set; }
		public BizAppUser UserCreator { get; set;  }
		[Required]
		public DateTime CreatedDate { get; set; } = DateTime.Now;


		public int?  CityId { get; set; }
		public virtual City City { get; set; }
		public int? ProvinceId { get; set; }
		public virtual Province Province { get; set; }

		public double Latitude { get; set; }
		public double Longitude { get; set; }

		public virtual ICollection<BusinessCallNumber> CallNumbers { get; set; }
		public virtual ICollection<BusinessGallery> Galleries { get; set; }
		public virtual ICollection<BusinessFeature> Features { get; set; }
		public virtual ICollection<BusinessTime> BusinessTimes{ get; set; }


	}
}
