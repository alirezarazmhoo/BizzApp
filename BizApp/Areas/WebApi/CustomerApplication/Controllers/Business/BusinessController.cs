using AutoMapper;
using BizApp.Areas.WebApi.Models;
using BizApp.Utility;
using DataLayer.Data;
using DataLayer.Extensions;
using DataLayer.Infrastructure;
using DomainClass.Businesses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessFeature = BizApp.Areas.WebApi.Models.BusinessFeature;
using BusinessGallery = BizApp.Areas.WebApi.Models.BusinessGallery;
using BusinessTime = BizApp.Areas.WebApi.Models.BusinessTime;

namespace BizApp.Areas.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BusinessController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public BusinessController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}
		[Route("GetOnMap")]
		public async Task<IEnumerable<BusinessOnMap>> GetOnMap(int categoryId , double latitude, double longitude , bool sortByMostReveiw , bool sortByRating  , bool filterByOpenNow , bool sortByDistance , bool sortByCreateDate , string featuresId )
		{			
			List<BusinessOnMap> categoryDto = new List<BusinessOnMap>();
			List<BusinessOnMap> _categoryDtoHelper = new List<BusinessOnMap>();
			try
			{			
				foreach (var item in await _UnitOfWork.BusinessRepo.GetBusinessOnMap(categoryId, longitude, latitude))
				{
					categoryDto.Add(new BusinessOnMap() { id = item.Id, latitude = item.Latitude, longitude = item.Longitude ,  totalreview = item.Reviews.Where(s=>s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted).Count() , name = item.Name , rate = item.Rate == 0? 1 : item.Rate , images = item.Galleries.Select(s=>s.FileAddress).ToList() , address = item.Address , description = item.Description , districtname = item.District.Name , featureimage = string.IsNullOrEmpty(item.FeatureImage) == false ? "/Upload/DefaultPicutres/Bussiness/business-strategy-success-target-goals_1421-33.jpg" : item.FeatureImage , boldfeature = string.IsNullOrEmpty( item.BoldFeature) ? "بدون ویژگی خاص" : item.BoldFeature, category = _UnitOfWork.CategoryRepo.GetCategoryHierarchyNamesById(item.CategoryId) !=null ? _UnitOfWork.CategoryRepo.GetCategoryHierarchyNamesById(item.CategoryId).ListName : item.Category.Name , phonenumber = item.CallNumber.ToString() , website = item.WebsiteUrl , isOpen = item.IsOpenNow , categoryId = item.CategoryId , date = item.CreatedDate , featuresId = item.Features.Select(s=>s.FeatureId).ToList()}  );  
				}
				if (sortByMostReveiw)
				{
					categoryDto = categoryDto.OrderByDescending(s => s.totalreview).ToList();
				}
				if (sortByRating)
				{
					categoryDto = categoryDto.OrderByDescending(s => s.rate).ToList();
				}
				if (filterByOpenNow)
				{
					categoryDto = categoryDto.Where(s => s.isOpen).ToList();
				}
				if (sortByDistance)
				{
					foreach (var item in categoryDto)
					{
						item.distance = GetDistance.distance(latitude, longitude, item.latitude, item.longitude, 'K');
					}
					categoryDto = categoryDto.OrderBy(s=>s.distance).ToList();
				}
				if (sortByCreateDate)
				{
					categoryDto = categoryDto.OrderByDescending(s => s.date).ToList();
				}
				if (!string.IsNullOrEmpty(featuresId) )
				{
					List<FeaturesHelperDto> featuresHelperDtos = new List<FeaturesHelperDto>();
					string[] _featuresIdList = new string[] { };
					_featuresIdList = featuresId.Split(",");
					foreach (var item in _featuresIdList)
					{
						featuresHelperDtos.Add(new FeaturesHelperDto() { Id = Convert.ToInt32(item) });  
					}
					foreach (var item in categoryDto)
					{
						foreach (var item2 in item.featuresId)
						{
							if(featuresHelperDtos.Any(s=>s.Id == item2))
							{
								_categoryDtoHelper.Add(item);
							}
						}
					}
					categoryDto.Clear();
					categoryDto = _categoryDtoHelper; 
				}
				return categoryDto;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public class FeaturesHelperDto { 
		public int Id { get; set; }

		}
		[Route("GetById")]
		public async Task<IActionResult> GetById(Guid id)
		{
			try
			{
				string UserToken = HttpContext.Request?.Headers["Token"];

				BusinessRecentlyViewed businessRecentlyViewed = new BusinessRecentlyViewed();
			BusinessItem businessPopop = new BusinessItem();
				List<Review> reviews = new List<Review>();
			var Item = await _UnitOfWork.BusinessRepo.GetById(id);
			if(Item != null)
			{
					businessPopop.address = Item.Address;
					businessPopop.isOpenNow =        Item.IsOpenNow; 
					businessPopop.description = Item.Description;
					businessPopop.districname = Item.District.Name;
					businessPopop.id = Item.Id;
					businessPopop.address = Item.Address;
					businessPopop.boldfeature  = Item.BoldFeature;
					businessPopop.category = Item.Category.Name;
					businessPopop.phonenumber = Item.CallNumber.ToString();
					businessPopop.website = Item.WebsiteUrl;
					businessPopop.longitude = Item.Longitude;
					businessPopop.latitude = Item.Latitude;
					businessPopop.totalreview = Item.Reviews.Where(s => s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted).Count();
					businessPopop.featureimage = string.IsNullOrEmpty(Item.FeatureImage) == true ?  "/Upload/DefaultPicutres/Bussiness/business-strategy-success-target-goals_1421-33.jpg" : Item.FeatureImage;
					businessPopop.name = Item.Name;
					businessPopop.rate = Item.Rate;
					businessPopop.images = Item.Galleries.Select(s => s.FileAddress).ToList();
					foreach (var item in Item.Reviews)
					{
						string UserPicture = string.IsNullOrEmpty(item.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : item.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault();
						reviews.Add(new Review() { Date =  item.Date.ToPersianDateString() , FullName = item.BizAppUser.FullName , 
						 Id = item.Id , Image = UserPicture  , Rate = item.Rate , Text = item.Description ,  TotalReview= item.BizAppUser.Reviews.Count , TotalBusinessMediaPicture =await _UnitOfWork.BusinessHomePageRepo.GetTotalUserMedia(item.BizAppUserId),  TotalReviewPicture = await _UnitOfWork.ReviewRepo.GetUserTotalReviewMedia(item.BizAppUserId) , Cool = item.CoolCount , Funny = item.FunnyCount  , UseFull = item.UsefulCount ,
							ReviewMedias =  FillMediaType(item.ReviewMedias) , UserId  = item.BizAppUserId
						});;
					}
					businessPopop.reviews = reviews;
					businessPopop.totalreview = Item.Reviews.Count;

					if (!string.IsNullOrEmpty(UserToken))
					{

						string UserId = await _UnitOfWork.UserRepo.UserTokenMaper(UserToken);
						if(!await _UnitOfWork.BusinessRecentlyViewdRepo.CheckDuplicate(UserId , id))
						{
							businessRecentlyViewed.BizAppUserId = UserId;
							businessRecentlyViewed.BusinessId = id;
							await _UnitOfWork.BusinessRecentlyViewdRepo.Add(businessRecentlyViewed);
							await _UnitOfWork.SaveAsync();
						}
					}
					return Ok( businessPopop);
			}
				else
				{
					return null; 
				}
			}
		   catch (Exception e)
			{
				return BadRequest(e.Message);

			}
		}
		private  ICollection<ReviewMedias> FillMediaType(ICollection<DomainClass.Review.ReviewMedia> reviewMedias)
		{
			List<ReviewMedias> medias = new List<ReviewMedias>();
			foreach (var item in reviewMedias)
			{
				medias.Add(new ReviewMedias() {  MediaType = FormatCheck.GetFormat(item.Image) == DomainClass.Enums.MediaType.Picture ? "Picture" : "Video" , Url = item.Image , Caption = item.Description});
			}
			return medias; 
		}
		[Route("GetBusinessCustomersMedia")]
		public async Task<IActionResult> GetBusinessCustomersMedia(Guid id)
		{
			if(await _UnitOfWork.BusinessRepo.GetById(id) == null)
			{
				return NotFound();
			}
			try
			{
			List<BusinessGallery> businessGalleries = new List<BusinessGallery>();
			foreach (var item in await _UnitOfWork.BusinessRepo.GetCustomerBusinessMedia(id))
			{
				foreach (var item2 in item.CustomerBusinessMediaPictures)
				{
					string UserPicture = string.IsNullOrEmpty(item.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : item.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault();
					businessGalleries.Add(new BusinessGallery() { Description = item2.Description, Id = item2.Id, Url = item2.Image, MediaType = FormatCheck.GetFormat(item2.Image) == DomainClass.Enums.MediaType.Picture ? "Picture" : "Video", UserId = item2.CustomerBusinessMedia.BizAppUserId, Date = item2.CustomerBusinessMedia.Date.ToPersianDateString(), UserName = item2.CustomerBusinessMedia.BizAppUser.UserName, UserTotalFriends = await _UnitOfWork.UserRepo.GetUserFriendsCount(item2.CustomerBusinessMedia.BizAppUserId)  , UserTotalPictures = await _UnitOfWork.BusinessHomePageRepo.GetTotalUserMedia(item2.CustomerBusinessMedia.BizAppUserId) , UserTotalReview = await _UnitOfWork.ReviewRepo.GetUserTotalReview(item2.CustomerBusinessMedia.BizAppUserId) ,  UserPicture = UserPicture  }); 
				}
			}
			return Ok(businessGalleries);
			}
			catch(Exception ex)
			{
				return BadRequest(ex);
			}

		}
		[Route("GetBusinessGallery")]
		public async Task<IActionResult> GetBusinessGallery(Guid Id)
		{
			if(await _UnitOfWork.BusinessRepo.GetById(Id) == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(await _UnitOfWork.BusinessRepo.GetBusinessGallery(Id));
			}
		}
		[Route("TimeAndFeatures")]
		public async Task<IActionResult> GetBusinessTimeAndFeatures(Guid id)
		{
			BusinessTimeAndFeature businessTimeAndFeature = new BusinessTimeAndFeature();
			List<BusinessFeature> businessFeatures = new List<BusinessFeature>();
			List<BusinessTime>   businessTimes = new List<BusinessTime>();

			if (await _UnitOfWork.BusinessRepo.GetById(id) == null)
			{
				return NotFound();
			}
			try
			{
			#region Resource
			var Features = await _UnitOfWork.BusinessHomePageRepo.GetBusinessFeatures(id);
			var Times = await _UnitOfWork.BusinessHomePageRepo.GetBusinessLocationHours(id);
			var Description = await _UnitOfWork.BusinessRepo.GetById(id);
			#endregion
			foreach (var item in  Features.Item2)
			{
				businessFeatures.Add(new BusinessFeature() { Icon = item.Feature.Icon, Title = item.Feature.Name });
			}
			foreach (var item in Times.Item4)
			{
				item.DayName = GetDayName.GetName(item.Day);
				businessTimes.Add( new BusinessTime() { DayName = item.DayName , FromTime = item.FromTime , ToTime = item.ToTime  });
			}
				#region FinalResult
				businessTimeAndFeature.BusinessFeatures = businessFeatures;
				businessTimeAndFeature.BusinessTimes = businessTimes;
				businessTimeAndFeature.Description = Description.Description; 
				#endregion
				return Ok(businessTimeAndFeature);

			}
			catch (Exception)
			{
				throw;
			}
		}
		[HttpPost]
		[Route("LikeGallery")]
		public async Task<IActionResult> LikeCustomerBusinessGallery(Guid Id)
		{
			try
			{
				bool state;
				string Token = HttpContext.Request?.Headers["Token"];
				if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
				{
					return NotFound();
				}
				if (await _UnitOfWork.ReviewRepo.ChangeLikeCount(Id, await _UnitOfWork.UserRepo.UserTokenMaper(Token)) == DomainClass.Enums.VotesAction.Add)
				{
					state = true;
				}
				else
				{
					state = false;

				}
				await _UnitOfWork.SaveAsync();
				return Ok(state);
			}
			catch (Exception)
			{
				throw;
			}
		}
		[HttpGet]
		[Route("CheckLikeGallery")]
		public async Task<IActionResult> CheckLikeGalleryAlreadyExists(Guid Id)
		{
			try
			{
				bool state;
				string Token = HttpContext.Request?.Headers["Token"];
				if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
				{
					return NotFound();
				}
				if (await _UnitOfWork.ReviewRepo.CheckUserAlreadyExistsInBusinessLikeGallery(await _UnitOfWork.UserRepo.UserTokenMaper(Token), Id ))
				{
					state = true;
				}
				else
				{
					state = false;
				}
				return Ok(state);
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpGet]
		[Route("GetByCategoryAndLocation")]
		public async Task<IActionResult> GetBusinessByCategoryId(int CategoryId, int CityId)
		{
			if(await _UnitOfWork.CategoryRepo.GetById(CategoryId) == null)
			{
				return NotFound("دسته بندی مورد نظر یافت نشد"); 
			}
			if(await _UnitOfWork.CityRepo.GetById(CityId) == null)
			{
				return NotFound("شهر  مورد نظر یافت نشد");
			}
			return Ok(await _UnitOfWork.BusinessRepo.GetByCategoryIdBasedLocation(CategoryId  , CityId));
		}
		[HttpGet]
		[Route("SearchByTitle")]
		public async Task<IActionResult> SearchBusinessByTitleAndLocations(string txtSearch , int DistrictId , double Longitude , double Latitude)
		{
			List<BusinessShortModel> businessShortModels = new List<BusinessShortModel>();
		  string UserToken = HttpContext.Request?.Headers["Token"];
          if(!await _UnitOfWork.UserRepo.CheckUserToken(UserToken))
			{
				return NotFound("کاربر مورد نظر یافت نشد ");
			}
			try
			{			
				foreach (var item in await _UnitOfWork.BusinessRepo.SearchBusinessByTitle(txtSearch, DistrictId, Longitude, Latitude))
				{
					businessShortModels.Add(new BusinessShortModel() { districtname =$"{ item.District.City.Name} {item.District.Name}" , featureimage = string.IsNullOrEmpty(item.FeatureImage) == true ? ReturnDefaults.BusinessImage() : item.FeatureImage, id = item.Id, name = item.Name });
				}
				return Ok(businessShortModels);
			}catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}
	}
}
