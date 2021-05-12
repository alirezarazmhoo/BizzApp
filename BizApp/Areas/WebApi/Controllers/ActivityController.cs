using BizApp.Areas.WebApi.Models;
using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Extensions;
using BizApp.Utility;

namespace BizApp.Areas.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ActivityController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public ActivityController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}

		[Route("Get")]
		public async Task<IActionResult> Get(string Id, int Page)
		{
			List<Activity> activity = new List<Activity>();
			if (await _UnitOfWork.UserRepo.GetById(Id) == null)
			{
				return NotFound();
			}
			try
			{
				var Items = await _UnitOfWork.UserActivityRepo.GetAllActivities(Id, Page);
				var UserItem = await _UnitOfWork.UserRepo.GetById(Id);

				foreach (var item in Items)
				{
					if (item.TableName ==  DomainClass.TableName.Reviews)
					{
						var ReviewItem = await _UnitOfWork.ReviewRepo.GetById(item.TableKey);
						activity.Add(new Activity()
						{
							Address = ReviewItem.BizAppUser.Address,
							BusinessImage = string.IsNullOrEmpty(ReviewItem.Business.FeatureImage) == false ?
							"/Upload/DefaultPicutres/Bussiness/business-strategy-success-target-goals_1421-33.jpg" : ReviewItem.Business.FeatureImage
							,
							BusinessName = ReviewItem.Business.Name
							,
							BusinessRate = ReviewItem.Business.Rate
							,
							BusinessTotalReview = await _UnitOfWork.ReviewRepo.GetBusinessTotalReview(ReviewItem.BusinessId)
							,
							CoolCount = ReviewItem.CoolCount,
							FunnyCount = ReviewItem.FunnyCount,
							Image = string.IsNullOrEmpty(ReviewItem.BizAppUser.ApplicationUserMedias
							.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : ReviewItem.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault(),
							ReviewRate = ReviewItem.Rate,
							Text = ReviewItem.Description,
							TotalBusinessMediaPicture = await _UnitOfWork.ReviewRepo.GetBusinessTotalCustomerMedia(ReviewItem.BusinessId),
							TotalFriends = await _UnitOfWork.UserRepo.GetUserFriendsCount(Id),
							TotalReview = await _UnitOfWork.ReviewRepo.GetUserTotalReview(Id),
							Type = 0,
							UseFullCount = ReviewItem.UsefulCount,
							UserName = ReviewItem.BizAppUser.UserName , 
							BusinessId = ReviewItem.BusinessId ,
							Date = item.CreatedAt.ToPersianDateString(),
							UserId = ReviewItem.BizAppUserId,
							ReviewMedias = FillMediaType(ReviewItem.ReviewMedias)
						});
					}
					else if(item.TableName ==  DomainClass.TableName.UserBusinessMedia)
					{
						Guid CustomerBusinessMediaItemId = new Guid(item.TableKey);
						var CustomerBusinessMediaItem = await _UnitOfWork.ReviewRepo.GetCustomerBusinessMediaById(CustomerBusinessMediaItemId);
						activity.Add(new Activity()
						{
							 BusinessId = CustomerBusinessMediaItem.BusinessId,
							BusinessImage = string.IsNullOrEmpty(CustomerBusinessMediaItem.Business.FeatureImage) == false ?
							"/Upload/DefaultPicutres/Bussiness/business-strategy-success-target-goals_1421-33.jpg" : CustomerBusinessMediaItem.Business.FeatureImage , BusinessName  = CustomerBusinessMediaItem.Business.Name ,
							 BusinessRate = CustomerBusinessMediaItem.Business.Rate ,BusinessTotalReview  = await _UnitOfWork.ReviewRepo.GetBusinessTotalReview(CustomerBusinessMediaItem.BusinessId) , 
							Pictures = FillPictures(CustomerBusinessMediaItem.CustomerBusinessMediaPictures.ToList()) ,
							 Type = 1
						});
					}
					else if(item.TableName == DomainClass.TableName.UserPhotos)
					{
						activity.Add(new Activity() { Text ="عکس پروفایل خود را تغییر داد", UserName =UserItem.UserName,
							TotalFriends = await _UnitOfWork.UserRepo.GetUserFriendsCount(Id),
							TotalReview = await _UnitOfWork.ReviewRepo.GetUserTotalReview(Id),
							TotalReviewPicture = await _UnitOfWork.BusinessHomePageRepo.GetTotalUserMedia(Id) , Date = item.CreatedAt.ToPersianDateString() , Type =2
						});
					}
					else if(item.TableName == DomainClass.TableName.SendRequest)
					{
						Guid _TableKey = new Guid(item.TableKey);
						if(await _UnitOfWork.FriendRepo.CheckRelation(_TableKey))
						{
							var FriendItem = await _UnitOfWork.FriendRepo.GetById(_TableKey);

							activity.Add(new Activity()
							{
								Text = "به دوستان وی اضافه شد",
								UserName = FriendItem.Receiver.FullName,
								 Image = string.IsNullOrEmpty(FriendItem.Receiver.ApplicationUserMedias
							.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : FriendItem.Receiver.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault() , 
								TotalFriends = await _UnitOfWork.UserRepo.GetUserFriendsCount(FriendItem.ReceiverUserId),
								TotalReview = await _UnitOfWork.ReviewRepo.GetUserTotalReview(FriendItem.ReceiverUserId),
								TotalReviewPicture = await _UnitOfWork.BusinessHomePageRepo.GetTotalUserMedia(FriendItem.ReceiverUserId),
								Date = item.CreatedAt.ToPersianDateString(),
								Type = 3
							});
						}
					}
				}
				return Ok(activity);
			}
			catch (Exception)
			{
				throw; 
			}
		}

		private Dictionary<Guid, string> FillPictures(List<DomainClass.Review.CustomerBusinessMediaPictures> lists)
		{
			Dictionary<Guid, string> keyValuePairs = new Dictionary<Guid, string>();
			foreach (var item in lists)
			{
				keyValuePairs.Add(item.Id, item.Image);
			}
			return keyValuePairs;
		}
		private List<(Guid Id, string Image, string Description)> FillMediaType(ICollection<DomainClass.Review.ReviewMedia> reviewMedias)
		{
			List<(Guid Id, string Image, string Description)> keyValuePairs = new List<(Guid Id, string Image, string Description)>();
			foreach (var item in reviewMedias)
			{
				keyValuePairs.Add((item.Id, item.Image, item.Description));
			}
			return keyValuePairs;
		}
	}
}
