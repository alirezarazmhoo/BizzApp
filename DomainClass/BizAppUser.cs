using DomainClass.Infrastructure;

namespace DomainClass
{
	public class BizAppUser : ApplicationUser, ISoftDelete
	{
		public bool IsDeleted { get; set; }
		public bool IsEnabled { get; set; }
	}
}
