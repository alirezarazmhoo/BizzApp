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
		Task<IEnumerable<Slider>> GetAll(); 
		Task AddOrUpdate(Slider model , IFormFile File);
		Task Remove(int Id);
		Task<Slider> GetById(int Id);
		Task<Slider> GetRandom(); 
	}
}
