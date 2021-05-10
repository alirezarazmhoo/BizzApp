using DataLayer.Data;
using DataLayer.Extensions;
using DataLayer.Infrastructure;
using DomainClass;
using DomainClass.Commands;
using DomainClass.Enums;
using DomainClass.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace DataLayer.Services
{
	public class FriendRepo : RepositoryBase<Friend>, IFriendRepo
	{
		public FriendRepo(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public async Task CreateRelation(CreateFriendRelationCommand model)
		{
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				// check request is excists or not 
				var sentRequest =
					await DbContext.Friends
							.AnyAsync(f => f.ApplicatorUserId == model.ApplicatorUserId && f.ReceiverUserId == model.ReceiverUserId);

				if (sentRequest) throw new DuplicateNameException();

				// check send request for myself
				var sentForMyself = model.ReceiverUserId == model.ApplicatorUserId;
				if (sentForMyself) throw new ApplicationException();

				// add friend for user
				var friend = new Friend
				{
					ApplicatorUserId = model.ApplicatorUserId,
					ReceiverUserId = model.ReceiverUserId,
					Status = StatusEnum.Waiting
				};
				DbContext.Friends.Add(friend);

				// add notfification
				var notification = new Notification
				{
					UserId = model.ReceiverUserId,
					Description = model.Description,
					Status = NotificationStatus.Unread
				};
				DbContext.Notifications.Add(notification);

				// save changes
				await DbContext.SaveChangesAsync();

				scope.Complete();
			}
		}

		public async Task<IEnumerable<SharedUserProfileDetailQuery>> GetAll(string userName, int page = 1)
		{
			var user = await DbContext.Users.FirstOrDefaultAsync(w => w.UserName == userName);
			if (user == null) throw new KeyNotFoundException();

			var result =
				await DbContext.Friends
						.Where(w => w.ApplicatorUserId == user.Id)
						.Select(s => new SharedUserProfileDetailQuery
						{
							Id = s.ReceiverUserId,
							UserName = s.Receiver.UserName,
							FullName = s.Receiver.FullName,
							MainPhotoPath =
								s.Receiver.ApplicationUserMedias.FirstOrDefault(f => f.IsMainImage && f.BizAppUserId == s.ReceiverUserId).UploadedPhoto,
							ReviewCount = s.Receiver.Reviews.Count,
							FriendsNumber = DbContext.Friends.Where(w => w.ApplicatorUserId == user.Id && w.Status == StatusEnum.Accepted).Count()
						})
						.Paginate(page, 48)
						.ToListAsync();

			return result;
		}

		public async Task RejectRelation(string receiverUserId, string applicatorUserId)
		{
			var relation = await DbContext.Friends.FirstOrDefaultAsync(f => f.ApplicatorUserId == applicatorUserId && f.ReceiverUserId == receiverUserId);

			if (relation == null) throw new KeyNotFoundException();

			// remove relation
			DbContext.Friends.Remove(relation);
			await DbContext.SaveChangesAsync();
		}

		public async Task AcceptedRelation(string receiverUserId, string applicatorUserId)
		{
			var relation = await DbContext.Friends.FirstOrDefaultAsync(f => f.ApplicatorUserId == applicatorUserId && f.ReceiverUserId == receiverUserId);

			if (relation == null) throw new KeyNotFoundException();

			// update saved relation status
			relation.Status = StatusEnum.Accepted;

			// add new relation for receiver user
			var newRelation = new Friend
			{
				Status = StatusEnum.Accepted,
				ApplicatorUserId = receiverUserId,
				ReceiverUserId = applicatorUserId
			};

			DbContext.Add(newRelation);

			await DbContext.SaveChangesAsync();
		}

		public async Task RemoveRelation(RemoveFriendRelationCommand model)
		{
			// check request is excists or not 
			var relations =
				await DbContext.Friends
					.Where(w => (w.ApplicatorUserId == model.RemoverUserId && w.ReceiverUserId == model.FriendUserId)
							|| (w.ReceiverUserId == model.RemoverUserId && w.ApplicatorUserId == model.FriendUserId))
					.ToListAsync();

			// if realtion not approved or not rejected then can't be delete
			if (relations.Count != 2)
			{
				throw new KeyNotFoundException();
			}

			// remove relations
			DbContext.Friends.RemoveRange(relations);

			// save changes
			await DbContext.SaveChangesAsync();
		}
	}
}
