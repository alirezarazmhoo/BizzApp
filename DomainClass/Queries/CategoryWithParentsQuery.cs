namespace DomainClass.Queries
{
	public class CategoryWithParentsQuery
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int? ParentCategoryId { get; set; }
	}
}
