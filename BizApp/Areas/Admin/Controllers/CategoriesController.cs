﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BizApp.Areas.Admin.Models;
using BizApp.Models.Basic;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;

namespace BizApp.Areas.Admin.Controllers
{
	[Area("admin")]
	public class CategoriesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkRepo _UnitOfWork;
        public CategoriesController(IUnitOfWorkRepo unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _UnitOfWork = unitOfWork;
        }
		public async Task<IActionResult> Index(string searchString, int? page)
		{
			bool shouldSearch = false;
			List<CategoryViewModel> categoryViewModel = new List<CategoryViewModel>();
			try
			{
				if (!string.IsNullOrEmpty(searchString)) shouldSearch = true;

				int pageSize = 5;
				var items = (shouldSearch == false) ?
						await _UnitOfWork.CategoryRepo.GetAll()
						: await _UnitOfWork.CategoryRepo.GetAll(searchString);

				foreach (var item in items.OrderByDescending(s=>s.Id))
				{
					categoryViewModel.Add(new CategoryViewModel() { CategoryId = item.Id, HasChild =await _UnitOfWork.CategoryRepo.HasChild(item.Id) , Name = item.Name , ParentCategoryId = item.ParentCategoryId , ChildCount = await _UnitOfWork.CategoryRepo.GetChildCount(item.Id) });
				}
				PagedList<CategoryViewModel> res = new PagedList<CategoryViewModel>(categoryViewModel.AsQueryable(), page ?? 1, pageSize);
                return View(res);
			}
			catch (Exception)
			{
				return Content(CustomeMessages.Try);
			}
		}
		[HttpPost]
		public async Task<IActionResult> CreateOrUpdate(CreateUpdateMainCategoryViewModel model)
		{
			ModelState.Remove("CategoryId");

			if (ModelState.IsValid)
			{
				var entity = _mapper.Map<CreateCategoryCommand>(model);
				try
				{
					await _UnitOfWork.CategoryRepo.Add(entity);
					return Json(new { success = true, responseText = CustomeMessages.Succcess });
				}
				catch (Exception ex)
				{
					return Json(new { success = false, responseText = CustomeMessages.Fail });
				}
			}
			return Json(new { success = false, responseText = CustomeMessages.Empty });
		}
		[HttpPost]
		public async Task<IActionResult> Remove(int categoryId)
		{
			if (categoryId == 0) return Json(new { success = true, responseText = CustomeMessages.Fail });
			var category = await _UnitOfWork.CategoryRepo.GetById(categoryId);
			if (category == null) return Json(new { success = true, responseText = CustomeMessages.Fail });
			try
			{
				_UnitOfWork.CategoryRepo.Remove(category);
				await _UnitOfWork.SaveAsync();
				return Json(new { success = true, responseText = CustomeMessages.Succcess });
			}
			catch
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}
		}
		public async Task<IActionResult> GetById(int ItemId)
		{
			if (ItemId == 0)
			{
				return NotFound();
			}

			var category = await _UnitOfWork.CategoryRepo.GetById(ItemId);
			if (category == null)
			{
				return NotFound();
			}
			var model = _mapper.Map<CategoryViewModel>(category);
			var edit = new List<EditViewModels>
			{
				new EditViewModels { key = "Name", value = model.Name },
				new EditViewModels { key = "CategoryId", value = model.CategoryId.ToString() },
				new EditViewModels { key = "ParentCategoryId", value = model.ParentCategoryId.ToString() }
			};

			return Json(new { success = true, listItem = edit.ToList(), majoritem = ItemId });
		}
		public async Task<IActionResult> ShowSubCateogries(int Id,int? page)
		{
			List<CategoryViewModel> categoryViewModel = new List<CategoryViewModel>();

			if (Id == 0)
			{
				return NotFound();
			}
			else
			{
				var items =await _UnitOfWork.CategoryRepo.GetChilds(Id);
				var CategoryItem = await _UnitOfWork.CategoryRepo.GetById(Id);
				ViewBag.ParentCategoryId = Id;
				ViewBag.ParentCategoryName = CategoryItem.Name; 
				var Categoires = items.Select(s => new CategoryViewModel {  CategoryId = s.Id,  Name = s.Name , ParentCategoryId = s.ParentCategoryId   }).OrderByDescending(o => o.CategoryId);
				foreach (var item in Categoires.OrderByDescending(S=>S.CategoryId))
				{
					categoryViewModel.Add(new CategoryViewModel() { CategoryId = item.CategoryId, HasChild = await _UnitOfWork.CategoryRepo.HasChild(item.CategoryId), Name = item.Name, ParentCategoryId = item.ParentCategoryId, ChildCount = await _UnitOfWork.CategoryRepo.GetChildCount(item.CategoryId) });
				}
				PagedList<CategoryViewModel> res = new PagedList<CategoryViewModel>(categoryViewModel.AsQueryable(), page ?? 1, 10);
                return View(res);
			}
		}

		[HttpGet]
		[ActionName("getHierarchyNames")]
		public JsonResult GetHierarchyNames(string searchString) 
		{
			if (string.IsNullOrEmpty(searchString))
				return Json(new { });

			try
			{
				var items = _UnitOfWork.CategoryRepo.GetCategoriesHierarchyNames(searchString);
				return Json(new SelectList(items, "Id", "ListName"));
			}
			catch (Exception ex)
			{
				return Json(ex.Message);
			}
			
		}

	}
}