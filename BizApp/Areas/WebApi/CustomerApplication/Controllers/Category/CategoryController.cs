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

		public CategoryController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;

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
		[Route("GetPopular")]
		public async Task<IEnumerable<CategoryDto>> GetPopular(double latitude , double longitude)
		{
			List<CategoryDto> categoryDto = new List<CategoryDto>();
			if (latitude == 0 || longitude == 0)
			{
				var items = await _UnitOfWork.CategoryRepo.GetUnChosens();
				foreach (var item in items)
				{
					string Image = string.IsNullOrEmpty(item.Terms.Where(s => s.Key.Equals("png-icon")).Select(s => s.Value).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/Category/categorydefault.jpg" : item.Terms.Where(s => s.Key.Equals("png-icon")).Select(s => s.Value).FirstOrDefault();
					categoryDto.Add(new CategoryDto() { Id = item.Id,  Image = Image, Name = item.Name });
				}
				return categoryDto;
			}
			else
			{
			var Items =   await	_UnitOfWork.CategoryRepo.GetPopular( longitude , latitude );

				if (Items.Count > 0)
				{
					foreach (var item in Items)
					{
						string Image = string.IsNullOrEmpty(item.Terms.Where(s => s.Key.Equals("png-icon")).Select(s => s.Value).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/Category/categorydefault.jpg" : item.Terms.Where(s => s.Key.Equals("png-icon")).Select(s => s.Value).FirstOrDefault();
						categoryDto.Add(new CategoryDto() { Id = item.Id, Image = Image, Name = item.Name });
					}
					return categoryDto;
				}
				else
				{
					var items = await _UnitOfWork.CategoryRepo.GetUnChosens();
					foreach (var item in items)
					{
						string Image = string.IsNullOrEmpty(item.Terms.Where(s => s.Key.Equals("png-icon")).Select(s => s.Value).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/Category/categorydefault.jpg" : item.Terms.Where(s => s.Key.Equals("png-icon")).Select(s => s.Value).FirstOrDefault();
						categoryDto.Add(new CategoryDto() { Id = item.Id, Image = Image, Name = item.Name });
					}
					return categoryDto;
				}

			}

		}
		[Route("Search")]
		public async Task<IActionResult> SearchCategoryByName(string txtSearch , int DistrictId)
		{
			List<CategoryDto> categoryDtos = new List<CategoryDto>();
			try
			{
			var Items = await _UnitOfWork.CategoryRepo.GetAll(txtSearch, DistrictId);
			foreach (var item in Items)
			{
				categoryDtos.Add(new CategoryDto() { Icon = string.Empty, Id = item.Id, Name = item.Name });
			}
				return Ok(categoryDtos);
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[Route("GetSubCategories")]
		public async Task<IActionResult> GetSubCategoriesByParentId(int Id)
		{
			List<CategoryDto> categoryDtos = new List<CategoryDto>();
			try
			{
				var Items = await _UnitOfWork.CategoryRepo.GetSubCategories(Id);
				foreach (var item in Items)
				{
					string Image = string.IsNullOrEmpty(item.Terms.Where(s => s.Key.Equals("png-icon")).Select(s => s.Value).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/Category/categorydefault.jpg" : item.Terms.Where(s => s.Key.Equals("png-icon")).Select(s => s.Value).FirstOrDefault();
					categoryDtos.Add(new CategoryDto() {   Icon = string.IsNullOrEmpty(item.Terms.Where(s => s.Key.Equals("icon")).Select(s => s.Value).FirstOrDefault()) == true ? Defaults.DefultCategoryIcon : item.Terms.Where(s => s.Key.Equals("icon")).Select(s => s.Value).FirstOrDefault(), Id = item.Id, Name = item.Name });
				}
				return Ok(categoryDtos);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[Route("GetAllParents")]
		public async Task<IActionResult> GetAllParents()
		{
			List<CategoryDto> categoryDtos = new List<CategoryDto>();
			try
			{
				var Items = await _UnitOfWork.CategoryRepo.GetAll();
				foreach (var item in Items)
				{
					string Image = string.IsNullOrEmpty(item.Terms.Where(s => s.Key.Equals("png-icon")).Select(s => s.Value).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/Category/categorydefault.jpg" : item.Terms.Where(s => s.Key.Equals("png-icon")).Select(s => s.Value).FirstOrDefault();
					categoryDtos.Add(new CategoryDto() { Icon = string.IsNullOrEmpty(item.Terms.Where(s => s.Key.Equals("icon")).Select(s => s.Value).FirstOrDefault()) == true ? Defaults.DefultCategoryIcon : item.Terms.Where(s => s.Key.Equals("icon")).Select(s => s.Value).FirstOrDefault(), Id = item.Id, Name = item.Name });
				}
				return Ok(categoryDtos);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
