using AutoMapper;
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
	public class BusinessController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly IUnitOfWorkRepo _UnitOfWork;
		private readonly IMapper _mapper;

		public BusinessController(ApplicationDbContext context , IUnitOfWorkRepo unitOfWork, IMapper mapper)
		{
			_context = context;
			_UnitOfWork = unitOfWork;
			_mapper = mapper;

		}
		[Route("GetOnMap")]
		public async Task<IEnumerable<BusinessOnMap>> GetOnMap(int categoryId , double latitude, double longitude)
		{
			List<BusinessOnMap> categoryDto = new List<BusinessOnMap>();
			try
			{			
				foreach (var item in await _UnitOfWork.BusinessRepo.GetBusinessOnMap(categoryId, longitude, latitude))
				{
					categoryDto.Add(new BusinessOnMap() { id = item.Id, latitude = item.Latitude, longitude = item.Longitude });  
				}
				return categoryDto;
			}
			catch (Exception)
			{
				throw;
			}
		}
		[Route("GetById")]
		public async Task<BusinessPopop> GetById(Guid id)
		{
			try
			{
			BusinessPopop businessPopop = new BusinessPopop();
			var Item = await _UnitOfWork.BusinessRepo.GetById(id);
			if(Item != null)
			{
					var item = _mapper.Map<BusinessPopop>(Item);
					if (string.IsNullOrEmpty(item.image))
					{
						item.image = "/Upload/DefaultPicutres/Bussiness/Business.jpg";
					}
					return item;
			}
				else
				{
					return null; 
				}
			}
		   catch (Exception)
			{
				throw;
			}
		}
	}
}
