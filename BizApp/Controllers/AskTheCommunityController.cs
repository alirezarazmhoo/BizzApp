using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Controllers
{
	public class AskTheCommunityController : Controller
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public AskTheCommunityController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{



			return View();
		}
	}
}
