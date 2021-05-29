using DataLayer.Data;
using DataLayer.Extensions;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class UserActivityRepo : RepositoryBase<UserActivity>, IUserActivityRepo
	{
		public UserActivityRepo(ApplicationDbContext dbContext, UserManager<BizAppUser> userManager) : base(dbContext, userManager)
		{
		}

		public async Task<IList<UserActivity>> GetAllActivities(string userId, int page = 1)
		{

			return await DbContext.UserActivities.Where(s => s.UserId == userId).Paginate(page, 10)
				.OrderByDescending(x => x.CreatedAt)
				.ToListAsync(); ;
		}

		public async Task AddAsync(TableName table, string tableKey, string currentUserId, string description)
		{
			var model = new UserActivity
			{
				CreatedAt = DateTime.Now,
				TableName = table,
				TableKey = tableKey,
				UserId = currentUserId
			};

			//if (string.IsNullOrEmpty(model.TableName)) return;

			await DbContext.UserActivities.AddAsync(model);
			await DbContext.SaveChangesAsync();
		}

		public async Task Remove(string tableKey)
		{
			var userActivity = await DbContext.UserActivities.FirstOrDefaultAsync(f => f.TableKey == tableKey);

			if (userActivity == null) return;

			DbContext.UserActivities.Remove(userActivity);
			await DbContext.SaveChangesAsync();
		}
	}

	//public enum ActivityObject
	//{
	//	UserPhoto,
	//	Review
	//}
}
