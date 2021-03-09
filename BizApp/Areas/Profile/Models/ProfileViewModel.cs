using System.Collections.Generic;

namespace BizApp.Areas.Profile.Models
{
	public class ProfileViewModel
	{
		public string Id { get; set; }
		public string FullName { get; set; }
		public string RegisterDate { get; set; }
		public int ReviewCount { get; set; }
		public int UploadedPhotoCount { get; set; }
		public string ProvinceName { get; set; }
		public string CityName { get; set; }
		public IList<string> Photos { get; set; }
	}
}
