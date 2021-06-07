using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Businesses;
using DomainClass.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class FeatureRepo : RepositoryBase<Feature>, IFeatureRepo
    {
        public FeatureRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task AddOrUpdate(Feature model)
        {
            if (model.Id == 0)
                await CreateAsync(model);
            else
            {
                var Item = await DbContext.Features.FirstOrDefaultAsync(s => s.Id == model.Id);
                if (Item != null)
                {
                    Item.Name = model.Name;
                    Item.ValueType = model.ValueType;
                }
            }
        }
        public async Task<List<Feature>> GetAll()
        {
            return await FindAll().ToListAsync();
        }
        public async Task<List<Feature>> GetAll(string searchString)
        {
            return await FindByCondition(f => f.Name.Contains(searchString)).ToListAsync();
        }
        public async Task<Feature> GetById(int id)
        {
            return await FindByCondition(f => f.Id == id).FirstOrDefaultAsync();
        }
        public void Remove(Feature model)
        {
            Delete(model);
        }
        public async Task<List<Feature>> GetAllIsBoolValue()
        {
            return await FindByCondition(f => f.ValueType == BusinessFeatureType.Boolean).ToListAsync();
        }
        public async Task<List<Feature>> ExtractFeaturesByCategoryId(int CategoryId)
		{
            List<Feature>  features      = new List<Feature>();
            List<Business> businesses    = new List<Business>();
            List<Category> categoryItem2 = new List<Category>();
            List<Category> categoryItem3 = new List<Category>();
            List<Category> categoryItem4 = new List<Category>();
            List<Category> categoryItem5 = new List<Category>();
            bool NeedNext = false; 
            var Items = await DbContext.Businesses.ToListAsync();
            foreach (var item in Items)
            {
                if(item.CategoryId == CategoryId)
				{
                    businesses.Add(item);
				}
				else
				{
                    var categoryItem1 = await DbContext.Categories.Where(s => s.ParentCategoryId == CategoryId).ToListAsync();
					foreach (var item2 in categoryItem1)
					{
                        if(item.CategoryId == item2.Id)
						{
                            businesses.Add(item);
                            NeedNext = true; 
                            break;
                        }
                    }
                    if(NeedNext == false)
					{
						foreach (var item3 in categoryItem1)
						{
                          categoryItem2 = await DbContext.Categories.Where(s => s.ParentCategoryId == item3.Id).ToListAsync();
                            foreach (var item4 in categoryItem2)
                            {
                                if (item.CategoryId == item4.Id)
                                {
                                    if(businesses.Any(s=>s.Id == item.Id) == false)
									{
                                    businesses.Add(item);
                                    NeedNext = true;
                                    break;
									}
                                }
                            }
                        }
                    }
                    if(NeedNext == false)
					{
                        foreach (var item5 in categoryItem2)
						{
                        categoryItem3 = await DbContext.Categories.Where(s => s.ParentCategoryId == item5.Id).ToListAsync();
							foreach (var item7 in categoryItem3)
							{
                                if (item.CategoryId == item7.Id)
                                {
                                    if (businesses.Any(s => s.Id == item.Id) == false)
                                    {
                                        businesses.Add(item);
                                        NeedNext = true;
                                        break;
                                    }
                                }
                            }
						}
					}
					if (NeedNext)
					{
                        foreach (var item6 in categoryItem3)
                        {
                            categoryItem4 = await DbContext.Categories.Where(s => s.ParentCategoryId == item6.Id).ToListAsync();
                            foreach (var item8 in categoryItem4)
                            {
                                if (item.CategoryId == item8.Id)
                                {
                                    if (businesses.Any(s => s.Id == item.Id) == false)
                                    {
                                        businesses.Add(item);
                                        NeedNext = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (NeedNext)
                    {
                        foreach (var item7 in categoryItem4)
                        {
                            categoryItem5 = await DbContext.Categories.Where(s => s.ParentCategoryId == item7.Id).ToListAsync();
                            foreach (var item9 in categoryItem5)
                            {
                                if (item.CategoryId == item9.Id)
                                {
                                    if (businesses.Any(s => s.Id == item.Id) == false)
                                    {
                                        businesses.Add(item);
                                        NeedNext = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
			foreach (var item in businesses)
			{
				foreach (var item2 in await DbContext.BusinessFeatures.Where(s => s.BusinessId.Equals(item.Id) && s.Value == null).Select(s => s.Feature).ToListAsync())
				{
					if (features.Any(s => s.Id.Equals(item2.Id)) == false)
					{
                        features.Add(item2);
                    }
				}
			}
            return features; 
        }
    }
}
