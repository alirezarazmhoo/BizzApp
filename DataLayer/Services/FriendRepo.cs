using DataLayer.Data;
using DataLayer.Infrastructure;
using DomainClass;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Services
{
	public class FriendRepo : RepositoryBase<Friend>, IFriendRepo
	{
		public FriendRepo(ApplicationDbContext dbContext) : base(dbContext)
		{

		}

		public async Task CreateRelation(Friend model)
		{
			// check request is excists or not 
			var sentRequest = 
				await DbContext.Friends
						.AnyAsync(f => f.ApplicatorUserId == model.ApplicatorUserId && f.ReceiverUserId == model.ReceiverUserId);

			//if (sentRequest) throw new DuplicateKeyException();

			// add friend for user
			DbContext.Friends.Add(model);

			// save changes
			await DbContext.SaveChangesAsync();
		}
	}
}
