using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.BusinessProfile.Controllers
{
	[Area("BusinessProfile")]
	public class SpecialtiesController : Controller
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public SpecialtiesController(IUnitOfWorkRepo unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			_UnitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task< IActionResult> Index()
		{
			return View();
		}
	}
}
