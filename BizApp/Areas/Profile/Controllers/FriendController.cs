using AutoMapper;
using BizApp.Areas.Profile.Models;
using BizApp.Areas.Profile.Models.Friends;
using DataLayer.Infrastructure;
using DomainClass.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
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

		private async Task<IActionResult> GetFriendsList(string userName, int page)
		{
			try
			{
				var result = await UnitOfWork.FriendRepo.GetAll(userName, page);

				var model = Mapper.Map<IEnumerable<SharedProfileDetailViewModel>>(result);
				var paginatedItems = new PagedList<SharedProfileDetailViewModel>(model.AsQueryable(), page, 48);

				return View(paginatedItems);
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Index(string userName = "", int page = 1)
		{
			// if user is authentitcate
			if (string.IsNullOrEmpty(userName)) return await Index(page);

			// get list of friends
			return await GetFriendsList(userName, page);
		}

		private async Task<IActionResult> Index(int page = 1)
		{
			// check user authentication
			SharedProfileDetailViewModel userDetail;
			try
			{
				userDetail = await GetUserDetailWithMainImage();
			}
			catch (UnauthorizedAccessException)
			{
				return Redirect("/identity/account/login?ReturnUrl=/profile/friend");
			}
			catch (Exception)
			{
				return StatusCode(500);
			}

			// get user firends
			return await GetFriendsList(userDetail.UserName, page);
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
				TempData["error-message"] = "درخواست دوستی شما برای این کاربر ارسال شده است";
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

		[HttpGet("remove")]
		[Authorize]
		public async Task<IActionResult> ConfirmRemoveRelation(string friendUserName)
		{
			var friendDetail = await GetUserDetailWithMainImage(friendUserName);

			return View(friendDetail);
		}

		[HttpGet]
		public async Task<IActionResult> Confirm()
		{
			var requests = await UnitOfWork.FriendRepo.GetRequests(CurrentUserId);

			//var relation = await UnitOfWork.FriendRepo.GetById(id);
			//if (relation == null) return NotFound();

			//var userName = await UnitOfWork.UserRepo.GetUserName(relation.ApplicatorUserId);
			//var userDetail = await UnitOfWork.UserProfileRepo.GetUserDetail(userName);

			//var model = new ConfirmRelationViewModel
			//{
			//	Id = relation.Id,
			//	Message = relation.Description,
			//	CreatedAt = relation.CreatedAt,

			//	UserDetail = userDetail
			//};
			
			return View(requests);
		}
		
		//[HttpPost("accepted")]
		//[Authorize]
		//public async Task<IActionResult> ApprovedRelation(Guid id)
		//{
			
		//}
		//[HttpPost("reject")]
		//[Authorize]
		//public async Task<IActionResult> RejectRelation(Guid id)
		//{
		//	return RedirectToAction("index");
		//}


		[HttpPost]
		[Authorize]
		public async Task<IActionResult> RemoveRelation(RemoveRelationViewModel model)
		{
			try
			{
				var command = new RemoveFriendRelationCommand 
				{ 
					FriendUserId = model.FriendUserId, 
					RemoverUserId = CurrentUserId 
				};

				await UnitOfWork.FriendRepo.RemoveRelation(command);
				TempData["message"] = $"ارتباط شما با {model.FriendFullName} حذف شد";

				return RedirectToAction("index");
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPost]
		[ActionName("approved")]
		public async Task<IActionResult> ApprovedRelation(string applicatorUserId)
		{
			try
			{
				await UnitOfWork.FriendRepo.AcceptedRelation(CurrentUserId, applicatorUserId);
				
				TempData["message"] = $"درخواست دوستی تایید شد";
				return RedirectToAction("index", "overview", new { area = "profile" });
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[ActionName("reject")]
		public async Task<IActionResult> RejectRelation(string applicatorUserId)
		{
			try
			{
				await UnitOfWork.FriendRepo.RejectRelation(CurrentUserId, applicatorUserId);

				TempData["message"] = $"درخواست دوستی رد شد";
				return RedirectToAction("index", "overview", new { area = "profile" });
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

	}
}
