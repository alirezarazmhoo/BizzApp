using BizzAppInfrastructure.Model;
using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Businesses;
using DomainClass.Review;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public	class BusinessHomePageRepo : RepositoryBase<Business>, IBusinessHomePageRepo
	{
		public BusinessHomePageRepo(ApplicationDbContext DbContext) : base(DbContext)
		{
		}
		public async Task<IEnumerable<string>> GetSlider(Guid id)
		{
			var BusinessItem = await DbContext.Businesses.FirstOrDefaultAsync(s=>s.Id.Equals(id));
			if(BusinessItem != null)
			{
				return await DbContext.BusinessGalleries.Where(s => s.BusinessId.Equals(id)).Select(s=>s.FileAddress).ToListAsync();
			}
			else
			{
				return null;  
			}
		}
		public async Task<Tuple<string , int , int , bool , int , string , string >> GetBusinessSummary(Guid id)
		{
			var BusinessItem = await DbContext.Businesses.FirstOrDefaultAsync(s => s.Id.Equals(id));
			if(BusinessItem != null)
			{
			return new Tuple<string, int, int, bool, int , string ,string >(BusinessItem.Name , BusinessItem.Rate, await GetTotalReview(id) ,BusinessItem.IsClaimed, await GetTotalMediaReview(id) , BusinessItem.Description , BusinessItem.WebsiteUrl );
			}
			else
			{
				return null; 
			}	
		}
		public async Task<Tuple<string ,List<BusinessFeature>>> GetBusinessFeatures(Guid id)
		{
			var BusinessItem = await DbContext.Businesses.FirstOrDefaultAsync(s => s.Id.Equals(id));
			if(BusinessItem != null)
			{
				return new Tuple<string, List<BusinessFeature>>(BusinessItem.BoldFeature , await GetBusinessFeaturesTitle(id));
			}
			else
			{
				return null; 
			}
		}
		public async Task<Tuple<string , double , double , List<LocationHours>>> GetBusinessLocationHours(Guid id)
		{
			var BusinessItem = await DbContext.Businesses.FirstOrDefaultAsync(s => s.Id.Equals(id));
			if(BusinessItem != null)
			{
			 return new Tuple<string, double, double,List<LocationHours>>(BusinessItem.Address , BusinessItem.Longitude , BusinessItem.Latitude, await GetLocationHours(id) );
			}
			else
			{
			return null; 
			}
		}
		public async Task<List<Tuple<string,string,int,int,Guid,string,string>>> GetNearByBusinessSponsored(Guid id)
		{
			var BusinessItem = await DbContext.Businesses.Include(s => s.District).FirstOrDefaultAsync(s => s.Id.Equals(id));
			List<Tuple<string, string, int, int, Guid, string, string>> MainList = new List<Tuple<string, string, int, int, Guid, string, string>>();
			if (BusinessItem != null)
			{
				foreach (var item in await DbContext.Businesses.Where(s => s.DistrictId == BusinessItem.DistrictId && s.IsSponsor).Take(2).ToListAsync())
				{
					MainList.Add(new Tuple<string, string, int, int, Guid, string,string>(item.Name,item.FeatureImage,item.Rate,await GetTotalReview(item.Id),item.Id, item.Description , item.District.Name));
				}
				return MainList; 
			}
			else
			{
				return null;
			}
		}
		public async Task<IEnumerable<Review>> GetBusinessReview(Guid id)
		{
			var Items = await DbContext.Reviews
				.Include(s => s.ReviewMedias)
				.Include(s => s.BizAppUser)
				.ThenInclude(s => s.ApplicationUserMedias)
				.Include(s => s.Business)
			
				.Where(s => s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted && s.BusinessId.Equals(id))
				.ToListAsync();
			return Items;
		}
		public async Task<Tuple<string, string,string,string>> GetBusinessOtherInfo(Guid id)
		{
			var BusinessItem = await DbContext.Businesses.FirstOrDefaultAsync(s => s.Id.Equals(id));
			if (BusinessItem != null)
			{
				return new Tuple<string,string,string,string>(BusinessItem.Description ,BusinessItem.WebsiteUrl,BusinessItem.CallNumber.ToString() , BusinessItem.Address);
			}
			else
			{
				return null;
			}
		}
		public async Task<IEnumerable<Business>> GetRelatedBusiness(Guid id)
		{
			var businessItem = await DbContext.Businesses.FirstOrDefaultAsync(s => s.Id.Equals(id));
			if (businessItem != null)
			{
				return await DbContext.Businesses.Where(s => s.DistrictId.Equals(businessItem.DistrictId) && s.CategoryId.Equals(businessItem.CategoryId) && !s.Id.Equals(businessItem.Id)).ToListAsync();
			}
			else
			{
				return new List<Business>();
			}
		}
		public async Task MessageToBusiness(MessageToBusiness model)
		{
			if(await DbContext.Businesses.AnyAsync(s => s.Id.Equals(model.BusinessId)))
			{
				model.Date = DateTime.Now; 
				await DbContext.MessageToBusinesses.AddAsync(model);
			}
		}
		public async Task<IEnumerable<CustomerBusinessMediaPictures>> GetBusinessGallery(Guid id)
		{
			var BusinessItem = await DbContext.Businesses.FirstOrDefaultAsync(s=>s.Id.Equals(id));
			if(BusinessItem != null)
			{
				return await DbContext.CustomerBusinessMediaPictures.Include(s=>s.CustomerBusinessMedia.BizAppUser.ApplicationUserMedias).Where(s => s.CustomerBusinessMedia.BusinessId.Equals(id) && s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted).ToListAsync();
			}
			else
			{
				return new List<CustomerBusinessMediaPictures>();
			}
		}
		//public class LocationHours
		//{
		//	public WeekDaysEnum Day { get; set; }
		//	public string DayName { get; set; }
		//	public TimeSpan? FromTime { get; set; }
		//	public TimeSpan? ToTime { get; set;  }
		//}
		public async Task<IEnumerable<Business>> PepoleAlsoViewd(Guid id)
		{
			var businessItem = await DbContext.Businesses.FirstOrDefaultAsync(s => s.Id.Equals(id));
			var DistricId = 0;
			if (businessItem != null)
			{
				var Items = await DbContext.Reviews.Include(s => s.Business).Where(s => s.BusinessId != businessItem.Id && s.Business.CategoryId == businessItem.CategoryId).ToListAsync();
				if (DistricId != 0)
				{
					return Items.Where(s => s.Business.DistrictId == businessItem.DistrictId).Select(s => s.Business).ToList();
				}
				else
				{
					return Items.Select(s => s.Business).ToList();

				}
			}
			else
			{
				return new List<Business>();
			}
		}
		public async Task<int> GetTotalUserMedia(string id)
		{
			var userItem = await DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(id));
			if (userItem != null)
			{
				return await DbContext.CustomerBusinessMediaPictures.Where(s => s.CustomerBusinessMedia.BizAppUserId.Equals(id)).CountAsync();
			}
			else
			{
				return 0;
			}
		}
		private async Task<List<LocationHours>> GetLocationHours(Guid id)
		{
			List<LocationHours> locationHours = new List<LocationHours>();
			var Times = await DbContext.BusinessTimes.Where(s => s.BusinessId.Equals(id)).ToListAsync();
			foreach (var item in Times)
			{
				locationHours.Add(new LocationHours() { Day = item.Day, FromTime = item.FromTime, ToTime = item.ToTime });
			}
			return locationHours;
		}
		private  async Task<List<BusinessFeature>> GetBusinessFeaturesTitle(Guid id)
		{
			return  await DbContext.BusinessFeatures.Include(s=>s.Feature).Where(s => s.BusinessId.Equals(id)).ToListAsync();
		}
		private async Task<int> GetTotalMediaReview(Guid id)
		{
			return await DbContext.CustomerBusinessMediaPictures.Where(s => s.CustomerBusinessMedia.BusinessId.Equals(id) && s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted).CountAsync();
		}
		private async Task<int> GetTotalReview(Guid id)
		{
			int Sum1 = await DbContext.Reviews.Where(s => s.BusinessId.Equals(id)).CountAsync();
			int Sum2 = await DbContext.CustomerBusinessMedias.Where(s => s.BusinessId.Equals(id)).CountAsync();
			return Sum1 + Sum2; 
		}
	}
}
