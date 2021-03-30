using DataLayer.Data;
using DataLayer.Extensions;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Businesses;
using DomainClass.Businesses.Commands;
using DomainClass.Businesses.Queries;
using DomainClass.Infrastructure;
using DomainClass.Queries;
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
                        FeatureName = item.Feature.Name
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
            int CatId = searchViewModel.CategoryId;
            var allCategories = DbContext.Categories;
            //var category = DbContext.Categories.FirstOrDefault(w => w.Id == searchViewModel.CategoryId);
            //var allChildCategory = allCategories.Where(w => w.ParentCategoryId == category.ParentCategoryId);
            var allChildCategory = allCategories.Where(w => w.ParentCategoryId == searchViewModel.CategoryId);
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
                var subcatFirstId = 0;
                foreach (var sub in subcatsFinder)
                {
                    subcatFirstId = allCategories.FirstOrDefault(w => w.Name.Replace("\t", "") == sub.Replace("-", " ")).Id;
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
            //while (CatId > 0)
            //{
            //    var parent = DbContext.Categories.FirstOrDefault(f => f.Id == CatId);
            //    if (parent != null)
            //    {
            //        cats.Add(parent.Id);
            //    }
            //    CatId = parent.ParentCategoryId == null ? 0 : (int)parent.ParentCategoryId;
            //}
            result = DbContext.Businesses.Where(x => cats.Contains(x.CategoryId)).OrderByDescending(x => x.CreatedDate);
            PagedList<Business> res = new PagedList<Business>(result, searchViewModel.page, 10);
            return res;
        }
    }
}
