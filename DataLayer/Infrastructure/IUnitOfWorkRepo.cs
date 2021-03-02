using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUnitOfWorkRepo
	{
		IBusinessRepo BusinessRepo { get; }
		IProvinceRepo ProvinceRepo { get; }
		ICityRepo CityRepo { get; }
		IUserRepo UserRepo { get; }
		IDistrictRepo DistrictRepo { get; }
		ICateogryRepo CategoryRepo { get; }
		IFeatureRepo FeatureRepo { get; }
		ICategoryFeatureRepo CategoryFeaturesRepo { get; }
		ISliderRepo SliderRepo { get; }

		Task SaveAsync();
	}
}
