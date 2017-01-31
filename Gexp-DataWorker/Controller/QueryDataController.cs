using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gexp_DataWorker.Models;
using VDS.RDF.Query;

namespace Gexp_DataWorker.Controller
{
    public class QueryDataController
    {
        private readonly StarDogInerface _starDogInerface = new StarDogInerface();

        public List<DataModel> GetListOfItems(List<string> countryUrl, string category, string subCategory, string indicator, string yearStart, string yearEnd, string gen)
        {
            var responseList = new List<DataModel>();

            //main query
            var sparqlQuery = "select distinct ?country ?countryLink ?an ?val ?subcateg ?categ ?gender ?indicator" +
                              "{ " +
                              "?countryLink dc:identifier ?country . " +
                              "?countryLink gexp:linkNode ?node . " +                              
                              "?node gexp:hasCategory ?categ . " +
                              "?node gexp:inYear ?an . " +
                              "?node gexp:hasValue ?val . " +
                              "?node gexp:hasSubcategory ?subcateg . " +                          
                              "?node foaf:gender ?gender . " +
                              "?node gexp:hasIndicatorName ?indicator " + Environment.NewLine +
                              "filter(str(?categ) = \""+category+"\")" + Environment.NewLine +
                              "filter(str(?subcateg) = \"" + subCategory + "\")" + Environment.NewLine +
                              "filter(str(?indicator) = \"" + indicator + "\")" + Environment.NewLine;

            var orderBy = "?country";

            if (countryUrl.Count() > 0)
            {
                var countryFilter = "";
                int countryNr = countryUrl.Count;
                int currentItem = 1;

                foreach (var countUri in countryUrl)
                {                 
                    if(currentItem < countryNr)
                        countryFilter += "str(?countryLink) = \"" + countUri.Trim() + "\" || ";
                    else if(currentItem == countryNr)
                        countryFilter += "str(?countryLink) = \"" + countUri.Trim() + "\"";

                    currentItem++;
                }
                sparqlQuery += Environment.NewLine + "filter("+countryFilter+")";
                orderBy = "?an";
            }

            #region old filtration categ/subcateg

            /*if (!String.IsNullOrEmpty(category))
            {
                sparqlQuery += Environment.NewLine + "filter(regex(str(?categ), \"" + category.Trim() + "\"))";
                orderBy = "?an";
            }
            if (!String.IsNullOrEmpty(subCategory))
            {
                sparqlQuery += Environment.NewLine + "filter(regex(?subcateg, \"" + subCategory.Trim() + "\"))";
                orderBy = "?an";
            }*/

            #endregion

            if (!String.IsNullOrEmpty(yearStart) && String.IsNullOrEmpty(yearEnd))
                sparqlQuery += Environment.NewLine + "filter((str(?an) >= '" + yearStart.Trim() + "'))";
            if (!String.IsNullOrEmpty(yearEnd) && String.IsNullOrEmpty(yearStart))
                sparqlQuery += Environment.NewLine + "filter((str(?an) <= '" + yearEnd.Trim() + "'))";
            if (!String.IsNullOrEmpty(yearStart) && !String.IsNullOrEmpty(yearEnd))
                sparqlQuery += Environment.NewLine + "filter((str(?an) >= '" + yearStart.Trim() + "' && str(?an) < '" + yearEnd.Trim() + "'))";

            if(!String.IsNullOrEmpty(gen))
                sparqlQuery += Environment.NewLine + "filter(str(?gender) = \""+gen.Trim()+"\")";

            if (orderBy != "")
                sparqlQuery += Environment.NewLine + "} order by " + orderBy;
            else
                sparqlQuery += Environment.NewLine + "}";

            try
            {
                var resItems = _starDogInerface.GetItemsList(sparqlQuery);
                if (resItems.Results.Count > 0)
                {
                    foreach (SparqlResult itemsResult in resItems.Results)
                    {
                        var respItem = new DataModel();

                        var country = new CountryModel();
                        var categ = new CategoryModel();
                        var subcateg = new SubcategoryModel();

                        var countryName = itemsResult[1].ToString().Split('/').LastOrDefault().Replace('_', ' ');
                        country.CountryName = countryName;
                        country.DbpediaUrl = itemsResult[1].ToString();
                        categ.CategoryName = itemsResult[5].ToString();
                        subcateg.SubcategoryName = itemsResult[4].ToString();

                        respItem.Country = country;
                        respItem.CategoryModel = categ;
                        respItem.Subcategory = subcateg;
                        respItem.Year = itemsResult[2].ToString().Split('^')[0];
                        respItem.Value = itemsResult[3].ToString().Split('^')[0];
                        respItem.Gender = itemsResult[6].ToString();
                        respItem.Indicator = itemsResult[7].ToString();

                        responseList.Add(respItem);
                    }
                    return responseList;
                }
                return null;
            }
            catch (Exception)
            {
            }

            return null;
        }
    }
}
