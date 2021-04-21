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
	public class BusinessQouteController : Controller
	{
		private IUnitOfWorkRepo _unitofwork;
		private readonly IMapper _mapper;

		public BusinessQouteController(IUnitOfWorkRepo uow, IMapper mapper)
		{
			_unitofwork = uow;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index(string searchString, int? page)
		{
			bool shouldSearch = false;
			try
			{
				if (!string.IsNullOrEmpty(searchString)) shouldSearch = true;

				int pageSize = 5;
				var items = (shouldSearch == false) ?
						await _unitofwork.BusinessQouteRepo.GetAll()
						: await _unitofwork.BusinessQouteRepo.GetAll(searchString);

				var BusinessQoutes = items.Select(s => new BusinessQouteViewModel { BusinessQouteId = s.Id, Ask = s.Ask })
									.OrderByDescending(o => o.BusinessQouteId);
				PagedList<BusinessQouteViewModel> res = new PagedList<BusinessQouteViewModel>(BusinessQoutes.AsQueryable(), page ?? 1, pageSize);
                return View(res);
			}
			catch (Exception)
			{
				return Content(CustomeMessages.Try);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrUpdate(BusinessQouteViewModel BusinessQoute)
		{
			ModelState.Remove("BusinessQouteId");
			if (ModelState.IsValid)
			{
				try
				{
					if (BusinessQoute.IsSelectedAnswer == false)
						BusinessQoute.Answer = "";
					var model = _mapper.Map<DomainClass.Businesses.BusinessQoute>(BusinessQoute);
					await _unitofwork.BusinessQouteRepo.AddOrUpdate(model);
					await _unitofwork.SaveAsync();
				}
				catch (Exception ex)
				{
					return Json(new { success = false, responseText = CustomeMessages.Fail });

				}
				return Json(new { success = true, responseText = CustomeMessages.Succcess });

			}

			return Json(new { success = false, responseText = CustomeMessages.Empty });

		}

		[HttpPost]
		public async Task<IActionResult> Remove(int BusinessQouteId) 
		{
			var model = await _unitofwork.BusinessQouteRepo.GetById(BusinessQouteId);
			try
			{
				_unitofwork.BusinessQouteRepo.Remove(model);
				await _unitofwork.SaveAsync();

				return Json(new { success = true, responseText = CustomeMessages.Succcess });

			}
			catch (Exception)
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
			var BusinessQoute = await _unitofwork.BusinessQouteRepo.GetById(ItemId);
			if (BusinessQoute == null)
			{
				return NotFound();
			}
			var model = _mapper.Map<BusinessQouteViewModel>(BusinessQoute);
			var edit = new List<EditViewModels>
			{
				new EditViewModels { key = "Ask", value = model.Ask },
				new EditViewModels { key = "BusinessQouteId", value = model.BusinessQouteId.ToString() },
				new EditViewModels { key = "Answer", value = model.Answer },
				new EditViewModels { key = "IsSelectedAnswer", value = model.IsSelectedAnswer.ToString() },
				new EditViewModels { key = "CategoryId", value = model.CategoryId.ToString() }
			};
			//var categoryItem = await _unitofwork.CategoryRepo.GetAll();
				var category = _unitofwork.CategoryRepo.GetCategoryHierarchyNamesById(BusinessQoute.CategoryId);

			return Json(new { success = true, listItem = edit.ToList(), majoritem = ItemId,categoryItem=category.ListName});
		}
	}
}