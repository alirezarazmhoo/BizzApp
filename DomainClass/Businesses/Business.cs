using DomainClass.Infrastructure;
using DomainClass.Review;
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
		[Column(TypeName = "nvarchar(300)")]
		public string Description { get; set; }
		[Column(TypeName = "nvarchar(200)")]
		public string Address { get; set; }
		[Column(TypeName = "nvarchar(11)")]
		public string PostalCode { get; set; }
		[Column(TypeName = "nvarchar(100)")]
		public string WebsiteUrl { get; set; }
		[Column(TypeName = "nvarchar(50)")]
		public string Email { get; set; }
		[Column(TypeName = "nvarchar(300)")]
		public string Biography { get; set; }
		[Required]
		public int DistrictId { get; set; }
		public int CategoryId { get; set; }
		[Column(TypeName = "nvarchar(255)")]
		public string FeatureImage { get; set; }
		[Required]
		public long CallNumber { get; set; }
		public string OwnerId { get; set; }
		[AllowNull]
		public string UserCreatorId { get; set; }
		[Required]
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public int Rate { get; set; }
		public bool IsSponsor { get; set; }
		public string BoldFeature { get; set; }
		public bool IsClaimed { get; set; }
		public bool IsOpenNow { get; set; }

		// Relations
		public virtual District District { get; set; }
		public virtual Category Category { get; set; }
		public virtual ICollection<BusinessCallNumber> CallNumbers { get; set; }
		public virtual ICollection<BusinessGallery> Galleries { get; set; }
		public virtual ICollection<BusinessFeature> Features { get; set; }
		public virtual ICollection<BusinessTime> BusinessTimes{ get; set; }
		public virtual ICollection< CustomerBusinessMedia>  CustomerBusinessMedias { get; set; }
		public virtual ICollection<BusinessFaq>  BusinessFaqs { get; set; }
		public virtual ICollection<MessageToBusiness> MessageToBusinesses { get; set; }
		public virtual ICollection<Review.Review>  Reviews { get; set; }

		public virtual BizAppUser Owner { get; set; }
		public virtual BizAppUser UserCreator { get; set;  }
	}
}
