using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass.Businesses
{
	public class BusinessQouteUser
	{
		[Key]
		public Guid Id { get; set; }
		public Guid BusinessId { get; set; }
		public int BusinessQouteId { get; set; }
		[Required]
		[Column(TypeName = "NVARCHAR(1000)")]
		public string AnswerQoute { get; set; }
		[Required]
		[Column(TypeName = "NVARCHAR(450)")]
		public string BizAppUserId { get; set; }

		public virtual Business Business { get; set; }
		public virtual BizAppUser User { get; set; }
		public virtual BusinessQoute BusinessQoute { get; set; }
	}
}
