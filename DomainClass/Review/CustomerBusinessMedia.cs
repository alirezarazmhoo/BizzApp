﻿using DomainClass.Businesses;
using DomainClass.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Review
{
  public class CustomerBusinessMedia
	{
		public Guid Id { get; set; }
		public string BizAppUserId { get; set; }
		public virtual BizAppUser BizAppUser { get; set;  }
		public Guid BusinessId { get; set;  }
		public virtual Business Business { get; set; }
		public StatusEnum StatusEnum { get; set;  }
		public string Image { get; set; }
		public string Description { get; set;  }
		public DateTime Date { get; set; }
		public long LikeCount { get; set;  }
		public virtual ICollection<UsersInCustomerBusinessMediaLike> UsersInCustomerBusinessMediaLikes { get; set;  }
	}
}
