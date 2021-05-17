using DomainClass.Queries;

namespace BizApp.Areas.Profile.Models
{
	public class SharedProfileDetailViewModel
	{
		private string fullName;

		public string Id { get; set; }
		public string FullName
		{
			get
			{
				return fullName;
			}
			set
			{
				fullName = !string.IsNullOrEmpty(value) ? value : "بدون نام";
			}
		}
		public string UserName { get; set; }
		public string MainPhotoPath { get; set; }
		public int ReviewCount { get; set; }
		public int FriendsNumber { get; set; }
	}
}
