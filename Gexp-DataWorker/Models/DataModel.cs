using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gexp_DataWorker.Models
{
    public class DataModel
    {
        public CategoryModel CategoryModel { get; set; }
        public SubcategoryModel Subcategory { get; set; }
        public CountryModel Country { get; set; }
        public string Year { get; set; }
        public string Value { get; set; }
        public string Gender { get; set; }
        public string Indicator { get; set; }
    }
}
