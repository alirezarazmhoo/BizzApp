using DataLayer.Data;
using DataLayer.Extensions;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Businesses;
using DomainClass.Businesses.Commands;
using DomainClass.Businesses.Queries;
using DomainClass.Infrastructure;
using DomainClass.Queries;
using DomainClass.Review;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;

namespace DataLayer.Services
{
	public class BusinessRepo : RepositoryBase<Business>, IBusinessRepo
	{
		private readonly ClaimsPrincipal _currentUser;
		private readonly UserManager<BizAppUser> _userManager;
		public BusinessRepo(ApplicationDbContext dbContext, ClaimsPrincipal currentUser, UserManager<BizAppUser> userManager) : base(dbContext)
		{
			_currentUser = currentUser;
			_userManager = userManager;
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
		private async Task<string> CreateOwner(long? mobile, long callNumber, string businessName)
		{
			// set username and mobile number
			string username;
			if (mobile == null) mobile = 0;
			// if owner have mobile number
			if (mobile > 0)
			{
				username = mobile.ToString();
			}
			else // if owner not have mobile number
			{
				// user CallNumber for username
				username = callNumber.ToString();
			}

			// Create owner password with Mobile Number or Call Number
			var fourDigit = username.ToString().Substring(4, 6);
			var owner = new BizAppUser
			{
				UserName = username,
				NormalizedUserName = username.Normalize().ToUpper(),
				LockoutEnabled = false,
				SecurityStamp = Guid.NewGuid().ToString(),
				Mobile = (long)mobile,
				FullName = "مالک " + businessName,
				Password = fourDigit
			};

			// Create New User with his/him role
			var result = await _userManager.CreateAsync(owner, owner.Password);
			if (result.Succeeded)
			{
				var result2 = await _userManager.AddToRoleAsync(owner, UserConfiguration.OwnerRoleName);
			}
			else
			{
				throw new Exception("User Exists");
			}
			return owner.Id;
		}
		private async Task<int?> CreateOrFindDistrict(int cityId)
		{
			// Create 'بدون ناحیه' district
			var district = new District
			{
				CityId = cityId,
				Name = "بدون ناحیه"
			};

			DbContext.Districts.Add(district);
			await DbContext.SaveChangesAsync();

			return district.Id;
		}
		public async Task Create(CreateBusinessCommand model, bool hasCity, IFormFile mainimage, IFormFile[] otherimages)
		{
			string ownerId = null;
			Business entity;

			// Create New User with his/him role

			// create owner
			try
			{
				ownerId = await CreateOwner(model.Mobile, model.CallNumber, model.Name);
			}
			catch (Exception ex)
			{
				var owner = DbContext.Users.FirstOrDefault(f => f.UserName == model.Mobile.ToString() || f.UserName == model.CallNumber.ToString());
				if (owner != null)
					ownerId = owner.Id;
				else
					throw ex;
			}

			if (hasCity)
			{
				try
				{
					model.DistrictId = Convert.ToInt32(await CreateOrFindDistrict(model.DistrictId));
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}

			// upload feature image
			model.FeatureImage = UploadFeutreImage(mainimage);

			// save business in database
			entity = new Business
			{
				Name = model.Name,
				Address = model.Address,
				Biography = model.Biography,
				CallNumber = model.CallNumber,
				CategoryId = model.CategoryId,
				Description = model.Description,
				DistrictId = model.DistrictId,
				Email = model.Email,
				FeatureImage = model.FeatureImage,
				Latitude = model.Latitude,
				Longitude = model.Longitude,
				PostalCode = model.PostalCode,
				WebsiteUrl = model.WebsiteUrl,
				UserCreatorId = model.UserCreatorId,
				OwnerId = ownerId
			};

			DbContext.Businesses.Add(entity);
			await DbContext.SaveChangesAsync();

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
						BusinessId = entity.Id,
						FileAddress = "/Upload/Bussiness/Files/" + fileName,
					});
				}
			}
			//DbContext.SaveChanges();

		}
		public async Task Update(Business model, bool hasCity, IFormFile mainImage, IFormFile[] gallery)
		{
			// Upload new feature image
			model.FeatureImage = UploadFeutreImage(mainImage);

			// Delete old feature iamge
			var oldEntity = await DbContext.Businesses.FirstOrDefaultAsync(f => f.Id == model.Id);
			// if business has imiage and user select new featrue image
			if (!string.IsNullOrEmpty(oldEntity.FeatureImage) && !string.IsNullOrEmpty(model.FeatureImage))
			{
				// delete old feature image
				File.Delete($"wwwroot/{oldEntity.FeatureImage}");
				model.FeatureImage = model.FeatureImage;
			}

			if (!string.IsNullOrEmpty(model.FeatureImage))
			{
				oldEntity.FeatureImage = model.FeatureImage;
			}


			if (hasCity)
			{
				try
				{
					model.DistrictId = Convert.ToInt32(await CreateOrFindDistrict(model.DistrictId));
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}

			oldEntity.Name = model.Name;
			oldEntity.Address = model.Address;
			oldEntity.Biography = model.Biography;
			oldEntity.CallNumber = model.CallNumber;
			oldEntity.CategoryId = model.CategoryId;
			oldEntity.Description = model.Description;
			oldEntity.DistrictId = model.DistrictId;
			oldEntity.Email = model.Email;
			oldEntity.Latitude = model.Latitude;
			oldEntity.Longitude = model.Longitude;
			oldEntity.PostalCode = model.PostalCode;
			oldEntity.WebsiteUrl = model.WebsiteUrl;
			string fileName, filePath;
			if (gallery != null && gallery.Count() > 0)
			{
				foreach (var item in gallery)
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
			return await FindByCondition(f => f.Id == id).Include(s => s.Galleries).Include(s => s.Reviews).ThenInclude(s => s.BizAppUser).ThenInclude(s => s.ApplicationUserMedias).Include(s => s.Reviews).ThenInclude(s => s.ReviewMedias).Include(s => s.District).Include(s => s.District).ThenInclude(s => s.City).ThenInclude(s => s.Province).Include(s => s.Category).FirstOrDefaultAsync();
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
			var MainList = new List<AllBusinessFeatureViewModel>();

			if (id.HasValue)
			{
				var currentFeatures = await DbContext.BusinessFeatures.Include(s => s.Feature).Where(s => s.BusinessId.Equals(id)).ToListAsync();
				var AllFeatures = await DbContext.Features.ToListAsync();
				foreach (var item in currentFeatures)
				{
					MainList.Add(new AllBusinessFeatureViewModel
					{
						FeatureId = item.FeatureId,
						Value = item.Value,
						IsInFeature = true,
						FeatureName = item.Feature.Name,
						ValueType = item.Feature.ValueType
					});
				}
				foreach (var item in AllFeatures)
				{
					if (!currentFeatures.Any(s => s.FeatureId == item.Id))
					{
						MainList.Add(new AllBusinessFeatureViewModel
						{
							FeatureId = item.Id,
							FeatureName = item.Name,
							IsInFeature = false,
							ValueType = item.ValueType
						});
					}
				}
				return MainList.OrderByDescending(s => s.Id);
			}
			else
			{
				return null;
			}
		}
		public async Task AssignFeature(Guid? id, int FeatureId, string value = null)
		{
			var businessFeature = new BusinessFeature
			{
				FeatureId = FeatureId,
				BusinessId = id.Value,
				Value = value
			};

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
			// check filePath and id validation 
			if (string.IsNullOrEmpty(filePath) || id == default) return false;

			// find business and chekc it
			var business = DbContext.Businesses.FirstOrDefault(f => f.Id == id);
			if (business == null) return false;

			// remove slash(/) from start of url
			filePath = filePath.Substring(1);

			// delete file from server
			if (!string.IsNullOrEmpty(filePath))
			{
				File.Delete($"wwwroot/{filePath}");
			}

			// update featrue image in database
			business.FeatureImage = null;
			DbContext.SaveChanges();

			return true;
		}
		public PagedList<Business> GetBussiness(SearchBussinessQuery searchViewModel)
		{
			IQueryable<Business> result = null;
			List<int> cats = new List<int>();
			List<int> features = new List<int>();
			List<int> districts = new List<int>();
			int CatId = searchViewModel.CategoryId;
			var allCategories = DbContext.Categories;
			var allChildCategory = allCategories.Where(w => w.ParentCategoryId == searchViewModel.CategoryId);
			cats.Add(CatId);
			if (String.IsNullOrEmpty(searchViewModel.catsFinder))
			{
				foreach (var categoryItem in allChildCategory.ToList())
				{
					// add all childs categories to list
					cats.Add(categoryItem.Id);
				}
				foreach (var item in allChildCategory.ToList())
				{
					foreach (var item2 in allCategories)
					{
						if (item2.ParentCategoryId == item.Id)
						{
							cats.Remove(item.Id);
							cats.Add(item2.Id);
						}
					}
				}
			}
			else
			{
				string[] subcatsFinder = searchViewModel.catsFinder.Split(',');
				foreach (var sub in subcatsFinder)
				{
					if (sub.Replace("-", " ") != "")
					{
						var subcatFirstId = 0;
						var substrCat = sub.Replace("-", " ");
						var subCatFirst = DbContext.Categories.FirstOrDefault(w => w.Name.Replace("\t", "").Trim() == substrCat);
						subcatFirstId = subCatFirst.Id;
						cats.Add(subcatFirstId);
						foreach (var item2 in allCategories)
						{
							if (item2.ParentCategoryId == subcatFirstId)
							{
								cats.Add(item2.Id);
							}
						}

					}
				}
			}
			if (!String.IsNullOrEmpty(searchViewModel.featuFinder))
			{
				string[] allfeatuFinder = searchViewModel.featuFinder.Split(',');
				var featuFinderId = 0;

				foreach (var sub in allfeatuFinder)
				{
					if (sub.Replace("-", " ") != "")
					{
						featuFinderId = DbContext.Features.FirstOrDefault(w => w.Name.Replace("\t", "") == sub.Replace("-", " ")).Id;
						features.Add(featuFinderId);
					}
				}
			}
			if (!String.IsNullOrEmpty(searchViewModel.districtFinder))
			{
				string[] alldistrictFinder = searchViewModel.districtFinder.Split(',');
				foreach (var sub in alldistrictFinder)
				{
					if (sub.Replace("-", " ") != "")
					{
						var province = DbContext.Provinces.FirstOrDefault(w => w.Name.Replace("\t", "") == sub.Replace("-", " "));
						if (province != null)
						{
							var allCities = DbContext.Cities.Where(x => x.ProvinceId == province.Id).Select(x => x.Id).ToList();
							var allDistrict = DbContext.Districts.Where(x => allCities.Contains(x.CityId)).Select(x => x.Id).ToList();
							districts.AddRange(allDistrict);

						}
						var city = DbContext.Cities.FirstOrDefault(w => w.Name.Replace("\t", "") == sub.Replace("-", " "));
						if (city != null)
						{
							var allDistrict = DbContext.Districts.Where(x => x.CityId == city.Id).Select(x => x.Id).ToList();
							districts.AddRange(allDistrict);
						}
					}
				}
			}
			result = DbContext.Businesses.Where(x => cats.Contains(x.CategoryId)).OrderByDescending(x => x.CreatedDate).ThenBy(x => x.IsSponsor);
			if (districts.Count() > 0)
			{
				result = result.Where(x => districts.Contains(x.DistrictId));
			}
			if (features.Count() > 0)
			{
				var bussinessFeature = DbContext.BusinessFeatures.Where(x => features.Contains(x.FeatureId)).Select(x => x.BusinessId).ToList();
				result = result.Where(x => bussinessFeature.Contains(x.Id));
			}
			PagedList<Business> res = new PagedList<Business>(result, searchViewModel.page, 10);
			return res;
		}
		public async Task<string> GetBusinessName(Guid Id)
		{
			var BusinessItem = await DbContext.Businesses.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			if (BusinessItem != null)
			{
				return BusinessItem.Name;
			}
			else
			{
				return string.Empty;
			}
		}
		public async Task<IEnumerable<Business>> GetBusinessOnMap(int? Id, double Longitude, double Latitude)
		{
			var CategroyItem = await DbContext.Categories.FirstOrDefaultAsync(s => s.Id.Equals(Id.Value));
			List<Business> businesses = new List<Business>();
			List<Business> BusinessItems = new List<Business>();
			List<Category> categoryItem = new List<Category>();
			List<Category> categoryItem2 = new List<Category>();
			List<Category> categoryItem3 = new List<Category>();
			List<Category> categoryItem4 = new List<Category>();
			List<Category> categoryItem5 = new List<Category>();
			List<Category> categoryItem6 = new List<Category>();
			List<Category> finalList = new List<Category>();
			bool searchBySubCategorys = false;
			if (await DbContext.Categories.AnyAsync(s => s.ParentCategoryId == Id))
			{
				categoryItem = await DbContext.Categories.Where(s => s.ParentCategoryId == Id).ToListAsync();
				foreach (var item in categoryItem)
				{
					if (!item.ParentCategoryId.HasValue)
					{
						finalList.Add(item);
					}
					else
					{
						categoryItem2 = await DbContext.Categories.Where(s => s.ParentCategoryId == item.Id).ToListAsync();
						if (categoryItem2.Count == 0)
						{
							finalList.Add(item);
							searchBySubCategorys = true;
						}
						else
						{
							foreach (var item2 in categoryItem2)
							{

								categoryItem3 = await DbContext.Categories.Where(s => s.ParentCategoryId == item2.Id).ToListAsync();
								if (categoryItem3.Count == 0)
								{
									finalList.Add(item2);
									searchBySubCategorys = true;

								}
								foreach (var item3 in categoryItem3)
								{

									categoryItem4 = await DbContext.Categories.Where(s => s.ParentCategoryId == item3.Id).ToListAsync();
									if (categoryItem4.Count == 0)
									{
										finalList.Add(item3);
										searchBySubCategorys = true;

									}
									foreach (var item4 in categoryItem4)
									{

										categoryItem5 = await DbContext.Categories.Where(s => s.ParentCategoryId == item4.Id).ToListAsync();
										if (categoryItem5.Count == 0)
										{
											finalList.Add(item4);
											searchBySubCategorys = true;

										}
										foreach (var item5 in categoryItem5)
										{
											categoryItem6 = await DbContext.Categories.Where(s => s.ParentCategoryId == item5.Id).ToListAsync();
											if (categoryItem6.Count == 0)
											{
												finalList.Add(item5);
												searchBySubCategorys = true;

											}
										}
									}
								}
							}
						}
					}
				}
			}
			if ((CategroyItem != null && Id != 0) || searchBySubCategorys)
			{
				BusinessItems = await DbContext.Businesses.Include(s => s.Reviews).Include(s => s.Galleries).Include(s => s.District).Include(s => s.Category).Include(s => s.Features).Where(s => s.CategoryId.Equals(CategroyItem.Id) || finalList.Select(s => s.Id).Contains(s.CategoryId)).ToListAsync();
				foreach (var item in BusinessItems)
				{

					if (GetDistance.distance(Latitude, Longitude, item.Latitude, item.Longitude, 'K') < 10)
					{
						businesses.Add(item);
					}
				}
				return businesses;
			}
			else if (Id == 0)
			{
				BusinessItems = await DbContext.Businesses.Include(s => s.Reviews).Include(s => s.Galleries).Include(s => s.District).Include(s => s.Category).Include(s => s.Features).ToListAsync();
				foreach (var item in BusinessItems)
				{
					if (GetDistance.distance(Latitude, Longitude, item.Latitude, item.Longitude, 'K') < 10)
					{
						businesses.Add(item);
					}
				}
				return businesses;
			}
			else
			{
				return new List<Business>();
			}
		}
		public async Task<bool> CheckBisinessFavorit(Guid Id, string UserId)
		{
			return await DbContext.UserFavorits.AnyAsync(s => s.BizAppUserId.Equals(UserId) && s.BusinessId.Equals(Id));
		}
		public async Task<IEnumerable<CustomerBusinessMedia>> GetCustomerBusinessMedia(Guid Id)
		{
			var MainItem = await GetById(Id);
			if (MainItem != null)
			{
				return await DbContext.CustomerBusinessMedias.Include(s => s.CustomerBusinessMediaPictures).Include(s => s.BizAppUser).ThenInclude(s => s.ApplicationUserMedias).Include(s => s.BizAppUser).Where(s => s.BusinessId.Equals(Id) && s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted).ToListAsync();
			}
			else
			{
				return new List<CustomerBusinessMedia>();
			}
		}
		public async Task<IEnumerable<BusinessGallery>> GetBusinessGallery(Guid Id)
		{
			var MainItem = await GetById(Id);
			if (MainItem != null)
			{
				return await DbContext.BusinessGalleries.Where(s => s.BusinessId.Equals(Id)).ToListAsync();
			}
			else
			{
				return new List<BusinessGallery>();
			}

		}
		public async Task<IEnumerable<Business>> GetByCategoryIdBasedLocation(int CategoryId, int CityId)
		{
			List<Business> businesses = new List<Business>();
			var Items = await DbContext.Businesses.Where(s => s.District.CityId.Equals(CityId)).ToListAsync();
			foreach (var item in Items)
			{
				if (item.CategoryId == CategoryId)
				{
					businesses.Add(item);
				}
				else
				{
					var categoryItem1 = await DbContext.Categories.Where(s => s.Id.Equals(item.CategoryId)).FirstOrDefaultAsync();
					var parentcategoryItem1 = await DbContext.Categories.Where(s => s.ParentCategoryId.Equals(categoryItem1.Id)).FirstOrDefaultAsync();
					if (item.CategoryId == parentcategoryItem1.Id)
					{
						businesses.Add(item);
					}
					else
					{
						var categoryItem2 = await DbContext.Categories.Where(s => s.Id.Equals(categoryItem1.Id)).FirstOrDefaultAsync();
						var parentcategoryItem2 = await DbContext.Categories.Where(s => s.ParentCategoryId.Equals(categoryItem2.Id)).FirstOrDefaultAsync();
						if (item.CategoryId == parentcategoryItem2.Id)
						{
							businesses.Add(item);
						}
						else
						{
							var categoryItem3 = await DbContext.Categories.Where(s => s.Id.Equals(categoryItem2.Id)).FirstOrDefaultAsync();
							var parentcategoryItem3 = await DbContext.Categories.Where(s => s.ParentCategoryId.Equals(categoryItem3.Id)).FirstOrDefaultAsync();
							if (item.CategoryId == parentcategoryItem3.Id)
							{
								businesses.Add(item);
							}
							else
							{
								var categoryItem4 = await DbContext.Categories.Where(s => s.Id.Equals(categoryItem3.Id)).FirstOrDefaultAsync();
								var parentcategoryItem4 = await DbContext.Categories.Where(s => s.ParentCategoryId.Equals(categoryItem4.Id)).FirstOrDefaultAsync();
								if (item.CategoryId == parentcategoryItem4.Id)
								{
									businesses.Add(item);
								}
								else
								{
									var categoryItem5 = await DbContext.Categories.Where(s => s.Id.Equals(categoryItem4.Id)).FirstOrDefaultAsync();
									var parentcategoryItem5 = await DbContext.Categories.Where(s => s.ParentCategoryId.Equals(categoryItem5.Id)).FirstOrDefaultAsync();
									if (item.CategoryId == parentcategoryItem5.Id)
									{
										businesses.Add(item);
									}
									else
									{
										continue;
									}
								}
							}
						}
					}



				}

			}
			return businesses;
		}
		public async Task<IEnumerable<Business>> SearchBusinessByTitle(string txtSearch, int DistrictId, double Longitude, double Latitude)
		{
			List<Business> List = new List<Business>();
			List<Business> GeoList = new List<Business>();
			List = await DbContext.Businesses.Include(s => s.District).Include(s => s.District).ThenInclude(s => s.City).ToListAsync();
			if (!string.IsNullOrEmpty(txtSearch))
			{
				List = List.Where(s => s.Name.Contains(txtSearch)).ToList();
			}
			if (DistrictId != 0)
			{
				List = List.Where(s => s.DistrictId == DistrictId).ToList();
			}
			if (Longitude != 0 && Latitude != 0 && DistrictId == 0)
			{
				foreach (var item in List)
				{
					if (GetDistance.distance(Latitude, Longitude, item.Latitude, item.Longitude, 'K') < 10)
					{
						GeoList.Add(item);
					}
				}
				List.Clear();
				List = GeoList;

			}
			return List;
		}
		public async Task UpdateFrequenstlyFeature(Guid Id, string FeaturesLists)
		{
			var Item = await DbContext.Businesses.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			List<BusinessFeature> businessFeatures = new List<BusinessFeature>();
			if (Item != null)
			{
				var o = await DbContext.BusinessFeatures.Where(s => s.BusinessId.Equals(Id)).ToListAsync();
				foreach (var item in await DbContext.BusinessFeatures.Where(s => s.BusinessId.Equals(Id)).ToListAsync())
				{
					DbContext.BusinessFeatures.Remove(item);
				}
				if (!string.IsNullOrEmpty(FeaturesLists))
				{
					string[] _featuresIdList = new string[] { };
					_featuresIdList = FeaturesLists.Split(",");
					foreach (var item in _featuresIdList)
					{
						businessFeatures.Add(new BusinessFeature() { BusinessId = Id, FeatureId = Convert.ToInt32(item) });
					}
					await DbContext.BusinessFeatures.AddRangeAsync(businessFeatures);
				}
			}
		}
		public string GetHiera(List<Category> categories, Category category, int parentId)
		{
			string hierarchy = "0000" + category.Id + " ";
			Category parentComm = categories.Where(a => a.Id == parentId).FirstOrDefault();
			if (parentComm.ParentCategoryId.HasValue)
			{
				hierarchy = GetHiera(categories, parentComm, parentComm.ParentCategoryId.Value) + hierarchy;
			}
			else
			{
				hierarchy = "0000" + parentId + " " + hierarchy + " ";
			}
			return hierarchy;
		}
		public async Task UpdateBaseInformations(Business business)
		{
			var Item = await DbContext.Businesses.FirstOrDefaultAsync(s => s.Id.Equals(business.Id));
			if (Item != null)
			{
				if (!string.IsNullOrEmpty(business.Address))
				{
					Item.Address = business.Address;
				}
				if (business.Longitude != 0 && business.Latitude != 0)
				{
					Item.Latitude = business.Latitude;
					Item.Longitude = business.Longitude;
				}
				if (!string.IsNullOrEmpty(business.WebsiteUrl))
				{
					Item.WebsiteUrl = business.WebsiteUrl;
				}
				if (business.CallNumber != 0)
				{
					Item.CallNumber = business.CallNumber;
				}
			}



		}
		public async Task UpdateBusinessTime(List<BusinessTime> times, Guid businessId)
		{
			List<BusinessTime> businessTimes = new List<BusinessTime>();
			var businessItem = await DbContext.Businesses.FirstOrDefaultAsync(s => s.Id.Equals(businessId));
			TimeSpan fromTime = new TimeSpan();
			TimeSpan toTime = new TimeSpan();
			if (businessItem != null)
			{
				DbContext.BusinessTimes.RemoveRange(await DbContext.BusinessTimes.Where(s => s.BusinessId.Equals(businessId)).ToListAsync());
				foreach (var item in times)
				{
					businessTimes.Add(new BusinessTime() { BusinessId = businessId, Day = item.Day, FromTime = fromTime.Add(new TimeSpan(item.FromTime.Hours, item.FromTime.Minutes, 0)), ToTime = toTime.Add(new TimeSpan(item.ToTime.Value.Hours, item.ToTime.Value.Minutes, 0)) });
				}
				await DbContext.BusinessTimes.AddRangeAsync(businessTimes);
			}
		}
		public async Task<List<Guid>> GetUserBusinessesIds(string UserId)
		{
			var Items = await DbContext.Businesses.Select(s => new { s.Id, s.OwnerId, s.CreatedDate }).Where(s => s.OwnerId.Equals(UserId)).OrderByDescending(s => s.CreatedDate).ToListAsync();
			return Items.Select(s => s.Id).ToList();
		}
		public async Task UpdateBusinessFeaturesInBusinessAccount(SelectedFeaturesDto[] model, Guid BusinessId)
		{
			List<BusinessFeature> businessFeatures = new List<BusinessFeature>();
			if (await DbContext.Businesses.AnyAsync(s => s.Id.Equals(BusinessId)))
			{
				DbContext.BusinessFeatures.RemoveRange(await DbContext.BusinessFeatures.Where(s => s.BusinessId.Equals(BusinessId)).ToListAsync());
				for (int i = 0; i < model.Count(); i++)
				{
					if (model[i].value == "true")
					{

						businessFeatures.Add(new BusinessFeature() { BusinessId = BusinessId, FeatureId = model[i].id, Value = null });
					}
					else
					{
						if(!string.IsNullOrEmpty(model[i].value))
						businessFeatures.Add(new BusinessFeature() { BusinessId = BusinessId, FeatureId = model[i].id, Value = model[i].value });
					}
				}
				await DbContext.BusinessFeatures.AddRangeAsync(businessFeatures);
			}
		}

		public async Task DeleteGalleryImage(int Id)
		{
			var Item = await DbContext.BusinessGalleries.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			if(Item != null)
			{
				File.Delete($"wwwroot/{Item}");
				DbContext.BusinessGalleries.Remove(Item);
			}
		}


	}
}
