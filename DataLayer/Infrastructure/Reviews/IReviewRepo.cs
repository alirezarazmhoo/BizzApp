using DomainClass.Businesses;
using DomainClass.Enums;
using DomainClass.Review;
using DomainClass.Review.Queries;
using Microsoft.AspNetCore.Http;
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
		
		Task<IEnumerable<UserReviewPaginateQuery>> GetUseReviews(string userName, int page);
		Task<UserReviewPaginateQuery> GetUseReview(Guid id);
		//Task<IEnumerable<UserReviewPaginateQuery>> GetCurrentUserReviews(string userId, int page);

		Task<IEnumerable<Review>> GetBusinessReviews(Guid Id);
		Task AddReview(Review model, IFormFile[] files , string[] captions);
		Task AddBusinessMedia(CustomerBusinessMedia model, IFormFile[] files);
		Task<IEnumerable<Business>> GuessReview(List<int> Districts, int DistricId, string UserId , int? pageNumber);
		Task<VotesAction> ChangeHelpFull(Guid Id, string UserId);
		Task<VotesAction> ChangeFunnyCount(Guid Id, string UserId);
		Task<VotesAction> ChangeCoolCount(Guid Id, string UserId);
		Task<VotesAction> ChangeLikeCount(Guid Id, string UserId);
		Task<int> GetUserTotalReview(string Id);
		Task<int> GetUserTotalReviewMedia(string Id);
		Task<int> GetUserTotalBusinessMedia(string Id);
		Task PostReview(Review Model, IFormFile[] file); 
	}
}
