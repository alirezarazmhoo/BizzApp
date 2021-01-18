using DomainClass.Businesses;
using DomainClass.Businesses.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IBusinessRepo
	{
		Task<List<BusinessListQuery>> GetAll();
		Task<List<BusinessListQuery>> GetAll(string searchString);
		Task<Business> GetById(Guid id);
		Task Add(Business model);
		void Update(Business model);
		void Remove(Business model);
	}
}
