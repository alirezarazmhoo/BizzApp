using BizApp.Models.Basic;
using BizApp.Utility;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BizApp.Controllers
{
	public class BusinessGalleryController : Controller
	{
		private readonly IUnitOfWorkRepo _unitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public BusinessGalleryController(IUnitOfWorkRepo unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor;
		}
		[Authorize]
		public IActionResult UserUploadPhoto(Guid Id)
		{
			return View();
		}
		public async Task<IActionResult> BusinessPictures(Guid Id)
		{
			#region Objects
			BusinessGalleryViewModel businessGalleryViewModel = new BusinessGalleryViewModel();
			List<BusinessGallery_PictureViewModel> businessGallery_PictureViewModels = new List<BusinessGallery_PictureViewModel>();
			#endregion
			#region Resource
			var BusinessItem = await _unitOfWork.BusinessRepo.GetById(Id);
			var BusinessGallery = await _unitOfWork.BusinessHomePageRepo.GetBusinessGallery(BusinessItem.Id);
			#endregion
			#region FinalResault
			businessGalleryViewModel.BusinessId = BusinessItem.Id;
			businessGalleryViewModel.BusinessName = BusinessItem.Name;
			businessGalleryViewModel.Image = string.IsNullOrEmpty(BusinessItem.FeatureImage) == true ? "/Upload/DefaultPicutres/Bussiness/Business.jpg" : BusinessItem.FeatureImage;
			businessGalleryViewModel.BusinessRate = BusinessItem.Rate == 0 ? 1 : BusinessItem.Rate;
			businessGalleryViewModel.BusinessTotalReview = BusinessItem.Reviews.Count;
			foreach (var item in BusinessGallery)
			{

		
				businessGallery_PictureViewModels.Add(new BusinessGallery_PictureViewModel() { description = item.Description, Image = item.Image, UserId = item.CustomerBusinessMedia.BizAppUserId, UserName = item.CustomerBusinessMedia.BizAppUser.FullName , Id =item.Id , UserTotalReview =await _unitOfWork.ReviewRepo.GetUserTotalReview(item.CustomerBusinessMedia.BizAppUserId) , Date =item.CustomerBusinessMedia.Date.ToPersianDateString() , UserImage= string.IsNullOrEmpty(item.CustomerBusinessMedia.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage).Select(s => s.UploadedPhoto).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart" : item.CustomerBusinessMedia.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage).Select(s => s.UploadedPhoto).FirstOrDefault() , 
			});
			}
			businessGalleryViewModel.businessGallery_PictureViewModels = businessGallery_PictureViewModels;
			#endregion
			return View(businessGalleryViewModel);
		}
		private string GetUserId()
		{
			return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
		}

	}
}
