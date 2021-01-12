﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainClass
{
	public class Feature
	{
		public Feature()
		{
		}

		public Feature(string name)
		{
			Name = name;
		}

		[Key]
		public int Id { get; set; }
		[Required]
		[Column(TypeName = "nvarchar(100)")]
		public string Name { get; set; }
	}
}
