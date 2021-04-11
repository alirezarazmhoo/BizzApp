using AutoMapper;
using BizApp.Areas.WebApi.Models;

using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		private readonly IMapper _mapper;
		public CategoryController(IUnitOfWorkRepo unitOfWork, IMapper mapper)
		{
			_UnitOfWork = unitOfWork;
			_mapper = mapper;
		}
		[Route("GetChosen")]
		public async Task<IEnumerable<CategoryDto>> GetChosen()
		{
			List<CategoryDto> categoryDto = new List<CategoryDto>();
			try
			{
				var items = await _UnitOfWork.CategoryRepo.GetChosens();
				foreach (var item in items)
				{
					categoryDto.Add(new CategoryDto() { Id = item.Id, Icon = string.IsNullOrEmpty( item.Terms.Where(s => s.Key.Equals("icon")).Select(s => s.Value).FirstOrDefault()) == true? Defaults.DefultCategoryIcon : item.Terms.Where(s => s.Key.Equals("icon")).Select(s => s.Value).FirstOrDefault(), Name = item.Name });
				}
				return categoryDto;
			}
			catch (Exception )
			{
				throw;
			}
		}
		[Route("GetMore")]
		public async Task<IEnumerable<CategoryDto>> More()
		{
			List<CategoryDto> categoryDto = new List<CategoryDto>();
			try
			{
				var items = await _UnitOfWork.CategoryRepo.GetUnChosens();
				foreach (var item in items)
				{
					categoryDto.Add(new CategoryDto() { Id = item.Id, Icon = string.IsNullOrEmpty(item.Terms.Where(s => s.Key.Equals("icon")).Select(s => s.Value).FirstOrDefault()) == true ? Defaults.DefultCategoryIcon : item.Terms.Where(s => s.Key.Equals("icon")).Select(s => s.Value).FirstOrDefault(), Name = item.Name });
				}
				return categoryDto;
			}
			catch (Exception)
			{
			throw;
			}
		}
	}
}
