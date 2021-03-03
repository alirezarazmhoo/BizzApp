using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Businesses.Queries;
using DomainClass.Commands;
using DomainClass.Queries;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace DataLayer.Services
{
	public class CategoryRepo : RepositoryBase<Category>, ICateogryRepo
	{
		private readonly string CategoryIconType;
		private readonly string CategoryIconWebType;
		private readonly string CategoryFeatureImageType;
		private readonly string CategoryPngIconType;
		public CategoryRepo(ApplicationDbContext DbContext) : base(DbContext)
		{
			CategoryIconType = "icon";
			CategoryIconWebType = "icon-web";
			CategoryFeatureImageType = "feature_image";
			CategoryPngIconType = "png_icon";
		}
		public async Task AddOrUpdate(Category model)
		{
			if (model.Id == 0)
			{
				await CreateAsync(model);
			}
			else
			{
				Update(model);
			}
		}
		private async Task CreateIcon(int categoryId, string icon, string iconWebClassName)
		{
			if (!string.IsNullOrEmpty(icon))
			{
				var categoryTerm = new CategoryTerm
				{
					Key = CategoryIconType,
					Value = icon,
					CategoryId = categoryId
				};

				var categoryTermIconWeb = new CategoryTerm
				{
					Key = CategoryIconWebType,
					Value = iconWebClassName,
					CategoryId = categoryId
				};

				await DbContext.CategoryTerms.AddAsync(categoryTerm);
				await DbContext.CategoryTerms.AddAsync(categoryTermIconWeb);
				await DbContext.SaveChangesAsync();
			}
		}
		public async Task Add(CreateCategoryCommand model)
		{
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				// create new category
				var category = new Category
				{
					Name = model.Name,
					Order = model.Order,
					ParentCategoryId = model.ParentCategoryId
				};

				// add new category to database and get new category id
				await DbContext.Categories.AddAsync(category);
				await DbContext.SaveChangesAsync();

				// create Icon 
				await CreateIcon(category.Id, model.Icon, model.IconWeb);

				scope.Complete();
			}
		}
		private async Task DeleteIcons(int id)
		{
			// get category items to edit
			var categoryTerms = await DbContext.CategoryTerms.Where(w => w.CategoryId == id).ToListAsync();

			// delete category icon
			var categoryIcon = categoryTerms.FirstOrDefault(f => f.Key == CategoryIconType);
			if (categoryIcon != null) DbContext.CategoryTerms.Remove(categoryIcon);

			// delete category icon web value
			var categoryIconWeb = categoryTerms.FirstOrDefault(f => f.Key == CategoryIconWebType);
			if (categoryIconWeb != null) DbContext.CategoryTerms.Remove(categoryIconWeb);

			await DbContext.SaveChangesAsync();
		}
		public async Task Update(UpdateCategoryCommand command)
		{
			// update category
			var category = await DbContext.Categories.FirstOrDefaultAsync(f => f.Id == command.Id);
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				category.Name = command.Name;
				category.Order = command.Order;

				// delete category icon
				await DeleteIcons(command.Id);

				// add new category icon if is exists
				await CreateIcon(category.Id, command.Icon, command.IconWeb);

				scope.Complete();

				await DbContext.SaveChangesAsync();
			}
		}
		public async Task<IEnumerable<Category>> GetAll()
		{
			return await FindByCondition(f => f.ParentCategoryId == null).OrderByDescending(o => o.Id).ToListAsync();
		}
		public async Task<List<Category>> GetAll(string searchString)
		{
			return await FindByCondition(f => f.Name.Contains(searchString)).OrderByDescending(o => o.Id).ToListAsync();
		}
		public async Task<GetCategoryByIdQuery> GetWithTermsById(int id)
		{
			var result = await
				(from c in DbContext.Categories
				 where c.Id == id
				 select new GetCategoryByIdQuery
				 {
					 Id = c.Id,
					 Name = c.Name,
					 Order = c.Order,
					 ParentCategoryId = c.ParentCategoryId,
					 Icon = c.Trems.FirstOrDefault(f => f.Key == CategoryIconType).Value,
					 IconWeb = c.Trems.FirstOrDefault(f => f.Key == CategoryIconWebType).Value,
					 FeatureImagePath = c.Trems.FirstOrDefault(f => f.Key == CategoryFeatureImageType).Value
				 }).FirstOrDefaultAsync();


			//return await FindByCondition(f => f.Id == id).FirstOrDefaultAsync();
			return result;
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
		public async Task<int> GetChildCount(int Id)
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
					items.Add(new ComboBoxViewModel() { id = item.Id, name = item.Name, havenext = DbContext.Categories.Any(s => s.ParentCategoryId == item.Id) });
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
					items.Add(new ComboBoxViewModel() { id = catitem.Id, name = catitem.Name });
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
						items.Add(new ComboBoxViewModel() { id = item.Id, name = item.Name, havenext = DbContext.Categories.Any(s => s.ParentCategoryId == item.Id) });
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
		public List<HierarchyNamesCategory> GetCategoriesHierarchyNames(string searchString)
		{
			var result =
				DbContext.CategoryHierarchyNames.FromSqlRaw("EXEC [dbo].[sp_GetCategoriesForAutoComplete] @SERACHKEY = {0}", searchString).ToList();

			return result;
		}
		public HierarchyNamesCategory GetCategoryHierarchyNamesById(int id)
		{
			var result =
				DbContext.CategoryHierarchyNames.FromSqlRaw("EXEC [dbo].[sp_GetCategoryWithParentsById] @id = {0}", id).ToList();

			return result.FirstOrDefault();
		}
		public async Task<CategoryTerm> GetCategoryTerm(int id)
		{
			return await DbContext.CategoryTerms.FirstOrDefaultAsync(s => s.CategoryId.Equals(id));
		}
		public async Task<List<Category>> GetChosens()
		{
			var Items = await DbContext.Categories.Where(s => s.Order != 0 && !s.ParentCategoryId.HasValue).ToListAsync();
			Items = Items.TakeWhile(s => s.Order <= 10).ToList();
			return Items;
		}
		public async Task<List<Category>> GetUnChosens()
		{
			var Items = await DbContext.Categories.Where(s => s.Order == 0 && !s.ParentCategoryId.HasValue).ToListAsync();
			return Items;
		}
	}
}
