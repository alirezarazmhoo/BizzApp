using DomainClass.Enums;
using System;

namespace DomainClass
{
	public class ApplicationUserMedia
	{
		public Guid Id { get; set; }
		public bool IsNew { get; set;  }
		public string BizAppUserId { get; set; }
		public BizAppUser BizAppUser { get; set;  }
		public StatusEnum Status { get; set;  }
		public string UploadedPhoto { get; set; }
		public bool IsMainImage { get; set; }
	}
}
