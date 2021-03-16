using AutoMapper;
using BizApp.Areas.Profile.Models;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BizApp.Areas.Profile.Controllers
{
	public class ProfileController : Controller
	{
		private readonly IUserProfileRepo _userProfileRepository;
		private readonly IUnitOfWorkRepo _unitOfWork;
		private readonly UserManager<BizAppUser> _userManager;
		private readonly IMapper _mapper;

		public ProfileController(IUnitOfWorkRepo unitOfWork, IUserProfileRepo userPorfile, UserManager<BizAppUser> userManager, IMapper mapper)
		{
			_userProfileRepository = userPorfile;
			_userManager = userManager;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		private async Task<ProfileViewModel> GetCurrentUserDetail()
		{
			var userId = _userManager.GetUserId(HttpContext.User);
			var userDetail = await _userProfileRepository.GetUserDetail(userId);

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
			
			var user = await _userProfileRepository.GetUserDetail(tempUser.Id);
			var result = _mapper.Map<ProfileViewModel>(user);

			return result;
		}

	}
}
