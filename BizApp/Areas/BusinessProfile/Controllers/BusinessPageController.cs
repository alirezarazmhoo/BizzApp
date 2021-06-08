using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.BusinessProfile.Controllers
{

	[Area("BusinessProfile")]
	public class BusinessPageController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult PageBusiness()
		{
			return View();
		}
		public IActionResult BusinessPageUpgrade()
		{
			return View();
		}
		public IActionResult PageAds()
		{
			return View(); 
		}
		public IActionResult HomeAndLocalService()
		{
			return View(); 
		}


	}
}
