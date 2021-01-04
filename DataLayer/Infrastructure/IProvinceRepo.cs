using DomainClass;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IProvinceRepo
	{
		Task<List<Province>> GetAll();
		Task<Province> GetById(int id);
		Task AddOrUpdate(Province province);
		void Remove(Province province);
	}
}
