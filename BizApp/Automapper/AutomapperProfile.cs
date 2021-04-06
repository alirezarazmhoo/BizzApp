using AutoMapper;
using BizApp.Areas.Admin.Models;
using BizApp.Areas.Profile.Models;
using BizApp.Areas.WebApi.Models;
using BizApp.Models.Basic;
using BizApp.Utility;
using DomainClass;
using DomainClass.Businesses;
using DomainClass.Businesses.Commands;
using DomainClass.Businesses.Queries;
using DomainClass.Commands;
using DomainClass.Queries;
using DomainClass.Review;
using System.Linq;

namespace BizApp.Automapper
{
	public class AutomapperProfile : Profile
	{
		public AutomapperProfile()
		{
			// Province
			CreateMap<Province, ProvinceViewModel>()
				.ForMember(dest => dest.ProvinceId, opt => opt.MapFrom(src => src.Id))
				.ReverseMap();
			CreateMap<Slider, SliderViewModel>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ReverseMap();

			// City
			CreateMap<City, CityViewModel>()
				.ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.ProvinceName, opt => opt.MapFrom(src => src.Province.Name))
				.ReverseMap();

			// District
			CreateMap<District, DistrictViewModel>()
				.ForMember(dest => dest.DistrictId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
				.ReverseMap();

			// Category
			CreateMap<Category, CategoryViewModel>()
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategoryId))
				.ReverseMap();

			CreateMap<CreateUpdateMainCategoryViewModel, CreateCategoryCommand>();
			CreateMap<CreateUpdateMainCategoryViewModel, UpdateCategoryCommand>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CategoryId));

			// Category Features
			CreateMap<CategoryFeature, CategoryFeaturesViewModel>()
				.ForMember(dest => dest.CategoryFeatureId, opt => opt.MapFrom(src => src.Id))
				.ReverseMap();

			// Feature
			CreateMap<Feature, FeatureViewModel>()
				.ForMember(dest => dest.FeatureId, opt => opt.MapFrom(src => src.Id))
			    .ForMember(dest => dest.FeatureType, opt => opt.MapFrom(src => src.ValueType))
				.ReverseMap();

			// Business List
			CreateMap<BusinessListQuery, BusinessListViewModel>()
				.ForMember(d => d.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.ToPersianShortDate()))
				.ReverseMap();

			// Users
			CreateMap<BizAppUser, UserViewModel>().ReverseMap();
			CreateMap<BizAppUser, UpdateOperatorViewModel>().ReverseMap();
			CreateMap<SharedUserProfileDetailQuery, SharedProfileDetailViewModel>().ReverseMap();
			CreateMap<UserProfileDetailQuery, ProfileViewModel>()
				//.ForMember(dest => dest.RegisterDate, opt => opt.MapFrom(src => src.RegisterDate.ToPersianShortDate()))
				.ReverseMap();

			// user photos
			CreateMap<ApplicationUserMedia, UserPhotosViewModel>()
				.ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.UploadedPhoto))
				.ReverseMap();

			// Business Create
			CreateMap<Business, CreateBusinessViewModel>().ReverseMap();
			CreateMap<CreateBusinessCommand, CreateBusinessViewModel>().ReverseMap();
			//CustomerBusinessMedia
			CreateMap<CustomerBusinessMedia, BusinessMediaViewModel>()
			.ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.CustomerBusinessMediaPictures.Select(s=>s.Image)))
			.ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
			.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BizAppUser.FullName))
			.ForMember(dest => dest.BusinessId, opt => opt.MapFrom(src => src.BusinessId))
			.ForMember(dest => dest.UserProfilePicture, opt => opt.MapFrom(src => src.BizAppUser.ApplicationUserMedias.Where(s=>s.IsMainImage).Select(s=>s.UploadedPhoto).FirstOrDefault()))
			.ReverseMap();

			// User Photo
			CreateMap<ApplicationUserMedia, RemoveUserPhotoViewModel>()
				.ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.UploadedPhoto))
				.ReverseMap();
	
		}
	}
}
