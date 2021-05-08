using BizApp.Areas.WebApi.Models;
using DataLayer.Data;
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
	public class ProfileController : ControllerBase
	{

		private readonly IUnitOfWorkRepo _UnitOfWork;
		public ProfileController(ApplicationDbContext context, IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}

		[Route("UserProfile")]
		public async Task<IActionResult> GetUsersInformation(string Id)
		{
			UserProfile userProfile = new UserProfile();
			string Token = HttpContext.Request?.Headers["Token"];
			if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
			{
				return NotFound();
			}
			try
			{
			var UserItem = await _UnitOfWork.UserRepo.GetById(await _UnitOfWork.UserRepo.UserTokenMaper(Token));
			userProfile.Address = string.IsNullOrEmpty(UserItem.Address) ? "بدون آدرس" : UserItem.Address;
			userProfile.Image = string.IsNullOrEmpty(UserItem.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : UserItem.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault();
			userProfile.TotalBusinessMediaPicture = await _UnitOfWork.BusinessHomePageRepo.GetTotalUserMedia(Id);
			userProfile.TotalReview = UserItem.Reviews.Count;
			userProfile.TotalReviewPicture = await _UnitOfWork.ReviewRepo.GetUserTotalReviewMedia(Id);
			userProfile.UserName = UserItem.UserName; 
		   return Ok(userProfile); 
			}
			catch(Exception ex)
			{
				return BadRequest(ex);
			}

		}
	}
}
