using BizApp.Models.Basic;
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
			#endregion
			#region Resource
			var BusinessItem = await _unitOfWork.BusinessRepo.GetById(Id);
			var BusinessGallery = await _unitOfWork.BusinessHomePageRepo.GetBusinessGallery(BusinessItem.Id);
			#endregion
			#region FinalResault
			businessGalleryViewModel.BusinessId = BusinessItem.Id;
			businessGalleryViewModel.BusinessName = BusinessItem.Name; 
			#endregion



			return View();
		}



		private string GetUserId()
		{
			return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
		}

	}
}
