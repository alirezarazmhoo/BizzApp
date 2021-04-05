using DataLayer.Infrastructure.Reviews;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUnitOfWorkRepo
	{
		IBusinessRepo BusinessRepo { get; }
		IProvinceRepo ProvinceRepo { get; }
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


		Task SaveAsync();
	}
}
