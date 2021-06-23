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
		[Route("GetByToken")]
		public async Task<IActionResult> GetByToken(int Page)
		{
			string UserToken = HttpContext.Request?.Headers["Token"];
			if (!await _UnitOfWork.UserRepo.CheckUserToken(UserToken))
			{
				return NotFound("کاربر مورد نظر یافت نشد ");
			}
			string url = string.Format("/api/Activity/Get?Id={0}&Page={1}",await _UnitOfWork.UserRepo.UserTokenMaper(UserToken), Page);
			return Redirect(url);
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
							TotalReviewPicture = await _UnitOfWork.BusinessHomePageRepo.GetTotalUserMedia(Id) , Date = item.CreatedAt.ToPersianDateString() , Type =2 ,
							Image = string.IsNullOrEmpty(UserItem.ApplicationUserMedias
							.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : UserItem.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault(),

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
							.Where(s => s.IsMainImage && s.Status == 
							DomainClass.Enums.StatusEnum.Accepted)
							.Select(s => s.UploadedPhoto).FirstOrDefault())
								 == true ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : FriendItem.Receiver.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault() , 
								TotalFriends = await _UnitOfWork.UserRepo.GetUserFriendsCount(FriendItem.ReceiverUserId),
								TotalReview = await _UnitOfWork.ReviewRepo.GetUserTotalReview(FriendItem.ReceiverUserId),
								TotalReviewPicture = await _UnitOfWork.BusinessHomePageRepo.GetTotalUserMedia(FriendItem.ReceiverUserId),
								Date = item.CreatedAt.ToPersianDateString(),
								Type = 3 ,UserId = FriendItem.ReceiverUserId
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
		[Route("RecentActivty")]
		public async Task<IActionResult> GetUerRecentActivity(int? page)
		{
			string Token = HttpContext.Request?.Headers["Token"];
			if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
			{
				return NotFound("کاربرموردنظریافت نشد");
			}
			#region Objects
			RecentActivityModel recentActivityModel = new RecentActivityModel();
			List<RecentActivityReviewModel> recentActivityReviewModel = new List<RecentActivityReviewModel>();
			List<RecentActivityUserBusinessMediaPicture> recentActivityUserBusinessMediaPicture = new List<RecentActivityUserBusinessMediaPicture>();
			#endregion
			try
			{
				#region Resource
				var GetRecentActivityItems = await _UnitOfWork.ReviewRepo.GetRecentActivity(page);
				var GetRecentActivityBusinessMediaItems = await _UnitOfWork.ReviewRepo.GetRecentActivityBusinessMedia(page);
				#endregion		
				foreach (var item in GetRecentActivityItems)
				{
					var UserItem = await _UnitOfWork.UserRepo.GetById(item.BizAppUserId);
					recentActivityReviewModel.Add(new RecentActivityReviewModel() { BusinessId = item.BusinessId, BusinessImage = string.IsNullOrEmpty(item.Business.FeatureImage) == false ? "/Upload/DefaultPicutres/Bussiness/business-strategy-success-target-goals_1421-33.jpg" : item.Business.FeatureImage, BusinessName = item.Business.Name, BusinessRate = item.Business.Rate, ReviewCool = item.CoolCount, ReviewFunny = item.FunnyCount, ReviewUseFull = item.UsefulCount, ReviewMedias = FillMediaType(item.ReviewMedias), ReviewRate = item.Rate, ReviewText = item.Description , UserId = UserItem.Id , UserImage = string.IsNullOrEmpty(UserItem.ApplicationUserMedias
					.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted)
					.Select(s => s.UploadedPhoto).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : UserItem.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault() , UserName = UserItem.UserName , UserTotalBusinessImage= await _UnitOfWork.BusinessHomePageRepo.GetTotalUserMedia(UserItem.Id), UserTotalFriends = await _UnitOfWork.UserRepo.GetUserFriendsCount(UserItem.Id) , UserTotalReviews = UserItem.Reviews.Count , BusinessTotalReview = item.Business.Reviews.Count , BusinessAddress = item.Business.District.City.Name + " " + item.Business.District.City.Province.Name + " " + item.Business.District.Name , ReviewId = item.Id
					});
				}
				foreach (var item in GetRecentActivityBusinessMediaItems)
				{
					var UserItem = await _UnitOfWork.UserRepo.GetById(item.BizAppUserId);
					recentActivityUserBusinessMediaPicture.Add(new RecentActivityUserBusinessMediaPicture() { BusinessId = item.BusinessId, BusinessImage = string.IsNullOrEmpty(item.Business.FeatureImage) == false ? "/Upload/DefaultPicutres/Bussiness/business-strategy-success-target-goals_1421-33.jpg" : item.Business.FeatureImage, BusinessName = item.Business.Name, BusinessRate = item.Business.Rate, LikeCount = item.LikeCount, Medias = UserBusinessMediaFillMediaType(item.CustomerBusinessMediaPictures)  ,
						UserId = UserItem.Id,
						UserImage = string.IsNullOrEmpty(UserItem.ApplicationUserMedias
					.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted)
					.Select(s => s.UploadedPhoto).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : UserItem.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault(),
						UserName = UserItem.UserName,
						UserTotalBusinessImage = await _UnitOfWork.BusinessHomePageRepo.GetTotalUserMedia(UserItem.Id),
						UserTotalFriends = await _UnitOfWork.UserRepo.GetUserFriendsCount(UserItem.Id),
						UserTotalReviews = UserItem.Reviews.Count ,
						BusinessTotalReview = item.Business.Reviews.Count,
						BusinessAddress = item.Business.District.City.Name + " " + item.Business.District.City.Province.Name + " " + item.Business.District.Name
					});
				}
				recentActivityModel.RecentActivityReviewModels = recentActivityReviewModel;
				recentActivityModel.RecentActivityUserBusinessMediaPictures = recentActivityUserBusinessMediaPicture;
				return Ok(recentActivityModel);
			}
			catch(Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		private List<(Guid Id, string Image, string Description)> UserBusinessMediaFillMediaType(ICollection<DomainClass.Review.CustomerBusinessMediaPictures> reviewMedias)
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
