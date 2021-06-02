using BizApp.Areas.WebApi.Models;
using DataLayer.Infrastructure;
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
	public class BusinessFeatureController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public BusinessFeatureController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}
		[Route("GetAll")]
		public async Task<IActionResult> GetAllFeatures()
		{
			List<BusinessFeature> businessFeatures = new List<BusinessFeature>();
			try
			{
				var Items = await _UnitOfWork.FeatureRepo.GetAll();
				foreach (var item in Items.Where(s=>s.ValueType == DomainClass.Enums.BusinessFeatureType.Boolean))
				{
					businessFeatures.Add(new BusinessFeature() { id = item.Id , Title = item.Name });
				}
				return Ok(businessFeatures); 
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[Route("GetByCategoryId")]
		public async Task<IActionResult> GetFeaturesBasedByCategoryId(int Id)
		{
			List<BusinessFeature> businessFeatures = new List<BusinessFeature>();
			try
			{
				var Items = await _UnitOfWork.FeatureRepo.ExtractFeaturesByCategoryId(Id);
				foreach (var item in Items)
				{
					businessFeatures.Add(new BusinessFeature() { id = item.Id, Title = item.Name });
				}
				return Ok(businessFeatures);
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
