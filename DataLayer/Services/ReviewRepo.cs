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
			if (pageNumber == 1)
			{
				pageNumber += 1;
			}
			else
			{
				pageNumber = pageNumber.HasValue == false ? 1 : pageNumber;
			}
			var Items = await DbContext.Reviews
				.Include(s => s.ReviewMedias)
				.Include(s => s.UsersInReviewLikes)
				.Include(s => s.BizAppUser)
				.ThenInclude(s => s.ApplicationUserMedias)
				.Include(s => s.Business).Where(s => s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted)
				.Skip((pageNumber.Value - 1) * 10).Take(3).ToListAsync();
			return Items;
		}
		public async Task<IEnumerable<CustomerBusinessMedia>> GetRecentActivityBusinessMedia(int? pageNumber)
		{
			pageNumber = pageNumber.HasValue == false ? 1 : pageNumber;

			var Items = await DbContext.CustomerBusinessMedias.Where(s => s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted)
				.Include(s => s.CustomerBusinessMediaPictures)
				.Include(s => s.BizAppUser)
				.ThenInclude(s => s.ApplicationUserMedias)
				.Include(s => s.UsersInCustomerBusinessMediaLikes).Where(s => s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted).Include(s => s.Business)
				.Skip((pageNumber.Value - 1) * 10).Take(3).ToListAsync();
			return Items;
		}
		public async Task<string> GetUsersFullName(Guid Id)
		{
			string FullNames = string.Empty;
			string newChar = "";
			var Item = await DbContext.CustomerBusinessMediaPictures.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			if (Item != null)
			{
				var ItemsObject = await DbContext.UsersInCustomerBusinessMediaLikes.Include(s=>s.BizAppUser).Where(s => s.CustomerBusinessMediaPicturesId.Equals(Item.Id)).ToListAsync();
				for (int i = 0; i < ItemsObject.Count; i++)
				{
					FullNames = ItemsObject[i].BizAppUser.FullName;
					newChar = ItemsObject[i].BizAppUser.FullName.Insert(i, "-");
					FullNames = newChar;
				}
			}
			return FullNames; 
		}
	}
}
