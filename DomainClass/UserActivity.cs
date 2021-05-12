using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass
{
	public class UserActivity
	{
		[Key]
		public Guid Id { get; set; }
		[Required]
		[Column(TypeName = "nvarchar(450)")]
		public string UserId { get; set; }
		[Required]
		public DateTime CreatedAt { get; set; }
		[Required]
		public TableName TableName { get; set; }
		[Required]
		[Column(TypeName = "nvarchar(450)")]
		public string TableKey { get; set; }
		[Column(TypeName = "nvarchar(500)")]
		public string Description { get; set; }

		public virtual BizAppUser User { get; set; }

	}

	public enum TableName
	{
		Reviews = 0 ,
		UserPhotos =1, 
		UserBusinessMedia = 2 , 
		SendRequest = 3  , 
		AddToFavorit = 4,
		Friend = 5
	}
}
