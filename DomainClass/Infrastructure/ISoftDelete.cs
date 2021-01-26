namespace DomainClass.Infrastructure
{
	public interface ISoftDelete
	{
		bool IsDeleted { get; set; }
	}
}
