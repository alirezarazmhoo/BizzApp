using System;

namespace DomainClass.Businesses.Commands
{
	public class CreateBusinessCommand
	{
		//public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Address { get; set; }
		public string PostalCode { get; set; }
		public string WebsiteUrl { get; set; }
		public string Email { get; set; }
		public string Biography { get; set; }
		public int DistrictId { get; set; }
		public int CategoryId { get; set; }
		public string FeatureImage { get; set; }
		public long CallNumber { get; set; }
		public long? Mobile { get; set; }
		public string OwnerId { get; set; }
		public string UserCreatorId { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
