using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gexp_DataWorker.Models;
using VDS.RDF.Query;

namespace Gexp_DataWorker.Controller
{
    public class QueryCategoryAndSubcategoryController
    {
        private readonly StarDogInerface _starDogInerface = new StarDogInerface();

        public List<CategoryModel> GetListOfCategory()
        {
            var sparqlQuery = "select ?p { ?s gexp:hasCategory ?p . } group by ?p";
            try
            {
                var resItems = _starDogInerface.GetItemsList(sparqlQuery);
                if (resItems.Results.Count > 0)
                {
                    var lastItem = String.Empty;
                    var categoryListResponse = new List<CategoryModel>();
                    foreach (SparqlResult itemsResult in resItems.Results)
                    {
                        if (itemsResult.LastOrDefault().Value.ToString() != lastItem)
                        {
                            var catIem = new CategoryModel();
                            catIem.CategoryName = itemsResult.LastOrDefault().Value.ToString();

                            categoryListResponse.Add(catIem);

                            lastItem = itemsResult.LastOrDefault().Value.ToString();
                        }
                    }
                    return categoryListResponse;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }

        public List<CategoryAndSubcategoryModel> GetListOfCategoryWihtSubCategory()
        {
            var sparqlQuery =
                "select distinct ?category ?subcateg where { ?ct gexp:hasCategory ?category . ?ct gexp:hasSubcategory ?subcateg } order by ?category";

            try
            {
                var resItems = _starDogInerface.GetItemsList(sparqlQuery);
                if (resItems.Results.Count > 0)
                {
                    var lastItem = String.Empty;
                    var subCategoryList = new List<SubcategoryModel>();
                    var categWithSubcategList = new List<CategoryAndSubcategoryModel>();
                    foreach (SparqlResult itemsResult in resItems.Results)
                    {

                        if (!String.IsNullOrEmpty(lastItem) && lastItem != itemsResult.FirstOrDefault().Value.ToString())
                            subCategoryList = new List<SubcategoryModel>();

                        //add subcateg
                        var subCategItem = new SubcategoryModel();
                        subCategItem.SubcategoryName = itemsResult.LastOrDefault().Value.ToString();
                        subCategoryList.Add(subCategItem);

                        if (lastItem == itemsResult.FirstOrDefault().Value.ToString())
                        {
                            var categWithSubcateItem = new CategoryAndSubcategoryModel();
                            categWithSubcateItem.CategoryName = itemsResult.FirstOrDefault().Value.ToString();
                            categWithSubcateItem.SubcategorieList = null;
                            categWithSubcateItem.SubcategorieList = subCategoryList;

                            //add to main list
                            categWithSubcategList.Add(categWithSubcateItem);
                        }
                        else if (lastItem != itemsResult.FirstOrDefault().Value.ToString() && subCategoryList.Count > 0)
                        {
                            var checkItemInList = categWithSubcategList.FirstOrDefault() != null ? categWithSubcategList.FirstOrDefault().CategoryName : "";
                            if (!String.IsNullOrEmpty(checkItemInList))
                            {
                                if (checkItemInList != lastItem)
                                {

                                    var categWithSubcateItem = new CategoryAndSubcategoryModel();
                                    categWithSubcateItem.CategoryName = itemsResult.FirstOrDefault().Value.ToString();
                                    categWithSubcateItem.SubcategorieList = null;
                                    categWithSubcateItem.SubcategorieList = subCategoryList;

                                    //add to main list
                                    categWithSubcategList.Add(categWithSubcateItem);
                                }
                            }
                        }

                        //asign category name
                        lastItem = itemsResult.FirstOrDefault().Value.ToString();
                    }
                    return categWithSubcategList;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }
    }
}
