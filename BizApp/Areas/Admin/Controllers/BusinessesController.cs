using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BizApp.Areas.Admin.Models;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Businesses.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BizApp.Areas.Admin.Controllers
{
	[Area("admin")]
	public class BusinessesController : Controller
	{
		private readonly IUnitOfWorkRepo _unitOfWork;
		private readonly IMapper _mapper;

		public BusinessesController(IUnitOfWorkRepo unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> Index(string searchString, int? pageNumber)
		{
			bool shouldSearch = false;
			try
			{
				if (!string.IsNullOrEmpty(searchString)) shouldSearch = true;

				int pageSize = 5;
				var items = (shouldSearch == false) ?
						await _unitOfWork.BusinessRepo.GetAll()
						: await _unitOfWork.BusinessRepo.GetAll(searchString);

				var businesses = items.Select(s => _mapper.Map<BusinessListQuery, BusinessListViewModel>(s))
									.OrderByDescending(o => o.Id);

				return View(PaginatedList<BusinessListViewModel>.CreateAsync(businesses.AsQueryable(), pageNumber ?? 1, pageSize));
			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}
		}

		[HttpGet, ActionName("create")]
		public async Task<IActionResult> Create()
		{
			try
			{
				// get provinces
				var provinceEntities = await _unitOfWork.ProvinceRepo.GetAll();

				// convert provinces to view model
				var provinces = provinceEntities.Select(s => _mapper.Map<Province, ProvinceViewModel>(s)).ToList();
				ViewBag.Provinces = provinces;
				return View(new CreateBusinessViewModel(provinces));
			}
			catch (Exception ex)
			{
				return Json(new { success = false, responseText = CustomeMessages.Fail });
			}

		}

		[HttpPost, ActionName("create")]
		public IActionResult CreatePost(CreateBusinessViewModel model)
		{


			return View();
		}
	}
}