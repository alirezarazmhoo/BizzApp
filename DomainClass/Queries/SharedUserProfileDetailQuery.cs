namespace DomainClass.Queries
{
	public class SharedUserProfileDetailQuery
	{
		public string Id { get; set; }
		public string FullName { get; set; }
		public string UserName { get; set; }
		public string MainPhotoPath { get; set; }
		public int ReviewCount { get; set; }
		public int FriendsNumber { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
	}
}
