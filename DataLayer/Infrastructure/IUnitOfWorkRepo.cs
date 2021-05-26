using DataLayer.Infrastructure.Reviews;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUnitOfWorkRepo
	{
		IBusinessRepo BusinessRepo { get; }
		IProvinceRepo ProvinceRepo { get; }
		IBusinessQouteRepo BusinessQouteRepo { get; }
		IBusinessQouteUserRepo BusinessQouteUserRepo { get; }
		ICityRepo CityRepo { get; }
		IUserRepo UserRepo { get; }
		IUserProfileRepo ProfileRepo { get; }
		IDistrictRepo DistrictRepo { get; }
		ICateogryRepo CategoryRepo { get; }
		IFeatureRepo FeatureRepo { get; }
		IUserProfileRepo UserProfileRepo { get; }
		ICategoryFeatureRepo CategoryFeaturesRepo { get; }
		ISliderRepo SliderRepo { get; }
		IReviewRepo  ReviewRepo { get; }
		IUserPhotoRepo UserPhotoRepo { get; }
		IBusinessReviewCountRepo BusinessReviewCountRepo { get; }
		IBusinessHomePageRepo  BusinessHomePageRepo { get; }
		IAskTheCommunityRepo  AskTheCommunityRepo { get; }
		IUserFavoritsRepo UserFavoritsRepo { get; }
		IUserActivityRepo UserActivityRepo { get; }
		INotificationRepo NotificationRepo { get; }
		IFriendRepo FriendRepo { get; }
		IBusinessRecentlyViewdRepo   BusinessRecentlyViewdRepo { get; }
		INearBusinessSuggestProfileRepo  NearBusinessSuggestProfileRepo { get; }
		Task SaveAsync();
	}
}
