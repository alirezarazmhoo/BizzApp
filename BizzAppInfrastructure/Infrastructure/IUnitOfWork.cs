using System;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IUnitOfWork : IDisposable
	{
		Task SaveChangesAsync();
	}
}
