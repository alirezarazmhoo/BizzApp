using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BizApp.Models.Basic;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Core;

namespace BizApp.Areas.Admin.Models
{
	[Area("admin")]
	public class DistrictController : Controller
	{
		private readonly IUnitOfWorkRepo _unitOfWork;
		private readonly IMapper _mapper;

		public DistrictController(IUnitOfWorkRepo unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
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
						await _unitOfWork.DistrictRepo.GetAll()
						: await _unitOfWork.DistrictRepo.GetAll(searchString);

				var districts = items.Select(s => _mapper.Map<District, DistrictViewModel>(s))
									.OrderByDescending(o => o.DistrictId);

				PagedList<DistrictViewModel> res = new PagedList<DistrictViewModel>(districts.AsQueryable(), page ?? 1, pageSize);
                return View(res);
			}
			catch //(Exception ex)
			{
				return Content(CustomeMessages.Try);
			}
		}

		[HttpGet]
		public async Task<IActionResult> LoadCities()
		{
			List<ComboBoxViewModel> items = new List<ComboBoxViewModel>();

			try
			{
				var cities = await _unitOfWork.CityRepo.GetAll();
				foreach (var item in cities)
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
		public async Task<IActionResult> CreateOrUpdate(DistrictViewModel model)
		{
			ModelState.Remove("DistrictId");
			if (ModelState.IsValid)
			{
				var entity = _mapper.Map<District>(model);
				entity.City = null;

				try
				{
					await _unitOfWork.DistrictRepo.AddOrUpdate(entity);
					await _unitOfWork.SaveAsync();

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
		public async Task<IActionResult> Remove(int districtId)
		{
			if (districtId == 0) return NotFound();

			var district = await _unitOfWork.DistrictRepo.GetById(districtId);
			if (district == null) return NotFound();

			try
			{
				_unitOfWork.DistrictRepo.Remove(district);
				await _unitOfWork.SaveAsync();

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

			var district = await _unitOfWork.DistrictRepo.GetById(ItemId);
			if (district == null)
			{
				return NotFound();
			}

			try
			{
				var model = _mapper.Map<DistrictViewModel>(district);
				var edit = new List<EditViewModels>
				{
					new EditViewModels { key = "Name", value = model.Name },
					new EditViewModels { key = "CityId", value = model.CityId.ToString() },
					new EditViewModels { key = "DistrictId", value = model.DistrictId.ToString() }
				};

				return Json(new { success = true, listItem = edit.ToList(), majoritem = ItemId });

			}
			catch //(Exception ex)
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });

			}

		}

		[HttpGet]
		[ActionName("getAllWithParentNames")]
		public async Task<JsonResult> GetAllWithParentNames(string searchString) 
		{
			if (string.IsNullOrEmpty(searchString))
				return Json(new { });

			try
			{
				var items = await _unitOfWork.DistrictRepo.GetAllWithParentNames(searchString);
				//var data = items.Cast<DistrictsWithParentNamesViewModel>().ToList();

				return Json(items);
			}
			catch (Exception ex)
			{
				return Json(ex.Message);
			}
		}

		[HttpGet, ActionName("getDistricts")]
		public async Task<JsonResult> GetDistricts(int cityId)
		{
			if (cityId == 0) throw new NullReferenceException();

			try
			{
				var items = await _unitOfWork.DistrictRepo.GetAll(cityId);
				return Json(new SelectList(items, "Id", "Name"));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}