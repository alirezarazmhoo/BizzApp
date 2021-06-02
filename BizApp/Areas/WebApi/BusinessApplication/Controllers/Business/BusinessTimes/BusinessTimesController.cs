using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.BusinessApplication.Controllers.Business.BusinessTimes
{
	[Route("api/[controller]")]
	[ApiController]
	public class BusinessTimesController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public BusinessTimesController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}
		//[Route("Get")]
		//public async Task<IActionResult> GetBusinessTimes(Guid Id)
		//{
		//	string Token = HttpContext.Request?.Headers["Token"];
		//	if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
		//	{
		//		return NotFound();
		//	}
		//	try
		//	{

		//	}
		//	catch (Exception ex)
		//	{

		//	}
		//}
	}
}
