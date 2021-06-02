using BizzAppInfrastructure.Model;
using DomainClass.Businesses;
using DomainClass.Review;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer.Infrastructure
{
	public interface IBusinessHomePageRepo
	{
		Task<IEnumerable<string>> GetSlider(Guid id);
		Task<Tuple<string, int, int, bool, int, string, string>> GetBusinessSummary(Guid id);
		Task<Tuple<string, List<BusinessFeature>>> GetBusinessFeatures(Guid id);
		Task<Tuple<string, double, double, List<LocationHours>>> GetBusinessLocationHours(Guid id);
		Task<List<Tuple<string, string, int, int, Guid, string, string>>> GetNearByBusinessSponsored(Guid id);
		Task<Tuple<string, string, string, string>> GetBusinessOtherInfo(Guid id); 
		Task<IEnumerable<Review>> GetBusinessReview(Guid id);
		Task<IEnumerable<Business>> GetRelatedBusiness(Guid id);
		Task MessageToBusiness(MessageToBusiness model);
		Task<int> GetTotalUserMedia(string id);
		Task<IEnumerable<CustomerBusinessMediaPictures>> GetBusinessGallery(Guid id);
		Task<IEnumerable<Business>> PepoleAlsoViewd(Guid id); 

	}
}
