using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using BizApp.Areas.Admin.Models;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Businesses;
using DomainClass.Businesses.Commands;
using DomainClass.Businesses.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

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
		public async Task<IActionResult> Index(string searchString, int? page, string userId = null)
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
				PagedList<BusinessListViewModel> res = new PagedList<BusinessListViewModel>(businesses.AsQueryable(), page ?? 1, pageSize);
                return View(res);
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
				Dictionary<int, string> BusinessGallery = new Dictionary<int, string>();
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
				foreach (var item in business.Galleries)
				{
					BusinessGallery.Add(item.Id , item.FileAddress);
				}
				model.GalleryImages = BusinessGallery; 

				// get galleryImages

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
				var entity = _mapper.Map<CreateBusinessCommand>(model);

				// if checked 
				using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
				{
					if (model.Id == default)
					{
						// create business
						entity.UserCreatorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
						await _unitOfWork.BusinessRepo.Create(entity, model.IsCity, file, BussinessFiles);
					}
					else
					{
						var updateModel = _mapper.Map<Business>(model);
						await _unitOfWork.BusinessRepo.Update(updateModel, model.IsCity, file, BussinessFiles);
					}

					await _unitOfWork.SaveAsync();
					scope.Complete();
				}
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

		[HttpPost, ActionName("AssingBusinessFeatureWithValue")]
		public async Task<IActionResult> AssingBusinessFeatureWithValue(SetBusinessFeatureValueViewModel model)
		{
			try
			{
				await _unitOfWork.BusinessRepo.AssignFeature(model.BusinessId, model.FeatureId, model.Value);
				await _unitOfWork.SaveAsync();
				return RedirectToAction("BusinessFeature", "Businesses", new { Id = model.BusinessId });
			}
			catch //(Exception ex)
			{
				return RedirectToAction("BusinessFeature", "Businesses", new { Id = model.BusinessId });
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

		[HttpPost, ActionName("deleteFeatureImage")]
		public IActionResult DeleteFeatureImage(Guid id, string filePath)
		{
			try
			{
				var result = _unitOfWork.BusinessRepo.DeleteFeatureImage(id, filePath);
				if (result)
				{
					return Json(new { success = true, responseText = CustomeMessages.Succcess });
				}

				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = ex.Message });
			}
		}

		[HttpPost, ActionName("deleteGalleryImage")]
		public async Task< IActionResult> DeleteGalleryImage(BusinessGalleryImage dto)
		{
			try
			{
				await _unitOfWork.BusinessRepo.DeleteGalleryImage(dto.ImageId);
				await _unitOfWork.SaveAsync();
				return Json(new { success = true});

			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = ex.Message });
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