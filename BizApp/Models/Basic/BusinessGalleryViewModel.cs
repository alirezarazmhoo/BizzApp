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
		public Dictionary<Guid , string> Pictures { get; set; }
	}
	#endregion

	
}
