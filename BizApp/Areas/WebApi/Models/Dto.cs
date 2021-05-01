﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.Models
{
	public class CategoryDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Icon { get; set; }
		public string Image { get; set; }
	}
	public class InputCategroy
	{
		public double longitude { get; set; }
		public double latitude { get; set; }
		public string id { get; set; }
	}
	public class BusinessOnMap
	{
		public string name { get; set; }
		public string website { get; set; }
		public string boldfeature { get; set; }
		public string phonenumber { get; set; }
		public string category { get; set;  }
		public string featureimage { get; set;  }
		public string address { get; set; }
		public string districtname { get; set;  }
		public string description { get; set;  }
		public List<string> images { get; set; }
		public int totalreview { get; set; }
		public int rate { get; set;  }
		public double longitude { get; set; }
		public double latitude { get; set; }
		public Guid id { get; set; }
	}
	public class BusinessItem
	{
		public Guid id { get; set;  }
		public string name { get; set; }
		public int rate { get; set;  }
		public int totalreview { get; set;  }
		public string address { get; set; }
		public string districname { get; set;  }
		public string description { get; set; }
		public string image { get; set;  }
		public List<Review> reviews { get; set; }
	}

	public class Review
	{
		public Guid Id { get; set; }
		public string Image { get; set; }
		public string FullName { get; set; }
		public int Rate { get; set; }
		public string Date { get; set; }
		public string Text { get; set; }
		public int TotalReviewPicture { get; set; }
		public int TotalReview { get; set; }
		public int TotalBusinessMediaPicture { get; set; }
		public int UseFull { get; set; }
		public int Cool { get; set; }
		public int Funny { get; set; }
	}



}
