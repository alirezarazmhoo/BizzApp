using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class UserFavoritsRepo : RepositoryBase<UserFavorits>, IUserFavoritsRepo
	{
		public UserFavoritsRepo(ApplicationDbContext DbContext) : base(DbContext)
		{
		}
		public async Task<IEnumerable< UserFavorits>> GetAll(Guid Id)
		{
			return await DbContext.UserFavorits.Where(s => s.BizAppUserId.Equals(Id)).ToListAsync();
		}
		public async  Task<VotesAction> AddOrRemove(string Id, string UserId)
		{
			UserFavorits userFavorits = new UserFavorits();
			if(await DbContext.UserFavorits.AnyAsync(s=>s.BusinessId.Equals(Id) && s.BizAppUserId.Equals(UserId)))
			{
				var Item = await DbContext.UserFavorits.Where(s => s.BizAppUserId.Equals(UserId) && s.BusinessId.Equals(Id)).FirstOrDefaultAsync();
				 DbContext.UserFavorits.Remove(Item);
				return VotesAction.Submission; 
			}
			else
			{
				userFavorits.BizAppUserId = UserId;
				userFavorits.BusinessId = Id;
				userFavorits.Date = DateTime.Now;
				
				await DbContext.UserFavorits.AddAsync(userFavorits);
				return VotesAction.Add; 
			}
		}


	}
}
