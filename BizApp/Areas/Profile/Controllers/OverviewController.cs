﻿using AutoMapper;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BizApp.Areas.Profile.Controllers
{
	[Area("profile")]
	public class OverviewController : Controller
	{
		public OverviewController(IUnitOfWorkRepo unitOfWork, IMapper mapper)
			//: base(unitOfWork, mapper, httpContextAccessor)
		{
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
