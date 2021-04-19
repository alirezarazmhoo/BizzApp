using DomainClass.Review.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.Profile.Models.UserActivities
{
	public class UserActivityViewModel
	{
		public IEnumerable<UserReviewPaginateQuery> Reviews { get; set; }

	}
}
