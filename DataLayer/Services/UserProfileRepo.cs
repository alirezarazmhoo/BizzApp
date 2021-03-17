using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
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
							MainPhotoPath = s.ApplicationUserMedias.FirstOrDefault(f => f.BizAppUserId == s.Id && f.IsMainImage).UploadedPhoto
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
			try
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
							Photos = (s.ApplicationUserMedias.Select(s => s.UploadedPhoto).ToList())
						})
						.FirstOrDefaultAsync();

				return userDetail;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
