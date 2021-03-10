using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Review
{
public	class UsersInCustomerBusinessMediaLike
	{
		public Guid Id { get; set; }
		public string BizAppUserId { get; set; }
		public BizAppUser BizAppUser { get; set; }
		public Guid CustomerBusinessMediaPicturesId { get; set; }
		public CustomerBusinessMediaPictures   CustomerBusinessMediaPictures { get; set; }
	}
}
