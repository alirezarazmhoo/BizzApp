
using DomainClass.Businesses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IAskTheCommunityRepo
	{
		Task<IEnumerable<BusinessFaq>> GetBusinessFaq(Guid Id);
		Task<IEnumerable<BusinessFaqAnswer>> GetBusinessFaqAnswers(Guid Id);
		Task AddBusinessFaqAnswers(BusinessFaqAnswer model);
		Task AddBusinessFaq(BusinessFaq model);
	
		
	}
}
