using AutoMapper;
using BizApp.Areas.WebApi.Models;
using BizApp.Utility;
using DataLayer.Data;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BusinessController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly IUnitOfWorkRepo _UnitOfWork;
		private readonly IMapper _mapper;

		public BusinessController(ApplicationDbContext context , IUnitOfWorkRepo unitOfWork, IMapper mapper)
		{
			_context = context;
			_UnitOfWork = unitOfWork;
			_mapper = mapper;

		}
		[Route("GetOnMap")]
		public async Task<IEnumerable<BusinessOnMap>> GetOnMap(int categoryId , double latitude, double longitude)
		{
			List<BusinessOnMap> categoryDto = new List<BusinessOnMap>();
			try
			{			
				foreach (var item in await _UnitOfWork.BusinessRepo.GetBusinessOnMap(categoryId, longitude, latitude))
				{
					categoryDto.Add(new BusinessOnMap() { id = item.Id, latitude = item.Latitude, longitude = item.Longitude ,  totalreview = item.Reviews.Where(s=>s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted).Count() , name = item.Name , rate = item.Rate == 0? 1 : item.Rate , images = item.Galleries.Select(s=>s.FileAddress).ToList() , address = item.Address , description = item.Description , districtname = item.District.Name , featureimage = string.IsNullOrEmpty(item.FeatureImage) == false ? "/Upload/DefaultPicutres/Bussiness/business-strategy-success-target-goals_1421-33.jpg" : item.FeatureImage , boldfeature = string.IsNullOrEmpty( item.BoldFeature) ? "بدون ویژگی خاص" : item.BoldFeature, category = _UnitOfWork.CategoryRepo.GetCategoryHierarchyNamesById(item.CategoryId) !=null ? _UnitOfWork.CategoryRepo.GetCategoryHierarchyNamesById(item.CategoryId).ListName : item.Category.Name , phonenumber = item.CallNumber.ToString() , website = item.WebsiteUrl}  );  
				}
				return categoryDto;
			}
			catch (Exception)
			{
				throw;
			}
		}
		[Route("GetById")]
		public async Task<BusinessItem> GetById(Guid id)
		{
			try
			{
			BusinessItem businessPopop = new BusinessItem();
				List<Review> reviews = new List<Review>();
			var Item = await _UnitOfWork.BusinessRepo.GetById(id);
			if(Item != null)
			{
					businessPopop.address = Item.Address;
					businessPopop.description = Item.Description;
					businessPopop.districname = Item.District.Name;
					businessPopop.id = Item.Id;
					businessPopop.image = string.IsNullOrEmpty(Item.FeatureImage) == true ?  "/Upload/DefaultPicutres/Bussiness/business-strategy-success-target-goals_1421-33.jpg" : Item.FeatureImage;
					businessPopop.name = Item.Name;
					businessPopop.rate = Item.Rate;
					foreach (var item in Item.Reviews)
					{
						string UserPicture = string.IsNullOrEmpty(item.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : item.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault();
						reviews.Add(new Review() { Date =  item.Date.ToPersianDateString() , FullName = item.BizAppUser.FullName , 
						 Id = item.Id , Image = UserPicture  , Rate = item.Rate , Text = item.Description ,  TotalReview= item.BizAppUser.Reviews.Count , TotalBusinessMediaPicture =await _UnitOfWork.BusinessHomePageRepo.GetTotalUserMedia(item.BizAppUserId),  TotalReviewPicture = await _UnitOfWork.ReviewRepo.GetUserTotalReviewMedia(item.BizAppUserId)
						});
					}
					businessPopop.reviews = reviews;
					businessPopop.totalreview = Item.Reviews.Count;
					return businessPopop;
			}
				else
				{
					return null; 
				}
			}
		   catch (Exception )
			{
				throw; 

			}
		}


	}
}
