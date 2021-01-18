using System;

namespace DomainClass.Businesses.Queries
{
	public class BusinessListQuery
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string DistrictName { get; set; }
		public string CategoryName { get; set; }
	}
}
