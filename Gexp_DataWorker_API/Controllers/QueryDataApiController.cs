using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text.RegularExpressions;
using System.Web.Http;
using Gexp_DataWorker.Controller;
using Gexp_DataWorker.Models;

namespace Gexp_DataWorker_API.Controllers
{
    /// <summary>
    /// Controller for querying data /country/category/subcategory/year/gender
    /// </summary>
    public class QueryDataApiController : ApiController
    {
        /// <summary>
        /// Get list of items and filter based on query parameters (by default none of them are applied,  excepting category, subcategory and indicator which are required parameters)
        /// </summary>
        /// <param name="indicator"></param>
        /// <param name="countryUrl"></param>
        /// <param name="category"></param>
        /// <param name="subcategory"></param>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("gexp/QueryDataApi")]
        public HttpResponseMessage GetListOfItems(string category, string subcategory, string indicator, string countryUrl = "all", string startYear = "n/a", string endYear = "n/a", string gender = "n/a")
        {
            QueryDataController queryData = new QueryDataController();

            //required field
            if (category == "")
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            if (subcategory == "")
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            if (indicator == "")
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            //check if we have a list of countries
            var countryCheck = countryUrl.Split(',');
            List<string> countryUriList = new List<string>();

            if (countryUrl != "all")
            {
                if (countryCheck.Length > 1)
                {
                    //we have a list :))
                    var list = countryUrl.Split(',');
                    foreach (var item in list)
                    {
                        if (!IsValidUrl(countryUrl))
                            return Request.CreateResponse(HttpStatusCode.BadRequest);
                        else
                        {
                            if (IsValidUrl(item))
                                countryUriList.Add(item);
                            else
                                return Request.CreateResponse(HttpStatusCode.BadRequest);
                        }
                    }
                }
                else if (IsValidUrl(countryUrl))
                    countryUriList.Add(countryUrl);
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var yearPattern = @"\b\d{4}\b";
            if (startYear != "n/a")
                if (!Regex.IsMatch(startYear, yearPattern))
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

            if (endYear != "n/a")
                if (!Regex.IsMatch(endYear, yearPattern))
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

            if(gender != "n/a")
                if (!IsValidGender(gender))
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                var response = new List<DataModel>();

                #region set params

                if (startYear == "n/a")
                    startYear = "";
                if (endYear == "n/a")
                    endYear = "";
                if (gender == "n/a")
                    gender = "";

                #endregion

                response = queryData.GetListOfItems(countryUriList, category, subcategory, indicator, startYear, endYear, gender);

                if (response != null)
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        static bool IsValidUrl(string urlString)
        {
            Uri uri;
            return Uri.TryCreate(urlString, UriKind.Absolute, out uri)
                && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }

        static bool IsValidGender(string gender)
        {
            switch (gender)
            {
                case "female":
                    return true;
                case "male":
                    return true;
                case "all":
                    return true;
            }
            return false;
        }
    }
}
