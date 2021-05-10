using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Models.Basic
{

	#region Main
	public class BusinessGalleryViewModel
	{
		public Guid BusinessId { get; set; }
		public string BusinessName { get; set; }
		public string Image { get; set; }
		public int BusinessTotalReview { get; set;  }
		public int BusinessRate { get; set;  }
		public List<BusinessGallery_PictureViewModel> businessGallery_PictureViewModels { get; set; }
	}
	#endregion
	#region User
	public class BusinessGallery_PictureViewModel
	{
		public Guid Id { get; set;  }
		public string Image { get; set;  }
		public string description { get; set;  }
		public string UserName { get; set; }
		public string UserId { get; set;  }
		public int  UserTotalReview { get; set; }

		public string Date { get; set; }
		public string UserImage { get; set;  }

	}
	#endregion


}
