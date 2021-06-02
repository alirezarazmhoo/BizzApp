using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.Controllers.Profile
{
	[Route("api/[controller]")]
	[ApiController]
	public class UploadProfilePhotoController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;

		public UploadProfilePhotoController(IUnitOfWorkRepo unitOfWork
		)
		{
			_UnitOfWork = unitOfWork;
		
		}
		[HttpPost]
		[Route("Add")]
		public async Task<IActionResult> AddProfilePhotoForUser(IFormFile[] files)
		{
			string UserToken = HttpContext.Request?.Headers["Token"];
			if (!await _UnitOfWork.UserRepo.CheckUserToken(UserToken))
			{
				return NotFound("کاربر مورد نظر یافت نشد ");
			}
			try
			{
				await _UnitOfWork.UserPhotoRepo.UploadPhotos(await _UnitOfWork.UserRepo.UserTokenMaper(UserToken), files);
				return Ok();
			}
			catch(Exception e)
			{
				return BadRequest(e.Message);
			}

		}
		[HttpPost]
		[Route("Remove")]
		public async Task<IActionResult> RemoveProfilePhotoForUser(
			Guid Id)
		{
			string UserToken = HttpContext.Request?.Headers["Token"];
			if (!await _UnitOfWork.UserRepo.CheckUserToken(UserToken))
			{
				return NotFound("کاربر مورد نظر یافت نشد ");
			}
			try
			{
				await _UnitOfWork.UserPhotoRepo.DeletePhoto(Id , await _UnitOfWork.UserRepo.UserTokenMaper(UserToken));
				return Ok();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

		}

	}
}
