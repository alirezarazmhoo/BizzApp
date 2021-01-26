using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass.Businesses;
using DomainClass.Businesses.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class BusinessRepo : RepositoryBase<Business>, IBusinessRepo
	{
		public BusinessRepo(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task Add(Business model , IFormFile mainimage, IFormFile[] otherimages)
		{
			string fileName = string.Empty;
			string filePath = string.Empty; 
			 fileName = Guid.NewGuid().ToString().Replace('-', '0') + Path.GetExtension(mainimage.FileName).ToLower(); ;
			 filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload\Bussiness\Files", fileName);
			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				mainimage.CopyTo(fileStream);
				model.FeatureImage = "/Upload/Bussiness/Files/" + fileName; ;
			}
			DbContext.Businesses.Add(model);
			DbContext.SaveChanges();
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
						BusinessId = model.Id , 
					 FileAddress = "/Upload/Bussiness/Files/" + fileName,
					}) ;
				}
			}
		
		}
		public void Update(Business model)
		{
			Update(model);
		}
		public async Task<List<BusinessListQuery>> GetAll()
		{
			return 
				await 
					FindAll().Select(s => new BusinessListQuery
					{
						Id = s.Id,
						Name = s.Name,
						DistrictName = s.District.Name,
						CategoryName = s.Category.Name,
						 CityName = s.City.Name
					})
					.ToListAsync();
		}

		public async Task<List<BusinessListQuery>> GetAll(string searchString)
		{
			return 
				await 
					FindByCondition(f => (f.Name.Contains(searchString) || f.District.Name.Contains(searchString) || f.Category.Name.Contains(searchString)))					
						.Select(s => new BusinessListQuery
						{
							Id = s.Id,
							Name = s.Name,
							DistrictName = s.District.Name,
							CategoryName = s.Category.Name
						})
					.ToListAsync();
		}

		public async Task<Business> GetById(Guid id)
		{
			return await FindByCondition(f => f.Id == id).Include(s=>s.Galleries).FirstOrDefaultAsync();
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


			if(id.HasValue )
			{
				
				var CurrentFeatures = await DbContext.BusinessFeatures.Include(s=>s.Feature).Where(s => s.BusinessId.Equals(id)).ToListAsync();
				var AllFeatures = await DbContext.Features.ToListAsync();
				foreach (var item in CurrentFeatures)
				{
					MainList.Add(new AllBusinessFeatureViewModel() {  FeatureId = item.FeatureId, IsInFeature = true , FeatureName  =  item.Feature.Name  });
				}
				foreach (var item in AllFeatures)
				{
					if(!CurrentFeatures.Any(s=>s.FeatureId == item.Id))
					{
						MainList.Add(new AllBusinessFeatureViewModel() {  FeatureId = item.Id,FeatureName = item.Name, IsInFeature = false }); 
					}
				}
				return MainList.OrderByDescending(s => s.Id); 
			}
			else
			{
				return null;  
			}
		}
		public async Task AssignFeature(Guid? id , int FeatureId)
		{
			BusinessFeature businessFeature = new BusinessFeature();
			businessFeature.FeatureId = FeatureId;
			businessFeature.BusinessId = id.Value;
			await DbContext.BusinessFeatures.AddAsync(businessFeature);
		}
		public async Task RemoveFeature(Guid? id, int FeatureId)
		{
			BusinessFeature businessFeature =await DbContext.BusinessFeatures.FirstOrDefaultAsync(s=>s.BusinessId.Equals(id) && s.FeatureId == FeatureId);
			if(businessFeature != null)
			{
				DbContext.BusinessFeatures.Remove(businessFeature);

			}
		}
	}
}
