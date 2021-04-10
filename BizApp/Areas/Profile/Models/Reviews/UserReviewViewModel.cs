using DomainClass.Enums;
using DomainClass.Review.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.Profile.Models.Reviews
{
	public class UserReviewViewModel
	{
		public Guid Id { get; set; }
		public int Rate { get; set; }
		public int UsefulCount { get; set; }
		public int FunnyCount { get; set; }
		public int CoolCount { get; set; }
		public string Description { get; set; }
		public StatusEnum Status { get; set; }

		public IEnumerable<ReviewMediaViewModel> Media { get; set; }
		public ReviewBusinessViewModel Business { get; set; }

		public class ReviewBusinessViewModel
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public int CityId { get; set; }
			public string FeatureImage { get; set; }
			public string CityName { get; set; }
			public string OwnerFullName { get; set; }
			//public string OwnerUserName { get; set; }
		}
		public class ReviewMediaViewModel
		{
			public DateTime CreatedAt { get; set; }
			public string Description { get; set; }
		}
	}
}
