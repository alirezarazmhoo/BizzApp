using BizApp.Areas.WebApi.BusinessApplication.Models;
using BizApp.Areas.WebApi.Models;
using BizApp.Utility;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.BusinessApplication.Controllers.Business.BusinessTimes
{
	[Route("api/[controller]")]
	[ApiController]
	public class BusinessTimesController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public BusinessTimesController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}
		[Route("Get")]
		public async Task<IActionResult> GetBusinessTimes(Guid Id)
		{
			string Token = HttpContext.Request?.Headers["Token"];
			List<BusinessTime> businessTimes = new List<BusinessTime>();

			if (await _UnitOfWork.UserRepo.CheckBusinessUserValidity(Id,Token) == false)
			{
				return NotFound("کاربر غیرمجاز");
			}
			try
			{
				var Times = await _UnitOfWork.BusinessHomePageRepo.GetBusinessLocationHours(Id);
				foreach (var item in Times.Item4)
				{
					item.DayName = GetDayName.GetName(item.Day);
					businessTimes.Add(new BusinessTime() { DayName = item.DayName, Day = item.Day, FromTime = item.FromTime, ToTime = item.ToTime });
				}
				return Ok(businessTimes.OrderBy(s=>s.Day)); 
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message );
			}
		}
		[Route("Update")]
		[HttpPost]
		public async Task<IActionResult> UpdateBusinessTime(BusinessUpdateTimeDto dto )
		{
			string Token = HttpContext.Request?.Headers["Token"];
			List<DomainClass.Businesses.BusinessTime> businessTimes = new List<DomainClass.Businesses.BusinessTime>();
			if (await _UnitOfWork.UserRepo.CheckBusinessUserValidity(dto.BusinessId, Token) == false)
			{
				return NotFound("کاربر غیرمجاز");
			}
			try
			{
				foreach (var item in dto.BusinessTimes)
				{
					if(!(item.FromTime.Value.Hours >24 || item.FromTime.Value.Hours < 1 || item.FromTime.Value.Minutes >60 || item.FromTime.Value.Minutes < 1 ||
						item.ToTime.Value.Hours > 24 || item.ToTime.Value.Hours < 1 || item.ToTime.Value.Minutes > 60 || item.ToTime.Value.Minutes < 1))
					{

					businessTimes.Add(new DomainClass.Businesses.BusinessTime() { Day = item.Day, FromTime = item.FromTime.Value, ToTime = item.ToTime });

					}

				}
				await _UnitOfWork.BusinessRepo.UpdateBusinessTime(businessTimes, dto.BusinessId);
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
