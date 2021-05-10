using DomainClass.Enums;
using DomainClass.Queries;
using System;
using System.Collections.Generic;

namespace DomainClass.Review.Queries
{
	public class UserReviewPaginateQuery : IUserActivityQuery
	{
		public Guid Id { get; set; }
		public int Rate { get; set; }
		public int UsefulCount { get; set; }
		public int FunnyCount { get; set; }
		public int CoolCount { get; set; }
		public string Description { get; set; }
		public StatusEnum Status { get; set; }
		public DateTime CreatedAt { get; set; }

		public ReviewMediaQuery[] Media { get; set; }
		public BusinessQuery Business { get; set; }

		public class BusinessQuery
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public int CategoryId { get; set; }
			public Dictionary<int, string> Categories { get; set; }
			public int CityId { get; set; }
			public string CityName { get; set; }
			public string FeatureImage { get; set; }
			public string OwnerFullName { get; set; }
			//public string OwnerUserName { get; set; }
		}
	}
}
