using DataLayer.Data;
using DataLayer.Extensions;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Businesses;
using DomainClass.Businesses.Queries;
using DomainClass.Commands;
using DomainClass.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace DataLayer.Services
{
	public class CategoryRepo : RepositoryBase<Category>, ICateogryRepo
	{
		private readonly string IconType;
		private readonly string IconWebType;
		private readonly string FeatureImageType;
		private readonly string PngIconType;
		public CategoryRepo(ApplicationDbContext DbContext) : base(DbContext)
		{
			IconType = "icon";
			IconWebType = "icon-web";
			FeatureImageType = "feature-image";
			PngIconType = "png-icon";
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
		private async Task CreateIconAsync(int categoryId, string icon, string iconWebClassName)
		{
			if (!string.IsNullOrEmpty(icon))
			{
				var categoryTerm = new CategoryTerm
				{
					Key = IconType,
					Value = icon,
					CategoryId = categoryId
				};

				var categoryTermIconWeb = new CategoryTerm
				{
					Key = IconWebType,
					Value = iconWebClassName,
					CategoryId = categoryId
				};

				await DbContext.CategoryTerms.AddAsync(categoryTerm);
				await DbContext.CategoryTerms.AddAsync(categoryTermIconWeb);
				await DbContext.SaveChangesAsync();
			}
		}
		private string UploadFile(IFormFile file)
		{
			string fileName, filePath;

			// if mainImage is not null then upload it
			if (file != null)
			{
				// Upload main images
				fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(file.FileName).ToLower();
				filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Categories\Files", fileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					file.CopyTo(fileStream);
					return "/Upload/Categories/Files/" + fileName;
				}
			}

			return null;
		}
		private async Task CreatePngIconAsync(int categoryId, IFormFile file)
		{
			// upload image
			var iconName = UploadFile(file);

			// if iconName is null means no selected icon
			if (iconName == null) return;

			// create png icon in db
			var iconTerm = new CategoryTerm
			{
				Key = PngIconType,
				Value = iconName,
				CategoryId = categoryId
			};

			// save changes
			await DbContext.CategoryTerms.AddAsync(iconTerm);
			await DbContext.SaveChangesAsync();
		}
		private async Task CreateFeatureImageAsync(int categoryId, IFormFile file)
		{
			// upload image
			var imageName = UploadFile(file);

			// if iconName is null means no selected icon
			if (imageName == null) return;

			// create png icon in db
			var iconTerm = new CategoryTerm
			{
				Key = FeatureImageType,
				Value = imageName,
				CategoryId = categoryId
			};

			// save changes
			await DbContext.CategoryTerms.AddAsync(iconTerm);
			await DbContext.SaveChangesAsync();
		}
		public async Task AddAsync(CreateCategoryCommand model, IFormFile pngIcon, IFormFile featureImage)
		{
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				// create new category first of all
				var category = new Category
				{
					Name = model.Name,
					Order = model.Order,
					ParentCategoryId = model.ParentCategoryId
				};

				// add new category to database and get new category id
				await DbContext.Categories.AddAsync(category);
				await DbContext.SaveChangesAsync();

				// create fontawesome icon 
				await CreateIconAsync(category.Id, model.Icon, model.IconWeb);

				// upload and save png icon for it
				await CreatePngIconAsync(category.Id, pngIcon);

				// save and uplaod feature image
				await CreateFeatureImageAsync(category.Id, featureImage);

				// save all changes together
				scope.Complete();
			}
		}
		private async Task DeleteIcons(int id)
		{
			// get category items to edit
			var categoryTerms = await DbContext.CategoryTerms.Where(w => w.CategoryId == id).ToListAsync();
			// check if category not have any terms then return 
			if (categoryTerms?.Count == 0) return;

			// delete category icon
			var categoryIcon = categoryTerms.FirstOrDefault(f => f.Key == IconType);
			if (categoryIcon != null) DbContext.CategoryTerms.Remove(categoryIcon);

			// delete category icon web value
			var categoryIconWeb = categoryTerms.FirstOrDefault(f => f.Key == IconWebType);
			if (categoryIconWeb != null) DbContext.CategoryTerms.Remove(categoryIconWeb);

			await DbContext.SaveChangesAsync();
		}
		private async Task DeleteFile(int id, string categoryFileType)
		{
			// get png icon to delete
			var categoryTerm = await DbContext.CategoryTerms.FirstOrDefaultAsync(w => w.CategoryId == id && w.Key == categoryFileType);

			if (categoryTerm != null)
			{
				// delete file from server
				File.Delete($"wwwroot/{categoryTerm.Value}");

				// delete from database
				DbContext.CategoryTerms.Remove(categoryTerm);
				await DbContext.SaveChangesAsync();
			}
		}
		public async Task UpdateAsync(UpdateCategoryCommand command, IFormFile pngIcon, IFormFile featureImage)
		{
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				// Update category
				var category = await DbContext.Categories.FirstOrDefaultAsync(f => f.Id == command.Id);

				if (category == null) return;

				category.Name = command.Name;
				category.Order = command.Order;

				await DbContext.SaveChangesAsync();

				// delete category icon
				await DeleteIcons(command.Id);

				// add new category icon if is exists
				await CreateIconAsync(category.Id, command.Icon, command.IconWeb);

				// if png icon changed
				if (command.ChangedPngIcon)
				{
					// delete category png icon
					await DeleteFile(category.Id, PngIconType);

					// add new category png icon if exists
					await CreatePngIconAsync(category.Id, pngIcon);
				}

				// if feature image changed
				if (command.ChangedFeatureImage)
				{
					// delete category feature image
					await DeleteFile(category.Id, FeatureImageType);

					// add new category feature image if exists
					await CreateFeatureImageAsync(category.Id, featureImage);
				}

				// save all chagnes
				await DbContext.SaveChangesAsync();

				scope.Complete();
			}
		}
		public async Task<IEnumerable<Category>> GetAll()
		{
			return await FindByCondition(f => f.ParentCategoryId == null).Include(s=>s.Terms).OrderByDescending(o => o.Id).ToListAsync();
		}
		public async Task<List<Category>> GetAll(string searchString, int DistrictId )
		{
			List<Category> categories = new List<Category>();  
			if(DistrictId !=0)
			{
				var BusinessItems = await DbContext.Businesses.Include(s=>s.Category).Select(s=>new { s.Category , s.DistrictId}).Where(s => s.DistrictId == DistrictId).ToListAsync();
				foreach (var item in BusinessItems)
				{
					categories.Add(item.Category);
				}
				if (!string.IsNullOrEmpty(searchString))
				{
					categories = categories.Where(s => s.Name.Contains(searchString)).ToList();
					return categories;  
				}
				else
				{
					return categories;
				}
			}
			else
			{
			return await FindByCondition(f => f.Name.Contains(searchString)).OrderByDescending(o => o.Id).ToListAsync();
			}
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
					 Icon = c.Terms.FirstOrDefault(f => f.Key == IconType).Value,
					 IconWeb = c.Terms.FirstOrDefault(f => f.Key == IconWebType).Value,
					 PngIconPath = c.Terms.FirstOrDefault(f => f.Key == PngIconType).Value,
					 FeatureImagePath = c.Terms.FirstOrDefault(f => f.Key == FeatureImageType).Value
				 })
				 .FirstOrDefaultAsync();


			//return await FindByCondition(f => f.Id == id).FirstOrDefaultAsync();
			return result;
		}
		public async Task<Category> GetById(int id)
		{
			return await FindByCondition(f => f.Id == id).FirstOrDefaultAsync();
		}
		public async Task Remove(Category model)
		{
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				// delete icon
				await DeleteIcons(model.Id);

				// delete png icon
				await DeleteFile(model.Id, PngIconType);

				// delete category
				Delete(model);

				// save all changes
				await DbContext.SaveChangesAsync();

				scope.Complete();
			}
		}
		public async Task<bool> HasChild(int Id)
		{
			return await DbContext.Categories.AnyAsync(s => s.ParentCategoryId == Id);
		}
		public async Task<List<Category>> GetChilds(int Id)
		{
			var allCategories = DbContext.Categories.ToList();
			var subCategories = allCategories.Where(f => f.ParentCategoryId == Id).ToList();
			List<Category> cats = subCategories;
			foreach (var item in subCategories.ToList())
			{
				foreach (var item2 in allCategories)
				{
					if (item2.ParentCategoryId == item.Id)
					{
						cats.Remove(item);
						cats.Add(item2);
					}
				}
			}
			return cats;
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
				DbContext.Categories
					.FromSqlRaw("EXEC [dbo].[sp_GetCategoriesForAutoComplete] @SERACHKEY = {0}", searchString)
					.Select(s => new HierarchyNamesCategory
					{
						Id = s.Id,
						ListName = s.Name
					})
					.ToList();

			return result;
		}
		public HierarchyNamesCategory GetCategoryHierarchyNamesById(int id)
		{
			var result =
				DbContext.Categories
					.FromSqlRaw("EXEC [dbo].[sp_GetCategoryWithParentsById] @id = {0}", id)
					 .Select(s => new HierarchyNamesCategory
					 {
						 Id = s.Id,
						 ListName = s.Name
					 })
					.ToList();

			return result.FirstOrDefault();
		}
		public async Task<CategoryTerm> GetCategoryTerm(int id)
		{
			return await DbContext.CategoryTerms.FirstOrDefaultAsync(s => s.CategoryId.Equals(id));
		}
		public async Task<List<Category>> GetChosens()
		{
			var Items = await DbContext.Categories.Include(s => s.Terms).Where(s => s.Order != 0 && !s.ParentCategoryId.HasValue).ToListAsync();
			Items = Items.TakeWhile(s => s.Order <= 10).ToList();
			return Items;
		}
		public async Task<List<Category>> GetUnChosens()
		{
			var Items = await DbContext.Categories.Include(s => s.Terms).Where(s => s.Order == 0 && !s.ParentCategoryId.HasValue).ToListAsync();
			return Items;
		}
		public async Task<List<MenuCategoryViewModel>> GetAllInSearchPage()
		{
			return await
				DbContext.Categories.OrderByDescending(x => x.Order)
					.Select(x => new MenuCategoryViewModel
					{
						Id = x.Id,
						ParentCategoryId = x.ParentCategoryId,
						Name = x.Name,
						svgIcon = x.Terms.FirstOrDefault(f => f.Key == "icon") == null ? string.Empty : x.Terms.FirstOrDefault(f => f.Key == "icon").Value
					}).ToListAsync();
		}
		public async Task<IEnumerable<Category>> GetAllParents(int id)
		{
			var result = await
				DbContext.Categories
					.FromSqlRaw("EXEC [dbo].[sp_GetAllCategoryWithParentsById] @id = {0}", id)
					.ToListAsync();

			return result;

		}
		public async Task<List<Category>> GetPopular(double Longitude, double Latitude)
		{
			double longitude = 0;
			double latitiude = 0;
			var BusinessItems = await DbContext.Businesses.Include(s => s.Category).ThenInclude(s => s.Terms).ToListAsync();
			List<Business> businesses = new List<Business>();
			List<Category> categories = new List<Category>();
			foreach (var item in BusinessItems)
			{
				longitude = item.Longitude;
				latitiude = item.Latitude;


				if ((Math.Pow(Longitude - longitude, 2) + Math.Pow(Latitude - latitiude, 2)) < 10)
				{
					businesses.Add(item);
				}
			}
			if (businesses.Count > 0)
			{
				foreach (var item in businesses)
				{
					categories.Add(item.Category);
				}
				return categories;
			}
			else
			{
				return new List<Category>();
			}
		}
		public class InputCategroy
		{
			public double longitude { get; set; }
			public double latitude { get; set; }
			public string id { get; set; }
		}
		public async Task<IEnumerable<Category>> GetSubCategories(int Id)
		{
			var Item = await DbContext.Categories.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			if(Item != null)
			{
				return await DbContext.Categories.Include(s=>s.Terms).Where(s => s.ParentCategoryId.Equals(Item.Id)).ToListAsync(); 
			}
			else
			{
				return new List<Category>();
			}

		}



	}
}
