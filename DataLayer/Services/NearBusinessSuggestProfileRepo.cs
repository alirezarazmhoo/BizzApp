using DataLayer.Data;
using DataLayer.Extensions;
using DataLayer.Infrastructure;
using DomainClass.Businesses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public  class NearBusinessSuggestProfileRepo : RepositoryBase<Business>, INearBusinessSuggestProfileRepo
	{
		public NearBusinessSuggestProfileRepo(ApplicationDbContext DbContext) : base(DbContext)
		{
		}
		public async Task<IEnumerable<Business>> Get(string UserId)
		{
			List<Business>  businesses = new List<Business>();
			var UserItem = await DbContext.Users.FirstOrDefaultAsync(s=>s.Id.Equals(UserId));
			var BusinessItems = await DbContext.Businesses.Include(s => s.District).Include(s => s.Category).Where(s => s.District.CityId == UserItem.CityId).ToListAsync();

			if (UserItem != null)
			{
				foreach (var item in BusinessItems)
				{
					if (!await DbContext.Reviews.AnyAsync(s => s.BizAppUserId.Equals(UserId) && s.BusinessId.Equals(item.Id)))
					{
						if (GetDistance.distance(UserItem.Latitude, UserItem.Longitude, item.Latitude, item.Longitude, 'K') < 10)
						{
							businesses.Add(item);
						}
					}
				}
				return businesses;
			}
			else return new List<Business>();
		}
	}
}
