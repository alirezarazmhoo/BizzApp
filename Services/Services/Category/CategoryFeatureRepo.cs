using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class CategoryFeatureRepo : RepositoryBase<CategoryFeature>, ICategoryFeatureRepo
	{
		public CategoryFeatureRepo(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task AddOrUpdate(CategoryFeature model)
		{
			if (model.Id == 0) 
				await CreateAsync(model);
			else
				Update(model);
		}

		public async Task<List<CategoryFeature>> GetAll(int categoryId)
		{
			return await FindByCondition(f => f.CategoryId == categoryId)
							//.Include(i => i.Category)
							.ToListAsync();
		}

		public async Task<List<CategoryFeature>> GetAll(int categoryId, string searchString)
		{
			return await FindByCondition(f => f.CategoryId == categoryId && f.Name.Contains(searchString))
							//.Include(i => i.Category)
							.ToListAsync();
		}

		public async Task<CategoryFeature> GetById(int id)
		{
			return await FindByCondition(f => f.Id == id).FirstOrDefaultAsync();
		}

		public void Remove(CategoryFeature model)
		{
			Delete(model);
		}
	}
}
