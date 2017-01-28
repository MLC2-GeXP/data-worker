using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gexp_DataWorker.Models
{
    public class CategoryModel
    {
        public string CategoryName { get; set; }
    }

    public class CategoryAndSubcategoryModel
    {
        public string CategoryName { get; set; }
        public List<SubcategoryModel> SubcategorieList { get; set; }
    }

    public class SubcategoryModel
    {
        public string SubcategoryName { get; set; }
    }
}
