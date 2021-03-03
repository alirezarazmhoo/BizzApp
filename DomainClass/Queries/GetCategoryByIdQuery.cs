namespace DomainClass.Queries
{
	public class GetCategoryByIdQuery
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int? Order { get; set; }
		public string Icon { get; set; }
		public string IconWeb { get; set; }
		public string FeatureImagePath { get; set; }
		public string PngIconPath { get; set; }
		public int? ParentCategoryId { get; set; }
	}
}
