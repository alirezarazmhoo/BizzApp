using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass.Businesses;
using DomainClass.Businesses.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

		public async Task Add(Business model)
		{
			await CreateAsync(model);
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
						CategoryName = s.Category.Name
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
			return await FindByCondition(f => f.Id == id).FirstOrDefaultAsync();
		}

		public void Remove(Business model)
		{
			Delete(model);
		}
	}
}
