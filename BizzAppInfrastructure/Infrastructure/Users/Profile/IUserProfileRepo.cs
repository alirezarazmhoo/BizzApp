using DomainClass.Enums;
using DomainClass.Queries;
using System;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUserProfileRepo
	{
		Task<Nullable<StatusEnum>> GetFriendShipStatus(string currentUserId, string friendUserName);
		Task<UserProfileDetailQuery> GetUserDetail(string userName);
		Task<SharedUserProfileDetailQuery> GetSharedUserDetail(string userName);
		Task<CalculateUserProfileCompleted> CalculateUserProfileCompleted(string UserId);
		Task<UserImpactDto> UserImpact(string UserId); 
	}
}
