using AutoMapper;
using BizApp.Areas.Profile.Models;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BizApp.Areas.Profile.Controllers
{
	public class ProfileController : Controller
	{
		protected readonly IUnitOfWorkRepo UnitOfWork;
		protected readonly ClaimsPrincipal CurrentUser;
		//private readonly UserManager<BizAppUser> _userManager;
		protected readonly IMapper Mapper;
		protected string CurrentUserId => CurrentUser?.FindFirst(ClaimTypes.NameIdentifier).Value;

		public ProfileController(IUnitOfWorkRepo unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
		{
			UnitOfWork = unitOfWork;
			CurrentUser = httpContextAccessor.HttpContext.User;
			Mapper = mapper;
		}

		protected async Task<SharedProfileDetailViewModel> GetUserDetailWithMainImage(string userName = null)
		{
			if (userName == null && CurrentUser != null)
			{
				userName = CurrentUser.Identity.Name;
			}

			if (string.IsNullOrEmpty(userName)) throw new UnauthorizedAccessException();

			var user = await UnitOfWork.UserProfileRepo.GetSharedUserDetail(userName);

			var result = Mapper.Map<SharedProfileDetailViewModel>(user);
			return result;
		}
		
		//protected async Task<ProfileViewModel> GetCurrentUserDetail()
		//{
		//	var userId = _userManager.GetUserId(HttpContext.User);
		//	var userDetail = await UnitOfWork.ProfileRepo.GetUserDetail(userId);

		//	var result = Mapper.Map<ProfileViewModel>(userDetail);

		//	return result;
		//}
		//protected async Task<ProfileViewModel> GetUserDetail(string userName = null)
		//{
		//	if (userName == null)
		//	{
		//		return await GetCurrentUserDetail();
		//	}

		//	var tempUser = await UnitOfWork.UserRepo.GetByUserName(userName);

		//	var user = await UnitOfWork.ProfileRepo.GetUserDetail(tempUser.Id);
		//	var result = Mapper.Map<ProfileViewModel>(user);

		//	return result;
		//}
	}
}
