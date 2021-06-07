using BizApp.Areas.WebApi.BusinessApplication.Models;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DomainClass.Businesses;
namespace BizApp.Areas.WebApi.BusinessApplication.Controllers.Business.BusinessInformation
{
	[Route("api/[controller]")]
	[ApiController]
	public class BusinessInformationController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public BusinessInformationController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}
		[Route("Get")]
		public async Task<IActionResult> GetBusinessLocationAndAddressAndCallNumberAndWebSite(Guid Id)
		{
			BusinessApplicationInformation businessApplicationInformation = new BusinessApplicationInformation();
			string Token = HttpContext.Request?.Headers["Token"];
			if (await _UnitOfWork.UserRepo.CheckBusinessUserValidity(Id, Token) == false)
			{
				return NotFound("کاربر غیرمجاز");
			}
			try
			{
			var Item = await _UnitOfWork.BusinessRepo.GetById(Id);
			businessApplicationInformation.Longitude = Item.Longitude;
			businessApplicationInformation.Latitude = Item.Latitude;
			businessApplicationInformation.Address = Item.Address;
			businessApplicationInformation.CallNumber = Item.CallNumber.ToString();
			businessApplicationInformation.WebSiteUrl = Item.WebsiteUrl;
			return Ok(businessApplicationInformation);
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[Route("ChangeLocationAndAddress")]
		[HttpPost]
		public async Task<IActionResult> ChangeLocationAndAddress(BusinessApplicationChangeInformation model)
		{
			DomainClass.Businesses.Business business = new DomainClass.Businesses.Business();
			business.Address = model.Address;
			business.CallNumber = model.CallNumber;
			business.WebsiteUrl = model.WebSiteUrl;
			business.Longitude = model.Longitude;
			business.Latitude = model.Latitude;
			business.Id = model.Id;  

			string Token = HttpContext.Request?.Headers["Token"];
			if (await _UnitOfWork.UserRepo.CheckBusinessUserValidity(model.Id, Token) == false)
			{
				return NotFound("کاربر غیرمجاز");
			}
			try
			{
				await _UnitOfWork.BusinessRepo.UpdateBaseInformations(business);
				await _UnitOfWork.SaveAsync();
				return Ok();
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message); 
			}

		}


	}
}
