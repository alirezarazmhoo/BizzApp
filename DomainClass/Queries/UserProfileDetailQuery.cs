﻿using System;
using System.Collections.Generic;

namespace DomainClass.Queries
{
	public class UserProfileDetailQuery
	{
		public string Id { get; set; }
		public string FullName { get; set; }
		public DateTime RegisterDate { get; set; }
		public int ReviewCount { get; set; }
		public int UploadedPhotoCount { get; set; }
		public string ProvinceName { get; set; }
		public string CityName { get; set; }
		public IList<string> Photos { get; set; }
	}
}
