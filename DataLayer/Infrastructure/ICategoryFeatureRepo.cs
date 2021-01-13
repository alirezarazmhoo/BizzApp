using DomainClass;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface ICategoryFeatureRepo
	{
		Task<List<CategoryFeature>> GetAll(int categoryId);
		Task<List<CategoryFeature>> GetAll(int categoryId, string searchString);
		Task<CategoryFeature> GetById(int id);
		Task AddOrUpdate(CategoryFeature model);
		void Remove(CategoryFeature model);
	}
}
