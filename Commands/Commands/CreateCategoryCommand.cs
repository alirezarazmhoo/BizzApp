namespace DomainClass.Commands
{
	public class CreateCategoryCommand
	{
		public string Name { get; set; }
		public string Icon { get; set; }
		public string IconWeb { get; set; }
		public int? Order { get; set; }
        public int? ParentCategoryId { get; set; }

	}
}
