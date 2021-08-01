using BizApp.Models.Basic;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass.Review;
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
mage).Select(s => s.UploadedPhoto).FirstOrDefault() , 
			});
			}
			businessGalleryViewModel.businessGallery_PictureViewModels = businessGallery_PictureViewModels;
			#endregion
			return View(businessGalleryViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> AddPhotoForBusinessByCustomer(IFormFile[] file)
		{
			try
			{
				//model.BizAppUserId = GetUserId();
				//model.StatusEnum = DomainClass.Enums.StatusEnum.Waiting;
				//await _unitOfWork.ReviewRepo.AddCustomerBusinessMedia(model, file, null);
				//await _unitOfWork.SaveAsync();
				return Json(new { id=4});

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}




		private string GetUserId()
		{
			return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
		}

	}
}
