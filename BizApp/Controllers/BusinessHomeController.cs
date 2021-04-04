using AutoMapper;
using BizApp.Models.Basic;
using BizApp.Utility;
using DataLayer.Infrastructure;
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
			#endregion
			#region Slider
			var SliderItem = await _UnitOfWork.BusinessHomePageRepo.GetSlider(BusinessId);
			businessHomePage_SliderViewModel.Images = SliderItem;
			#endregion
			#region BusinessSummary
			var SummaryItem = await _UnitOfWork.BusinessHomePageRepo.GetBusinessSummary(BusinessId);
			businessHomePage_SummaryViewModel.Title = SummaryItem.Item1;
			businessHomePage_SummaryViewModel.Rate = SummaryItem.Item2;
			businessHomePage_SummaryViewModel.Reviews = SummaryItem.Item3;
			businessHomePage_SummaryViewModel.IsClaimed = SummaryItem.Item4;
			businessHomePage_SummaryViewModel.TotalPhotos = SummaryItem.Item5;
			#endregion
			#region Featrues
			var FeaturesItem = await _UnitOfWork.BusinessHomePageRepo.GetBusinessFeatures(BusinessId);
			businessHomePage_FeatureViewModel.BoldFeature = FeaturesItem.Item1;
			businessHomePage_FeatureViewModel.Features = FeaturesItem.Item2;
			#endregion
			#region NearSponseredBusiness
			var SponseredBusinessItem = await _UnitOfWork.BusinessHomePageRepo.GetNearByBusinessSponsored(BusinessId);
			foreach (var item in SponseredBusinessItem)
			{
				businessHomePage_NearSponseredViewModel.Add(new BusinessHomePage_NearSponseredViewModel() { Name = item.Item1, Image = item.Item2, Rate = item.Item3, Review = item.Item4, Id = item.Item5, Descripton = item.Item6 });
			}
			#endregion
			#region Description
			var BusinessOtherInfoItem = await _UnitOfWork.BusinessHomePageRepo.GetBusinessOtherInfo(BusinessId);
			businessHomePage_DescriptionViewModel.Descripton = string.IsNullOrEmpty(BusinessOtherInfoItem.Item1) == true ? "فاقد توضیحات" : BusinessOtherInfoItem.Item1;
			#endregion
			#region RightPageBusinessInfo
			businessHomePage_RightPageBusinessInfoViewModel.WebSiteUrl = BusinessOtherInfoItem.Item2;
			businessHomePage_RightPageBusinessInfoViewModel.PhoneNumber = BusinessOtherInfoItem.Item3;
			businessHomePage_RightPageBusinessInfoViewModel.Address = BusinessOtherInfoItem.Item4;
			#endregion

			#region Reviews
			var ReviewsItem = await _UnitOfWork.BusinessHomePageRepo.GetBusinessReview(BusinessId);
			foreach (var item in ReviewsItem)
			{
				businessHomePage_ReviewsViewModel.Add(new BusinessHomePage_ReviewsViewModel() { Cool = item.CoolCount , Date = DateChanger.ToPersianDateString(item.Date) , DistricName = item.BizAppUser.Address , Funny = item.FunnyCount , Rate = item.Rate , Text = item.Description , UseFull = item.UsefulCount , TotalReviews = item.BizAppUser.Reviews.Count , TotalPictures = 0 , UserName = item.BizAppUser.FullName , Id = item.BizAppUser.Id});
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
			#endregion
			return View();
		}
	}
}
