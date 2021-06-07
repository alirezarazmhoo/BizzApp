using BizApp.Areas.WebApi.BusinessApplication.Models;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.BusinessApplication.Controllers.Business.BusinessFeatures
{
	[Route("api/[controller]")]
	[ApiController]
	public class BusinessFeaturesController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public BusinessFeaturesController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}
		[Route("Get")]
		public async Task<IActionResult> GetBusinessFeatrues(Guid Id)
		{
		     List<BusinessApplicationFeatures> businessApplicationFeatures = new List<BusinessApplicationFeatures> ();
			string Token = HttpContext.Request?.Headers["Token"];
			if (await _UnitOfWork.UserRepo.CheckBusinessUserValidity( Id,Token) == false)
			{
				return NotFound("کاربر غیرمجاز");
			}
			try
			{
				var Item = await _UnitOfWork.BusinessRepo.GetBusinessFature(Id);
				foreach (var item in Item)
				{
					businessApplicationFeatures.Add(new BusinessApplicationFeatures() { Id = item.FeatureId, Name = item.FeatureName, Icon = item.Icon ,IsInFeatrue = item.IsInFeature });
				}
				return Ok(businessApplicationFeatures);
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}
		[Route("Update")]
		[HttpPost]
		public async Task<IActionResult> UpdateFeatures(Guid Id , string FeaturesId)
		{
			string Token = HttpContext.Request?.Headers["Token"];
			if (await _UnitOfWork.UserRepo.CheckBusinessUserValidity(Id, Token) == false)
			{
				return NotFound("کاربر غیرمجاز");
			}
			try
			{
				await _UnitOfWork.BusinessRepo.UpdateFrequenstlyFeature(Id , FeaturesId);
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
