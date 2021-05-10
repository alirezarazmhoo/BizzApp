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
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BizApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWorkRepo _UnitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SearchController(ILogger<HomeController> logger, IUnitOfWorkRepo unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _UnitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
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
                return RedirectToAction("Index", new { CategoryId = searchViewModel.CategoryId, page = searchViewModel.page, catsFinder = searchViewModel.catsFinder, featuFinder = searchViewModel.featuFinder, districtFinder = searchViewModel.districtFinder });
            //PagedList<Business> bussiness = _UnitOfWork.BusinessRepo.GetBussiness(searchViewModel.CategoryId, searchViewModel.page);
            PagedList<Business> bussiness = _UnitOfWork.BusinessRepo.GetBussiness(searchViewModel);
            ViewBag.CategoryId = searchViewModel.CategoryId;
            return PartialView("Partials/AllBusiness_Partial", bussiness);
        }

        public async Task<IActionResult> GetByCategoryId(int CategoryId)
        {

            var BusinessQoutes = await _UnitOfWork.BusinessQouteRepo.GetByCategoryId(CategoryId);
            if (BusinessQoutes == null)
            {
                return NotFound();
            }
            return Json(new { success = true, BusinessQoutes });
        }
        [HttpPost]
        public async Task<IActionResult> AddBussinessQuoteUser(Guid BusinessId, List<string> AllAnswerQoute)
        {
            try
            {
                var BizAppUserId = GetUserId();
                await _UnitOfWork.BusinessQouteUserRepo.Add(BusinessId, AllAnswerQoute, BizAppUserId);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = CustomeMessages.Fail });
            }
            return Json(new { success = true, responseText = CustomeMessages.Succcess });
        }
        private string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }


    }
}
