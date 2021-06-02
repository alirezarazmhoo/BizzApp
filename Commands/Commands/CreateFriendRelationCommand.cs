namespace DomainClass.Commands
{
	public class CreateFriendRelationCommand
	{
		public string ApplicatorUserId { get; set; }
		public string ReceiverUserId { get; set; }
		public string Description { get; set; }
	}
}
