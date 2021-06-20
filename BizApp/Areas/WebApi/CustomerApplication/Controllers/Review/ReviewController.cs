using BizApp.Areas.WebApi.Models;
using BizApp.Utility;
using DataLayer.Extensions;
using DataLayer.Infrastructure;
using DomainClass.Review;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Review = DomainClass.Review.Review;

namespace BizApp.Areas.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		public ReviewController(IUnitOfWorkRepo unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}
		[HttpPost]
		[Route("ChangeUseFullCount")]
		public async Task<IActionResult> ChangeUseFullCount(Guid Id)
		{
			try
			{
				bool state;
				string Token = HttpContext.Request?.Headers["Token"];
				if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
				{
					return NotFound();
				}
				if (await _UnitOfWork.ReviewRepo.ChangeHelpFull(Id, await _UnitOfWork.UserRepo.UserTokenMaper(Token)) == DomainClass.Enums.VotesAction.Add)
				{
					state = true;
				}
				else
				{
					state = false;
				}
				await _UnitOfWork.SaveAsync();
				return Ok(state);
			}
			catch (Exception)
			{
				throw;
			}

		}
		[HttpPost]
		[Route("ChangeFunnyCount")]

		public async Task<IActionResult> ChangeFunnyCount(Guid Id)
		{
			try
			{
				bool state;
				string Token = HttpContext.Request?.Headers["Token"];
				if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
				{
					throw new Exception();
				}
				if (await _UnitOfWork.ReviewRepo.ChangeFunnyCount(Id, await _UnitOfWork.UserRepo.UserTokenMaper(Token)) == DomainClass.Enums.VotesAction.Add)
				{
					state = true;
				}
				else
				{
					state = false;

				}
				await _UnitOfWork.SaveAsync();
				return Ok(state);
			}
			catch (Exception)
			{
				throw;

			}

		}
		[HttpPost]
		[Route("ChangeCoolCount")]
		public async Task<IActionResult> ChangeCoolCount(Guid Id)
		{
			try
			{
				bool state;
				string Token = HttpContext.Request?.Headers["Token"];
				if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
				{
					throw new Exception();
				}
				if (await _UnitOfWork.ReviewRepo.ChangeCoolCount(Id, await _UnitOfWork.UserRepo.UserTokenMaper(Token)) == DomainClass.Enums.VotesAction.Add)
				{
					state = true;
				}
				else
				{
					state = false;
				}
				await _UnitOfWork.SaveAsync();
				return Ok(state);

			}
			catch (Exception)
			{
				throw;
			}
		}
		[HttpPost]
		[Route("Add")]
		public async Task<IActionResult> Add([FromForm] Review model , IFormFile[] files )
		{
			string Token = HttpContext.Request?.Headers["Token"];
			if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
			{
				return NotFound();
			}
			try
			{
				model.BizAppUserId = await _UnitOfWork.UserRepo.UserTokenMaper(Token); 
				await _UnitOfWork.ReviewRepo.AddReview(model , files , model.caption);
				await _UnitOfWork.SaveAsync();
				return Ok();
			}
			catch(Exception)
			{
				throw; 
			}
		}

		[Route("GetUserReview")]
		public async Task<IActionResult> GetUserReview(string Id)
		{
			List<ReviewProfile> reviews = new List<ReviewProfile>(); 
			if (await _UnitOfWork.UserRepo.GetById(Id) == null)
			{
				return NotFound();
			}
			try
			{
				var Items = await _UnitOfWork.ReviewRepo.GetUserReview(Id);
				foreach (var item in Items)
				{
					reviews.Add(new ReviewProfile() { Image = string.IsNullOrEmpty(item.Business.FeatureImage) == false ? "/Upload/DefaultPicutres/Bussiness/business-strategy-success-target-goals_1421-33.jpg" : item.Business.FeatureImage, Rate = item.Rate, TotalImages = item.ReviewMedias.Count, Text = item.Description, BusinessName = item.Business.Name, Id = item.Id, Cool = item.CoolCount , Funny = item.FunnyCount , UseFull = item.UsefulCount ,  ReviewMedias = FillMediaType(item.ReviewMedias) , Date = item.Date.ToPersianDateString() , UserId = item.BizAppUserId , UserName = item.BizAppUser.UserName , UserPicture = string.IsNullOrEmpty(item.BizAppUser.ApplicationUserMedias
							.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : item.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault() , UserTotalBusinessMedia = await _UnitOfWork.ReviewRepo.GetUserTotalBusinessMedia(item.BizAppUserId) , UserTotalFriend = await _UnitOfWork.UserRepo.GetUserFriendsCount(Id) ,  UserTotalReview = await _UnitOfWork.ReviewRepo.GetUserTotalReview(Id)
					});
				}
				return Ok(reviews);
			}
			catch(Exception)
			{
				throw;  
			}
		}

		[Route("GetUserReviewByToken")]
		public async Task<IActionResult> GetUserReviewByToken()
		{
			string UserToken = HttpContext.Request?.Headers["Token"];
			if (!await _UnitOfWork.UserRepo.CheckUserToken(UserToken))
			{
				return NotFound("کاربر مورد نظر یافت نشد ");
			}
			return RedirectToAction(nameof(GetUserReview), "Review", new { Id = await _UnitOfWork.UserRepo.UserTokenMaper(UserToken) });
		}
		[Route("GetReviewPicture")]
		public async Task<IActionResult> GetReviewPictureDetail(Guid Id)
		{
			BusinessGallery reviewMedias = new BusinessGallery();
			var Item = await _UnitOfWork.ReviewRepo.GetReviewMediaDetail(Id);
			if(Item == null)
			{
				return NotFound();  
			}
			else
			{
				reviewMedias.Date = Item.CreatedAt.ToPersianDateString();
				reviewMedias.Description = Item.Description;
				reviewMedias.Id = Item.Id;
				reviewMedias.MediaType  = FormatCheck.GetFormat(Item.Image) == DomainClass.Enums.MediaType.Picture ? "Picture" : "Video";
				reviewMedias.Url = Item.Image;
				reviewMedias.UserId = Item.Review.BizAppUserId;
				reviewMedias.UserName = Item.Review.BizAppUser.UserName;  
				reviewMedias.UserPicture  = string.IsNullOrEmpty(Item.Review.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : Item.Review.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault();
				reviewMedias.UserTotalFriends = await _UnitOfWork.UserRepo.GetUserFriendsCount(Item.Review.BizAppUserId);
				reviewMedias.UserTotalPictures = await _UnitOfWork.BusinessHomePageRepo.GetTotalUserMedia(Item.Review.BizAppUserId);
				reviewMedias.UserTotalReview = await _UnitOfWork.ReviewRepo.GetUserTotalReview(Item.Review.BizAppUserId);
				reviewMedias.BusinessId = Item.Review.BusinessId; 
				return Ok(reviewMedias);
			}
		}
		[Route("UserMediaBusinessGallery")]
		public async Task<IActionResult> UserMediaBusinessGallery(string Id)
		{
			List<BusinessGallery> businessGalleries = new List<BusinessGallery>();
			try
			{
			if(await _UnitOfWork.UserRepo.GetById(Id) == null)
			{
				return NotFound();
			}
			else
			{
				var Items = await _UnitOfWork.ReviewRepo.GetCustomerBusinessMediaPictures(Id);
				foreach (var item in Items)
				{
					businessGalleries.Add(new BusinessGallery()
					{
						Date = item.CustomerBusinessMedia.Date.ToPersianDateString(),
						Description = item.Description,
						Id = item.Id,
						MediaType = FormatCheck.GetFormat(item.Image) == DomainClass.Enums.MediaType.Picture ? "Picture" : "Video",
						Url = item.Image,
						UserId = item.CustomerBusinessMedia.BizAppUserId,
						UserName = item.CustomerBusinessMedia.BizAppUser.UserName,
						UserPicture = string.IsNullOrEmpty(item.CustomerBusinessMedia.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : item.CustomerBusinessMedia.BizAppUser.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault(),
						UserTotalFriends = await _UnitOfWork.UserRepo.GetUserFriendsCount(item.CustomerBusinessMedia.BizAppUserId),
						UserTotalPictures = await _UnitOfWork.BusinessHomePageRepo.GetTotalUserMedia(item.CustomerBusinessMedia.BizAppUserId),
						UserTotalReview = await _UnitOfWork.ReviewRepo.GetUserTotalReview(item.CustomerBusinessMedia.BizAppUserId) , 
						 BusinessId = item.CustomerBusinessMedia.BusinessId
					}); 
				}
				return Ok(businessGalleries);
			}
			}
			catch(Exception)
			{
				throw; 
			}
		}

		[Route("OwnerUserMediaBusinessGallery")]
		public async Task<IActionResult> GetUserMediaBusinessGalleryFromProfile()
		{
			string Token = HttpContext.Request?.Headers["Token"];
			if (await _UnitOfWork.UserRepo.CheckUserToken(Token) == false)
			{
				return NotFound("کاربر مورد نظریافت نشد");
			}
			return RedirectToAction(nameof(UserMediaBusinessGallery), "Review", new { Id = await _UnitOfWork.UserRepo.UserTokenMaper(Token) });
		}
		private List<(Guid Id, string Image, string Description)> FillMediaType(ICollection<DomainClass.Review.ReviewMedia> reviewMedias)
		{
			List<(Guid Id, string Image , string Description)> keyValuePairs = new List<(Guid Id, string Image, string Description)>();
			foreach (var item in reviewMedias)
			{
				keyValuePairs.Add( (item.Id , item.Image , item.Description));
			}
			return keyValuePairs;
		}
	}
}
