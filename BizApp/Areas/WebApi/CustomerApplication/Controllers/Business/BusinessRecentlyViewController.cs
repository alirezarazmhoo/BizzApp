using BizApp.Areas.WebApi.Models;
using BizApp.Utility;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BusinessRecentlyViewController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public BusinessRecentlyViewController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}

		[Route("Get")]
		public async Task<IActionResult> Get()
		{
			string Token = HttpContext.Request?.Headers["Token"];
			List<UserBusinessRecentlyViewed> userBusinessRecentlyVieweds = new List<UserBusinessRecentlyViewed>();
			var Items = await _UnitOfWork.BusinessRecentlyViewdRepo.Get(await _UnitOfWork.UserRepo.UserTokenMaper(Token));

			if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
			{
				return NotFound();
			}
			foreach (var item in Items)
			{
				userBusinessRecentlyVieweds.Add(new UserBusinessRecentlyViewed()
				{
					id = item.BusinessId,
					featureimage = string.IsNullOrEmpty(item.Business.FeatureImage) == true ? ReturnDefaults.BusinessImage() : item.Business.FeatureImage,
					name = item.Business.Name,
					recentlyviewedid = item.Id,
					 districtname = "اصفهان خ ابن سینا"
				
				});
			}
			return Ok(userBusinessRecentlyVieweds);
		}

		[Route("Remove")]
		[HttpPost]
		public async Task<IActionResult> Delete(Guid Id)
		{
			string Token = HttpContext.Request?.Headers["Token"];
			if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
			{
				return NotFound("کاربر مورد نظر یافت نشد");
			}
			try
			{
				await _UnitOfWork.BusinessRecentlyViewdRepo.Remove(Id);
				await _UnitOfWork.SaveAsync();
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
