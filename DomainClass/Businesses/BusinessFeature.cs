using DomainClass.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DomainClass.Businesses
{
	public class BusinessFeature
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public Guid BusinessId { get; set; }
		[Required]
		public int FeatureId { get; set; }
		public string Value { get; set; }
		public virtual Business Business { get; set; }
		public virtual Feature Feature { get; set; }
		//public BusinessFeatureType  BusinessFeatureType { get; set; }
	}
}
