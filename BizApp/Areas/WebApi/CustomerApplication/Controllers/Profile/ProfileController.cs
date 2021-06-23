using BizApp.Areas.WebApi.Models;
using BizApp.Utility;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BizApp.Areas.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProfileController : ControllerBase
	{
		private readonly IUnitOfWorkRepo _UnitOfWork;
		private readonly UserManager<BizAppUser> _userManager;
		private readonly SignInManager<BizAppUser> _signInManager;
		public ProfileController( IUnitOfWorkRepo unitOfWork, UserManager<BizAppUser> userManager , SignInManager<BizAppUser> signInManager
		)
		{
			_UnitOfWork = unitOfWork;
			_signInManager = signInManager;
			_userManager = userManager;
		}
		[Route("UserProfile")]
		public async Task<IActionResult> GetUsersInformation(string Id)
		{
			UserProfile userProfile = new UserProfile();
			List<Tuple<Guid, string, string>> UserProfileImages = new List<Tuple<Guid, string, string>>();
			//Dictionary<Guid, string> UserProfileImages = new Dictionary<Guid, string>();
			if (await _UnitOfWork.UserRepo.GetById(Id) == null)
			{
				return NotFound();
			}
			try
			{
			var UserItem = await _UnitOfWork.UserRepo.GetById(Id);
			userProfile.Address = string.IsNullOrEmpty(UserItem.Address) ? "بدون آدرس" : UserItem.Address;
			userProfile.Image = string.IsNullOrEmpty(UserItem.ApplicationUserMedias
				.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted)
				.Select(s => s.UploadedPhoto).FirstOrDefault()) == true ? "/Upload/DefaultPicutres/User/66-660853_png-file-svg-business-person-icon-png-clipart.jpg" : UserItem.ApplicationUserMedias.Where(s => s.IsMainImage && s.Status == DomainClass.Enums.StatusEnum.Accepted).Select(s => s.UploadedPhoto).FirstOrDefault();
			userProfile.TotalBusinessMediaPicture = await _UnitOfWork.BusinessHomePageRepo.GetTotalUserMedia(Id);
			userProfile.TotalReview = UserItem.Reviews.Count;
			userProfile.TotalReviewPicture = await _UnitOfWork.ReviewRepo.GetUserTotalReviewMedia(Id);
			userProfile.UserName = UserItem.UserName;
			userProfile.Id = UserItem.Id;
			userProfile.TotalVotes = await _UnitOfWork.ReviewRepo.GetUserTotalVotes(UserItem.Id);
			userProfile.TotalLikes = await _UnitOfWork.ReviewRepo.GetUserTotalBusinessLike(UserItem.Id);

				foreach (var item in UserItem.ApplicationUserMedias
				.ToList())
				{
					UserProfileImages.Add(new Tuple<Guid, string, string>(item.Id, item.UploadedPhoto, item.CreatedAt.ToPersianDateString()));
				}
				userProfile.UserProfileImages = UserProfileImages; 
		   return Ok(userProfile); 
			}
			catch(Exception ex)
			{
				return BadRequest(ex);
			}

		}

		[Route("AboutUserByToken")]
		public async Task<IActionResult> AboutUserByToken()
		{
			string UserToken = HttpContext.Request?.Headers["Token"];
			if (!await _UnitOfWork.UserRepo.CheckUserToken(UserToken))
			{
				return NotFound("کاربر مورد نظر یافت نشد ");
			}
			return RedirectToAction(nameof(AboutUser), "Profile", new { Id = await _UnitOfWork.UserRepo.UserTokenMaper(UserToken) });
		}
		[Route("AboutUser")]
		public async Task<IActionResult> AboutUser(string Id)
		{
			AbutUserProfile abutUserProfile = new AbutUserProfile();
			try
			{
			var UserItem = await _UnitOfWork.UserRepo.GetById(Id);
			if(UserItem == null)
			{
				return NotFound();
			}
			else
			{
				abutUserProfile.HomeTown = string.IsNullOrEmpty( UserItem.HomeTown) == true? "تعیین نشده" : UserItem.HomeTown;
				abutUserProfile.MyFavoritMovie = string.IsNullOrEmpty(UserItem.MyFavoritMovie) == true ? "تعیین نشده" : UserItem.MyFavoritMovie;
				abutUserProfile.WhenImNotYelping = string.IsNullOrEmpty(UserItem.WhenImNotYelping) == true ? "تعیین نشده" : UserItem.WhenImNotYelping;
				abutUserProfile.WhyYouShouldReadMyReviews = string.IsNullOrEmpty(UserItem.WhyYouShouldReadMyReviews) == true ? "تعیین نشده" : UserItem.WhyYouShouldReadMyReviews; ;
				return Ok(abutUserProfile);
			}
			}
			catch (Exception)
			{
				throw; 
			}
		}

		[Route("Register")]
		public async Task<IActionResult> OnPostAsync(UserInputModel Input)
		{
			try
			{
			var user = new BizAppUser
			{
				FullName = Input.FullName,
				Mobile = Input.Mobile,
				UserName = Input.UserName,
				Email = Input.Email,
				PostalCode = Input.PostalCode,
				CityId = Input.CityId
			};
			if (Input.Year > 0 && (Input.Month > 0 && Input.Month < 13) && (Input.Day > 0 && Input.Day < 32))
			{
				var persianDate = $"{Input.Year}/{Input.Month:00}/{Input.Day:00}";
				var geoBirthDate = persianDate.ToGeorgianDateTime();
				user.BirthDate = geoBirthDate;
			}
			using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
			var result = await _userManager.CreateAsync(user, Input.Password);
			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, UserConfiguration.MemberRoleName);
				scope.Complete();
				scope.Dispose();
				return Ok(user);
			}
				return BadRequest();
			}
			catch(Exception)
			{
				throw; 
			}
		}

		[Route("Login")]
		public async Task<IActionResult> UserLogin(UserLoginModel Input)
		{
			var user = await _userManager.FindByNameAsync(Input.UserName);

			if (user == null)
			{
				return NotFound("نام کاربری صحیح نیست");
			}
			if (!user.IsEnabled)
			{
				return NotFound("حساب کاربری شما غیرفعال است");
			}
			var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, false, lockoutOnFailure: false);
			if (result.Succeeded)
			{
				return Ok(user.SecurityStamp);
			}
			else
			{
				return NotFound("رمز عبور صحیح نیست ");
			}
		}

		[Route("OwnerProfile")]
		public async Task<IActionResult> OwnerProfile()
		{
			string UserToken = HttpContext.Request?.Headers["Token"];
			if (!await _UnitOfWork.UserRepo.CheckUserToken(UserToken))
			{
				return NotFound("کاربر مورد نظر یافت نشد ");
			}
			UserProfile userProfile = new UserProfile();
			if (await _UnitOfWork.UserRepo.GetById(await _UnitOfWork.UserRepo.UserTokenMaper(UserToken)) == null)
			{ 
				return NotFound();
			}
			try
			{
				return RedirectToAction(nameof(GetUsersInformation), "Profile", new { Id = await _UnitOfWork.UserRepo.UserTokenMaper(UserToken)});
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

		[Route("CalculateUserProfileCompleted")]
		public async Task<IActionResult> CalculateUserProfileCompleted()
		{
			string UserToken = HttpContext.Request?.Headers["Token"];
			if (!await _UnitOfWork.UserRepo.CheckUserToken(UserToken))
			{
				return NotFound("کاربر مورد نظر یافت نشد ");
			}

			try
			{
				return Ok(await _UnitOfWork.ProfileRepo.CalculateUserProfileCompleted(await _UnitOfWork.UserRepo.UserTokenMaper(UserToken)));
			}
			catch(Exception e)
			{
				return BadRequest(e.Message);
			}
		}
		[Route("UserImpact")]
		public async Task<IActionResult> UserImpact()
		{
			string UserToken = HttpContext.Request?.Headers["Token"];
			if (!await _UnitOfWork.UserRepo.CheckUserToken(UserToken))
			{
				return NotFound("کاربر مورد نظر یافت نشد ");
			}

			try
			{
				return Ok(await _UnitOfWork.ProfileRepo.UserImpact(await _UnitOfWork.UserRepo.UserTokenMaper(UserToken)));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}


		}



	}

}
