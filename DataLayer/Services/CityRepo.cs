using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class CityRepo : RepositoryBase<City>, ICityRepo
	{
		public CityRepo(ApplicationDbContext DbContext) : base(DbContext)
		{
		}

		public async Task AddOrUpdate(City model)
		{
			if (model.Id == 0)
				await CreateAsync(model);
			else
				Update(model);
		}

		public async Task<List<City>> GetAll()
		{
			return await FindAll().Include(i => i.Province).ToListAsync();
		}

		public async Task<List<City>> GetAll(string searchString)
		{
			return await FindByCondition(f => f.Name.Contains(searchString)).Include(i => i.Province).ToListAsync();
		}

		public async Task<City> GetById(int id)
		{
			return await FindByCondition(f => f.Id == id).FirstOrDefaultAsync();
		}

		public void Remove(City city)
		{
			Delete(city);
		}
	}
}
