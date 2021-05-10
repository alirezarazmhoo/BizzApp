namespace BizApp.Areas.Profile.Models.Friends
{
	public class CreateFriendRelationViewModel
	{
		public string ReceiverUserId { get; set; }
		public string ReceiverFullName { get; set; }
		public string ReceiverUserName { get; set; }
		public string Description { get; set; }
	}
}
