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
        /// Get list of items and filter based on query parameters (by default none of them are applied,  unless they are specified)
        /// </summary>
        /// <param name="countryUrl"></param>
        /// <param name="category"></param>
        /// <param name="subcategory"></param>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public HttpResponseMessage GetListOfItems(string countryUrl = "all", string category = "all", string subcategory = "all", string startYear = "n/a", string endYear = "n/a", string gender = "all")
        {
            QueryDataController queryData = new QueryDataController();

            if(countryUrl != "all")
                if (!IsValidUrl(countryUrl))
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

            var yearPattern = @"\b\d{4}\b";
            if (startYear != "n/a")            
                if(!Regex.IsMatch(startYear, yearPattern))
                    return Request.CreateResponse(HttpStatusCode.BadRequest);                            

            if (endYear != "n/a")
                if (!Regex.IsMatch(endYear, yearPattern))
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

            if(gender != "all")
                if (!IsValidGender(gender))
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                var response = new List<DataModel>();

                #region set params

                if (countryUrl == "all")
                    countryUrl = "";
                if (category == "all")
                    category = "";
                if (subcategory == "all")
                    subcategory = "";
                if (startYear == "n/a")
                    startYear = "";
                if (endYear == "n/a")
                    endYear = "";              

                #endregion

                response = queryData.GetListOfItems(countryUrl, category, subcategory, startYear, endYear, gender);

                if (response.Count > 0)
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
            }
            return false;
        }
    }
}
