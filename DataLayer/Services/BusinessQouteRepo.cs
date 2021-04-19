using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Businesses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class BusinessQouteRepo : RepositoryBase<BusinessQoute>, IBusinessQouteRepo
	{
		public BusinessQouteRepo(ApplicationDbContext DbContext) : base(DbContext)
		{
		}

		public async Task AddOrUpdate(BusinessQoute BusinessQoute)
		{
			if (BusinessQoute.Id == 0)
				await CreateAsync(BusinessQoute);
			else
				Update(BusinessQoute);
		}

		public async Task<List<BusinessQoute>> GetAll()
		{
			return await FindAll().ToListAsync();
		}

		public async Task<List<BusinessQoute>> GetAll(string searchString)
		{
			return await FindByCondition(f => f.Ask.Contains(searchString)).ToListAsync();
		}

		public async Task<BusinessQoute> GetById(int id)
		{
			return await FindByCondition(f => f.Id == id).FirstOrDefaultAsync();
		}
		public async Task<List<BusinessQoute>> GetByCategoryId(int CategoryId)
		{
			return await FindByCondition(f => f.CategoryId==CategoryId).ToListAsync();
		}
		public void Remove(BusinessQoute BusinessQoute)
		{
			Delete(BusinessQoute);
		}
	}
}
