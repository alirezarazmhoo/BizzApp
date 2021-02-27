using BizApp.Areas.Admin.Models;
using BizApp.Models;
using BizApp.Models.Basic;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BizApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWorkRepo _UnitOfWork;

		public HomeController(ILogger<HomeController> logger , IUnitOfWorkRepo unitOfWork)
		{
			_logger = logger;
			_UnitOfWork = unitOfWork; 
		}
		public async Task<IActionResult>  Index()
		{
			#region Objects
			MainPageViewModel MainPageViewModel = new MainPageViewModel();
			MainPage_SliderViewModel MainPageViewModel_Slider = new MainPage_SliderViewModel();
			//MainPage_Category = new MainPage_Category();
			List<MainPage_Category> MainPage_Categories = new List<MainPage_Category>();
			#endregion
			#region Slider
			var SliderItem = await _UnitOfWork.SliderRepo.GetRandom();
			MainPageViewModel_Slider.Image = SliderItem.Image;
			MainPageViewModel_Slider.Title = SliderItem.Title;
			#endregion
			#region Category
			var CategoryItems = await _UnitOfWork.CategoryRepo.GetChosens();

			foreach (var item in CategoryItems)
			{
				MainPage_Categories.Add(new MainPage_Category() { Id = item.Id, Name = item.Name, CategoryChilds = await GetCategoyChilds(item.Id), Image = await GetCategoryTerm(item.Id), PngIcon = string.Empty, MoreCategories = await GetMoreCategoies() }); ; 
			}
			MainPageViewModel_Slider.MainPage_Category = MainPage_Categories;
			MainPageViewModel.MainPage_SliderViewModel = MainPageViewModel_Slider;
			#endregion			
			return View(MainPageViewModel);
		}
		private async Task<List<Tuple<string, string, int>>> GetMoreCategoies()
		{
			var Items = await _UnitOfWork.CategoryRepo.GetUnChosens();
			List<Tuple<string, string, int>> tuples = new List<Tuple<string, string, int>>();
			foreach (var item in Items)
			{
				tuples.Add(new Tuple<string, string, int>(item.Name, "PngIcon", item.Id));
			}
			return tuples; 
		}
		private async Task<string> GetCategoryTerm(int id)
		{
			var TermItem = await _UnitOfWork.CategoryRepo.GetCategoryTerm(id);
			if(TermItem != null)
			{
				return TermItem.Value; 
			}
			return string.Empty; 
		}
		private async Task<Dictionary<int, string>> GetCategoyChilds(int id)
		{
			var Items = await _UnitOfWork.CategoryRepo.GetChilds(id);
			Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
			foreach (var item in Items)
			{
				keyValuePairs.Add(item.Id, item.Name);
			}
			return keyValuePairs; 
		}
		public async Task<JsonResult> SearchCategory(string txtSearch)
		{
			var Items = await _UnitOfWork.CategoryRepo.GetAll(txtSearch);
			List<CategorySearchViewModel> categories = new List<CategorySearchViewModel>();
			foreach (var item in Items)
			{
				categories.Add(new CategorySearchViewModel() {  name = item.Name , categoryId  = item.Id });
			}
			return Json(new { success = true, categories = categories }) ;
		}
		private class CategorySearchViewModel
		{
			public string name { get; set; }
			public int categoryId { get; set; }
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
