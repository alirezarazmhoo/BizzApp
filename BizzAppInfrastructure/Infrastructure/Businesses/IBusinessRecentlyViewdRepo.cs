using DomainClass.Businesses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public	interface IBusinessRecentlyViewdRepo
	{
		Task Add(BusinessRecentlyViewed model);
		Task<IEnumerable<BusinessRecentlyViewed>> Get(string UserId);
		Task<bool> CheckDuplicate(string UserId, Guid BusinessId);
		Task Remove(Guid Id);  
	}
}
