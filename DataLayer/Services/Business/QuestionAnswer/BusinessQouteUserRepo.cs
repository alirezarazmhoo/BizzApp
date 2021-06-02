using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Businesses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class BusinessQouteUserRepo : RepositoryBase<BusinessQouteUser>, IBusinessQouteUserRepo
	{
		public BusinessQouteUserRepo(ApplicationDbContext DbContext) : base(DbContext)
		{
		}
		public async Task Add(Guid BusinessId, List<string> AllAnswerQoute,string BizAppUserId)
		{
			List<BusinessQouteUser> BusinessQouteUsers = new List<BusinessQouteUser>();
            foreach (var item in AllAnswerQoute)
            {
				BusinessQouteUser businessQouteUsers = new BusinessQouteUser();
				string[] values = item.Split('&');
				businessQouteUsers.BusinessId = BusinessId;
				businessQouteUsers.BizAppUserId = BizAppUserId;
				businessQouteUsers.AnswerQoute = values[0];
				businessQouteUsers.BusinessQouteId = Convert.ToInt32( values[1]);
				BusinessQouteUsers.Add(businessQouteUsers);
            }
			DbContext.BusinessQouteUsers.AddRange(BusinessQouteUsers);
			DbContext.SaveChanges();
		}

		
	}
}
