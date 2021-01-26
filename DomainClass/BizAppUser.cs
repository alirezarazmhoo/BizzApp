using DomainClass.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass
{
	public class BizAppUser : ApplicationUser, ISoftDelete
	{
		public bool IsDeleted { get; set; }
	}
}
