using AutoMapper;
using BizApp.Models.Basic;
using BizApp.Utility;
using BizAppQrCode;
using BizzAppInfrastructure.Model;
using DataLayer.Infrastructure;
using DomainClass.Businesses;
using DomainClass.Review;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BizApp.Controllers
{
	public class BusinessHomeController : Controller
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public BusinessHomeController(IUnitOfWorkRepo unitOfWork, IMapper mapper , IHttpContextAccessor httpContextAccessor)
		{
			_UnitOfWork = unitOfWork;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;

		}
		public async  Task<IActionResult> Index(Guid Id)
		{
			var BusinessId = Id;
			ViewBag.BusinessId = Id;
			#region Objects
			BusinessHomePageViewModel businessHomePageViewModel = new BusinessHomePageViewModel();
			BusinessHomePage_SliderViewModel businessHomePage_SliderViewModel = new BusinessHomePage_SliderViewModel();
			BusinessHomePage_SummaryViewModel businessHomePage_SummaryViewModel = new BusinessHomePage_SummaryViewModel();
			BusinessHomePage_FeatureViewModel businessHomePage_FeatureViewModel = new BusinessHomePage_FeatureViewModel();
			List<BusinessHomePage_NearSponseredViewModel> businessHomePage_NearSponseredViewModel = new List<BusinessHomePage_NearSponseredViewModel>();
			BusinessHomePage_DescriptionViewModel businessHomePage_DescriptionViewModel = new BusinessHomePage_DescriptionViewModel();
			BusinessHomePage_RightPageBusinessInfoViewModel businessHomePage_RightPageBusinessInfoViewModel = new BusinessHomePage_RightPageBusinessInfoViewModel();
			List<BusinessHomePage_ReviewsViewModel> businessHomePage_ReviewsViewModel = new List<BusinessHomePage_ReviewsViewModel>();
			List<BusinessHomePage_FaqViewModel> businessHomePage_FaqViewModels = new List<BusinessHomePage_FaqViewModel>();
			List<BusinessHomePage_RelatedBusinessViewModel> businessHomePage_RelatedBusinessViewModels = new List<BusinessHomePage_RelatedBusinessViewModel>();
			BusinessHomePage_HoursAndLocationViewModel businessHomePage_HoursAndLocationViewModel = new BusinessHomePage_HoursAndLocationViewModel();
			List<LocationHours> locationHours = new List<LocationHours>();
			List<BusinessFeatureItem> businessFeatureItems = new List<BusinessFeatureItem>();
			ClassGenerator classGenerator = new ClassGenerator();

			#endregion
			#region Resource
			var SliderItem = await _UnitOfWork.BusinessHomePageRepo.GetSlider(BusinessId);
			var SummaryItem = await _UnitOfWork.BusinessHomePageRepo.GetBusinessSummary(BusinessId);
			var FeaturesItem = await _UnitOfWork.BusinessHomePageRepo.GetBusinessFeatures(BusinessId);
			var SponseredBusinessItem = await _UnitOfWork.BusinessHomePageRepo.GetNearByBusinessSponsored(BusinessId);
			var BusinessOtherInfoItem = await _UnitOfWork.BusinessHomePageRepo.GetBusinessOtherInfo(BusinessId);
			var ReviewsItem = await _UnitOfWork.BusinessHomePageRepo.GetBusinessReview(BusinessId);
			var CommunityItems = await _UnitOfWork.AskTheCommunityRepo.GetBusinessFaq(BusinessId);
			var RelatedBusinessItem = await _UnitOfWork.BusinessHomePageRepo.GetRelatedBusiness(BusinessId);
			var LocationAndHours = await _UnitOfWork.BusinessHomePageRepo.GetBusinessLocationHours(BusinessId);
			var BusinessName = await _UnitOfWork.BusinessRepo.GetBusinessName(BusinessId);
			#endregion
			#region Slider
			businessHomePage_SliderViewModel.Images = SliderItem;
			#endregion
			#region BusinessSummary
			businessHomePage_SummaryViewModel.Title = SummaryItem.Item1;
			businessHomePage_SummaryViewModel.Rate = SummaryItem.Item2;
			businessHomePage_SummaryViewModel.Reviews = SummaryItem.Item3;
			businessHomePage_SummaryViewModel.IsClaimed = SummaryItem.Item4;
			businessHomePage_SummaryViewModel.TotalPhotos = SummaryItem.Item5;
			businessHomePage_SummaryViewModel.Description = SummaryItem.Item6;
			businessHomePage_SummaryViewModel.BusinessId = BusinessId;
			#endregion
			#region Featrues
			businessHomePage_FeatureViewModel.BoldFeature = FeaturesItem.Item1;
			foreach (var item in FeaturesItem.Item2)
			{
				businessFeatureItems.Add(new BusinessFeatureItem() { Icon = item.Feature.Icon, Name = item.Feature.Name }); 
			}
			businessHomePage_FeatureViewModel.Features = businessFeatureItems;
			#endregion
			#region NearSponseredBusiness
			foreach (var item in SponseredBusinessItem)
			{
				businessHomePage_NearSponseredViewModel.Add(new BusinessHomePage_NearSponseredViewModel() { Name = item.Item1, Image = item.Item2, Rate = item.Item3, Review = item.Item4, Id = item.Item5, Descripton = item.Item6  , DistricName = item.Item7});
			}
			#endregion
			#region Description
			businessHomePage_DescriptionViewModel.Descripton = string.IsNullOrEmpty(BusinessOtherInfoItem.Item1) == true ? "فاقد توضیحات" : BusinessOtherInfoItem.Item1;
			businessHomePage_DescriptionViewModel.BusinessName = BusinessName;
			#endregion
			#region RightPageBusinessInfo
			businessHomePage_RightPageBusinessInfoViewModel.WebSiteUrl = BusinessOtherInfoItem.Item2;
			businessHomePage_RightPageBusinessInfoViewModel.PhoneNumber = BusinessOtherInfoItem.Item3;
			businessHomePage_RightPageBusinessInfoViewModel.Address = BusinessOtherInfoItem.Item4;
			#endregion
			#region Reviews
			foreach (var item in ReviewsItem)
			{
				string UserPicture = string.IsNullOrEmpty(item.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s=>s.UploadedPhoto).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : item.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault();
				businessHomePage_ReviewsViewModel.Add(new BusinessHomePage_ReviewsViewModel() { ReviewId = item.Id ,Cool = item.CoolCount , Date = DateChanger.ToPersianDateString(item.Date) , DistricName = item.BizAppUser.Address , Funny = item.FunnyCount , Rate = item.Rate , Text = item.Description , UseFull = item.UsefulCount , TotalReviews = item.BizAppUser.Reviews.Count , TotalPictures = await _UnitOfWork.BusinessHomePageRepo.GetTotalUserMedia(item.BizAppUserId) , UserName = item.BizAppUser.FullName , Id = item.BizAppUser.Id , UserPicture = UserPicture  , FullName = item.BizAppUser.FullName , ReviewTotalPictures = item.ReviewMedias.Count, ReviewPictures = item.ReviewMedias.Select(s=>s.Image).ToList() });
			}
			#endregion
			#region AsktheCommunity
			foreach (var item in CommunityItems.OrderByDescending(s=>s.Date).TakeLast(5))
			{
				string Date = item.Date == DateTime.MinValue ? "نامشخص" : item.Date.ToPersianDateString();

				businessHomePage_FaqViewModels.Add(new BusinessHomePage_FaqViewModel() { Question = item.Question, Answers = item.BusinessFaqAnswers.Select(s => s.Text).ToList(), Date = Date, AnswersCount = item.BusinessFaqAnswers.Where(s=>s.StatusEnum== DomainClass.Enums.StatusEnum.Accepted).Count()  ,Id = item.Id}) ;
			}
			
			#endregion
			#region RelatedBusiness
			foreach (var item in RelatedBusinessItem)
			{
				businessHomePage_RelatedBusinessViewModels.Add(new BusinessHomePage_RelatedBusinessViewModel() { Image = string.IsNullOrEmpty(item.FeatureImage) == false ? "/Upload/DefaultPicutres/Bussiness/Business.jpg" : item.FeatureImage , Description = item.Description }); 
			}
			#endregion
			#region LocationAndHours
			businessHomePage_HoursAndLocationViewModel.Address = LocationAndHours.Item1;
			businessHomePage_HoursAndLocationViewModel.Lon = LocationAndHours.Item2;
			businessHomePage_HoursAndLocationViewModel.Lat = LocationAndHours.Item3;
			foreach (var item in LocationAndHours.Item4)
			{
				item.DayName = GetDayName.GetName(item.Day);
				locationHours.Add(item);
			}
			businessHomePage_HoursAndLocationViewModel.LocationHours = locationHours;
			#endregion
			#region QrCode
			businessHomePage_RightPageBusinessInfoViewModel.QrCode =  classGenerator.Generate($"http://45.159.113.39/BusinessHome/Index?Id={Id}");

			#endregion
			#region FinalResults
			businessHomePageViewModel.businessHomePage_SliderViewModel = businessHomePage_SliderViewModel;
			businessHomePageViewModel.businessHomePage_SummaryViewModel = businessHomePage_SummaryViewModel;
			businessHomePageViewModel.businessHomePage_FeatureViewModel = businessHomePage_FeatureViewModel;
			businessHomePageViewModel.businessHomePage_NearSponseredViewModel = businessHomePage_NearSponseredViewModel;
			businessHomePageViewModel.businessHomePage_DescriptionViewModel = businessHomePage_DescriptionViewModel;
			businessHomePageViewModel.businessHomePage_RightPageBusinessInfoViewModel = businessHomePage_RightPageBusinessInfoViewModel;
			businessHomePageViewModel.businessHomePage_ReviewsViewModel = businessHomePage_ReviewsViewModel;
			businessHomePageViewModel.businessHomePage_FaqViewModels = businessHomePage_FaqViewModels;
			businessHomePageViewModel.businessHomePage_RelatedBusinessViewModels = businessHomePage_RelatedBusinessViewModels;
			businessHomePageViewModel.businessHomePage_HoursAndLocationViewModel = businessHomePage_HoursAndLocationViewModel;
			businessHomePageViewModel.BusinessId = BusinessId;
			businessHomePageViewModel.BusinessName = BusinessName;
			if (User.Identity.IsAuthenticated)
			{
				businessHomePageViewModel.FavoritConditation =await _UnitOfWork.BusinessRepo.CheckBisinessFavorit(BusinessId , GetUserId()) ;
			}
			else
			{
				businessHomePageViewModel.FavoritConditation = false; 
			}

			#endregion
			return View(businessHomePageViewModel);
		}
		[HttpPost]
		public async Task<JsonResult> SendMessageToBusiness(MessageToBusiness model )
		{
			try
			{
				if (User.Identity.IsAuthenticated)
				{
					model.BizAppUserId = GetUserId();
				}
				await _UnitOfWork.BusinessHomePageRepo.MessageToBusiness(model);
				await _UnitOfWork.SaveAsync();
				return Json(new { success = true });
			}
			catch (Exception )
			{
				return Json(new { success = false });
			}
		}
		[HttpPost]
		public async Task<IActionResult> AddFaqsAnswers(Review model, IFormFile[] files , string[] caption)
		{
			try
			{
				await _UnitOfWork.ReviewRepo.AddReview(model, files , caption);
				await _UnitOfWork.SaveAsync();
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				return RedirectToAction(nameof(Index));
			}
		}
		[HttpPost]
		public async Task<IActionResult> AddMediaForBusiness(CustomerBusinessMedia model, IFormFile[] files)
		{
			try
			{
				await _UnitOfWork.ReviewRepo.AddBusinessMedia(model, files);
				await _UnitOfWork.SaveAsync();
				return Content("");
			}
			catch (Exception)
			{
				return Content("");

			}
		}
		private string GetUserId()
		{
			return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
		}


	}
}
