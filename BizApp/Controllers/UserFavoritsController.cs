using DataLayer.Infrastructure;
using DomainClass.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Controllers
{

	public class UserFavoritsController : Controller
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public UserFavoritsController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
         public async Task<JsonResult> AddOrRemove(string Id)
		{
			var UserId = "8ad1f65a-c47d-4f1a-a601-5c64c186c09b";
			try
			{
				string type;

			if (await _UnitOfWork.UserFavoritsRepo.AddOrRemove(Id , UserId) == VotesAction.Add)
			{
				type = "add";
			}
			else
			{
				type = "submission";

			}
			await _UnitOfWork.SaveAsync();
			return Json(new { success = true, type });
			}
			catch (Exception)
			{
				return Json(new { success = false });
			}
		}

	}
}
