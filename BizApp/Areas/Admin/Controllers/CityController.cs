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
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;

namespace BizApp.Areas.Admin.Controllers
{
	[Area("admin")]
	public class CityController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkRepo _UnitOfWork;

		public CityController(IUnitOfWorkRepo unitOfWork, IMapper mapper)
		{
			_mapper = mapper;
			_UnitOfWork = unitOfWork;
		}

		[HttpGet]
		public async Task<IActionResult> Index(string searchString, int? page)
		{
			bool shouldSearch = false;
			try
			{
				if (!string.IsNullOrEmpty(searchString)) shouldSearch = true;

				int pageSize = 5;
				var items = (shouldSearch == false) ?
						await _UnitOfWork.CityRepo.GetAll()
						: await _UnitOfWork.CityRepo.GetAll(searchString);

				var cities = items.Select(city => _mapper.Map<City, CityViewModel>(city))
									.OrderByDescending(o => o.CityId);

				//return View(PaginatedList<CityViewModel>.CreateAsync(cities.AsQueryable(), pageNumber ?? 1, pageSize));
				PagedList<CityViewModel> res = new PagedList<CityViewModel>(cities.AsQueryable(), page ?? 1, pageSize);
                return View(res);
			}
			catch //(Exception ex)
			{
				return Content(CustomeMessages.Try);
			}
		}

		[HttpGet]
		public async Task<IActionResult> LoadProvinces()
		{
			List<ComboBoxViewModel> items = new List<ComboBoxViewModel>();

			try
			{
				foreach (var item in await _UnitOfWork.ProvinceRepo.GetAll())
				{
					items.Add(new ComboBoxViewModel() { id = item.Id, name = item.Name });

				}
				return Json(new { success = true, list = items.ToList() });
			}
			catch //(Exception ex)
			{
				return Json(new { response = false, responseText = "Faild To Get Genres Data" });
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrUpdate(CityViewModel model)
		{
			ModelState.Remove("CityId");
			if (ModelState.IsValid)
			{
				var entity = _mapper.Map<City>(model);
				entity.Province = null;

				try
				{
					await _UnitOfWork.CityRepo.AddOrUpdate(entity);
					await _UnitOfWork.SaveAsync();

					return Json(new { success = true, responseText = CustomeMessages.Succcess });
				}
				catch //(Exception ex)
				{
					return Json(new { success = false, responseText = CustomeMessages.Fail });
				}
			}

			return Json(new { success = false, responseText = CustomeMessages.Empty });

		}

		[HttpPost]
		public async Task<IActionResult> Remove(int cityId)
		{
			if (cityId == 0) return NotFound();

			var city = await _UnitOfWork.CityRepo.GetById(cityId);
			if (city == null) return NotFound();

			try
			{
				_UnitOfWork.CityRepo.Remove(city);
				await _UnitOfWork.SaveAsync();

				return Json(new { success = true, responseText = CustomeMessages.Succcess });

			}
			catch // (Exception)
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

			var city = await _UnitOfWork.CityRepo.GetById(ItemId);
			if (city == null)
			{
				return NotFound();
			}

			try
			{
				var model = _mapper.Map<CityViewModel>(city);
				var edit = new List<EditViewModels>
				{
					new EditViewModels { key = "Name", value = model.Name },
					new EditViewModels { key = "CityId", value = model.CityId.ToString() },
					new EditViewModels { key = "ProvinceId", value = model.ProvinceId.ToString() }
				};

				return Json(new { success = true, listItem = edit.ToList(), majoritem = ItemId });

			}
			catch //(Exception ex)
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });

			}

		}

		[HttpGet, ActionName("getCities")]
		public async Task<JsonResult> GetCities(int provinceId)
		{
			if (provinceId == 0) throw new NullReferenceException();

			try
			{
				var items = await _UnitOfWork.CityRepo.GetAll(provinceId);
				return Json(new SelectList(items, "Id", "Name"));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}