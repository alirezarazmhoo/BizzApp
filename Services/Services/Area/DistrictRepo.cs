using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DomainClass.Queries;

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
			return await FindAll().Include(i => i.City).ToListAsync();
		}

		public async Task<List<District>> GetAll(int cityId)
		{
			return await FindByCondition(f => f.CityId == cityId).ToListAsync();
		}

		public async Task<List<District>> GetAll(string searchString)
		{
			return await FindByCondition(f => f.Name.Contains(searchString))
								.Include(i => i.City)
								.ToListAsync();
		}

		public async Task<List<DistrictWithParentsNameQuery>> GetAllWithParentNames(string searchString)
		{
			if (string.IsNullOrEmpty(searchString))
			{
				return new List<DistrictWithParentsNameQuery>();
			}

			var query = from city in DbContext.Cities
						join district in DbContext.Districts
						   on city.Id equals district.CityId into joined
						from j in joined.DefaultIfEmpty()
						join province in DbContext.Provinces
						   on city.ProvinceId equals province.Id
						select new DistrictWithParentsNameQuery
						{
							Id = j.Id == null ? city.Id : j.Id,
							ListName = province.Name + " - " + city.Name + " - " + (j.Name ?? "بدون ناحیه"),
							IsCity = j.Id == null ? true : false
						};

			var result = await query.Where(w => w.ListName.Contains(searchString)).ToListAsync();

			return result;
		}

		public async Task<DistrictWithParentsNameQuery> GetAllWithParentNamesById(int id)
		{

			var resutlt = await DbContext.Districts
					.Select(s => new DistrictWithParentsNameQuery
					{
						Id = s.Id,
						ListName = s.City.Province.Name + " - " + s.City.Name + " - " + s.Name
					})
					.FirstOrDefaultAsync(f => f.Id == id);

			return resutlt;
		}

		public async Task<District> GetById(int id)
		{
			return await FindByCondition(f => f.Id == id).FirstOrDefaultAsync();
		}

		public void Remove(District district)
		{
			Delete(district);
		}
		public async Task<List<int>> GetDeafults()
		{
			return await FindByCondition(s=>s.IsDefault).Select(s=>s.Id).ToListAsync();
		}
	}
}
