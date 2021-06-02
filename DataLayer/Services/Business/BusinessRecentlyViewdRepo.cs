using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass.Businesses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public	class BusinessRecentlyViewdRepo : RepositoryBase<BusinessRecentlyViewdRepo>, IBusinessRecentlyViewdRepo
	{
        public BusinessRecentlyViewdRepo(ApplicationDbContext DbContext) : base(DbContext)
        {
           
        }
        public async Task Add(BusinessRecentlyViewed model)
		{
            await DbContext.BusinessRecentlyVieweds.AddAsync(model);
		}
        public async Task<IEnumerable<BusinessRecentlyViewed>> Get(string UserId)
        {
            List<BusinessRecentlyViewed> businessRecentlyVieweds = new List<BusinessRecentlyViewed>();
            var List = await DbContext.BusinessRecentlyVieweds.Include(s=>s.Business).Include(s=>s.Business).ThenInclude(s=>s.District).Where(s => s.BizAppUserId.Equals(UserId)).OrderByDescending(s=>s.Date).ToListAsync();
			foreach (var item in List)
			{
                if(!await DbContext.Reviews.AnyAsync(s=>s.BizAppUserId.Equals(UserId)&& s.BusinessId.Equals(item.BusinessId)))
				{
                    businessRecentlyVieweds.Add(item);
                }
			}
            return businessRecentlyVieweds;
        }
        public async Task<bool> CheckDuplicate(string UserId ,Guid BusinessId)
		{
            return await DbContext.BusinessRecentlyVieweds.AnyAsync(s => s.BizAppUserId.Equals(UserId) && s.BusinessId.Equals(BusinessId));
		}
        public async Task Remove(Guid Id)
		{
            var Item = await DbContext.BusinessRecentlyVieweds.FirstOrDefaultAsync(s=>s.Id.Equals(Id));
            if(Item != null)
			{
                 DbContext.BusinessRecentlyVieweds.Remove(Item);
			}
		}


    }
}
