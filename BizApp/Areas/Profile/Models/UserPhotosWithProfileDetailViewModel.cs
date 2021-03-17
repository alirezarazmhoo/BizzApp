using PagedList.Core;

namespace BizApp.Areas.Profile.Models
{
	public class UserPhotosWithProfileDetailViewModel
	{
		public SharedProfileDetailViewModel ProfileDetail { get; set; }
		public PagedList<UserPhotosViewModel> UserPhotos { get; set; }
	}
}
