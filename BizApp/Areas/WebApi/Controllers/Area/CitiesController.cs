using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using DomainClass;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Identity;
using DomainClass.Queries;

namespace BizApp.Areas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IUnitOfWorkRepo _UnitOfWork;

        public CitiesController(IUnitOfWorkRepo unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        // GET: api/Cities
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetCities(string txtSearch)
        {
            if(string.IsNullOrEmpty(txtSearch))
			{
                return Ok(string.Empty);
			}
            Dictionary<int, string> Districts = new Dictionary<int, string>(); 
            var items = await _UnitOfWork.CityRepo.GetAllWithProvinces(txtSearch);
			foreach (var item in items.Take(10))
			{
                Districts.Add(item.Id, item.ListName);
            }
            return Ok(Districts);
        }
    }
}
