using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass.Enums;
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
	public class BookMarkController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public BookMarkController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}
		[HttpPost]
		[Route("AddOrRemove")]
		public async Task<bool> AddOrRemove(Guid Id)
		{
			try
			{
				bool state;
				string Token = HttpContext.Request?.Headers["Token"];
				if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
				{
					throw new Exception();
				}
				if (await _UnitOfWork.BusinessRepo.GetById(Id) == null)
				{
					throw new Exception();
				}
				if (await _UnitOfWork.UserFavoritsRepo.AddOrRemove(Id, await _UnitOfWork.UserRepo.UserTokenMaper(Token)) == VotesAction.Add)
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

		[HttpGet]
		[Route("Check")]
		public async Task<bool> CheckIsAlreadyExists(Guid Id)
		{
			try
			{
				bool state;
				string Token = HttpContext.Request?.Headers["Token"];
				if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
				{
					throw new Exception();
				}
				if (await _UnitOfWork.BusinessRepo.GetById(Id) == null)
				{
					throw new Exception();
				}
				if (await _UnitOfWork.BusinessRepo.CheckBisinessFavorit(Id,await _UnitOfWork.UserRepo.UserTokenMaper(Token)) )
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

	}
}
