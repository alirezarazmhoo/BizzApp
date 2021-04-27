using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public ReviewController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}




	}
}
