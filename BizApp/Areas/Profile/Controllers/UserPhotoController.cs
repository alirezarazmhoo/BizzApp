using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.Profile.Controllers
{
	[Area("profile")]
	public class UserPhotoController : Controller
	{
		private readonly IUnitOfWorkRepo UnitOfWork;

		public UserPhotoController(IUnitOfWorkRepo unitOfWork)
		{
			UnitOfWork = unitOfWork;
		}

		[HttpGet, ActionName("index")]
		public IActionResult Index(string username) 
		{
			// get user photos 
			//var photos = await UnitOfWork.UserPhotoRepo.GetAll();

			return View();
		}

		[HttpGet, ActionName("upload")]
		public IActionResult CreateNew() 
		{
			return View();
		}
	}
}
