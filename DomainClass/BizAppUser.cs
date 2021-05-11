using DomainClass.Businesses;
using DomainClass.Enums;
using DomainClass.Infrastructure;
using DomainClass.Review;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DomainClass
{
	public class BizAppUser : ApplicationUser, ISoftDelete
	{
		public bool IsDeleted { get; set; }
		public bool IsEnabled { get; set; }
		public string UploadedPhoto { get; set; }
		public GenderEnum Gender { get; set; }
		public int? CityId { get; set; }
		public DateTime? BirthDate { get; set; }
		public string ShortDescription { get; set; }
		public string FindMeIn { get; set; }
		public string HomeTown { get; set; }
		public string Webstie { get; set; }
		public string LongDescription { get; set; }
		public bool PhotoChanged { get; set; }
		public string PostalCode { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string MyFavoritMovie { get; set; }
		public string WhyYouShouldReadMyReviews {get; set; }
		public string WhenImNotYelping { get; set;  }

		public virtual ICollection<Review.Review> Reviews { get; set; }
		public virtual ICollection<CustomerBusinessMedia> CustomerBusinessMedia { get; set; }
		public virtual ICollection<BusinessFaq>  BusinessFaqs { get; set; }
		public virtual City City { get; set; }
		public ClaimsIdentity GenerateUserIdentityAsync(UserManager<BizAppUser> manager, ClaimsIdentity identity)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			//var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

			// Add custom user claims here
			identity.AddClaim(new Claim("FullName", this.FullName));
			// ... add other claims as you like.

			return identity;
		}

	}
}
