using DomainClass.Queries;

namespace BizApp.Areas.Profile.Models.UserActivities
{
	public class AddedUserPhotoViewModel : IUserActivityQuery
	{
		public AddedUserPhotoViewModel(string imagePath)
		{
			ImagePath = imagePath;
		}

		public string ImagePath { get; private set; }
	}
}
