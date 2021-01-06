using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BizApp.Areas.Admin.Models;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.AspNetCore.Mvc;

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
		public async Task<IActionResult> Index(string searchString, int? pageNumber)
		{
			bool shouldSearch = false;
			try
			{
				if (!string.IsNullOrEmpty(searchString)) shouldSearch = true;

				int pageSize = 5;
				var items = (shouldSearch == false) ?
						await _unitofwork.ProvinceRepo.GetAll()
						: await _unitofwork.ProvinceRepo.GetAll(searchString);

				var provinces = items.Select(s => new ProvinceViewModel { Id = s.Id, Name = s.Name });

				return View(PaginatedList<ProvinceViewModel>.CreateAsync(provinces.AsQueryable(), pageNumber ?? 1, pageSize));
			}
			catch (Exception)
			{
				return Content(CustomeMessages.Try);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrUpdate(ProvinceViewModel province)
		{
			ModelState.Remove("id");
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
					return Json("Error");
				}

				RedirectToAction(nameof(Index));
			}

			return Json("Done");
		}

		[HttpPost]
		public async Task<IActionResult> Remove(int provinceId) 
		{
			var model = await _unitofwork.ProvinceRepo.GetById(provinceId);
			try
			{
				_unitofwork.ProvinceRepo.Remove(model);
				await _unitofwork.SaveAsync();

				return Json("Done");
			}
			catch (Exception)
			{
				return Json("Error");
				throw;
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
			return Json(model);
		}

	}
}