using BizApp.Areas.WebApi.BusinessApplication.Models;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.BusinessApplication.Controllers.Business.Slider
{
	[Route("api/[controller]")]
	[ApiController]
	public class BusinessAppSliderController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public BusinessAppSliderController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}

		[HttpGet]
		[Route("Get")]
		public async Task<IActionResult> GetSilderForBusinessApplication(Guid Id)
		{
			BusinessApplicationSlider businessApplicationSlider = new BusinessApplicationSlider();
			string Token = HttpContext.Request?.Headers["Token"];
			if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
			{
				return NotFound();
			}
			try
			{
			var Item = await _UnitOfWork.BusinessRepo.GetById(Id);
			businessApplicationSlider.BusinessFeatureImage = Item.FeatureImage == null ? Utility.ReturnDefaults.BusinessImage() : Item.FeatureImage;
			businessApplicationSlider.BusinessName = Item.Name;
			businessApplicationSlider.Rate = Item.Rate;
			businessApplicationSlider.TotalReview = Item.Reviews.Where(s => s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted).Count();
			businessApplicationSlider.PhoneNumber = Item.CallNumber.ToString();
		    return Ok(businessApplicationSlider);			
			}catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}


		}


	}
}
