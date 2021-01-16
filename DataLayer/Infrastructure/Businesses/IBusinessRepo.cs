using DomainClass;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IBusinessRepo
	{
		Task<List<City>> GetAll();
		Task<List<City>> GetAll(string searchString);
		Task<City> GetById(int id);
		Task AddOrUpdate(City city);
		void Remove(City city);
	}
}
