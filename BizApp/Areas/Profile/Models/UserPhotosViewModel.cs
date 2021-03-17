using System;

namespace BizApp.Areas.Profile.Models
{
	public class UserPhotosViewModel
	{
		public Guid Id { get; set; }
		public string Path { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
