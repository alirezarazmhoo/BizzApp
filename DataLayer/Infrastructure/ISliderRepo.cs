using DomainClass;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface ISliderRepo
	{
		Task<List<Slider>> GetAll();
		Task<List<Slider>> GetAll(string searchString);

		Task AddOrUpdate(Slider model , IFormFile File);
		Task Remove(Slider model);
		Task<Slider> GetById(int Id);
		Task<Slider> GetRandom(); 
	}
}
