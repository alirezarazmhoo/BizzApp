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
using DomainClass.Queries;

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
        public async Task<IActionResult> Index(SearchBussinessQuery searchViewModel)
        {
            searchViewModel.categories = await _UnitOfWork.CategoryRepo.GetChilds(searchViewModel.CategoryId);
            searchViewModel.features = await _UnitOfWork.FeatureRepo.GetAllIsBoolValue();
            searchViewModel.provinces = await _UnitOfWork.ProvinceRepo.GetAll();
            searchViewModel.cities = await _UnitOfWork.CityRepo.GetAll();
            ViewBag.CategoryId = searchViewModel.CategoryId;
            return View(searchViewModel);
        }
        public IActionResult AllBussiness(SearchBussinessQuery searchViewModel)
        {
            bool isAjax = Request.IsAjaxRequest();
            if (isAjax == false)
                return RedirectToAction("Index",new {CategoryId=searchViewModel.CategoryId ,page=searchViewModel.page,catsFinder=searchViewModel.catsFinder});
            //PagedList<Business> bussiness = _UnitOfWork.BusinessRepo.GetBussiness(searchViewModel.CategoryId, searchViewModel.page);
            PagedList<Business> bussiness = _UnitOfWork.BusinessRepo.GetBussiness(searchViewModel);
            ViewBag.CategoryId = searchViewModel.CategoryId;
            return PartialView("Partials/AllBusiness_Partial", bussiness);
        }

    }
}
