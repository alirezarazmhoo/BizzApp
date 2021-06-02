using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IRepositoryBase<T>	{
		IQueryable<T> FindAll(Expression<Func<T, object>> expression);
		IQueryable<T> FindAll();
		IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
		Task CreateAsync(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}
