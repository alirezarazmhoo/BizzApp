using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.BusinessProfile.Controllers
{
	[Area("BusinessProfile")]
	public class BusinessInformationController : Controller
	{
		public  async Task<IActionResult> Index()
		{
			return View();
		}
	}
}
