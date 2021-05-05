using AutoMapper;
using BizApp.Areas.Profile.Models;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.Profile.Controllers
{
	[Area("profile")]
	//[Route("profile/friends")]
	public class FriendController : ProfileController
	{
		public FriendController(IUnitOfWorkRepo unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(unitOfWork, httpContextAccessor, mapper)
		{
		}

		[HttpGet]
		public IActionResult Index(string userName)
		{
			if (string.IsNullOrEmpty(userName)) return Index();

			return View();
		}

		private IActionResult Index()
		{
			// check user authentication

			// get user firends

			// return view
			return View();
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Add(string userName)
		{
			// validate userName
			if (string.IsNullOrEmpty(userName)) return NotFound();

			// get reciver detail
			var receiverInfo = await GetUserDetailWithMainImage(userName);
			if (receiverInfo == null) return NotFound();

			// check is not owner user
			if (receiverInfo.UserName.Equals(CurrentUser.Identity.Name, StringComparison.OrdinalIgnoreCase)) return NotFound();

			return View(receiverInfo);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateRelation(CreateFriendRelationViewModel model)
		{
			var receiverInfo = await GetUserDetailWithMainImage(model.ReceiverUserName);

			//ModelState.Remove("Id");
			if (!ModelState.IsValid)
			{
				return View("add", receiverInfo);
			}

			// user friend model 
			var command = Mapper.Map<CreateFriendRelationCommand>(model);
			command.ApplicatorUserId = CurrentUserId;

			try
			{
				await UnitOfWork.FriendRepo.CreateRelation(command);

				TempData["message"] = $"در خواست دوستی شما برای {model.ReceiverFullName} ارسال شد";
				return Redirect("/profile/overview");
			}
			catch (DuplicateNameException)
			{
				ModelState.AddModelError("UserName", "");
				return View("add", receiverInfo);
			}
			catch (ApplicationException)
			{
				return BadRequest();
			}
			catch (Exception ex)
			{
				return StatusCode(580);
			}

		}
	}
}
