using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BizApp.Areas.Admin.Models;
using BizApp.Models;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.AspNetCore.Mvc;

namespace BizApp.Areas.Admin.Controllers
{
	public class CityController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkRepo _UnitOfWork;

		public CityController(IUnitOfWorkRepo unitOfWork, IMapper mapper)
		{
			_mapper = mapper;
			_UnitOfWork = unitOfWork;
		}

		public async Task<IActionResult> Index(string searchString, int? pageNumber)
		{
			bool shouldSearch = false;
			try
			{
				if (!string.IsNullOrEmpty(searchString)) shouldSearch = true;

				int pageSize = 5;
				var items = (shouldSearch == false) ?
						await _UnitOfWork.CityRepo.GetAll()
						: await _UnitOfWork.CityRepo.GetAll(searchString);

				var provinces = items.Select(s => new ProvinceViewModel { ProvinceId = s.Id, Name = s.Name })
									.OrderByDescending(o => o.ProvinceId);

				return View(PaginatedList<ProvinceViewModel>.CreateAsync(provinces.AsQueryable(), pageNumber ?? 1, pageSize));
			}
			catch (Exception)
			{
				return Content(CustomeMessages.Try);
			}
		}
		
		[HttpPost]
		public async Task<IActionResult> CreateOrUpdate(CityViewModel model)
		{
			ModelState.Remove("CityId");
			if (ModelState.IsValid)
			{
				var entity = _mapper.Map<City>(model);

				try
				{
					await _UnitOfWork.CityRepo.AddOrUpdate(entity);
					await _UnitOfWork.SaveAsync();

					return Json(new { success = true, responseText = CustomeMessages.Succcess });
				}
				catch //(Exception)
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

			var city = await _UnitOfWork.ProvinceRepo.GetById(ItemId);
			if (city == null)
			{
				return NotFound();
			}


			var model = _mapper.Map<CityViewModel>(city);
			var edit = new List<EditViewModels>
			{
				new EditViewModels { key = "Name", value = model.Name },
				new EditViewModels { key = "CityId", value = model.CityId.ToString() },
				new EditViewModels { key = "ProvinceId", value = model.ProvinceId.ToString() }
			};

			return Json(new { success = true, listItem = edit.ToList(), majoritem = ItemId });
		}

	}
}