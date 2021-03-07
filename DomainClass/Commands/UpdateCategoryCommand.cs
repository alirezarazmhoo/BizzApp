namespace DomainClass.Commands
{
	public class UpdateCategoryCommand : CreateCategoryCommand
	{
		public int Id { get; set; }
		public bool ChangedPngIcon { get; set; }
	}
}
