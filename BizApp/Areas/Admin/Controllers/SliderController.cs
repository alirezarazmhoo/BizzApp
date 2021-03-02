using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using BizApp.Areas.Admin.Models;
using BizApp.Models.Basic;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

namespace BizApp.Areas.Admin.Controllers
{
	[Area("admin")]
	
	public class SliderController : Controller
	{
		private IUnitOfWorkRepo _unitofwork;
		private readonly IMapper _mapper;

		public SliderController(IUnitOfWorkRepo uow, IMapper mapper)
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
						await _unitofwork.SliderRepo.GetAll()
						: await _unitofwork.SliderRepo.GetAll(searchString);

				var sliders = items.Select(s => new SliderViewModel { Id = s.Id, Title = s.Title })
									.OrderByDescending(o => o.Id);
				PagedList<SliderViewModel> res = new PagedList<SliderViewModel>(sliders.AsQueryable(), page ?? 1, pageSize);
                return View(res);
			}
			catch (Exception ex )
			{
				return Content(CustomeMessages.Try);
			}
		}

		
		[HttpPost]
		public async Task<IActionResult> Remove(int Id) 
		{
			var model = await _unitofwork.SliderRepo.GetById(Id);
			try
			{
				_unitofwork.SliderRepo.Remove(model);
				await _unitofwork.SaveAsync();

				return Json(new { success = true, responseText = CustomeMessages.Succcess });

			}
			catch (Exception)
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}
		}

		[HttpGet, ActionName("create")]
		public IActionResult Create()
		{
			return View(new SliderViewModel());
		}

		[HttpGet, ActionName("edit")]
		public async Task<IActionResult> Edit(int id)
		{
			try
			{
				var slider = await _unitofwork.SliderRepo.GetById(id);
				if (slider == null) return NotFound();
				var model = _mapper.Map<Slider, SliderViewModel>(slider);
				return View("create", model);
			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}
		}

		[HttpPost, ActionName("create")]
		public async Task<IActionResult> Create(SliderViewModel model)
		{

			try
			{
				var entity = _mapper.Map<Slider>(model);
				using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
				{
						await _unitofwork.SliderRepo.AddOrUpdate(entity, model.imageUrl);
					

					await _unitofwork.SaveAsync();
					scope.Complete();
				}
			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = ex.Message });
			}

			return RedirectToAction(nameof(Index));
		}


	}
}