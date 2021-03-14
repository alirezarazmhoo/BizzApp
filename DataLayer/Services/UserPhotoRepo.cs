using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class UserPhotoRepo : RepositoryBase<ApplicationUserMedia>, IUserPhotoRepo
	{
		private readonly string directoryPath;
		private readonly string databasePath;
		public UserPhotoRepo(ApplicationDbContext dbContext) : base(dbContext)
		{
			directoryPath = @"wwwroot\Upload\Profile\Files\";
			databasePath = "/Upload/Profile/Files/";
		}

		public async Task<IEnumerable<ApplicationUserMedia>> GetAll(string userId)
		{
			// get list of user photos
			var items = await FindByCondition(f => f.BizAppUserId == userId).ToListAsync();
			return items;
		}

		private string UploadPhoto(IFormFile file)
		{
			try
			{
				var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(file.FileName).ToLower();
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), directoryPath, fileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					file.CopyTo(stream);
				}

				return fileName;
			}
			catch
			{
				return null;
			}

		}

		public async Task UploadPhoto(string userId, IFormFile[] files)
		{
			// upload photos in directory
			string fileName;
			var addedItems = new List<ApplicationUserMedia>();
			if (files != null && files.Count() > 0)
			{
				foreach (var file in files)
				{
					fileName = UploadPhoto(file);

					// if file not uploadded
					if (fileName == null) continue;

					// save info in memeory
					addedItems.Add(new ApplicationUserMedia()
					{
						BizAppUserId = userId,
						Status = StatusEnum.Accepted,
						IsNew = true,
						UploadedPhoto = databasePath + fileName
					});
				}
			}

			// check if user not have primary photo set for him or her
			var hasPhoto = await FindByCondition(f => f.BizAppUserId == userId).AnyAsync();
			if (!hasPhoto && addedItems.Count > 0)
			{
				addedItems.First().IsMainImage = true;
			}

			// add new items to database
			await DbContext.ApplicationUserMedias.AddRangeAsync(addedItems);

			// save changes in database 
			await DbContext.SaveChangesAsync();
		}

		public async Task DeletePhoto(Guid id)
		{
			// find photo
			var photo = await DbContext.ApplicationUserMedias.FirstOrDefaultAsync(f => f.Id == id);

			// delete from database
			if (!string.IsNullOrEmpty(photo.UploadedPhoto))
			{
				File.Delete($"wwwroot/{photo.UploadedPhoto}");
			}

			// delete from database
			DbContext.ApplicationUserMedias.Remove(photo);

			await DbContext.SaveChangesAsync();
		}

		public async Task SetAsPrimary(Guid id, string userId)
		{
			// get selected photo
			var photo = await FindByCondition(f => f.Id == id).FirstOrDefaultAsync();
			photo.IsMainImage = true;

			// get other user photos
			var userPhotos = await FindByCondition(f => f.BizAppUserId == userId).ToListAsync();
			foreach (var userPhoto in userPhotos)
			{
				userPhoto.IsMainImage = false;
			}

			// save all caanges
			await DbContext.SaveChangesAsync();
		}
	}
}
