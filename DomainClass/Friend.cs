﻿using DomainClass.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DomainClass
{
	public class Friend
	{
		[Key]
		public Guid Id { get; set; }
		[Required]
		[MaxLength(450)]
		public string ApplicatorUserId { get; set; }
		[Required]
		[MaxLength(450)]
		public string ReceiverUserId { get; set; }
		[Required]
		public StatusEnum Status { get; set; } = StatusEnum.Waiting;

		public virtual BizAppUser Applicator { get; set; }
		public virtual BizAppUser Receiver { get; set; }
	}
}
