using BizApp.Areas.WebApi.Models;
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
		[HttpGet]
		[Route("UserBookMark")]
		public async Task<IActionResult> GetUserBookMark()
		{
			List<BookMark> bookMarks = new List<BookMark>();
			try
			{
				string Token = HttpContext.Request?.Headers["Token"];
				if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
				{
					return NotFound();
				}
				var Items = await _UnitOfWork.UserFavoritsRepo.GetAll(await _UnitOfWork.UserRepo.UserTokenMaper(Token));
				foreach (var item in Items)
				{
					bookMarks.Add(new BookMark() { id = item.BusinessId , name = item.Business.Name ,  rate = item.Business.Rate , businessImage = string.IsNullOrEmpty(item.Business.FeatureImage) == true ? "/Upload/DefaultPicutres/Bussiness/business-strategy-success-target-goals_1421-33.jpg" : item.Business.FeatureImage });
				}
				return Ok(bookMarks); 
			}
			catch (Exception)
			{
				throw; 
			}
		}

		[HttpPost]
		[Route("AddOrRemove")]
		public async Task<IActionResult> AddOrRemove(Guid Id)
		{
			try
			{
				bool state;
				string Token = HttpContext.Request?.Headers["Token"];
				if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
				{
					return NotFound();
				}
				if (await _UnitOfWork.BusinessRepo.GetById(Id) == null)
				{
					return NotFound();

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
				return Ok(state);
			}
			catch (Exception)
			{
				throw;

			}
		}
		[HttpGet]
		[Route("Check")]
		public async Task<IActionResult> CheckIsAlreadyExists(Guid Id)
		{
			try
			{
				bool state;
				string Token = HttpContext.Request?.Headers["Token"];
				if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
				{
					return NotFound();

				}
				if (await _UnitOfWork.BusinessRepo.GetById(Id) == null)
				{
					return NotFound();

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
				return Ok(state);
			}
			catch (Exception)
			{
				throw;

			}
		}

	}
}
