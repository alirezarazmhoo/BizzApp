using DomainClass;
using DomainClass.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface ICityRepo
	{
		Task<List<City>> GetAll();
		Task<IEnumerable<City>> GetAll(int provinceId);
		Task<List<City>> GetAll(string searchString);
		Task<List<CityWithProvinceNamesQuery>> GetAllWithProvinces(string searchString);
		Task<CityWithProvinceNamesQuery> GetWithProvince(int id);
		Task<City> GetById(int id);
		Task AddOrUpdate(City city);
		void Remove(City city);

	}
}
