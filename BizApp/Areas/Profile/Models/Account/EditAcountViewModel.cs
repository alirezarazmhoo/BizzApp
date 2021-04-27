using DomainClass.Enums;
using System;

namespace BizApp.Areas.Profile.Models.Account
{
	public class EditAcountViewModel
	{
		public string Id { get; set; }
		public string FullName { get; set; }
		public GenderEnum Gender { get; set; }
		public string NationalCode { get; set; }
		public int? CityId { get; set; }
		public string PostalCode { get; set; }
		public string Address { get; set; }
		public string MainPhoto { get; set; }
		
		// birth date fields
		public int? Year { get; set; }
		public int? Month { get; set; }
		public int? Day { get; set; }

	}
}
