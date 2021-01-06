using AutoMapper;
using BizApp.Areas.Admin.Models;
using DomainClass;

namespace BizApp.Automapper
{
	public class AutomapperProfile : Profile
	{
		public AutomapperProfile()
		{
			CreateMap<Province, ProvinceViewModel>().ReverseMap();
		}
	}
}
