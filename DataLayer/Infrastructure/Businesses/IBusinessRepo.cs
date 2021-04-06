using DomainClass.Businesses;
using DomainClass.Businesses.Commands;
using DomainClass.Businesses.Queries;
using DomainClass.Queries;
using Microsoft.AspNetCore.Http;
using PagedList.Core;
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
		Task Create(CreateBusinessCommand model, bool hasCity, IFormFile mainimage, IFormFile[] otherimages);
		Task Update(Business model, bool hasCity, IFormFile mainimage, IFormFile[] gallery);
		Task<IEnumerable<AllBusinessFeatureViewModel>> GetBusinessFature(Guid? id);
		Task AssignFeature(Guid? id, int FeatureId, string value = null);
		Task RemoveFeature(Guid? id, int FeatureId);
		Task Remove(Business model);
		bool DeleteFeatureImage(Guid id, string filePath);
		PagedList<Business> GetBussiness(SearchBussinessQuery searchViewModel);
		Task<string> GetBusinessName(Guid Id);


	}
}
