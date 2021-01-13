using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUnitOfWorkRepo
	{
		IProvinceRepo ProvinceRepo { get; }
		ICityRepo CityRepo { get; }
		IDistrictRepo DistrictRepo { get; }
		ICateogryRepo CategoryRepo { get; }
		IFeatureRepo FeatureRepo { get; }
		ICategoryFeatureRepo CategoryFeaturesRepo { get; }
		Task SaveAsync();
	}
}
