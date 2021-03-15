using DataLayer.Data;
using DataLayer.Infrastructure.Reviews;
using DomainClass;
using DomainClass.Review;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class ReviewRepo : RepositoryBase<Review>, IReviewRepo
	{
		public ReviewRepo(ApplicationDbContext DbContext) : base(DbContext)
		{

		}
		public async Task<IEnumerable<Review>> GetRecentActivity(int? pageNumber)
		{
			pageNumber = pageNumber.HasValue== false ? 1 : pageNumber;

			var Items = await DbContext.Reviews
				.Include(s=>s.ReviewMedias)
				.Include(s=>s.UsersInReviewLikes)
				.Include(s=>s.BizAppUser)
				.ThenInclude(s=>s.ApplicationUserMedias)
				.Include(s=>s.Business).Where(s=>s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted)
				.Skip((pageNumber.Value - 1) * 10).Take(10).ToListAsync();
			return Items; 
		}
		public async Task<IEnumerable<CustomerBusinessMedia>> GetRecentActivityBusinessMedia(int? pageNumber)
		{
			pageNumber = pageNumber.HasValue == false ? 1 : pageNumber;

			var Items = await DbContext.CustomerBusinessMedias.Where(s => s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted)
				.Include(s => s.CustomerBusinessMediaPictures)
				.Include(s => s.BizAppUser)
				.ThenInclude(s => s.ApplicationUserMedias)
				.Include(s => s.UsersInCustomerBusinessMediaLikes).Where(s => s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted).Include(s=>s.Business)
				.Skip((pageNumber.Value - 1) * 10).Take(10).ToListAsync();
			return Items; 
		}




	}
}
