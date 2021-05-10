using BizApp.Areas.WebApi.Models;
using DataLayer.Infrastructure;
using DomainClass.Review;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Review = DomainClass.Review.Review;

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
		public async Task<bool> ChangeCoolCount(Guid Id)
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
		[HttpPost]
		[Route("Add")]
		public async Task<IActionResult> Add([FromForm] Review model , IFormFile[] files )
		{
			string Token = HttpContext.Request?.Headers["Token"];
			if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
			{
				return NotFound();
			}
			try
			{
				model.BizAppUserId = await _UnitOfWork.UserRepo.UserTokenMaper(Token); 
				await _UnitOfWork.ReviewRepo.AddReview(model , files , model.caption);
				await _UnitOfWork.SaveAsync();
				return Ok();
			}
			catch(Exception)
			{
				throw; 
			}
		}

		[Route("GetUserReview")]
		public async Task<IActionResult> GetUserReview(string Id)
		{
			List<ReviewProfile> reviews = new List<ReviewProfile>(); 
			if (await _UnitOfWork.UserRepo.GetById(Id) == null)
			{
				return NotFound();
			}
			try
			{
				var Items = await _UnitOfWork.ReviewRepo.GetUserReview(Id);
				foreach (var item in Items)
				{
					reviews.Add(new ReviewProfile() { Image = string.IsNullOrEmpty(item.Business.FeatureImage) == false ? "/Upload/DefaultPicutres/Bussiness/business-strategy-success-target-goals_1421-33.jpg" : item.Business.FeatureImage, Rate = item.Rate, TotalImages = item.ReviewMedias.Count, Text = item.Description, BusinessName = item.Business.Name, Id = item.Id });
				}
				return Ok(reviews);
			}
			catch(Exception)
			{
				throw;  
			}
		}
	}
}
