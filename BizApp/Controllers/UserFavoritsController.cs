using DataLayer.Infrastructure;
using DomainClass.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BizApp.Controllers
{

	public class UserFavoritsController : Controller
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserFavoritsController(IUnitOfWorkRepo unitOfWork, IHttpContextAccessor httpContextAccessor)
		{
			_UnitOfWork = unitOfWork;
			_httpContextAccessor = httpContextAccessor;

		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<JsonResult> AddOrRemove(Guid Id)
		{
			try
			{
				string type;
				if (!User.Identity.IsAuthenticated)
				{
					type = "authorize";
					return Json(new { success = true, type });
				}
			    var UserId = GetUserId();
				if (await _UnitOfWork.UserFavoritsRepo.AddOrRemove(Id, UserId) == VotesAction.Add)
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
		private string GetUserId()
		{
			return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
		}

	}
}
