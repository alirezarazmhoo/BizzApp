using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Extensions
{
	public static class PagingExtensions
	{
		public static IQueryable<TSource> Paginate<TSource>(this IQueryable<TSource> query, int page, int pageSize)
		{
			return query.Skip((page - 1) * pageSize).Take(pageSize);
		}

		public static IEnumerable<TSource> Paginate<TSource>(this IEnumerable<TSource> soruce, int page, int pageSize)
		{
			return soruce.Skip((page - 1) * pageSize).Take(pageSize);
		}
	}
}
