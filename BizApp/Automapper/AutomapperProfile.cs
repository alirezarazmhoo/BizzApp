using AutoMapper;
using BizApp.Areas.Admin.Models;
using BizApp.Utility;
using DomainClass;
using DomainClass.Businesses;
using DomainClass.Businesses.Commands;
using DomainClass.Businesses.Queries;
using DomainClass.Commands;

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

			// Business Create
			CreateMap<Business, CreateBusinessViewModel>().ReverseMap();
			CreateMap<CreateBusinessCommand, CreateBusinessViewModel>().ReverseMap();

		}
	}
}
