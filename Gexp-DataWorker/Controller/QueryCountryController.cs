using System;
using System.Collections.Generic;
using System.Linq;
using Gexp_DataWorker.Models;
using VDS.RDF.Query;

namespace Gexp_DataWorker.Controller
{
    public class QueryCountryController
    {
        private readonly StarDogInerface _starDogInerface = new StarDogInerface();

        public List<CountryModel> GetListOfCountries()
        {
            var responseList = new List<CountryModel>();
            var sparqlQuery = "SELECT distinct ?p ?o {?p <http://purl.org/dc/terms/identifier> ?o .} order by ?o";
            try
            {
                var resItems = _starDogInerface.GetItemsList(sparqlQuery);                
                if (resItems.Count > 0)
                {
                    var lastItem = String.Empty;
                    foreach (SparqlResult itemsResult in resItems.Results)
                    {
                        var countryItem = new CountryModel();

                        if (itemsResult.LastOrDefault().Value.ToString().Length > 3)
                        {
                            if (IsAllUpper(itemsResult.LastOrDefault().Value.ToString()))
                            {
                                if (lastItem != itemsResult.FirstOrDefault().Value.ToString())
                                {
                                    countryItem.CountryName = itemsResult.LastOrDefault().Value.ToString();
                                    countryItem.DbpediaUrl = itemsResult.FirstOrDefault().Value.ToString();

                                    if (countryItem.CountryName != "" && countryItem.DbpediaUrl != "")
                                        responseList.Add(countryItem);

                                    lastItem = itemsResult.FirstOrDefault().Value.ToString();
                                }
                            }
                        }
                    }
                    return responseList;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return responseList;
        }

        public CountryModel GetCountry(string countryName)
        {
            CountryModel countryItem = null;

            if (!String.IsNullOrEmpty(countryName))
            {
                var whereStatment = "{?p <http://purl.org/dc/terms/identifier> ?o . filter regex(?o, \"" + countryName + "\", \"i\")}";
                var sparqlQuery = String.Format("SELECT distinct ?p ?o {0} order by ?o", whereStatment);
                var resItems = _starDogInerface.GetItemsList(sparqlQuery);
                if (resItems.Count > 0)
                {
                    var item = resItems.Results.FirstOrDefault();
                    if (item.LastOrDefault().Value.ToString().Length > 3)
                    {
                        if (IsAllUpper(item.LastOrDefault().Value.ToString()))
                        {
                            countryItem = new CountryModel();

                            countryItem.CountryName = item.LastOrDefault().Value.ToString();
                            countryItem.DbpediaUrl = item.FirstOrDefault().Value.ToString();

                            return countryItem;
                        }
                    }
                }
            }

            return countryItem;
        }

        private bool IsAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsLetter(input[i]) && !Char.IsUpper(input[i]))
                    return false;
            }
            return true;
        }
    }
}
