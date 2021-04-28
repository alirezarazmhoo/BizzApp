using DomainClass.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Commands
{
	public class EditAcountCommand
	{
		public string Id { get; set; }
		public string FullName { get; set; }
		public GenderEnum Gender { get; set; }
		public string NationalCode { get; set; }
		public int? CityId { get; set; }
		public string PostalCode { get; set; }
		public string Address { get; set; }
		public DateTime? BirthDate { get; set; }
	}
}
