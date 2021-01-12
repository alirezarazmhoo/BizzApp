using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUnitOfWorkRepo
	{
		IProvinceRepo ProvinceRepo { get; }
		ICityRepo CityRepo { get; }
		IDistrictRepo DistrictRepo { get; }
		ICateogryRepo CateogryRepo { get; }
		IFeatureRepo FeatureRepo { get; }
		Task SaveAsync();
	}
}
