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
				//.ForMember(dest => dest.ProvinceId, opt => opt.MapFrom(src => src.ProvinceId))
				.ReverseMap();
		}
	}
}
