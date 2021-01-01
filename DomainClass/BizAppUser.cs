using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass
{
	public class BizAppUser : IdentityUser
	{
		public long NationalCode { get; set; }
	}
}
