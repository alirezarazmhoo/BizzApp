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
		private readonly IUserActivityRepo _userActivity;
		private readonly INotificationRepo _notification;

		public FriendRepo(ApplicationDbContext dbContext, IUserActivityRepo userActivity, INotificationRepo notificationRepo) : base(dbContext)
		{
			_userActivity = userActivity;
			_notification = notificationRepo;
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
				await _notification.Add(model.ReceiverUserId, NotificationModel.Friend);

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
							City = s.Receiver.City.Name,
							MainPhotoPath =
								s.Receiver.ApplicationUserMedias.FirstOrDefault(f => f.IsMainImage && f.BizAppUserId == s.ReceiverUserId).UploadedPhoto,
							ReviewCount = s.Receiver.Reviews.Count,
							FriendsNumber = DbContext.Friends.Where(w => w.ApplicatorUserId == user.Id && w.Status == StatusEnum.Accepted).Count(),
							Address = s.Receiver.Address
						})
						.Paginate(page, 48)
						.ToListAsync();

			return result;
		}

		public async Task RejectRelation(string receiverUserId, string applicatorUserId)
		{
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			{
				var relation = await DbContext.Friends.
						FirstOrDefaultAsync(f => f.ApplicatorUserId == applicatorUserId && f.ReceiverUserId == receiverUserId);

				if (relation == null) throw new KeyNotFoundException();

				// remove relation
				DbContext.Friends.Remove(relation);

				// remove notification
				await _notification.Remove(NotificationModel.Friend, receiverUserId, applicatorUserId);

				await DbContext.SaveChangesAsync();

				scope.Complete();
			}


		}

		public async Task AcceptedRelation(string receiverUserId, string applicatorUserId)
		{
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
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

				// remove notification
				await _notification.Remove(NotificationModel.Friend, receiverUserId, applicatorUserId);

				// set user activity
				await _userActivity.AddAsync(TableName.Friend, relation.Id.ToString(), relation.ReceiverUserId);
				await _userActivity.AddAsync(TableName.Friend, newRelation.Id.ToString(), newRelation.ReceiverUserId);

				await DbContext.SaveChangesAsync();

				scope.Complete();
			}

		}

		public async Task RemoveRelation(RemoveFriendRelationCommand model)
		{
			using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
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

				scope.Complete();
			}
		}
	}
}
