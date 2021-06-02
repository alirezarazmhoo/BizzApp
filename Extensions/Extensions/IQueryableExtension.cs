using DomainClass.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace DataLayer.Extensions
{
	public static class IQueryableExtension
	{
		public static IQueryable<T> ApplyRowsAuthFilter<T>(this IQueryable<T> query, ClaimsPrincipal currentUser)
		{
			// if type of enttiy is not ICreator 
			var isCreator = typeof(ICreator).IsAssignableFrom(typeof(T));
			if (!(isCreator)) throw new NotSupportedException(nameof(query));

			// get user admin and id information
			var isAdmin = currentUser.IsInRole(UserConfiguration.AdminRoleName);
			var userId = currentUser.GetUserId();

			// if user is not admin apply row filter
			if (!isAdmin)
			{
				return query.Cast<ICreator>().Where(w => w.UserCreatorId == userId).Cast<T>();
			}

			return query;
		}

	}
}
