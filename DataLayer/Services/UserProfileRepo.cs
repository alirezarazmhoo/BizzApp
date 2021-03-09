using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DataLayer.Extensions;

namespace DataLayer.Services
{
	public class UserProfileRepo : RepositoryBase<BizAppUser>, IUserProfileRepo
	{
		public UserProfileRepo(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<UserProfileDetailQuery> GetUserDetail(string userId)
		{
			try
			{
				var userDetail = await
					DbContext.Users
						.Select(s => new UserProfileDetailQuery
						{
							Id = s.Id,
							FullName = s.FullName,
							RegisterDate = s.CreateDate,
							CityName = s.City.Name,
							ProvinceName = s.City.Province.Name,
							//UploadedPhotoCount = s.CustomerBusinessMedia.Count,
							ReviewCount = s.Reviews.Count,
							//Photos = (s.ApplicationUserMedias.Select(s => s.UploadedPhoto).ToList())
						})
						.FirstOrDefaultAsync(f => f.Id == userId);

				return userDetail;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
