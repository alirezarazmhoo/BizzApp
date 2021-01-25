using AutoMapper;
using BizApp.Areas.Admin.Models;
using DomainClass;
using DomainClass.Businesses;
using DomainClass.Businesses.Queries;

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

			// Category Features
			CreateMap<CategoryFeature, CategoryFeaturesViewModel>()
				.ForMember(dest => dest.CategoryFeatureId, opt => opt.MapFrom(src => src.Id))
				.ReverseMap();

			// Feature
			CreateMap<Feature, FeatureViewModel>()
				.ForMember(dest => dest.FeatureId, opt => opt.MapFrom(src => src.Id))
				.ReverseMap();

			// Business List
			CreateMap<BusinessListQuery, BusinessListViewModel>().ReverseMap();

			// Business Create
			CreateMap<Business, CreateBusinessViewModel>()
			.ForMember(dest => dest.address, opt => opt.MapFrom(src => src.Address))
			.ForMember(dest => dest.biograpy, opt => opt.MapFrom(src => src.Biography))
			.ForMember(dest => dest.cityId, opt => opt.MapFrom(src => src.CityId))
			.ForMember(dest => dest.provinceId, opt => opt.MapFrom(src => src.ProvinceId))
			.ForMember(dest => dest.districtId, opt => opt.MapFrom(src => src.DistrictId))
			.ForMember(dest => dest.longitude, opt => opt.MapFrom(src => src.Longitude))
			.ForMember(dest => dest.latitude, opt => opt.MapFrom(src => src.Latitude))
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
			.ForMember(dest => dest.websiteurl, opt => opt.MapFrom(src => src.WebsiteUrl))
			.ForMember(dest => dest.categoryId, opt => opt.MapFrom(src => src.CategoryId))
			.ReverseMap();
		}
	}
}
