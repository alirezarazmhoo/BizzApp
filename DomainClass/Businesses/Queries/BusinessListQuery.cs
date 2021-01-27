using System;

namespace DomainClass.Businesses.Queries
{
	public class BusinessListQuery
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string DistrictName { get; set; }
		public string CategoryName { get; set; }
		public string CityName { get; set; }
		public DateTime CreatedDate { get; set; }
		public string Creator { get; set; }

	}
}
