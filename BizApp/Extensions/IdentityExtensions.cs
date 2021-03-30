using BizApp.Areas.Profile.Models;
using DataLayer.Infrastructure;

namespace BizApp.Extensions
{
	public static class IdentityExtensions
	{
        public static ProfileViewModel GetProfileDetail(this System.Security.Principal.IIdentity identity, IUnitOfWorkRepo unitOfWork, string userId)
        {
            if (identity.IsAuthenticated)
            {
                var user = unitOfWork.UserProfileRepo.GetUserDetail(userId);

                // if (user == null) redirect

                var viewModel = new ProfileViewModel 
                {
                    //Id = user.Id,
                    //FullName = user.FullName
                };

                return viewModel;
            }
            
            return null;
        }
    }
}
