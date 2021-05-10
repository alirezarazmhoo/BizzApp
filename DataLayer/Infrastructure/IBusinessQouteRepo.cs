using DomainClass;
using DomainClass.Businesses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IBusinessQouteRepo
	{
		Task<List<BusinessQoute>> GetAll();
		Task<List<BusinessQoute>> GetAll(string searchString);
		Task<BusinessQoute> GetById(int id);
		Task AddOrUpdate(BusinessQoute BusinessQoute);
		void Remove(BusinessQoute BusinessQoute);
		Task<List<BusinessQoute>> GetByCategoryId(int CategoryId);
	}
}
