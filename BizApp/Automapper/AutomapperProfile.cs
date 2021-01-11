using AutoMapper;
using BizApp.Areas.Admin.Models;
using DomainClass;

namespace BizApp.Automapper
{
	public class AutomapperProfile : Profile
	{
		public AutomapperProfile()
		{
			CreateMap<Province, ProvinceViewModel>()
				.ForMember(dest => dest.ProvinceId, opt => opt.MapFrom(src => src.Id))
				.ReverseMap();

			CreateMap<City, CityViewModel>()
				.ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.ProvinceName, opt => opt.MapFrom(src => src.Province.Name))
				.ReverseMap();

			CreateMap<District, DistrictViewModel>()
				.ForMember(dest => dest.DistrictId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
				.ReverseMap();


			CreateMap<Category, CategoryViewModel>()
			.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategoryId))

			.ReverseMap();

		}
	}
}
