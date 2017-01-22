using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinqToExcel;
using Gexp_DataWorker.Helpers;
using Gexp_DataWorker.Models;

namespace Gexp_DataWorker.Helpers
{
    public class LoadFile
    {
        public string LoadFileFromPath(string filePath)
        {
            var xmlStringResponse = String.Empty;

            try
            {
                var excelFactory = new ExcelQueryFactory(filePath);
                if (excelFactory != null)
                {
                    var worksheet = excelFactory.GetWorksheetNames();
                    if (worksheet.Count() == 0)
                        return null;

                    var worksheetData = worksheet.FirstOrDefault(x => x.Contains("Data"));
                    var colNames = excelFactory.GetColumnNames(worksheetData);
                    var contrylist = from c in excelFactory.WorksheetRangeNoHeader("A2", "A300", worksheetData) select c;

                    //filter columns
                    var yearPatern = "^[12][0-9]{3}$";
                    var colNamesFiltered = new List<string>();
                    foreach (var colName in colNames)
                    {
                        if (Regex.Match(colName, yearPatern, RegexOptions.IgnoreCase) != Match.Empty)
                            colNamesFiltered.Add(colName);
                    }

                    //create country list
                    var countryList = new List<CountryModel>();
                    foreach (var countryItem in contrylist)
                    {
                        if (countryItem.ToString() != "")
                        {
                            var dbInfo = GetCountryWikipage(countryItem.FirstOrDefault().Value.ToString());
                            if (dbInfo.CountryName != null || dbInfo.WikiCountryUrl != null)
                                countryList.Add(dbInfo);
                        }
                    }

                    if (countryList.Count > 0)
                    {
                        var test = from c in excelFactory.Worksheet(worksheetData) where c["1979"] == "1979" select c;
                    }
                }
            }
            catch (Exception exception)
            {
                xmlStringResponse = "There was an error: " + exception.Message;
            }

            return xmlStringResponse;
        }

        private CountryModel GetCountryWikipage(string countryName)
        {
            var responseCountry = new CountryModel();

            if (!String.IsNullOrEmpty(countryName))
            {
                var endpoint = "http://dbpedia.org/sparql";
                var endpointGraph = "http://dbpedia.org";

                var prefix = "prefix prov: <http://www.w3.org/ns/prov#> " + Environment.NewLine +
                             "prefix dbpedia: <http://dbpedia.org/resource/>";

                var qTestSpace = countryName.Split(' ');
                if (qTestSpace.Count() > 1)
                    countryName = countryName.Replace(' ', '_');

                string query = "select ?p ?o where " +
                           "{ dbpedia:" + countryName + " ?p ?o " +
                           "filter strstarts(str(?p),str(prov:))" +
                           "}";

                var createFullQuery = prefix + query;
                var dbpediaResponse = SparqlHelper.QueryRemoteEndpoint(createFullQuery, endpoint, endpointGraph);

                if (dbpediaResponse.Count > 0)
                {
                    foreach (var responseResult in dbpediaResponse.Results)
                    {
                        if (responseResult.Count == 2)
                        {
                            var deriveItem4Check = responseResult[0].ToString().Split('#').LastOrDefault();
                            if (deriveItem4Check == "wasDerivedFrom")
                            {
                                var wikiUri4Check = responseResult[1];

                                responseCountry.CountryName = countryName;
                                responseCountry.WikiCountryUrl = wikiUri4Check.ToString();
                                break;
                            }
                        }
                    }
                }
                else
                {
                    responseCountry.CountryName = countryName;
                    responseCountry.WikiCountryUrl = "n/a";
                }
            }

            return responseCountry;
        }
    }
}
