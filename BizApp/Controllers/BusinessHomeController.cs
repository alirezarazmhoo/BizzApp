using AutoMapper;
using BizApp.Models.Basic;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass.Businesses;
using DomainClass.Review;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Controllers
{
	public class BusinessHomeController : Controller
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		private readonly IMapper _mapper;
		public BusinessHomeController(IUnitOfWorkRepo unitOfWork, IMapper mapper)
		{
			_UnitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async  Task<IActionResult> Index()
		{
			var BusinessId = new Guid("4e9b06be-2a73-4c40-fea1-08d8e04ff1b3");
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
			#endregion
			#region Resource
			var SliderItem = await _UnitOfWork.BusinessHomePageRepo.GetSlider(BusinessId);
			var SummaryItem = await _UnitOfWork.BusinessHomePageRepo.GetBusinessSummary(BusinessId);
			var FeaturesItem = await _UnitOfWork.BusinessHomePageRepo.GetBusinessFeatures(BusinessId);
			var SponseredBusinessItem = await _UnitOfWork.BusinessHomePageRepo.GetNearByBusinessSponsored(BusinessId);
			var BusinessOtherInfoItem = await _UnitOfWork.BusinessHomePageRepo.GetBusinessOtherInfo(BusinessId);
			var ReviewsItem = await _UnitOfWork.BusinessHomePageRepo.GetBusinessReview(BusinessId);
			var CommunityItems = await _UnitOfWork.BusinessHomePageRepo.GetBusinessFaq(BusinessId);
			var RelatedBusinessItem = await _UnitOfWork.BusinessHomePageRepo.GetRelatedBusiness(BusinessId);
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
			#endregion
			#region Featrues
			businessHomePage_FeatureViewModel.BoldFeature = FeaturesItem.Item1;
			businessHomePage_FeatureViewModel.Features = FeaturesItem.Item2;
			#endregion
			#region NearSponseredBusiness
			foreach (var item in SponseredBusinessItem)
			{
				businessHomePage_NearSponseredViewModel.Add(new BusinessHomePage_NearSponseredViewModel() { Name = item.Item1, Image = item.Item2, Rate = item.Item3, Review = item.Item4, Id = item.Item5, Descripton = item.Item6 });
			}
			#endregion
			#region Description
			businessHomePage_DescriptionViewModel.Descripton = string.IsNullOrEmpty(BusinessOtherInfoItem.Item1) == true ? "فاقد توضیحات" : BusinessOtherInfoItem.Item1;
			#endregion
			#region RightPageBusinessInfo
			businessHomePage_RightPageBusinessInfoViewModel.WebSiteUrl = BusinessOtherInfoItem.Item2;
			businessHomePage_RightPageBusinessInfoViewModel.PhoneNumber = BusinessOtherInfoItem.Item3;
			businessHomePage_RightPageBusinessInfoViewModel.Address = BusinessOtherInfoItem.Item4;
			#endregion
			#region Reviews
			foreach (var item in ReviewsItem)
			{
				businessHomePage_ReviewsViewModel.Add(new BusinessHomePage_ReviewsViewModel() { Cool = item.CoolCount , Date = DateChanger.ToPersianDateString(item.Date) , DistricName = item.BizAppUser.Address , Funny = item.FunnyCount , Rate = item.Rate , Text = item.Description , UseFull = item.UsefulCount , TotalReviews = item.BizAppUser.Reviews.Count , TotalPictures = 0 , UserName = item.BizAppUser.FullName , Id = item.BizAppUser.Id});
			}
			#endregion
			#region AsktheCommunity
			foreach (var item in CommunityItems)
			{
				businessHomePage_FaqViewModels.Add(new BusinessHomePage_FaqViewModel() { Question = item.Question, Answers = item.BusinessFaqAnswers.Select(s => s.Text).ToList()}) ;
			}
			#endregion
			#region RelatedBusiness
			foreach (var item in RelatedBusinessItem)
			{
				businessHomePage_RelatedBusinessViewModels.Add(new BusinessHomePage_RelatedBusinessViewModel() { Image = string.IsNullOrEmpty(item.FeatureImage) == false ? "/Upload/DefaultPicutres/Bussiness/Business.jpg" : item.FeatureImage }); 
			}
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
			#endregion
			return View();
		}
		[HttpPost]
		public async Task<JsonResult> SendMessageToBusiness(MessageToBusiness model )
		{
			try
			{
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
		public async Task<IActionResult> AddFaqsAnswers(Review model, IFormFile[] files)
		{
			try
			{
				await _UnitOfWork.ReviewRepo.AddReview(model, files);
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
		public async Task<IActionResult> GetGalleryForBusiness(Guid Id)
		{
			#region Objects
			BusinessGalleryViewModel businessGalleryViewModel = new BusinessGalleryViewModel();
			Dictionary<Guid, string> Gallery = new Dictionary<Guid, string>();
			#endregion
			#region Resource
			var BusinessInfo = await _UnitOfWork.BusinessRepo.GetById(Id);
			var TotalReviews = await _UnitOfWork.ReviewRepo.BusinessReviewCount(Id);
			var GalleryItems = await _UnitOfWork.BusinessHomePageRepo.GetBusinessGallery(Id);
			#endregion
			#region Main
			businessGalleryViewModel.BusinessId = BusinessInfo.Id;
			businessGalleryViewModel.BusinessName = BusinessInfo.Name;
			businessGalleryViewModel.BusinessRate = BusinessInfo.Rate;
			businessGalleryViewModel.BusinessTotalReview = TotalReviews;
			foreach (var item in GalleryItems)
			{
				Gallery.TryAdd(item.Id, item.Image);
			}
			businessGalleryViewModel.Pictures = Gallery; 
			#endregion
			return View(businessGalleryViewModel);
		}


	}
}
