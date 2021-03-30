using DomainClass.Enums;
using System;

namespace DomainClass
{
	/// <summary>
	/// Save user photos in database
	/// </summary>
	public class ApplicationUserMedia
	{
		public Guid Id { get; set; }
		public bool IsNew { get; set;  }
		public string BizAppUserId { get; set; }
		public StatusEnum Status { get; set;  }
		public string UploadedPhoto { get; set; }
		public bool IsMainImage { get; set; }
		public DateTime CreatedAt { get; set; }

		public BizAppUser BizAppUser { get; set;  }
	}
}
