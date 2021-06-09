using AutoMapper;
using BizApp.Areas.Admin.Models;
using BizApp.Models;
using BizApp.Models.Basic;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BizApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWorkRepo _UnitOfWork;
		private readonly IMapper _mapper;
		private readonly UserManager<BizAppUser> _userManager;



		public HomeController(ILogger<HomeController> logger, IUnitOfWorkRepo unitOfWork, IMapper mapper , UserManager<BizAppUser> userManager)
		{
			_logger = logger;
			_UnitOfWork = unitOfWork;
			_mapper = mapper;
			_userManager = userManager; 

		}
		public async Task<IActionResult> Index()
		{


			#region Objects
			MainPageViewModel MainPageViewModel = new MainPageViewModel();
			MainPage_SliderViewModel MainPageViewModel_Slider = new MainPage_SliderViewModel();
			List<MainPage_Category> MainPage_Categories = new List<MainPage_Category>();
			List<MainPage_BusinessesByCategory> MainPage_BrowseBusinessesByCategory = new List<MainPage_BusinessesByCategory>();
			List<Tuple<string, string, int>> MoreCategoriesTuples = new List<Tuple<string, string, int>>();
			MainPage_BusinessesByCategoryMain MainPage_BusinessesByCategoryMain = new MainPage_BusinessesByCategoryMain();
			MainPage_BusinessesByCategoryMoreCategories MainPage_BusinessesByCategoryMoreCategories = new MainPage_BusinessesByCategoryMoreCategories();
			List<MainPage_RecentActivity> MainPage_RecentActivity = new List<MainPage_RecentActivity>();

			#endregion
			#region Slider
			var SliderItem = await _UnitOfWork.SliderRepo.GetRandom();
			MainPageViewModel_Slider.Image = SliderItem.Image;
			MainPageViewModel_Slider.Title = SliderItem.Title;
			if (User.Identity.IsAuthenticated)
			{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			MainPageViewModel_Slider.UserRoles = await _userManager.GetRolesAsync(user);
			}
			#endregion
			#region Category
			var CategoryItems = await _UnitOfWork.CategoryRepo.GetChosens();
			var UnChosenCategoryItems = await _UnitOfWork.CategoryRepo.GetUnChosens();
			foreach (var item in CategoryItems)
			{
				MainPage_Categories.Add(new MainPage_Category() { Id = item.Id, Name = item.Name, CategoryChilds = await GetCategoyChilds(item.Id), Image = await GetCategoryTerm(item.Id), PngIcon = string.Empty, MoreCategories = await GetMoreCategoies() }); ;
			}
			MainPageViewModel_Slider.MainPage_Category = MainPage_Categories;
			#endregion
			#region BrowseBusinessesByCategory
			foreach (var item in CategoryItems)
			{
				MainPage_BrowseBusinessesByCategory.Add(new MainPage_BusinessesByCategory() { Id = item.Id, Name = item.Name, PngIcon = item.Terms.Where(s=>s.CategoryId == item.Id && s.Key.Equals("png-icon")).Select(s=>s.Value).FirstOrDefault() == null ? "/Upload/DefaultPicutres/Category/categorydefault.jpg" : item.Terms.Where(s => s.CategoryId == item.Id && s.Key.Equals("png-icon")).Select(s => s.Value).FirstOrDefault()});
			}
			foreach (var item in UnChosenCategoryItems)
			{
				MoreCategoriesTuples.Add(new Tuple<string, string, int>(item.Name, item.Terms.Where(s => s.CategoryId == item.Id && s.Key.Equals("icon-web")).Select(s => s.Value).FirstOrDefault() == null ? "fa fa-cubes" : item.Terms.Where(s => s.CategoryId == item.Id && s.Key.Equals("icon-web")).Select(s => s.Value).FirstOrDefault(), item.Id));
			}
			MainPage_BusinessesByCategoryMain.MainPage_BusinessesByCategories = MainPage_BrowseBusinessesByCategory;
			MainPage_BusinessesByCategoryMoreCategories.MoreCategories = MoreCategoriesTuples;
			MainPage_BusinessesByCategoryMain.MainPage_BusinessesByCategoryMoreCategories = MainPage_BusinessesByCategoryMoreCategories;
			#endregion
			#region RecentActivity
			foreach (var item in await _UnitOfWork.ReviewRepo.GetRecentActivity(null))
			{
				MainPage_RecentActivityContent MainPage_RecentActivityContent = new MainPage_RecentActivityContent();
				MainPage_RecentActivityCreator MainPage_RecentActivityCreator = new MainPage_RecentActivityCreator();
				MainPage_RecentActivityContent.CoolCount = item.CoolCount;
				MainPage_RecentActivityContent.FunnyCount = item.FunnyCount;
				MainPage_RecentActivityContent.Id = item.Id;
				MainPage_RecentActivityContent.BusinessId = item.BusinessId;
				MainPage_RecentActivityContent.Image = item.Business.FeatureImage;
				MainPage_RecentActivityContent.Name = item.Business.Name;
				MainPage_RecentActivityContent.Rate = item.Rate;
				MainPage_RecentActivityContent.Text = item.Description;
				MainPage_RecentActivityContent.UseFulCount = item.UsefulCount;
				MainPage_RecentActivityCreator.Id = item.BizAppUser.Id;
				MainPage_RecentActivityCreator.Name = item.BizAppUser.FullName;
				MainPage_RecentActivityCreator.Image = item.BizAppUser.ApplicationUserMedias.FirstOrDefault(s => s.IsMainImage) == null ? string.Empty : item.BizAppUser.ApplicationUserMedias.FirstOrDefault(s => s.IsMainImage).UploadedPhoto;
				MainPage_RecentActivity.Add(new MainPage_RecentActivity() { MainPage_RecentActivityContent = MainPage_RecentActivityContent, MainPage_RecentActivityCreator = MainPage_RecentActivityCreator, ActivityType = ActivityType.WriteReview });
			}
			foreach (var item in await _UnitOfWork.ReviewRepo.GetRecentActivityBusinessMedia(null))
			{
				List<MainPage_RecentActivityUserMediaBusiness> MainPage_RecentActivityUserMediaBusinesses = new List<MainPage_RecentActivityUserMediaBusiness>();
				if (item.CustomerBusinessMediaPictures.Count > 0 )
				{
					MainPage_RecentActivityCreator MainPage_RecentActivityCreator = new MainPage_RecentActivityCreator();
					MainPage_RecentActivityContent MainPage_RecentActivityContent = new MainPage_RecentActivityContent();
					MainPage_RecentActivityCreator.Id = item.BizAppUser.Id;
					MainPage_RecentActivityCreator.Name = item.BizAppUser.FullName;
					MainPage_RecentActivityCreator.Image = item.BizAppUser.ApplicationUserMedias.FirstOrDefault(s => s.IsMainImage) == null ? string.Empty : item.BizAppUser.ApplicationUserMedias.FirstOrDefault(s => s.IsMainImage).UploadedPhoto;
					MainPage_RecentActivityContent.Image = item.Business.FeatureImage;
					MainPage_RecentActivityContent.Name = item.Business.Name;
					MainPage_RecentActivityContent.BusinessId = item.BusinessId;
					MainPage_RecentActivityContent.Text = string.IsNullOrEmpty(item.Business.Description) ? "بدون توضیحات" : item.Business.Description;
					MainPage_RecentActivityContent.Rate = item.Business.Rate == 0 ? 1 : item.Business.Rate;
					MainPage_RecentActivityContent.Id = item.CustomerBusinessMediaPictures.FirstOrDefault().CustomerBusinessMediaId;
					foreach (var item2 in item.CustomerBusinessMediaPictures)
					{
						if (item2.StatusEnum == DomainClass.Enums.StatusEnum.Accepted)
						{
							MainPage_RecentActivityUserMediaBusinesses.Add(new MainPage_RecentActivityUserMediaBusiness()
							{
								
								Description = item2.Description,
								Id = item2.Id,
								Image = item2.Image,
								LikeCount = item2.LikeCount,
								UsersName = await _UnitOfWork.ReviewRepo.GetUsersFullName(item2.Id)
							});
						}
					}
					MainPage_RecentActivity.Add(new MainPage_RecentActivity() { MainPage_RecentActivityContent = MainPage_RecentActivityContent, MainPage_RecentActivityCreator = MainPage_RecentActivityCreator, ActivityType = ActivityType.AddPhoto, MainPage_RecentActivityUserMediaBusinesses = MainPage_RecentActivityUserMediaBusinesses });
				}
			}
			#endregion
			#region FinalResults
			MainPageViewModel.MainPage_SliderViewModel = MainPageViewModel_Slider;
			MainPageViewModel.MainPage_BusinessesByCategoryMain = MainPage_BusinessesByCategoryMain;
			MainPageViewModel.MainPage_RecentActivity = MainPage_RecentActivity;
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
			if (TermItem != null)
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
			var Items = await _UnitOfWork.CategoryRepo.GetAll(txtSearch,0);
			List<CategorySearchViewModel> categories = new List<CategorySearchViewModel>();
			foreach (var item in Items.Take(10))
			{
				categories.Add(new CategorySearchViewModel() { name = item.Name, categoryId = item.Id });
			}
			return Json(new { success = true, categories = categories });
		}
		private class CategorySearchViewModel
		{
			public string name { get; set; }
			public int categoryId { get; set; }
		}
		[HttpGet]
		public async Task<JsonResult> GetAllWithParentNames(string txtSearch)
		{
			if (string.IsNullOrEmpty(txtSearch))
				return Json(new { });
			try
			{
				var items = await _UnitOfWork.DistrictRepo.GetAllWithParentNames(txtSearch);
				return Json(new { success = true, districts = items.ToList().Take(10) });
			}
			catch (Exception ex)
			{
				return Json(ex.Message);
			}
		}
		[HttpGet]
		public async Task<JsonResult> GetMoreAcivites(int? page)
		{
			List<MainPage_RecentActivity> MainPage_RecentActivity = new List<MainPage_RecentActivity>();
			bool HasNext = true;
			int CurrentPage = 0;
			foreach (var item in await _UnitOfWork.ReviewRepo.GetRecentActivity(page))
			{
				MainPage_RecentActivityContent MainPage_RecentActivityContent = new MainPage_RecentActivityContent();
				MainPage_RecentActivityCreator MainPage_RecentActivityCreator = new MainPage_RecentActivityCreator();
				MainPage_RecentActivityContent.CoolCount = item.CoolCount;
				MainPage_RecentActivityContent.FunnyCount = item.FunnyCount;
				MainPage_RecentActivityContent.Id = item.Id;
				MainPage_RecentActivityContent.Image = item.Business.FeatureImage;
				MainPage_RecentActivityContent.Name = item.Business.Name;
				MainPage_RecentActivityContent.Rate = item.Rate;
				MainPage_RecentActivityContent.BusinessId = item.BusinessId; 
				MainPage_RecentActivityContent.Text = item.Description;
				MainPage_RecentActivityContent.UseFulCount = item.UsefulCount;
				MainPage_RecentActivityContent.Likes = item.UsersInReviewLikes.Count;
				MainPage_RecentActivityCreator.Id = item.BizAppUser.Id;
				MainPage_RecentActivityCreator.Name = item.BizAppUser.FullName;
				MainPage_RecentActivityCreator.Image = item.BizAppUser.ApplicationUserMedias.FirstOrDefault(s => s.IsMainImage) == null ? string.Empty : item.BizAppUser.ApplicationUserMedias.FirstOrDefault(s => s.IsMainImage).UploadedPhoto;
				MainPage_RecentActivity.Add(new MainPage_RecentActivity() { MainPage_RecentActivityContent = MainPage_RecentActivityContent, MainPage_RecentActivityCreator = MainPage_RecentActivityCreator, ActivityType = ActivityType.WriteReview });
			}
			foreach (var item in await _UnitOfWork.ReviewRepo.GetRecentActivityBusinessMedia(page))
			{
				List<MainPage_RecentActivityUserMediaBusiness> MainPage_RecentActivityUserMediaBusinesses = new List<MainPage_RecentActivityUserMediaBusiness>();
				MainPage_RecentActivityContent MainPage_RecentActivityContent = new MainPage_RecentActivityContent();

				if (item.CustomerBusinessMediaPictures.Count > 0)
				{
					MainPage_RecentActivityCreator MainPage_RecentActivityCreator = new MainPage_RecentActivityCreator();
					MainPage_RecentActivityCreator.Id = item.BizAppUser.Id;
					MainPage_RecentActivityCreator.Name = item.BizAppUser.FullName;
					MainPage_RecentActivityCreator.Image = item.BizAppUser.ApplicationUserMedias.FirstOrDefault(s => s.IsMainImage) == null ? string.Empty : item.BizAppUser.ApplicationUserMedias.FirstOrDefault(s => s.IsMainImage).UploadedPhoto;
					MainPage_RecentActivityContent.Id = item.Id;
					MainPage_RecentActivityContent.Name = item.Business.Name;
					MainPage_RecentActivityContent.Text = string.IsNullOrEmpty(item.Business.Description) ? "بدون توضیحات" : item.Business.Description;
					MainPage_RecentActivityContent.Rate = item.Business.Rate == 0 ? 1 : item.Business.Rate;
					MainPage_RecentActivityContent.Image = item.Business.FeatureImage;
					MainPage_RecentActivityContent.Id = item.CustomerBusinessMediaPictures.FirstOrDefault().CustomerBusinessMediaId;
					foreach (var item2 in item.CustomerBusinessMediaPictures)
					{
						if (item2.StatusEnum == DomainClass.Enums.StatusEnum.Accepted)
						{
							MainPage_RecentActivityUserMediaBusinesses.Add(new MainPage_RecentActivityUserMediaBusiness()
							{
								Description = item2.Description,
								Id = item2.Id,
								Image = item2.Image,
								LikeCount = item2.LikeCount,
								UsersName = Regex.Replace(await _UnitOfWork.ReviewRepo.GetUsersFullName(item2.Id), "<.*?>", String.Empty)
							});
						}
					}
					MainPage_RecentActivity.Add(new MainPage_RecentActivity() { MainPage_RecentActivityContent = MainPage_RecentActivityContent, MainPage_RecentActivityCreator = MainPage_RecentActivityCreator, ActivityType = ActivityType.AddPhoto, MainPage_RecentActivityUserMediaBusinesses = MainPage_RecentActivityUserMediaBusinesses });
				}
			}
			if (HasNext)
			{
				CurrentPage = (page.HasValue ? page.Value : 1) + 1;
			}
			else
			{
				CurrentPage = page.Value;
			}
			return Json(new
			{
				success = true,
				items = MainPage_RecentActivity,
				currentpage = CurrentPage,
				hasnext = HasNext = MainPage_RecentActivity.Count > 0 ? true : false
			});
		}
		[HttpGet]
		public async Task<JsonResult> GetActivityById(Guid id)
		{
			var item = _mapper.Map<BusinessMediaViewModel>(await _UnitOfWork.ReviewRepo.GetCustomerBusinessMediaById(id));
			if (item != null)
			{
				item.Description = string.IsNullOrEmpty(item.Description) ? "بدون توضیحات" : item.Description;
				item.PersianDate = DateChanger.ToPersianDateString(item.Date);
				item.TotalReview = await _UnitOfWork.BusinessReviewCountRepo.Count(item.BusinessId);
				item.UserProfilePicture = item.UserProfilePicture == null ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : item.UserProfilePicture;
				return Json(new
				{
					success = true,
					item = item,
				});
			}
			else
			{
				return Json(new
				{
					success = false,
					item = "",
				});
			}
		}

		[HttpGet]
		public  JsonResult FillCitySession(int Id)
		{
			HttpContext.Session.SetInt32("districId", Id);
			return Json(new
			{
				success = true,
			});
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
