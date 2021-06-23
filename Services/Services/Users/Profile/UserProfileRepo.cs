using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Enums;
using DomainClass.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class UserProfileRepo : RepositoryBase<BizAppUser>, IUserProfileRepo
	{
		public UserProfileRepo(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<Nullable<StatusEnum>> GetFriendShipStatus(string currentUserId, string friendUserName)
		{
			var friend = await DbContext.Users.FirstOrDefaultAsync(f => f.UserName == friendUserName);
			if (friend == null) return null;

			var relation = await
				DbContext.Friends.Where(f => f.ApplicatorUserId == friend.Id ||
											 f.ReceiverUserId == friend.Id)
								 .Where(f => f.ApplicatorUserId == currentUserId ||
											 f.ReceiverUserId == currentUserId)
							.FirstOrDefaultAsync();

			if (relation == null) return null;

			return relation.Status;
		}

		public async Task<SharedUserProfileDetailQuery> GetSharedUserDetail(string userName)
		{
			try
			{
				var userDetail = await
					DbContext.Users
						.Where(w => w.UserName == userName)
						.Select(s => new SharedUserProfileDetailQuery
						{
							Id = s.Id,
							UserName = s.UserName,
							FullName = s.FullName,
							ReviewCount = s.Reviews.Count,
							MainPhotoPath = s.ApplicationUserMedias.FirstOrDefault(f => f.BizAppUserId == s.Id && f.IsMainImage).UploadedPhoto,
							FriendsNumber = DbContext.Friends.Where(w => w.ApplicatorUserId == s.Id && w.Status == StatusEnum.Accepted).Count()
						})
						.FirstOrDefaultAsync();

				return userDetail;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<UserProfileDetailQuery> GetUserDetail(string userName)
		{
			var userDetail = await
				DbContext.Users
					.Where(w => w.UserName == userName)
					.Select(s => new UserProfileDetailQuery
					{
						Id = s.Id,
						UserName = s.UserName,
						FullName = s.FullName,
						RegisterDate = s.CreateDate,
						CityName = s.City.Name,
						ProvinceName = s.City.Province.Name,
						UploadedPhotoCount = s.CustomerBusinessMedia.Count,
						ReviewCount = s.Reviews.Count,
						Photos = (s.ApplicationUserMedias.Select(s => s.UploadedPhoto).ToList()),
						FriendNumber = DbContext.Friends.Where(w => (w.ApplicatorUserId == s.Id || w.ReceiverUserId == s.Id) && w.Status == StatusEnum.Accepted).Count() / 2,
						MainPhotoPath = s.ApplicationUserMedias.FirstOrDefault(f => f.IsMainImage).UploadedPhoto
					})
					.FirstOrDefaultAsync();

			return userDetail;
		}

		public async Task<CalculateUserProfileCompleted> CalculateUserProfileCompleted(string UserId)
		{
			CalculateUserProfileCompleted calculateUserProfileCompleted = new CalculateUserProfileCompleted();
			calculateUserProfileCompleted.DidLikeACustomerBusinessPicture = await DbContext.UsersInCustomerBusinessMediaLikes.AnyAsync(s => s.BizAppUserId.Equals(UserId));
			calculateUserProfileCompleted.DidSaveAPictureForBusiness = await DbContext.CustomerBusinessMedias.AnyAsync(s => s.BizAppUserId.Equals(UserId));
			calculateUserProfileCompleted.HasAReview = await DbContext.Reviews.AnyAsync(s => s.BizAppUserId.Equals(UserId));
			calculateUserProfileCompleted.HasBookMark = await DbContext.UserFavorits.AnyAsync(s => s.BizAppUserId.Equals(UserId));
			calculateUserProfileCompleted.HasUseFullVoteForAReview = await DbContext.UsersInReviewVotes.AnyAsync(s => s.BizAppUserId.Equals(UserId) && s.VotesType == VotesType.HelpFull);
			return calculateUserProfileCompleted; 
		}
		public async Task<UserImpactDto> UserImpact(string UserId)
		{
			UserImpactDto userImpactDto = new UserImpactDto();
			userImpactDto.ReviewVotedAllTime = await DbContext.UsersInReviewVotes.Where(s => s.BizAppUserId.Equals(UserId)).CountAsync();
			userImpactDto.ReviewVotedUseFull = await DbContext.UsersInReviewVotes.Where(s => s.BizAppUserId.Equals(UserId) && s.VotesType== VotesType.HelpFull).CountAsync();
			userImpactDto.CustomerBusinessPictureLikedAllTime = await DbContext.UsersInCustomerBusinessMediaLikes.Where(s => s.BizAppUserId.Equals(UserId)).CountAsync();
			userImpactDto.CustomerBusinessPictureLikedInPreviousDays = await DbContext.UsersInCustomerBusinessMediaLikes.Where(s => s.BizAppUserId.Equals(UserId)).CountAsync();
			return userImpactDto; 
		}

		



	}
}
