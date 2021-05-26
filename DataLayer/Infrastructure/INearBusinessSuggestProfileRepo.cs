using DomainClass.Businesses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface INearBusinessSuggestProfileRepo
	{
		Task<IEnumerable<Business>> Get(string UserId);
	}
}
