using DomainClass;
using DomainClass.Businesses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Infrastructure
{
	public interface IBusinessQouteUserRepo
	{
		Task Add(Guid BusinessId, List<string> AllAnswerQoute, string BizAppUserId);
	}
}
