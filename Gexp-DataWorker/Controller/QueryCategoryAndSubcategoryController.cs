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

        #region old method

        //public List<CategoryAndSubcategoryModel> GetListOfCategoryWihtSubCategory()
        //{
        //    var sparqlQuery =
        //        "select distinct ?category ?subcateg ?prop " +
        //                "where { " +
        //                    "?ct gexp:hasCategory ?category . " +
        //                    "?ct gexp:hasSubcategory ?subcateg . " +
        //                    "?ct gexp:indicatorName ?prop ." +
        //                "} order by ?category";

        //    try
        //    {
        //        var resItems = _starDogInerface.GetItemsList(sparqlQuery);
        //        if (resItems.Results.Count > 0)
        //        {
        //            var lastCateg = String.Empty;
        //            var lastSubCateg = String.Empty;
        //            var subCategoryList = new List<SubcategoryModel>();
        //            var categWithSubcategList = new List<CategoryAndSubcategoryModel>();
        //            var indicatorList = new List<SubcategoryIndicators>();

        //            foreach (SparqlResult itemsResult in resItems.Results)
        //            {

        //                if (!String.IsNullOrEmpty(lastCateg) && lastCateg != itemsResult[0].ToString())
        //                    subCategoryList = new List<SubcategoryModel>();

        //                if (!String.IsNullOrEmpty(lastSubCateg) && lastSubCateg != itemsResult[2].ToString())
        //                    indicatorList = new List<SubcategoryIndicators>();

        //                var checkSubCateg = subCategoryList.FirstOrDefault(x => x.SubcategoryName == itemsResult[1].ToString());
        //                if (checkSubCateg == null)
        //                {
        //                    //add subcateg
        //                    var subCategItem = new SubcategoryModel();
        //                    subCategItem.SubcategoryName = itemsResult[1].ToString();
        //                    subCategoryList.Add(subCategItem);
        //                }                        


        //                //add indicator
        //                var indicatorItem = new SubcategoryIndicators();
        //                indicatorItem.Indicator = itemsResult[2].ToString();
        //                indicatorList.Add(indicatorItem);

        //                var indicator2Check = indicatorList.FirstOrDefault(x => x.Indicator != indicatorItem.Indicator);
        //                if(indicator2Check == null && subCategoryList.Count > 0)
        //                    subCategoryList.FirstOrDefault().Indicators = indicatorList;                        

        //                if (lastCateg == itemsResult[0].ToString())
        //                {
        //                    var categWithSubcateItem = new CategoryAndSubcategoryModel();
        //                    categWithSubcateItem.CategoryName = itemsResult[0].ToString();
        //                    categWithSubcateItem.SubcategorieList = null;
        //                    categWithSubcateItem.SubcategorieList = subCategoryList;

        //                    //add to main list
        //                    var itemCheck = categWithSubcategList.FirstOrDefault(x => x.CategoryName == lastCateg);
        //                    if (itemCheck == null)
        //                        categWithSubcategList.Add(categWithSubcateItem);
        //                    else
        //                    {
        //                        //remove old
        //                        categWithSubcategList.Remove(itemCheck);
        //                        //add new
        //                        categWithSubcategList.Add(categWithSubcateItem);
        //                    }
        //                }
        //                else if (lastCateg != itemsResult[0].ToString() && subCategoryList.Count > 0)
        //                {
        //                    var checkItemInList = categWithSubcategList.FirstOrDefault() != null ? categWithSubcategList.FirstOrDefault().CategoryName : "";
        //                    if (!String.IsNullOrEmpty(checkItemInList))
        //                    {
        //                        if (checkItemInList != lastCateg)
        //                        {

        //                            var categWithSubcateItem = new CategoryAndSubcategoryModel();
        //                            categWithSubcateItem.CategoryName = itemsResult[0].ToString();
        //                            categWithSubcateItem.SubcategorieList = null;
        //                            categWithSubcateItem.SubcategorieList = subCategoryList;

        //                            //add to main list
        //                            categWithSubcategList.Add(categWithSubcateItem);
        //                        }
        //                    }
        //                }

        //                //asign category name
        //                lastCateg = itemsResult[0].ToString();
        //            }
        //            return categWithSubcategList;
        //        }
        //    }
        //    catch (Exception)
        /*   {
                throw;
            }

            return null;
        }*/
        #endregion

        public List<CategoryAndSubcategoryModel> GetListOfCategoryWihtSubCategory()
        {
            try
            {
                //get category
                var sparqlCateg = "select distinct ?category  where{ ?c gexp:hasCategory ?category . }";
                var responseCategList = _starDogInerface.GetItemsList(sparqlCateg);
                var responseList = new List<CategoryAndSubcategoryModel>();
                if (responseCategList.Results.Count > 0)
                {                    
                    foreach (SparqlResult resultCateg in responseCategList.Results)
                    {
                        var itemCateg = new CategoryAndSubcategoryModel();

                        //create subCategList
                        var sparqlSubCateg = "select distinct ?category ?subcateg where{ " +
                                             "?c gexp:hasCategory ?category . " +
                                             "?c gexp:hasSubcategory ?subcateg . " + Environment.NewLine +
                                             "filter(str(?category) = \"" + resultCateg[0].ToString() + "\") }";
                        var responseSubCateg = _starDogInerface.GetItemsList(sparqlSubCateg);
                        var subCategoryList = new List<SubcategoryModel>();
                        if (responseSubCateg.Results.Count > 0)
                        {                                                        
                            foreach (SparqlResult resultSubCateg in responseSubCateg.Results)
                            {
                                var subCategItem = new SubcategoryModel();
                                //create indicators
                                var sparqlIndicators = "select distinct ?category ?subcateg ?prop where{ " +
                                                       "?c gexp:hasCategory ?category . " +
                                                       "?c gexp:hasSubcategory ?subcateg . " +
                                                       "?c gexp:hasIndicatorName ?prop . " + Environment.NewLine +
                                                       "filter(str(?category) = \"" + resultCateg[0].ToString() + "\") " + Environment.NewLine +
                                                       "filter(str(?subcateg) = \"" + resultSubCateg[1].ToString() + "\") }";
                                var responseIndicators = _starDogInerface.GetItemsList(sparqlIndicators);
                                var indicatorList = new List<SubcategoryIndicators>();
                                if (responseIndicators.Results.Count > 0)
                                {                                    
                                    foreach (SparqlResult responseIndicator in responseIndicators)
                                    {
                                        var indicatorItem = new SubcategoryIndicators();
                                        indicatorItem.Indicator = responseIndicator[2].ToString();

                                        //add indicators
                                        indicatorList.Add(indicatorItem);
                                    }
                                }                                
                                subCategItem.SubcategoryName = resultSubCateg[1].ToString();
                                subCategItem.Indicators = indicatorList;
                                //add sublist
                                subCategoryList.Add(subCategItem);
                            }
                        }
                        itemCateg.CategoryName = resultCateg[0].ToString();
                        itemCateg.SubcategorieList = subCategoryList;

                        responseList.Add(itemCateg);
                    }
                }
                return responseList;
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }
    }
}
