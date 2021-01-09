using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class DistrictRepo : RepositoryBase<District>, IDistrictRepo
	{
		public DistrictRepo(ApplicationDbContext DbContext) : base(DbContext)
		{
		}

		public async Task AddOrUpdate(District district)
		{
			if (district.Id == 0)
				await CreateAsync(district);
			else
				Update(district);
		}

		public async Task<List<District>> GetAll()
		{
			return await FindAll().ToListAsync();
		}

		public async Task<District> GetById(int id)
		{
			return await FindByCondition(f => f.Id == id).FirstOrDefaultAsync();
		}

		public void Remove(District district)
		{
			Delete(district);
		}
	}
}
