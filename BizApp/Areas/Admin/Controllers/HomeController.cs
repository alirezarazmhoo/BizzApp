using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BizApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "admin, operator")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}