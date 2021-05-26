
using DomainClass.Businesses;
using DomainClass.Enums;
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
		Task<BusinessFaq> GetBusinessFaqById(Guid Id);
		Task<int> AnswerCount(Guid Id);
		Task<CommunityVoteType> AddHelpFull(Guid Id, string UserId);
		Task<CommunityVoteType> AddNotHelpFull(Guid Id, string UserId);
		Task RemoveFaqAnswer(Guid Id);
		Task EditFactAnswer(BusinessFaqAnswer model);
		Task<BusinessFaqAnswer> GetBusinessFaqAnswerById(Guid Id); 
	}
}
