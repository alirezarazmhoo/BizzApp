using AutoMapper;
using BizApp.Areas.Profile.Models;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BizApp.Areas.Profile.Controllers
{
	public class ProfileController : Controller
	{
		protected readonly IUnitOfWorkRepo UnitOfWork;
		protected readonly IMapper AutoMapper;

		public ProfileController(IUnitOfWorkRepo unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
		{
			UnitOfWork = unitOfWork;
			AutoMapper = mapper;

			var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			SetProfileDetail(userId);
		}

		private void SetProfileDetail(string userId) 
		{
			// Todo: Cash it
			// if (ViewData["Profile"] not exists or is null) {
			var user = UnitOfWork.UserProfileRepo.GetUserDetail(userId);

			var viewModel = AutoMapper.Map<ProfileViewModel>(user);

			ViewData["Profile"] = Profile;
		}

		public ProfileViewModel Profile { get; private set; }
	}
}
