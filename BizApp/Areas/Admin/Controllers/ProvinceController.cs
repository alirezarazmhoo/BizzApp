using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BizApp.Areas.Admin.Models;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BizApp.Areas.Admin.Controllers
{
	[Area("admin")]
	[Route("admin/province")]
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
		[ValidateAntiForgeryToken]
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
				catch //(DbUpdateConcurrencyException)
				{
					return Json("Error");
				}


				RedirectToAction(nameof(Index));
			}

			return Json("Done");
		}

	}
}