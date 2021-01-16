using System;
using System.Collections.Generic;

namespace DomainClass.Businesses
{
	public class Business
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Address { get; set; }
		public string WebsiteUrl { get; set; }
		public int DistrictId { get; set; }
		public int CategoryId { get; set; }
		public string FeatureImage { get; set; }

		public virtual ICollection<BusinessCallNumber> CallNumbers { get; set; }
		public virtual ICollection<BusinessGallery> Galleries { get; set; }
		public virtual ICollection<BusinessTime> BusinessTimes{ get; set; }


	}
}
