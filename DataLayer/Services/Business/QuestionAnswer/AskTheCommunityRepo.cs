using DataLayer.Data;
using DataLayer.Infrastructure;

using DomainClass;
using DomainClass.Businesses;
using DomainClass.Enums;
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
		public async Task<IEnumerable<BusinessFaq>> GetBusinessFaq(Guid Id)
		{
			var businessItem = await DbContext.Businesses.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			if (businessItem != null)
			{
				return await DbContext.BusinessFaqs.Include(s => s.Business).Include(s => s.BusinessFaqAnswers).ThenInclude(s => s.BizAppUser).ThenInclude(s => s.ApplicationUserMedias).Where(s => s.BusinessId.Equals(Id) && s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted).ToListAsync();
			}
			else
			{
				return null;
			}
		}
		public async Task<IEnumerable<BusinessFaqAnswer>> GetBusinessFaqAnswers(Guid Id)
		{
			var BusinessFaqItem = await DbContext.BusinessFaqs.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			if (BusinessFaqItem != null)
			{
				return await DbContext.BusinessFaqAnswers.Include(s => s.BusinessFaq).ThenInclude(s => s.Business).Include(s => s.BizAppUser).Include(s => s.BizAppUser).ThenInclude(s => s.ApplicationUserMedias).Where(s => s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted && s.BusinessFaqId.Equals(Id)).ToListAsync();
			}
			else
			{
				return new List<BusinessFaqAnswer>();
			}
		}
		public async Task AddBusinessFaqAnswers(BusinessFaqAnswer model)
		{
			model.Date = DateTime.Now;
			await DbContext.BusinessFaqAnswers.AddAsync(model);
		}
		public async Task AddBusinessFaq(BusinessFaq model)
		{
			model.Date = DateTime.Now;
			await DbContext.BusinessFaqs.AddAsync(model);
		}
		public async Task<BusinessFaq> GetBusinessFaqById(Guid Id)
		{
			var BusinessFaqItem = await DbContext.BusinessFaqs.Include(s => s.BizAppUser).FirstOrDefaultAsync(s => s.Id.Equals(Id));
			if (BusinessFaqItem != null)
			{
				return BusinessFaqItem;

			}
			else
			{
				return null;
			}
		}
		public async Task RemoveFaqAnswer(Guid Id)
		{
			var Item = await DbContext.BusinessFaqAnswers.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			if (Item != null)
			{
				DbContext.BusinessFaqAnswers.Remove(Item);
			}
		}
		public async Task<int> AnswerCount(Guid Id)
		{
			return await DbContext.BusinessFaqAnswers.Where(s => s.BusinessFaqId.Equals(Id) && s.StatusEnum == DomainClass.Enums.StatusEnum.Accepted).CountAsync();
		}
		public async Task<CommunityVoteType> AddHelpFull(Guid Id, string UserId)
		{
				UsersInCommunityVotes usersInCommunityVotes = new UsersInCommunityVotes();
				var Items = await DbContext.BusinessFaqAnswers.FirstOrDefaultAsync(s => s.Id.Equals(Id));
				var UserItem = await DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(UserId));
				if (Items != null && UserItem != null)
				{
					if (!await DbContext.UsersInCommunityVotes.AnyAsync(s => s.BizAppUserId == UserId && s.BusinessFaqAnswerId == Id && s.VotesType == DomainClass.Enums.VotesType.HelpFull) && !await DbContext.UsersInCommunityVotes.AnyAsync(s => s.BizAppUserId == UserId && s.BusinessFaqAnswerId == Id && s.VotesType == DomainClass.Enums.VotesType.NotHelpFull))
					{
						Items.HelpFullCount += 1;
						usersInCommunityVotes.BizAppUserId = UserId;
						usersInCommunityVotes.BusinessFaqAnswerId = Id;
						usersInCommunityVotes.VotesType = VotesType.HelpFull;
						await DbContext.UsersInCommunityVotes.AddAsync(usersInCommunityVotes);
						return CommunityVoteType.AddHelpFullCount;
					}
					if (!await DbContext.UsersInCommunityVotes.AnyAsync(s => s.BizAppUserId == UserId && s.BusinessFaqAnswerId == Id && s.VotesType == DomainClass.Enums.VotesType.HelpFull) && await DbContext.UsersInCommunityVotes.AnyAsync(s => s.BizAppUserId == UserId && s.BusinessFaqAnswerId == Id && s.VotesType == DomainClass.Enums.VotesType.NotHelpFull))
					{
						Items.HelpFullCount += 1;
						Items.NotHelpFullCount -= 1;
						usersInCommunityVotes.BizAppUserId = UserId;
						usersInCommunityVotes.BusinessFaqAnswerId = Id;
						usersInCommunityVotes.VotesType = VotesType.HelpFull;
						await DbContext.UsersInCommunityVotes.AddAsync(usersInCommunityVotes);
						var RemoveItem = await DbContext.UsersInCommunityVotes
						.FirstOrDefaultAsync(s => s.BusinessFaqAnswerId.Equals(Id) &&
						s.VotesType == VotesType.NotHelpFull && s.BizAppUserId.Equals(UserId));
						DbContext.UsersInCommunityVotes.Remove(RemoveItem);
						return CommunityVoteType.AddHelpFullCountAndRemoveNotHelpFull;
					}
					else
					{
						Items.HelpFullCount -= 1;
						var RemoveItem = await DbContext.UsersInCommunityVotes
						.FirstOrDefaultAsync(s => s.BusinessFaqAnswerId.Equals(Id) && s.BizAppUserId.Equals(UserId) && s.VotesType == VotesType.HelpFull);
						DbContext.UsersInCommunityVotes.Remove(RemoveItem);
						return CommunityVoteType.RemoveHelpFullCount;
					}
				}
				return CommunityVoteType.Undefined;
			}
		public async Task<CommunityVoteType> AddNotHelpFull(Guid Id, string UserId)
		{
			UsersInCommunityVotes usersInCommunityVotes = new UsersInCommunityVotes();
			var Items = await DbContext.BusinessFaqAnswers.FirstOrDefaultAsync(s => s.Id.Equals(Id));
			var UserItem = await DbContext.Users.FirstOrDefaultAsync(s => s.Id.Equals(UserId));
			if (Items != null && UserItem != null)
			{
				if (!await DbContext.UsersInCommunityVotes.AnyAsync(s => s.BizAppUserId == UserId && s.BusinessFaqAnswerId == Id && s.VotesType == DomainClass.Enums.VotesType.NotHelpFull) && !await DbContext.UsersInCommunityVotes.AnyAsync(s => s.BizAppUserId == UserId && s.BusinessFaqAnswerId == Id && s.VotesType == DomainClass.Enums.VotesType.HelpFull))
				{
					Items.NotHelpFullCount += 1;
					usersInCommunityVotes.BizAppUserId = UserId;
					usersInCommunityVotes.BusinessFaqAnswerId = Id;
					usersInCommunityVotes.VotesType = VotesType.NotHelpFull;
					await DbContext.UsersInCommunityVotes.AddAsync(usersInCommunityVotes);
					return CommunityVoteType.AddNotHelpFullCount;
				}
				if (!await DbContext.UsersInCommunityVotes.AnyAsync(s => s.BizAppUserId == UserId && s.BusinessFaqAnswerId == Id && s.VotesType == DomainClass.Enums.VotesType.NotHelpFull) && await DbContext.UsersInCommunityVotes.AnyAsync(s => s.BizAppUserId == UserId && s.BusinessFaqAnswerId == Id && s.VotesType == DomainClass.Enums.VotesType.HelpFull))
				{
					Items.NotHelpFullCount += 1;
					Items.HelpFullCount -= 1;
					usersInCommunityVotes.BizAppUserId = UserId;
					usersInCommunityVotes.BusinessFaqAnswerId = Id;
					usersInCommunityVotes.VotesType = VotesType.NotHelpFull;
					await DbContext.UsersInCommunityVotes.AddAsync(usersInCommunityVotes);
					var RemoveItem = await DbContext.UsersInCommunityVotes
					.FirstOrDefaultAsync(s => s.BusinessFaqAnswerId.Equals(Id) &&
					s.VotesType == VotesType.HelpFull && s.BizAppUserId.Equals(UserId));
					DbContext.UsersInCommunityVotes.Remove(RemoveItem);
					return CommunityVoteType.AddNotHelpFullCountAndRemoveHelpFull;
				}
				else
				{
					Items.NotHelpFullCount -= 1;
					var RemoveItem = await DbContext.UsersInCommunityVotes
                    .FirstOrDefaultAsync(s => s.BusinessFaqAnswerId.Equals(Id) && 
					s.BizAppUserId.Equals(UserId) &&
					s.VotesType == VotesType.NotHelpFull);
					DbContext.UsersInCommunityVotes.Remove(RemoveItem);
					return CommunityVoteType.RemoveNotHelpFullCount;

				}
			}
			return CommunityVoteType.Undefined;
		}
		public async Task EditFactAnswer(BusinessFaqAnswer model)
		{
			var Item = await DbContext.BusinessFaqAnswers.FirstOrDefaultAsync(s => s.Id.Equals(model.Id));
			if (Item != null)
			{
				Item.Text = model.Text;
				await DbContext.SaveChangesAsync();
			}

		}
		public async Task<BusinessFaqAnswer> GetBusinessFaqAnswerById(Guid Id)
		{
			return await DbContext.BusinessFaqAnswers.FirstOrDefaultAsync(s => s.Id.Equals(Id));
		}


	}
}
