using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.Controllers.City
{
	[Route("api/[controller]")]
	[ApiController]
	public class DistrictController : ControllerBase
	{
        private readonly IUnitOfWorkRepo _UnitOfWork;

        public DistrictController(IUnitOfWorkRepo unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        // GET: api/Cities
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAllWithProvinces(string txtSearch)
        {
            if (string.IsNullOrEmpty(txtSearch))
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
        [HttpGet]
        [Route("GetDistricts")]
        public async Task<IActionResult> GetDistricts(string txtSearch)
        {
            if (string.IsNullOrEmpty(txtSearch))
            {
                return Ok(string.Empty);
            }
            Dictionary<int, string> Districts = new Dictionary<int, string>();
            var items = await _UnitOfWork.DistrictRepo.GetAllWithParentNames(txtSearch);

            foreach (var item in items.Take(10))
            {
				if (!Districts.ContainsKey(item.Id))
				{

                Districts.Add(item.Id, item.ListName);
				}
            }
            return Ok(Districts);
        }
    }
}
