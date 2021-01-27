using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BizApp.Areas.Admin.Models;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Businesses;
using DomainClass.Businesses.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BizApp.Areas.Admin.Controllers
{
	[Area("admin")]
	public class BusinessesController : Controller
	{
		private readonly IUnitOfWorkRepo _unitOfWork;
		private readonly IMapper _mapper;

		public BusinessesController(IUnitOfWorkRepo unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> Index(string searchString, int? pageNumber)
		{
			bool shouldSearch = false;
			try
			{
				if (!string.IsNullOrEmpty(searchString)) shouldSearch = true;

				int pageSize = 5;
				var items = (shouldSearch == false) ?
						await _unitOfWork.BusinessRepo.GetAll()
						: await _unitOfWork.BusinessRepo.GetAll(searchString);

				var businesses = items.Select(s => _mapper.Map<BusinessListQuery, BusinessListViewModel>(s))
									.OrderByDescending(o => o.Id);

				return View(PaginatedList<BusinessListViewModel>.CreateAsync(businesses.AsQueryable(), pageNumber ?? 1, pageSize));
			}
			catch (Exception)
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}
		}

		[HttpGet, ActionName("create")]
		public async Task<IActionResult> Create()
		{
			try
			{
				// get provinces
				var provinceEntities = await _unitOfWork.ProvinceRepo.GetAll();
				// convert provinces to view model
				var provinces = provinceEntities.Select(s => _mapper.Map<Province, ProvinceViewModel>(s)).ToList();
				ViewBag.Provinces = provinces;
				ViewData["Categorie"] = await _unitOfWork.CategoryRepo.GetAll();
				return View(new CreateBusinessViewModel(provinces));
			}
			catch (Exception)
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}
		}
		[HttpPost, ActionName("create")]
		public async Task<IActionResult>  CreatePost(CreateBusinessViewModel model, IFormFile file, IFormFile[] BussinessFiles)
		{
			try
			{
				var entity = _mapper.Map<Business>(model);
				await _unitOfWork.BusinessRepo.Add(entity, file , BussinessFiles );
				await _unitOfWork.SaveAsync();
			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = ex.Message });
			}
			return RedirectToAction(nameof(Index));
		}

		[HttpGet, ActionName("getchildscategories")]
		public async Task<JsonResult> getchildscategories(int Id)
		{
			if(Id != 0)
			{
				var data =await _unitOfWork.CategoryRepo.AdminGetChildsCateogry(Id);
				return Json(new { success = true, list = data.items.ToList(), isfinal = data.Isfinal, parentid = data.Parentid });
			}
			else
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}
		}

		[HttpGet, ActionName("getbackcategories")]
		public async Task<JsonResult> getbackcategories(int Id)
		{
			if (Id != 0)
			{
				var data = await _unitOfWork.CategoryRepo.GetBackCategories(Id);
				return Json(new { success = true, list = data.items.ToList(), isfinal = data.Isfinal, parentid = data.Parentid });
			}
			else
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}
		}


		[HttpGet, ActionName("BusinessFeature")]
		public async Task<IActionResult> BusinessFeature(Guid? Id)
		{
			if (Id.HasValue)
			{
				Business businessItem =await _unitOfWork.BusinessRepo.GetById(Id.Value);
				if(businessItem != null)
				{
				ViewBag.BussinessId = Id;
		      	ViewBag.BussinessName = businessItem.Name; 
				return View(await _unitOfWork.BusinessRepo.GetBusinessFature(Id));
				}
				else
				{
					return NotFound();
				}
			}
			else
			{
				return NotFound();
			}
		}


		[HttpGet, ActionName("AssingBusinessFeature")]
		public async Task<IActionResult> AssingBusinessFeature(Guid? Id ,int FeatureId)
		{
			if (Id.HasValue && FeatureId !=0)
			{
				await _unitOfWork.BusinessRepo.AssignFeature(Id.Value , FeatureId);
				await _unitOfWork.SaveAsync();
				return RedirectToAction("BusinessFeature", "Businesses", new { Id = Id.Value });
			}
			else
			{
				return RedirectToAction("BusinessFeature", "Businesses", new { Id = Id.Value });

			}
		}

		[HttpGet, ActionName("RemoveBusinessFeature")]
		public async Task<IActionResult> RemoveBusinessFeature(Guid? Id, int FeatureId)
		{
			if (Id.HasValue && FeatureId != 0)
			{

				await _unitOfWork.BusinessRepo.RemoveFeature(Id.Value, FeatureId);
				await _unitOfWork.SaveAsync();
				return RedirectToAction("BusinessFeature", "Businesses", new { Id = Id.Value });
			}
			else
			{
				return RedirectToAction("BusinessFeature", "Businesses", new { Id = Id.Value });
			}
		}

		[HttpPost]
		public async Task<JsonResult> Remove(Guid BusinessId)
		{
			try
			{
				var item = await _unitOfWork.BusinessRepo.GetById(BusinessId);
				if (item == null)
				{
					return Json(new { success = false, responseText = CustomeMessages.Try });
				}
			await	_unitOfWork.BusinessRepo.Remove(item);
				await _unitOfWork.SaveAsync();
				return Json(new { success = true, responseText = CustomeMessages.Succcess });
			}
			catch (Exception)
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}
		}



	}
}