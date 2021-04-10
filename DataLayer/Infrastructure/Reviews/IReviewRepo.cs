using DomainClass.Review;
using DomainClass.Review.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure.Reviews
{
	public  interface IReviewRepo
	{
		Task<IEnumerable<Review>> GetRecentActivity(int? pageNumber);
		Task<IEnumerable<CustomerBusinessMedia>> GetRecentActivityBusinessMedia(int? pageNumber);
		Task<string> GetUsersFullName(Guid Id);
		Task<CustomerBusinessMedia> GetCustomerBusinessMediaById(Guid id);
		Task<int> BusinessReviewCount(Guid Id);
		
		Task<IEnumerable<ReviewPaginateQuery>> GetUseReviews(string userName, int page);
	}
}
