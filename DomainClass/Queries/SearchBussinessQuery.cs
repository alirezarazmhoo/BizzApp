﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Queries
{
   public class SearchBussinessQuery
    {
        public int CategoryId { get; set; }
		public int page { get; set; } = 1;
		public string catsFinder { get; set; }
		public List<Category> categories{ get; set; }
    }
}