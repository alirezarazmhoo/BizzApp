using DomainClass.Businesses;
using DomainClass.Businesses.Commands;
using DomainClass.Businesses.Queries;
using DomainClass.Queries;
using DomainClass.Review;
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
		Task<IEnumerable<Business>> GetBusinessOnMap(int? Id, double Longitude, double Latitude);
		Task<bool> CheckBisinessFavorit(Guid Id, string UserId);
		Task<IEnumerable<CustomerBusinessMedia>> GetCustomerBusinessMedia(Guid Id);
		Task<IEnumerable<BusinessGallery>> GetBusinessGallery(Guid Id);
		Task<IEnumerable<Business>> GetByCategoryIdBasedLocation(int CategoryId, int CityId);
		Task<IEnumerable<Business>> SearchBusinessByTitle(string txtSearch, int DistrictId, double Longitude, double Latitude);
		Task UpdateFrequenstlyFeature(Guid Id, string FeaturesLists);
		Task UpdateBaseInformations(Business business);
		Task UpdateBusinessTime(List<BusinessTime> times, Guid businessId);
		Task<List<Guid>> GetUserBusinessesIds(string UserId); Task UpdateBusinessFeaturesInBusinessAccount(SelectedFeaturesDto[] model, Guid BusinessId);
		Task DeleteGalleryImage(int Id); 

	}
}
