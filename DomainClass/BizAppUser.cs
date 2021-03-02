using DomainClass.Enums;
using DomainClass.Infrastructure;
using System;

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

		public virtual City City { get; set; }
	}
}
