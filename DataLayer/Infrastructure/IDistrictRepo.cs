using DomainClass;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IDistrictRepo
	{
		Task<List<District>> GetAll();
		Task<List<District>> GetAll(int cityId);
		Task<List<District>> GetAll(string searchString);
		Task<District> GetById(int id);
		Task AddOrUpdate(District district);
		void Remove(District district);
	}
}
