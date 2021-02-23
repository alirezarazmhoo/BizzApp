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
	//[Route("admin/province")]
	public class ProvinceController : Controller
	{
		private IUnitOfWorkRepo _unitofwork;
		private readonly IMapper _mapper;

		public ProvinceController(IUnitOfWorkRepo uow, IMapper mapper)
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
						await _unitofwork.ProvinceRepo.GetAll()
						: await _unitofwork.ProvinceRepo.GetAll(searchString);

				var provinces = items.Select(s => new ProvinceViewModel { ProvinceId = s.Id, Name = s.Name })
									.OrderByDescending(o => o.ProvinceId);
				PagedList<ProvinceViewModel> res = new PagedList<ProvinceViewModel>(provinces.AsQueryable(), page ?? 1, pageSize);
                return View(res);
			}
			catch (Exception)
			{
				return Content(CustomeMessages.Try);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrUpdate(ProvinceViewModel province)
		{
			ModelState.Remove("ProvinceId");
			if (ModelState.IsValid)
			{
				try
				{
					var model = _mapper.Map<Province>(province);
					await _unitofwork.ProvinceRepo.AddOrUpdate(model);
					await _unitofwork.SaveAsync();
				}
				catch //(Exception ex)
				{
					return Json(new { success = false, responseText = CustomeMessages.Fail });

				}
				return Json(new { success = true, responseText = CustomeMessages.Succcess });

			}

			return Json(new { success = false, responseText = CustomeMessages.Empty });

		}

		[HttpPost]
		public async Task<IActionResult> Remove(int provinceId) 
		{
			var model = await _unitofwork.ProvinceRepo.GetById(provinceId);
			try
			{
				_unitofwork.ProvinceRepo.Remove(model);
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

			var province = await _unitofwork.ProvinceRepo.GetById(ItemId);
			if (province == null)
			{
				return NotFound();
			}


			var model = _mapper.Map<ProvinceViewModel>(province);
			var edit = new List<EditViewModels>
			{
				new EditViewModels { key = "Name", value = model.Name },
				new EditViewModels { key = "ProvinceId", value = model.ProvinceId.ToString() }
			};

			return Json(new { success = true, listItem = edit.ToList(), majoritem = ItemId});
		}

	}
}