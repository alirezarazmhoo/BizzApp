using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class CityRepo : RepositoryBase<City>, ICityRepo
	{
		public CityRepo(ApplicationDbContext DbContext) : base(DbContext)
		{
		}

		public Task AddOrUpdate(City city)
		{
			throw new NotImplementedException();
		}

		public Task<List<City>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<City> GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Remove(City city)
		{
			throw new NotImplementedException();
		}
	}
}
