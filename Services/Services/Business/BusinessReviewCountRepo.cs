using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Businesses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class BusinessReviewCountRepo : RepositoryBase<Business>, IBusinessReviewCountRepo
	{
		public BusinessReviewCountRepo(ApplicationDbContext DbContext) : base(DbContext)
		{
		}

		public async Task<int> Count(Guid id)
		{
			if(await DbContext.Businesses.AnyAsync(s=>s.Id.Equals(id))) {
				return DbContext.CustomerBusinessMedias.Where(s => s.BusinessId.Equals(id)).Count();			
			}
			else
			{
				return 0; 
			}
		}
	}
}
