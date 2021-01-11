using DomainClass;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public  interface ICateogryRepo
	{
		Task<List<Category>> GetAll();
		Task<List<Category>> GetAll(string searchString);
		Task<Category> GetById(int id);
		Task AddOrUpdate(Category city);
		void Remove(Category city);
	    Task<bool> HasChild(int Id);
		Task<List<Category>> GetChilds(int Id);
	}
}
