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
		private readonly IUserActivityRepo _userActivity;

		public UserPhotoRepo(ApplicationDbContext dbContext, IUserActivityRepo userActivity) : base(dbContext)
		{
			directoryPath = @"wwwroot\Upload\User\Profiles\";
			databasePath = "/Upload/User/Profiles/";

			_userActivity = userActivity;
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
			// get list of user photos
			var items = 
				await DbContext.ApplicationUserMedias.Where(f => f.BizAppUserId == userId)
						.OrderByDescending(o => o.IsMainImage)
						.ThenBy(c => c.CreatedAt)
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
			bool hasPhoto = await DbContext.ApplicationUserMedias.AnyAsync(f => f.BizAppUserId == userId);
			if (!hasPhoto)
			{
				addedItem.IsMainImage = true;
			}

			// add new items to database
			await DbContext.ApplicationUserMedias.AddAsync(addedItem);

			// add user activity
			await _userActivity.AddAsync(TableName.UserPhotos, addedItem.Id.ToString(), userId);

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

			if (photo == null) throw new KeyNotFoundException();

			// delete from directory
			if (!string.IsNullOrEmpty(photo.UploadedPhoto))
			{
				File.Delete($"wwwroot/{photo.UploadedPhoto}");
			}

			// delete from database
			DbContext.ApplicationUserMedias.Remove(photo);
			
			// change user primary image 
			if (photo.IsMainImage)
			{
				//get user photos count
				var firstUploadedPhoto =
					await DbContext.ApplicationUserMedias.Where(w => w.BizAppUserId == currentUserId).OrderBy(x => x.CreatedAt).FirstOrDefaultAsync();

				if (firstUploadedPhoto != null)
				{
					firstUploadedPhoto.IsMainImage = true;
				}
			}

			// delete created activity
			await _userActivity.Remove(id.ToString());

			await DbContext.SaveChangesAsync();
		}
		public async Task SetAsPrimary(Guid id, string userId)
		{
			var isOwner = await IsOwner(id, userId);
			if (!isOwner) throw new UnauthorizedAccessException();

			// get selected photo
			var photo = await DbContext.ApplicationUserMedias.FirstOrDefaultAsync(f => f.Id == id);
			photo.IsMainImage = true;

			// get other user photos
			var userPhotos = await DbContext.ApplicationUserMedias.Where(f => f.BizAppUserId == userId && f.Id != id).ToListAsync();
			foreach (var userPhoto in userPhotos)
			{
				userPhoto.IsMainImage = false;
			}

			try
			{
				// save all caanges
				await DbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public async Task<ApplicationUserMedia> GetById(Guid id)
		{
			return await DbContext.ApplicationUserMedias.FirstOrDefaultAsync(f=> f.Id == id);
		}
		public async Task<string> GetPathById(Guid id)
		{
			var fileDetail = await DbContext.ApplicationUserMedias.FirstOrDefaultAsync(f => f.Id == id);
			if (fileDetail != null) return fileDetail.UploadedPhoto;

			return null;
		}

		public async Task<UploadResult> UploadPhotos(string userId, IFormFile[] files)
		{
			if (files == null) return UploadResult.EmptyFile;
			// upload photos in directory
			string fileName;
			ApplicationUserMedia addedItem;
			foreach (var item in files)
			{
			fileName = await UploadPhoto(item, userId);
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
			await DbContext.ApplicationUserMedias.AddAsync(addedItem);
			// check if user not have primary photo set for him or her
			bool hasPhoto = await DbContext.ApplicationUserMedias.AnyAsync(f => f.BizAppUserId == userId);
			if (!hasPhoto)
			{
				addedItem.IsMainImage = true;
			}
			await _userActivity.AddAsync(TableName.UserPhotos, addedItem.Id.ToString(), userId);
			}
			await DbContext.SaveChangesAsync();
			return UploadResult.Succeed;
		}

	}
}
