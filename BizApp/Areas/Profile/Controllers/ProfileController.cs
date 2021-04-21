using AutoMapper;
using BizApp.Areas.Profile.Models;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Principal;
using System.Threading.Tasks;

namespace BizApp.Areas.Profile.Controllers
{
	public class ProfileController : Controller
	{
		private readonly IUnitOfWorkRepo _unitOfWork;
		private readonly IIdentity _currentUser;
		private readonly UserManager<BizAppUser> _userManager;
		private readonly IMapper _mapper;

		public ProfileController(IUnitOfWorkRepo unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public ProfileController(IUnitOfWorkRepo unitOfWork, IHttpContextAccessor httpContextAccessor): this(unitOfWork)
		{
			_currentUser = httpContextAccessor.HttpContext.User.Identity;

		}
		public ProfileController(IUnitOfWorkRepo unitOfWork, UserManager<BizAppUser> userManager) : this(unitOfWork)
		{
			_userManager = userManager;

		}
		public ProfileController(IUnitOfWorkRepo unitOfWork, UserManager<BizAppUser> userManager, IMapper mapper) : this(unitOfWork, userManager)
		{
			_mapper = mapper;
		}

		protected async Task<SharedProfileDetailViewModel> GetUserDetailWithMainImage(string userName = null)
		{
			if (userName == null && _currentUser != null)
			{
				userName = _currentUser.Name;
			}

			if (string.IsNullOrEmpty(userName)) throw new UnauthorizedAccessException();

			var user = await _unitOfWork.UserProfileRepo.GetSharedUserDetail(userName);

			var result = _mapper.Map<SharedProfileDetailViewModel>(user);
			return result;
		}
		protected async Task<ProfileViewModel> GetCurrentUserDetail()
		{
			var userId = _userManager.GetUserId(HttpContext.User);
			var userDetail = await _unitOfWork.ProfileRepo.GetUserDetail(userId);

			var result = _mapper.Map<ProfileViewModel>(userDetail);

			return result;
		}


		protected async Task<ProfileViewModel> GetUserDetail(string userName = null)
		{
			if (userName == null)
			{
				return await GetCurrentUserDetail();
			}

			var tempUser = await _unitOfWork.UserRepo.GetByUserName(userName);

			var user = await _unitOfWork.ProfileRepo.GetUserDetail(tempUser.Id);
			var result = _mapper.Map<ProfileViewModel>(user);

			return result;
		}

	}
}
