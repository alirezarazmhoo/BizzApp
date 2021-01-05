using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUnitOfWorkRepo
	{
		IProvinceRepo ProvinceRepo { get; }

		Task SaveAsync();
	}
}
