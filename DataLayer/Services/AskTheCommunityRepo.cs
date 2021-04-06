using DataLayer.Data;
using DataLayer.Infrastructure;

using DomainClass;
using DomainClass.Businesses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class AskTheCommunityRepo : RepositoryBase<BusinessFaq>, IAskTheCommunityRepo
	{
		public AskTheCommunityRepo(ApplicationDbContext DbContext) : base(DbContext)
		{
		}
		public async Task<IEnumerable< BusinessFaq>> GetBusinessFaq(Guid Id)
		{
			var businessItem = await DbContext.Businesses.FirstOrDefaultAsync(s=>s.Id.Equals(Id));
			if(businessItem != null)
			{
				return await DbContext.BusinessFaqs.Include(s=>s.Business).Include(s=>s.BusinessFaqAnswers).ThenInclude(s=>s.BizAppUser).ThenInclude(s=>s.ApplicationUserMedias).Where(s=>s.BusinessId.Equals(Id) && s.BusinessFaqAnswers.Count()>0).ToListAsync();
			}
			else
			{
				return null;
			}
		}
		public async Task<IEnumerable<BusinessFaqAnswer>> GetBusinessFaqAnswers(Guid Id)
		{
			var BusinessFaqItem = await DbContext.BusinessFaqs.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			if(BusinessFaqItem != null)
			{
				return await DbContext.BusinessFaqAnswers.Include(s=>s.BusinessFaq).ThenInclude(s=>s.Business).Where(s => s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted && s.BusinessFaqId.Equals(Id)).ToListAsync();
			}
			else
			{
				return new List<BusinessFaqAnswer>();
			}
		}
		public async Task AddBusinessFaqAnswers(BusinessFaqAnswer model)
		{
			await DbContext.BusinessFaqAnswers.AddAsync(model);
		}
		public async Task AddBusinessFaq(BusinessFaq model)
		{
			model.Date = DateTime.Now; 
			await DbContext.BusinessFaqs.AddAsync(model);
		}
		public async Task<BusinessFaq> GetBusinessFaqById(Guid Id)
		{
			var BusinessFaqItem = await DbContext.BusinessFaqs.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			if(BusinessFaqItem != null)
			{
				return BusinessFaqItem; 

			}
			else
			{
				return null; 
			}
		}
	}
}
