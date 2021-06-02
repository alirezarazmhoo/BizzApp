using AutoMapper;
using BizApp.Areas.WebApi.Models;

using DataLayer.Infrastructure;
using DomainClass;
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
	public class SliderController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		private readonly IMapper _mapper;
		public SliderController(IUnitOfWorkRepo unitOfWork, IMapper mapper)
		{
			_UnitOfWork = unitOfWork;
			_mapper = mapper;
		}
		[Route("GetRandom")]
		public async Task<Slider> Get()
		{
			try
			{
				return await _UnitOfWork.SliderRepo.GetRandom(); 
			
			}
			catch (Exception )
			{
				throw; 
			}
		}
	}
}
