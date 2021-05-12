using DomainClass.Queries;
using System;

namespace BizApp.Areas.Profile.Models.Friends
{
	public class ConfirmRelationViewModel
	{
		public Guid Id { get; set; }
		public string Message { get; set; }

		public UserProfileDetailQuery UserDetail { get; set; }
	}
}
