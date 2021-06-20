using BizApp.Areas.BusinessProfile.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BizApp.Areas.BusinessProfile.Controllers
{

	[Area("BusinessProfile")]

	public class ManageBusinessAccountController : Controller
	{
		public async Task<IActionResult> Index()
		{
			return View();
		}




	}
}
