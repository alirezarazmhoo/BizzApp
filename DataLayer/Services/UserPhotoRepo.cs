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
			directoryPath = @"wwwroot\Upload\User\Profiles\";
			databasePath = "/Upload/User/Profiles/";
		}

		private async Task<string> UploadPhoto(IFormFile file, string userId)
		{
			try
			{
				// Create the folder if not existing for a full file name
				if (!Directory.Exists(directoryPath + userId))
				{
					Directory.CreateDirectory(directoryPath + userId);
				}

				// create file
				var fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(file.FileName).ToLower();
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), directoryPath + userId, fileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}

				return fileName;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		private async Task<bool> IsOwner(Guid id, string currentUserId) 
		{
			var photo = await DbContext.ApplicationUserMedias.FirstOrDefaultAsync(w => w.Id == id);
			return photo.BizAppUserId == currentUserId;
		}

		public async Task<IEnumerable<ApplicationUserMedia>> GetAll(string userId)
		{
			// 
			
			// get list of user photos
			var items = await FindByCondition(f => f.BizAppUserId == userId)
				.OrderByDescending(o => o.IsMainImage).ThenBy(c => c.CreatedAt)
				.ToListAsync();

			return items;
		}
		public async Task<UploadResult> UploadPhoto(string userId, IFormFile file)
		{
			if (file == null) return UploadResult.EmptyFile;

			// upload photos in directory
			string fileName;
			ApplicationUserMedia addedItem;

			fileName = await UploadPhoto(file, userId);

			// if file not uploadded
			if (fileName == null) return UploadResult.Failed;

			// save info in memeory
			addedItem = new ApplicationUserMedia
			{
				BizAppUserId = userId,
				Status = StatusEnum.Accepted,
				IsNew = true,
				UploadedPhoto = $"{databasePath}{userId}/{fileName}", 
				CreatedAt = DateTime.Now
			};

			// check if user not have primary photo set for him or her
			var hasPhoto = await FindByCondition(f => f.BizAppUserId == userId).AnyAsync();
			if (!hasPhoto)
			{
				addedItem.IsMainImage = true;
			}

			// add new items to database
			await DbContext.ApplicationUserMedias.AddAsync(addedItem);

			// save changes in database 
			await DbContext.SaveChangesAsync();

			return UploadResult.Succeed;
		}
		public async Task DeletePhoto(Guid id, string currentUserId)
		{
			var isOwner = await IsOwner(id, currentUserId);
			if (!isOwner) throw new UnauthorizedAccessException();

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
			var isOwner = await IsOwner(id, userId);
			if (!isOwner) throw new UnauthorizedAccessException();

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
