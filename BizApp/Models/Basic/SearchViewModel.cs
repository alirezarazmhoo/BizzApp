using DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Models.Basic
{
	#region Search
	public class SearchViewModel
	{
		public int CategoryId { get; set; }
		public List<Category> categories{ get; set; }

	}
	#endregion
	
	}
