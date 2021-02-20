using DomainClass.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Businesses.Queries
{
	public class ComboBoxViewModel
	{
		public int id { get; set; }
		public string name { get; set; }
		public bool havenext { get; set; }
	}

	public class ChildsCategoryResponse
	{
		public bool Isfinal { get; set; }

		public int? Parentid { get; set; }

		public List<ComboBoxViewModel> items { get; set; }

	}
	public class AllBusinessFeatureViewModel
	{
		public int Id { get; set; }
		public int FeatureId { get; set; }
		public string Value { get; set; }
		public BusinessFeatureType ValueType { get; set; }
		public bool IsInFeature { get; set; }
		public string FeatureName { get; set; }
		public string BusinessName { get; set; }

	}

}
