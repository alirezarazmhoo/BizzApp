using System;
using System.Collections.Generic;
using System.Text;

namespace DomainClass.Queries
{
   public class SearchBussinessQuery
    {
        public int CategoryId { get; set; }
		public int page { get; set; } = 1;
		public string catsFinder { get; set; }
		public string featuFinder { get; set; }
		public string districtFinder { get; set; }
		public List<Category> categories{ get; set; }
		public List<Feature> features{ get; set; }
		public List<Province> provinces{ get; set; }
		public List<City> cities{ get; set; }
    }
}
