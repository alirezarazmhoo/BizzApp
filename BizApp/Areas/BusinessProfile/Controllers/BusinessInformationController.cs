using BizApp.Areas.BusinessProfile.Models;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BizApp.Areas.BusinessProfile.Controllers
{
	[Area("BusinessProfile")]
	public class BusinessInformationController : Controller
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public BusinessInformationController(IUnitOfWorkRepo unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			_UnitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor;

		}
		public  async Task<IActionResult> Index()
		{
			#region Objects
			BusinessAccountBusinessInformationDto businessAccountBusinessInformationDto = new BusinessAccountBusinessInformationDto();
			#endregion
			#region Resource
			var BusinessIdes = await _UnitOfWork.BusinessRepo.GetUserBusinessesIds(GetUserId());
			var Item = await _UnitOfWork.BusinessRepo.GetById(BusinessIdes.FirstOrDefault());
			businessAccountBusinessInformationDto.Id = Item.Id;
			businessAccountBusinessInformationDto.Name = Item.Name;
			businessAccountBusinessInformationDto.Longitude = Item.Longitude;
			businessAccountBusinessInformationDto.Latitude = Item.Latitude;
			businessAccountBusinessInformationDto.TotalReview = Item.Reviews.Count;
			businessAccountBusinessInformationDto.WebSiteUrl = Item.WebsiteUrl;
			businessAccountBusinessInformationDto.Address = Item.Address;
			businessAccountBusinessInformationDto.CallNumber = Item.CallNumber;
			#endregion
			return View(businessAccountBusinessInformationDto);
		}
		public async Task<IActionResult> BasicInformations(Guid Id)
		{
			return View(); 
		}
		private string GetUserId()
		{
			return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
		}
	}
}
