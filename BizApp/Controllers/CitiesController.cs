using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace BizApp.Controllers
{
	public class CitiesController : Controller
	{
		private readonly IUnitOfWorkRepo _unitOfWork;

		public CitiesController(IUnitOfWorkRepo unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet, ActionName("getCitiesWithProviceNames")]
		public async Task<JsonResult> GetCitiesWithProviceNames(string searchString)
		{
			try
			{
				var items = await _unitOfWork.CityRepo.GetAllWithProvinces(searchString);
				var result = new SelectList(items, "Id", "ListName");
				return Json(result);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
