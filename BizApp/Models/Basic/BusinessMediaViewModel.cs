using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Models.Basic
{
	public class BusinessMediaViewModel
	{
		public Guid Id { get; set; }
		public Guid BusinessId { get; set; }
		public List<string> Images { get; set;  }
		public DateTime Date { get; set; }
		public string PersianDate { get; set;  }
		public string Name { get; set; }
		public int TotalReview { get; set;  }
		public string Description { get; set; }
		public string UserProfilePicture { get; set;  }
	}
}
