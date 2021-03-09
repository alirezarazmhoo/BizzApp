using AutoMapper;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BizApp.Areas.Profile.Controllers
{
	[Area("profile")]
	public class OverviewController : ProfileController
	{
		public OverviewController(IUnitOfWorkRepo unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) 
			: base(unitOfWork, mapper, httpContextAccessor)
		{
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
