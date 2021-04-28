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
		public async Task<bool> ChangeUseFullCount(Guid Id, string UserId)
		{

			try
			{
				bool state;
				if (await _UnitOfWork.UserRepo.GetById(UserId) == null)
				{
					throw new Exception();
				}
				if (await _UnitOfWork.ReviewRepo.ChangeHelpFull(Id, UserId) == DomainClass.Enums.VotesAction.Add)
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

		public async Task<bool> ChangeFunnyCount(Guid Id, string UserId)
		{
			try
			{

				bool state;

				if (await _UnitOfWork.UserRepo.GetById(UserId) == null )
				{
					throw new Exception();
				}


				if (await _UnitOfWork.ReviewRepo.ChangeFunnyCount(Id, UserId) == DomainClass.Enums.VotesAction.Add)
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
				if (await _UnitOfWork.UserRepo.GetById(UserId) == null)
				{
					throw new Exception();
				}
				if (await _UnitOfWork.ReviewRepo.ChangeCoolCount(Id, UserId) == DomainClass.Enums.VotesAction.Add)
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
