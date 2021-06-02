using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IBusinessReviewCountRepo
	{
		Task<int> Count(Guid id);
	}
}
