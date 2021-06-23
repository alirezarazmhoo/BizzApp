using AutoMapper;
using BizApp.Areas.BusinessProfile.Models;
using DataLayer.Infrastructure;
using DomainClass.Businesses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
		private readonly IMapper _mapper;

		public BusinessInformationController(IUnitOfWorkRepo unitOfWork, IHttpContextAccessor httpContextAccessor , IMapper mapper)
		{
			_UnitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor;
			_mapper = mapper;
			 

		}
		public  async Task<IActionResult> Index(Guid Id)
		{
			#region Objects
			BusinessAccountBusinessInformationDto businessAccountBusinessInformationDto = new BusinessAccountBusinessInformationDto();
			Business Item = new Business();
			#endregion
			#region Resource
			var BusinessIdes = await _UnitOfWork.BusinessRepo.GetUserBusinessesIds(GetUserId());
			if(Id ==  Guid.Empty)
			{
			 Item = await _UnitOfWork.BusinessRepo.GetById(BusinessIdes.FirstOrDefault());
			}
			else
			{
				Item = await _UnitOfWork.BusinessRepo.GetById(Id);
			}
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
			#region Objects
			BusinessAccountBusinessInformationDto businessAccountBusinessInformationDto = new BusinessAccountBusinessInformationDto();
			List<(int FeatureId, string FeatureName, bool IsInFeatrue , DomainClass.Enums.BusinessFeatureType , string Value) > FeaturesInBusiness = new List<(int FeatureId, string FeatureName, bool IsInFeatrue , DomainClass.Enums.BusinessFeatureType , string Value)>();
			#endregion
			#region Resource
			var Item = await _UnitOfWork.BusinessRepo.GetById(Id);
			var BusinessFeatures = await _UnitOfWork.BusinessRepo.GetBusinessFature(Id);
			businessAccountBusinessInformationDto.Id = Item.Id;
			businessAccountBusinessInformationDto.Name = Item.Name;
			businessAccountBusinessInformationDto.Longitude = Item.Longitude;
			businessAccountBusinessInformationDto.Latitude = Item.Latitude;
			businessAccountBusinessInformationDto.TotalReview = Item.Reviews.Count;
			businessAccountBusinessInformationDto.WebSiteUrl = Item.WebsiteUrl;
			businessAccountBusinessInformationDto.Address = Item.Address;
			businessAccountBusinessInformationDto.CallNumber = Item.CallNumber;
			businessAccountBusinessInformationDto.Email = Item.Email;
			businessAccountBusinessInformationDto.DistricyName = Item.District.City.Name +" - "+Item.District.City.Province.Name + " - " + Item.District.Name;
			businessAccountBusinessInformationDto.CategoryName = Item.Category.Name;
			businessAccountBusinessInformationDto.Biography = Item.Biography;
			businessAccountBusinessInformationDto.Description = Item.Description;
			businessAccountBusinessInformationDto.PostalCode = Item.PostalCode;
			businessAccountBusinessInformationDto.DistrictId = Item.DistrictId;
			businessAccountBusinessInformationDto.MainImage = Item.FeatureImage;
			businessAccountBusinessInformationDto.CategoryId = Item.CategoryId;

			foreach (var item in BusinessFeatures)
			{
				FeaturesInBusiness.Add((item.FeatureId, item.FeatureName, item.IsInFeature ,item.ValueType , item.Value));		
			}

			businessAccountBusinessInformationDto.BusinessFeatrues = FeaturesInBusiness; 

			#endregion
			return View(businessAccountBusinessInformationDto); 
		}

		[HttpPost]
		public async Task<IActionResult> UpdateBusinessInformations(BusinessAccountBusinessInformationDto dto)
		{
			var BusinessNameToValidate = ModelState["Name"];

			string Errors = string.Empty;
			bool Isvalid = true; 

			if(BusinessNameToValidate.ValidationState ==  Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
			{
				Errors +=  "نام کسب و کار خالی است";
				Isvalid = false; 

			}
			if (dto.CategoryId == 0 )
			{
				Errors += "دسته بندی انتخابی معتبر نمی باشد";
				Isvalid = false;

			}
			if (dto.DistrictId == 0)
			{
				Errors += "ناحیه مورد نظر وجود ندارد ";
				Isvalid = false;

			}
			if (!Isvalid)
			{

			return Json(new { success = false , responseText = Errors });
			}

			try
			{
            var FeaturesJson = JsonConvert.DeserializeObject<DomainClass.Queries.SelectedFeaturesDto[]>(dto.SelectedFeatures);
			await _UnitOfWork.BusinessRepo.UpdateBusinessFeaturesInBusinessAccount(FeaturesJson , dto.Id);
			await _UnitOfWork.BusinessRepo.Update(_mapper.Map<Business>(dto) , false,dto.file ,null);
			await _UnitOfWork.SaveAsync();
				return Json(new { success = true});
			}
			catch (Exception e)
			{
				return Json(new { success = false, responseText = e.Message });
			}
		}
		public async Task<IActionResult> Specialities()
		{



			return View();
		}



		private string GetUserId()
		{
			return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
		}
	}
}
