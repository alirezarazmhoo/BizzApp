using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BizApp.Areas.Admin.Models;
using BizApp.Models.Basic;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

namespace BizApp.Areas.Admin.Controllers
{
	[Area("admin")]
	[Route(template: "admin/category/{categoryId}/features", Name = "categoryFeatures")]
	public class CategoryFeaturesController : Controller
	{
		private readonly IUnitOfWorkRepo _unitOfWork;
		private readonly IMapper _mapper;

		public CategoryFeaturesController(IUnitOfWorkRepo unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		private async Task<string> GetCategoryName(int categoryId)
		{

			var category = await _unitOfWork.CategoryRepo.GetById(categoryId);
			//if (category == null) return null;

			return null ?? category.Name;
		}

		private async Task<IQueryable<CategoryFeaturesViewModel>> GetAll(int categoryId, string searchString = null)
		{
			var shouldSearch = !string.IsNullOrEmpty(searchString);
			var items = (shouldSearch == false) ?
						await _unitOfWork.CategoryFeaturesRepo.GetAll(categoryId)
						: await _unitOfWork.CategoryFeaturesRepo.GetAll(categoryId, searchString);
			
			var categoryFeatures =
					items.Select(s => _mapper.Map<CategoryFeature, CategoryFeaturesViewModel>(s))
					.OrderByDescending(o => o.CategoryFeatureId)
					.AsQueryable();

			return categoryFeatures;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int categoryId, int? page)
		{
			try
			{
				ViewBag.CategoryId = categoryId;

				var categoryName = await GetCategoryName(categoryId);
				if (categoryName == null) return NotFound();

				ViewBag.CategoryName = categoryName;

				var categoryFeatures = await GetAll(categoryId);

				PagedList<CategoryFeaturesViewModel> res = new PagedList<CategoryFeaturesViewModel>(categoryFeatures, page ?? 1, 100);
                return View(res);
			}
			catch (Exception)
			{
				return Content(CustomeMessages.Try);
			}
		}

		[HttpPost("index")]
		public async Task<IActionResult> Index(int categoryId, string searchString, int? pageNumber)
		{
			try
			{
				var categoryName = await GetCategoryName(categoryId);
				if (categoryName == null) return NotFound();

				ViewBag.CategoryName = categoryName;
				ViewBag.CategoryId = categoryId;

				var categoryFeatures = await GetAll(categoryId, searchString);
				
				int pageSize = 5;
				PagedList<CategoryFeaturesViewModel> res = new PagedList<CategoryFeaturesViewModel>(categoryFeatures, pageNumber ?? 1, 100);

				return View(res);
			}
			catch (Exception)
			{
				return Content(CustomeMessages.Try);
			}
		}
		[HttpPost("createorupdate")]
		public async Task<IActionResult> CreateOrUpdate(CategoryFeaturesViewModel model)
		{
			ModelState.Remove(nameof(model.CategoryFeatureId));
			ModelState.Remove(nameof(model.CategoryId));
			if (ModelState.IsValid)
			{
				var entity = _mapper.Map<CategoryFeature>(model);
				try
				{
					await _unitOfWork.CategoryFeaturesRepo.AddOrUpdate(entity);
					await _unitOfWork.SaveAsync();
					return Json(new { success = true, responseText = CustomeMessages.Succcess });
				}
				catch
				{
					return Json(new { success = false, responseText = CustomeMessages.Fail });
				}
			}
			return Json(new { success = false, responseText = CustomeMessages.Empty });
		}
		[HttpPost("remove")]
		public async Task<IActionResult> Remove(int categoryFeatureId)
		{
			if (categoryFeatureId == 0) return Json(new { success = true, responseText = CustomeMessages.Fail });
			var categoryFeature = await _unitOfWork.CategoryFeaturesRepo.GetById(categoryFeatureId);
			if (categoryFeature == null) return Json(new { success = true, responseText = CustomeMessages.Fail });
			try
			{
				_unitOfWork.CategoryFeaturesRepo.Remove(categoryFeature);
				await _unitOfWork.SaveAsync();
				return Json(new { success = true, responseText = CustomeMessages.Succcess });
			}
			catch
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}
		}
		[HttpPost("getbyid")]
		public async Task<IActionResult> GetById(int ItemId)
		{
			if (ItemId == 0)
			{
				return NotFound();
			}

			var categoryFeature = await _unitOfWork.CategoryFeaturesRepo.GetById(ItemId);
			if (categoryFeature == null)
			{
				return NotFound();
			}
			var model = _mapper.Map<CategoryFeaturesViewModel>(categoryFeature);
			var edit = new List<EditViewModels>
			{
				new EditViewModels { key = "Name", value = model.Name },
				new EditViewModels { key = "CategoryFeatureId", value = model.CategoryFeatureId.ToString() }
			};

			return Json(new { success = true, listItem = edit.ToList() });
		}
	}
}