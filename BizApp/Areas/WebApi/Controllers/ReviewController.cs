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
		[HttpPost]
		[Route("ChangeUseFullCount")]
		public async Task<bool> ChangeUseFullCount(Guid Id)
		{
			try
			{
				bool state;
				string Token = HttpContext.Request?.Headers["Token"];
				if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
				{
					throw new Exception();
				}
				if (await _UnitOfWork.ReviewRepo.ChangeHelpFull(Id, await _UnitOfWork.UserRepo.UserTokenMaper(Token)) == DomainClass.Enums.VotesAction.Add)
				{
					state = true;
				}
				else
				{
					state = false;

				}
				await _UnitOfWork.SaveAsync();
				return state;
			}
			catch (Exception)
			{
				throw;

			}

		}
		[HttpPost]
		[Route("ChangeFunnyCount")]

		public async Task<bool> ChangeFunnyCount(Guid Id)
		{
			try
			{
				bool state;
				string Token = HttpContext.Request?.Headers["Token"];
				if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
				{
					throw new Exception();
				}
				if (await _UnitOfWork.ReviewRepo.ChangeFunnyCount(Id, await _UnitOfWork.UserRepo.UserTokenMaper(Token)) == DomainClass.Enums.VotesAction.Add)
				{
					state = true;
				}
				else
				{
					state = false;

				}
				await _UnitOfWork.SaveAsync();
				return state;
			}
			catch (Exception)
			{
				throw;

			}

		}
		[HttpPost]
		[Route("ChangeCoolCount")]
		public async Task<bool> ChangeCoolCount(Guid Id, string UserId)
		{
			try
			{
				bool state;
				string Token = HttpContext.Request?.Headers["Token"];
				if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
				{
					throw new Exception();
				}
				if (await _UnitOfWork.ReviewRepo.ChangeCoolCount(Id, await _UnitOfWork.UserRepo.UserTokenMaper(Token)) == DomainClass.Enums.VotesAction.Add)
				{
					state = true;
				}
				else
				{
					state = false;

				}
				await _UnitOfWork.SaveAsync();
				return state;

			}
			catch (Exception)
			{
				throw;
			}

		}
		//[HttpPost]
		//[Route("Add")]
		//public async Task<IActionResult> Add( )
		//{
		//	string Token = HttpContext.Request?.Headers["Token"];
		//	if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
		//	{
		//		return NotFound();
		//	}





		//}


	}
}
