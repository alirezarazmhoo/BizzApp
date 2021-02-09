using DataLayer.Data;
using DataLayer.Extensions;
using DataLayer.Infrastructure;
using DomainClass.Businesses;
using DomainClass.Businesses.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class BusinessRepo : RepositoryBase<Business>, IBusinessRepo
	{
		private readonly ClaimsPrincipal _currentUser;

		public BusinessRepo(ApplicationDbContext dbContext, ClaimsPrincipal currentUser) : base(dbContext)
		{
			_currentUser = currentUser;
		}

		private string UploadFeutreImage(IFormFile image)
		{
			string fileName, filePath;

			// if mainImage is not null then upload it
			if (image != null)
			{
				// Upload main images
				fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(image.FileName).ToLower();
				filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Bussiness\Files", fileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					image.CopyTo(fileStream);
					return "/Upload/Bussiness/Files/" + fileName;
				}
			}

			return null;
		}

		public void Create(Business model, IFormFile mainimage, IFormFile[] otherimages)
		{
			// upload feature image
			model.FeatureImage = UploadFeutreImage(mainimage);

			// save business in database
			DbContext.Businesses.Add(model);
			//DbContext.SaveChanges();

			// upload image gallery
			string fileName, filePath;
			if (otherimages != null && otherimages.Count() > 0)
			{
				foreach (var item in otherimages)
				{
					fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(item.FileName).ToLower(); ;
					filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Bussiness\Files\", fileName);
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						item.CopyTo(stream);
					}

					DbContext.BusinessGalleries.Add(new BusinessGallery()
					{
						BusinessId = model.Id,
						FileAddress = "/Upload/Bussiness/Files/" + fileName,
					});
				}
			}
			//DbContext.SaveChanges();

		}
		public async Task Update(Business model, IFormFile mainImage, IFormFile[] gallery)
		{
			// Upload new feature image
			var newFeatureImage = UploadFeutreImage(mainImage);

			// Delete old feature iamge
			var oldEntity = await DbContext.Businesses.FirstOrDefaultAsync(f => f.Id == model.Id);
			// if business has imiage and user select new featrue image
			if (!string.IsNullOrEmpty(oldEntity.FeatureImage) && !string.IsNullOrEmpty(newFeatureImage))
			{
				// delete old feature image
				File.Delete($"wwwroot/{oldEntity.FeatureImage}");
				model.FeatureImage = newFeatureImage;
			}

			oldEntity.Name = model.Name;
			oldEntity.Address = model.Address;
			oldEntity.Biography = model.Biography;
			oldEntity.CallNumber = model.CallNumber;
			oldEntity.CategoryId = model.CategoryId;
			oldEntity.Description = model.Description;
			oldEntity.DistrictId = model.DistrictId;
			oldEntity.Email = model.Email;
			oldEntity.FeatureImage = model.FeatureImage;
			oldEntity.Latitude = model.Latitude;
			oldEntity.Longitude = model.Longitude;
			oldEntity.PostalCode = model.PostalCode;
			oldEntity.WebsiteUrl = model.WebsiteUrl;
			
			//Update(model);
			//await DbContext.SaveChangesAsync();
		}
		public async Task<List<BusinessListQuery>> GetAll()
		{
			return
				await
					FindAll()
					.ApplyRowsAuthFilter(_currentUser)
					.Select(s => new BusinessListQuery
					{
						Id = s.Id,
						Name = s.Name,
						DistrictName = s.District.Name,
						CategoryName = s.Category.Name,
						CityName = s.District.City.Name,
						CreatedDate = s.CreatedDate,
						Creator = s.UserCreator.FullName
					}).ToListAsync();
		}
		public async Task<List<BusinessListQuery>> GetAll(string userId)
		{
			return
				await
					FindByCondition(f => f.UserCreatorId == userId)
					.ApplyRowsAuthFilter(_currentUser)
					.Select(s => new BusinessListQuery
					{
						Id = s.Id,
						Name = s.Name,
						DistrictName = s.District.Name,
						CategoryName = s.Category.Name,
						CityName = s.District.City.Name,
						Creator = s.UserCreator.FullName,
						CreatedDate = s.CreatedDate
					})
					.ToListAsync();
		}
		public async Task<List<BusinessListQuery>> GetAll(string searchString, string userId = null)
		{
			var query = FindByCondition(f => (f.Name.Contains(searchString) ||
											  f.District.Name.Contains(searchString) ||
											  f.Category.Name.Contains(searchString)));

			if (userId != null)
				query = FindByCondition(f => (f.Name.Contains(searchString) ||
											  f.District.Name.Contains(searchString) ||
											  f.Category.Name.Contains(searchString))
											&& f.UserCreatorId == userId);
			return await query.ApplyRowsAuthFilter(_currentUser)
				.Select(s => new BusinessListQuery
				{
					Id = s.Id,
					Name = s.Name,
					DistrictName = s.District.Name,
					CategoryName = s.Category.Name,
					CreatedDate = s.CreatedDate,
					CityName = s.District.City.Name,
					Creator = s.UserCreator.FullName
				})

				.ToListAsync();
		}
		public async Task<Business> GetById(Guid id)
		{
			return await FindByCondition(f => f.Id == id).Include(s => s.Galleries).FirstOrDefaultAsync();
		}
		public async Task Remove(Business model)
		{
			var MainItem = await GetById(model.Id);
			if (!string.IsNullOrEmpty(model.FeatureImage))
			{
				File.Delete($"wwwroot/{MainItem.FeatureImage}");
			}
			if (MainItem.Galleries.Count > 0)
			{
				foreach (var item in MainItem.Galleries)
				{
					File.Delete($"wwwroot/{item.FileAddress}");
				}
			}
			Delete(model);
		}
		public async Task<IEnumerable<AllBusinessFeatureViewModel>> GetBusinessFature(Guid? id)
		{
			List<AllBusinessFeatureViewModel> MainList = new List<AllBusinessFeatureViewModel>();


			if (id.HasValue)
			{

				var CurrentFeatures = await DbContext.BusinessFeatures.Include(s => s.Feature).Where(s => s.BusinessId.Equals(id)).ToListAsync();
				var AllFeatures = await DbContext.Features.ToListAsync();
				foreach (var item in CurrentFeatures)
				{
					MainList.Add(new AllBusinessFeatureViewModel() { FeatureId = item.FeatureId, IsInFeature = true, FeatureName = item.Feature.Name });
				}
				foreach (var item in AllFeatures)
				{
					if (!CurrentFeatures.Any(s => s.FeatureId == item.Id))
					{
						MainList.Add(new AllBusinessFeatureViewModel() { FeatureId = item.Id, FeatureName = item.Name, IsInFeature = false });
					}
				}
				return MainList.OrderByDescending(s => s.Id);
			}
			else
			{
				return null;
			}
		}
		public async Task AssignFeature(Guid? id, int FeatureId)
		{
			BusinessFeature businessFeature = new BusinessFeature();
			businessFeature.FeatureId = FeatureId;
			businessFeature.BusinessId = id.Value;
			await DbContext.BusinessFeatures.AddAsync(businessFeature);
		}
		public async Task RemoveFeature(Guid? id, int FeatureId)
		{
			BusinessFeature businessFeature = await DbContext.BusinessFeatures.FirstOrDefaultAsync(s => s.BusinessId.Equals(id) && s.FeatureId == FeatureId);
			if (businessFeature != null)
			{
				DbContext.BusinessFeatures.Remove(businessFeature);

			}
		}
		public bool DeleteFeatureImage(Guid id, string filePath)
		{
			// update featrue image in database
			var business = DbContext.Businesses.FirstOrDefault(f => f.Id == id);
			business.FeatureImage = null;
			DbContext.SaveChanges();
			
			// remove slash(/) from start of url
			filePath = filePath.Substring(1);

			// delete file from server
			if (!string.IsNullOrEmpty(filePath))
			{
				File.Delete($"wwwroot/{filePath}");
				return true;
			}

			return false;
		}
	}
}
