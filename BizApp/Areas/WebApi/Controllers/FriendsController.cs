using BizApp.Areas.WebApi.Models;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FriendsController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public FriendsController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}
		[Route("Get")]
		public async Task<IActionResult> GetUserFriends(string Id,  int Page)
		{
			List<UserProfile> userProfiles = new List<UserProfile>();
			try
			{
			if (await _UnitOfWork.UserRepo.GetById(Id) == null)
			{
				return NotFound(); 
			}
			else
			{
				var Items = await _UnitOfWork.FriendRepo.GetAll(await _UnitOfWork.UserRepo.GetUserName(Id) , Page);

				foreach (var item in Items)
				{
					userProfiles.Add(new UserProfile() {  TotalFriends = item.FriendsNumber , Image = item.MainPhotoPath , TotalReview = item.ReviewCount , UserName = item.FullName ,  TotalBusinessMediaPicture = await _UnitOfWork.ReviewRepo.GetUserTotalBusinessMedia(item.Id) , Address = string.IsNullOrEmpty(item.Address) ?
						"بدون آدرس" :  item.Address , Id = item.Id , City = item.City });
				}
				return Ok(userProfiles);
			}

			}
			catch (Exception)
			{
				throw; 
			}
		}
 
	}
}
