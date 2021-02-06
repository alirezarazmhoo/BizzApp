using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
		public async Task<IActionResult> Index(string searchString, int? pageNumber, string userId = null)
		{
			bool shouldSearch = false;
			try
			{
				if (!string.IsNullOrEmpty(searchString)) shouldSearch = true;

				int pageSize = 5;

				var hasUserFilter = !string.IsNullOrEmpty(userId);

				List<BusinessListQuery> items;
				if (!shouldSearch && !hasUserFilter) // no search no user filter
					items = await _unitOfWork.BusinessRepo.GetAll();
				else if (shouldSearch && !hasUserFilter) // search without user
					items = await _unitOfWork.BusinessRepo.GetAll(searchString, null);
				else if (!shouldSearch && hasUserFilter) // no seach, just user filter
					items = await _unitOfWork.BusinessRepo.GetAll(userId);
				else
					items = await _unitOfWork.BusinessRepo.GetAll(searchString, userId); // search with user filter

				var businesses = items.Select(s => _mapper.Map<BusinessListQuery, BusinessListViewModel>(s))
									.OrderByDescending(o => o.Id);

				return View(PaginatedList<BusinessListViewModel>.CreateAsync(businesses.AsQueryable(), pageNumber ?? 1, pageSize));
			}
			catch //(Exception ex)
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}
		}

		[HttpGet, ActionName("create")]
		public IActionResult Create()
		{
			return View(new CreateBusinessViewModel());
		}

		[HttpGet, ActionName("edit")]
		public async Task<IActionResult> Edit(Guid id)
		{
			try
			{
				// get business
				var business = await _unitOfWork.BusinessRepo.GetById(id);
				if (business == null) return NotFound();

				// get category name
				var categoryId = (int)business.CategoryId;
				var category = _unitOfWork.CategoryRepo.GetCategoryHierarchyNamesById(categoryId);

				// get district name
				var district = await _unitOfWork.DistrictRepo.GetAllWithParentNamesById(business.DistrictId);

				// create business model
				var model = _mapper.Map<Business, CreateBusinessViewModel>(business);
				model.CategoryName = category.ListName;
				model.DistrictName = district.ListName;

				return View("create", model);
			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}
		}

		[HttpPost, ActionName("create")]
		public async Task<IActionResult> Create(CreateBusinessViewModel model, IFormFile file, IFormFile[] BussinessFiles)
		{
			//if (!ModelState.IsValid) return View(model);

			try
			{
				var entity = _mapper.Map<Business>(model);
				entity.UserCreatorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
				await _unitOfWork.BusinessRepo.Add(entity, file, BussinessFiles);
				await _unitOfWork.SaveAsync();
			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = ex.Message });
			}
			return RedirectToAction(nameof(Index));
		}

		[HttpGet, ActionName("BusinessFeature")]
		public async Task<IActionResult> BusinessFeature(Guid? Id)
		{
			if (Id.HasValue)
			{
				Business businessItem = await _unitOfWork.BusinessRepo.GetById(Id.Value);
				if (businessItem != null)
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
		public async Task<IActionResult> AssingBusinessFeature(Guid? Id, int FeatureId)
		{
			if (Id.HasValue && FeatureId != 0)
			{
				await _unitOfWork.BusinessRepo.AssignFeature(Id.Value, FeatureId);
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
				await _unitOfWork.BusinessRepo.Remove(item);
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