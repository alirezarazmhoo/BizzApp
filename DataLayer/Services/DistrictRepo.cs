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

		public async Task<List<DistrictWithParentsName>> GetAllWithParentNames(string searchString)
		{
			if (string.IsNullOrEmpty(searchString))
			{
				return new List<DistrictWithParentsName>();
			}

			var resutlt = await DbContext.Districts
				.Select(s => new DistrictWithParentsName
				{
					Id = s.Id,
					ListName = s.City.Province.Name + " - " + s.City.Name + " - " + s.Name
				})
				.Where(w => w.ListName.Contains(searchString))
				.ToListAsync();

			return resutlt;
		}

		public async Task<DistrictWithParentsName> GetAllWithParentNamesById(int id)
		{

			var resutlt = await DbContext.Districts
					.Select(s => new DistrictWithParentsName
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
	}
}
