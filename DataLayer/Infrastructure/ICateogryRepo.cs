using DomainClass;
using DomainClass.Businesses.Queries;
using DomainClass.Commands;
using DomainClass.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public  interface ICateogryRepo
	{
		Task<IEnumerable<Category>> GetAll();
		Task<List<Category>> GetAll(string searchString);
		Task<Category> GetById(int id);
		Task<GetCategoryByIdQuery> GetWithTermsById(int id);
		Task Add(CreateCategoryCommand model);
		Task Update(UpdateCategoryCommand command);
		Task AddOrUpdate(Category city);
		void Remove(Category city);
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
	}
}
