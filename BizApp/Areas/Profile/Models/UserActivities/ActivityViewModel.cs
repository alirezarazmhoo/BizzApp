using DomainClass.Queries;
using System;

namespace BizApp.Areas.Profile.Models.UserActivities
{
	public class ActivityViewModel
	{
		public ActivityViewModel(DateTime createdAt, string description)
		{
			Description = description;
			CreatedAt = createdAt;
		}

		public string Description { get; private set; }
		public DateTime CreatedAt { get; private set; }
		public IUserActivityQuery Data { get; set; }
	}
}
