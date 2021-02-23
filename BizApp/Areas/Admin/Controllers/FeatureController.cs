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
    public class FeatureController : Controller
    {
		private IUnitOfWorkRepo _unitofwork;
		private readonly IMapper _mapper;

		public FeatureController(IUnitOfWorkRepo uow, IMapper mapper)
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
						await _unitofwork.FeatureRepo.GetAll()
						: await _unitofwork.FeatureRepo.GetAll(searchString);

				var features = items.Select(s => _mapper.Map<Feature, FeatureViewModel>(s))
									.OrderByDescending(o => o.FeatureId);
				PagedList<FeatureViewModel> res = new PagedList<FeatureViewModel>(features.AsQueryable(), page ?? 1, pageSize);
                return View(res);
			}
			catch (Exception)
			{
				return Content(CustomeMessages.Try);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrUpdate(FeatureViewModel feature)
		{
			ModelState.Remove("FeatureId");
			if (ModelState.IsValid)
			{
				try
				{
					var model = _mapper.Map<Feature>(feature);
					await _unitofwork.FeatureRepo.AddOrUpdate(model);
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
		public async Task<IActionResult> Remove(int featureId)
		{
			var entity = await _unitofwork.FeatureRepo.GetById(featureId);
			try
			{
				_unitofwork.FeatureRepo.Remove(entity);
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

			var feature = await _unitofwork.FeatureRepo.GetById(ItemId);
			if (feature == null)
			{
				return NotFound();
			}


			var model = _mapper.Map<FeatureViewModel>(feature);
			List<EditViewModels> FeatureTypeItem = new List<EditViewModels>();

			var edit = new List<EditViewModels>
			{
				new EditViewModels { key = "Name", value = model.Name },
				new EditViewModels { key = "FeatureId", value = model.FeatureId.ToString() },
			
			};
			FeatureTypeItem.Add(new EditViewModels() { key = model.FeatureType.ToString(), value = "" });

			return Json(new { success = true, listItem = edit.ToList(), majoritem = ItemId , featuretype = FeatureTypeItem });
		}
	}
}