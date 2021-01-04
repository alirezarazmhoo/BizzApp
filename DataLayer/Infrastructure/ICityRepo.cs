using DomainClass;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface ICityRepo
	{
		Task<List<City>> GetAll();
		Task<City> GetById(int id);
		Task AddOrUpdate(City city);
		void Remove(City city);
	}
}
