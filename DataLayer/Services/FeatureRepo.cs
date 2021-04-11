using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
    }
}
