using DomainClass;
using DomainClass.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IDistrictRepo
	{
		Task<List<District>> GetAll();
		Task<List<District>> GetAll(int cityId);
		Task<List<District>> GetAll(string searchString);
		Task<List<int>> GetDeafults(); 
		Task<District> GetById(int id);
		Task<List<DistrictWithParentsNameQuery>> GetAllWithParentNames(string searchString);
		Task<DistrictWithParentsNameQuery> GetAllWithParentNamesById(int id);
		Task AddOrUpdate(District district);
		void Remove(District district);
	}
}
