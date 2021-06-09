using DomainClass;
using DomainClass.Businesses.Queries;
using DomainClass.Commands;
using DomainClass.Queries;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public  interface ICateogryRepo
	{
		Task<IEnumerable<Category>> GetAll();
		Task<List<Category>> GetAll(string searchString , int DistrictId);
		Task<Category> GetById(int id);
		Task<GetCategoryByIdQuery> GetWithTermsById(int id);
		//Task<int> Add(CreateCategoryCommand model);
		Task AddAsync(CreateCategoryCommand model, IFormFile pngIcon, IFormFile featureImage);
		Task UpdateAsync(UpdateCategoryCommand command, IFormFile pngIcon, IFormFile featureImage);
		Task AddOrUpdate(Category city);
		Task Remove(Category city);
	    Task<bool> HasChild(int Id);
		Task<List<Category>> GetChilds(int Id);
		Task<int> GetChildCount(int Id);
		Task<ChildsCategoryResponse> AdminGetChildsCateogry(int Id);
		Task<ChildsCategoryResponse> GetBackCategories(int Id);
		List<HierarchyNamesCategory> GetCategoriesHierarchyNames(string searchString);
		HierarchyNamesCategory GetCategoryHierarchyNamesById(int id);
		Task<CategoryTerm> GetCategoryTerm(int id);
		Task<List<Category>> GetChosens();
		Task<List<Category>> GetUnChosens();
		Task<List<MenuCategoryViewModel>> GetAllInSearchPage();

		Task<IEnumerable<Category>> GetAllParents(int id);
		Task<List<Category>> GetPopular(double Longitude, double Latitude);
		Task<IEnumerable<Category>> GetSubCategories(int Id); 
	}
}
