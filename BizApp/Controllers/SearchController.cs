using BizApp.Areas.Admin.Models;
using BizApp.Models;
using BizApp.Models.Basic;
using DataLayer.Infrastructure;
using DomainClass.Businesses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Controllers
{
	public class SearchController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWorkRepo _UnitOfWork;

		public SearchController(ILogger<HomeController> logger , IUnitOfWorkRepo unitOfWork)
		{
			_logger = logger;
			_UnitOfWork = unitOfWork; 
		}
		public async Task<IActionResult>  Index(SearchViewModel searchViewModel)
		{
			
			return View(searchViewModel);
		}
		public IActionResult AllBussiness(int? CategoryId,int page=1)
        {
			PagedList<Business> bussiness=_UnitOfWork.BusinessRepo.GetBussiness(CategoryId, page);
            return PartialView("Partials/AllBusiness_Partial", bussiness);
        }
		
	}
}
