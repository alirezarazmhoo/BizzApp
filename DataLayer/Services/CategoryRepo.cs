using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Businesses.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
		public async Task<IEnumerable<Category>> GetAll()
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

		public async Task<int> GetChildCount (int Id)
		{
			return await FindByCondition(f => f.ParentCategoryId == Id).CountAsync();
		}

		public async Task<ChildsCategoryResponse> AdminGetChildsCateogry(int Id)
		{
			List<ComboBoxViewModel> items = new List<ComboBoxViewModel>();
			ChildsCategoryResponse childsCategoryResponse = new ChildsCategoryResponse();
			var categoryitems = await DbContext.Categories.Where(s => s.ParentCategoryId == Id).ToListAsync();
			if (categoryitems.Count() > 0)
			{
				foreach (var item in categoryitems)
				{
					items.Add(new ComboBoxViewModel() {  id = item.Id,  name = item.Name,   havenext = DbContext.Categories.Any(s => s.ParentCategoryId == item.Id) });
				}
				childsCategoryResponse.items = items;
				childsCategoryResponse.Isfinal = false;
				childsCategoryResponse.Parentid = Id;
				return childsCategoryResponse; 
			}
			else
			{
				var catitem = await DbContext.Categories.Where(s => s.Id == Id).FirstOrDefaultAsync();
				if (catitem != null)
				{
					items.Add(new ComboBoxViewModel() {   id = catitem.Id,   name = catitem.Name  });
				}
				childsCategoryResponse.items = items;
				childsCategoryResponse.Isfinal = true;
				childsCategoryResponse.Parentid = Id;
				return childsCategoryResponse;
			}
		}

		public async Task<ChildsCategoryResponse> GetBackCategories(int Id)
		{
			bool isfinal = true;
			List<ComboBoxViewModel> items = new List<ComboBoxViewModel>();
			ChildsCategoryResponse childsCategoryResponse = new ChildsCategoryResponse();

			var categoriitem = await DbContext.Categories.Where(s => s.Id == Id).FirstOrDefaultAsync();
			if (categoriitem != null)
			{
				var previouscategores = await DbContext.Categories.Where(s => s.ParentCategoryId == categoriitem.ParentCategoryId).ToListAsync();
				if (previouscategores.Count() > 0)
				{
					foreach (var item in previouscategores)
					{
						items.Add(new ComboBoxViewModel() {  id = item.Id,  name = item.Name,   havenext = DbContext.Categories.Any(s => s.ParentCategoryId == item.Id) });
					}
					foreach (var item2 in previouscategores)
					{
						if (item2.ParentCategoryId != null)
						{
							isfinal = false;
							break;
						}
					}
					childsCategoryResponse.items = items;
					childsCategoryResponse.Isfinal = true;
					if (categoriitem.ParentCategoryId.HasValue)
					{
						childsCategoryResponse.Parentid = categoriitem.ParentCategoryId.Value;
					}
					else
					{
						childsCategoryResponse.Parentid = null; 
					}
				}
				return childsCategoryResponse; 
			}

			return null; 


		}

		public List<HierarchyNamesCategory> GetCategoryHierarchyNames(string searchString) 
		{
			var result = 
				DbContext.CategoryHierarchyNames.FromSqlRaw("EXEC [dbo].[sp_GetCategoriesForAutoComplete] @SERACHKEY = {0}", searchString).ToList();

			return result;
		}

	}
}
