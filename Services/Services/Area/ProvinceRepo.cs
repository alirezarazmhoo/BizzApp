using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class ProvinceRepo : RepositoryBase<Province>, IProvinceRepo
	{
		public ProvinceRepo(ApplicationDbContext DbContext) : base(DbContext)
		{
		}

		public async Task AddOrUpdate(Province province)
		{
			if (province.Id == 0)
				await CreateAsync(province);
			else
				Update(province);
		}

		public async Task<List<Province>> GetAll()
		{
			return await FindAll().ToListAsync();
		}

		public async Task<List<Province>> GetAll(string searchString)
		{
			return await FindByCondition(f => f.Name.Contains(searchString)).ToListAsync();
		}

		public async Task<Province> GetById(int id)
		{
			return await FindByCondition(f => f.Id == id).FirstOrDefaultAsync();
		}

		public void Remove(Province province)
		{
			Delete(province);
		}
	}
}
