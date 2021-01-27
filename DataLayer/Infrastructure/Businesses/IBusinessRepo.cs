using DomainClass.Businesses;
using DomainClass.Businesses.Queries;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IBusinessRepo
	{
		Task<List<BusinessListQuery>> GetAll();
		Task<List<BusinessListQuery>> GetAll(string userId);
		Task<List<BusinessListQuery>> GetAll(string searchString, string userId = null);
		Task<Business> GetById(Guid id);
		Task Add(Business model, IFormFile mainimage, IFormFile[] otherimages); 
		void Update(Business model);
		Task<IEnumerable<AllBusinessFeatureViewModel>> GetBusinessFature(Guid? id);
		Task AssignFeature(Guid? id, int FeatureId);
		Task RemoveFeature(Guid? id, int FeatureId);
		Task Remove(Business model); 

		}
}
