using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class CategoryRepo : RepositoryBase<Category>, ICateogryRepo
	{
		public CategoryRepo(ApplicationDbContext DbContext) : base(DbContext)
		{
		}
		public async Task AddOrUpdate(Category model)
		{
			if (model.Id == 0)
				await CreateAsync(model);
			else
				Update(model);
		}
		public async Task<List<Category>> GetAll()
		{
			return await FindByCondition(f=>f.ParentCategoryId == null).ToListAsync();
		}
		public async Task<List<Category>> GetAll(string searchString)
		{
			return await FindByCondition(f => f.Name.Contains(searchString)).ToListAsync();
		}
		public async Task<Category> GetById(int id)
		{
			return await FindByCondition(f => f.Id == id).FirstOrDefaultAsync();
		}
		public void Remove(Category model)
		{
			Delete(model);
		}
		public async Task<bool> HasChild(int Id)
		{
			return await DbContext.Categories.AnyAsync(s => s.ParentCategoryId == Id);
		}

		public async Task<List<Category>> GetChilds(int Id)
		{
			return await FindByCondition(f => f.ParentCategoryId == Id).ToListAsync();
		}


		
	}
}
