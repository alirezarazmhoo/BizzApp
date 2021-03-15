﻿using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Queries;
using System;
using System.Linq;

namespace DataLayer.Services
{
	public class UserProfileRepo : RepositoryBase<BizAppUser>, IUserProfileRepo
	{
		public UserProfileRepo(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public UserProfileDetailQuery GetUserDetail(string userName)
		{
			try
			{
				var userDetail = 
					DbContext.Users
						.Where(w => w.UserName == userName)
						.Select(s => new UserProfileDetailQuery
						{
							Id = s.Id,
							UsserName = s.UserName,
							FullName = s.FullName,
							RegisterDate = s.CreateDate,
							CityName = s.City.Name,
							ProvinceName = s.City.Province.Name,
							UploadedPhotoCount = s.CustomerBusinessMedia.Count,
							ReviewCount = s.Reviews.Count,
							Photos = (s.ApplicationUserMedias.Select(s => s.UploadedPhoto).ToList())
						})
						.FirstOrDefault();

				return userDetail;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
