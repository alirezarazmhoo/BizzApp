using DataLayer.Infrastructure;
using DomainClass.Review;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.Controllers.Business
{
	[Route("api/[controller]")]
	[ApiController]
	public class BusinessGalleryController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public BusinessGalleryController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}

		[HttpPost]
	
		public async Task<IActionResult> AddPhotoForBusinessByCustomer([FromForm] CustomerBusinessMedia model, IFormFile[] files)
		{
			string UserToken = HttpContext.Request?.Headers["Token"];
			if (!await _UnitOfWork.UserRepo.CheckUserToken(UserToken))
			{
				return NotFound("کاربر مورد نظر یافت نشد ");
			}
			if(await _UnitOfWork.BusinessRepo.GetById(model.BusinessId) == null)
			{
				return NotFound("کسب و کار مورد نظر یافت نشد ");
			}
			try
			{
				model.BizAppUserId = await _UnitOfWork.UserRepo.UserTokenMaper(UserToken);
				await _UnitOfWork.ReviewRepo.AddCustomerBusinessMedia(model , files  , model.caption);
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
