using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizApp.Areas.WebApi.Models
{
	public class ApiModel
	{
		public bool success { get; set; }
		public int status { get; set; }
		public string message { get; set; }
		public object data { get; set; }
	}
}
