using DomainClass.Review;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure.Reviews
{
	public  interface IReviewRepo
	{
		Task<IEnumerable<Review>> GetRecentActivity(int? pageNumber);
		Task<IEnumerable<CustomerBusinessMedia>> GetRecentActivityBusinessMedia(int? pageNumber);
		Task<string> GetUsersFullName(Guid Id);
		Task<CustomerBusinessMedia> GetCustomerBusinessMediaById(Guid id); 
	}
}
