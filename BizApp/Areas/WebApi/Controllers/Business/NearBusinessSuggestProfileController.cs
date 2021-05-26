using BizApp.Areas.WebApi.Models;
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
	public class NearBusinessSuggestProfileController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public NearBusinessSuggestProfileController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}
		public async Task<IActionResult> Get()
		{
			List<BusinessShortModel> businessShortModels = new List<BusinessShortModel>();  
			string Token = HttpContext.Request?.Headers["Token"];
			if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
			{
				return NotFound();
			}
			try
			{
				var Items = await _UnitOfWork.NearBusinessSuggestProfileRepo.Get(await _UnitOfWork.UserRepo.UserTokenMaper(Token));
				foreach (var item in Items)
				{
					businessShortModels.Add(new BusinessShortModel() { id = item.Id, name = item.Name, districtname = item.District.Name , featureimage = item.FeatureImage}) ;
				}
				return Ok(businessShortModels);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}


		}



	}
}
