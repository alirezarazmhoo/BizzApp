using BizApp.Models.Basic;
using DataLayer.Infrastructure;
using DomainClass.Businesses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagedList.Core;
using System.Threading.Tasks;
using BizApp.Extensions;
using System;
using BizApp.Utility;

namespace BizApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWorkRepo _UnitOfWork;
        public SearchController(ILogger<HomeController> logger, IUnitOfWorkRepo unitOfWork)
        {
            _logger = logger;
            _UnitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(SearchViewModel searchViewModel)
        {
            searchViewModel.categories = await _UnitOfWork.CategoryRepo.GetChilds(searchViewModel.CategoryId);

            return View(searchViewModel);
        }
        public IActionResult AllBussiness(int CategoryId, int page = 1)
        {
            //var httpClient = new HttpClient();
            bool isAjax = Request.IsAjaxRequest();
            if (isAjax == false)
                return RedirectToAction("Index",new {CategoryId=CategoryId });
            PagedList<Business> bussiness = _UnitOfWork.BusinessRepo.GetBussiness(CategoryId, page);
            ViewBag.CategoryId = CategoryId;
            return PartialView("Partials/AllBusiness_Partial", bussiness);
        }

    }
}
