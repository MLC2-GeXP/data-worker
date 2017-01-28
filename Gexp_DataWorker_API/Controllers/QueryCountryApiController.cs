using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Gexp_DataWorker;
using Gexp_DataWorker.Controller;
using Gexp_DataWorker.Models;

namespace Gexp_DataWorker_API.Controllers
{
    /// <summary>
    /// Controller for countries requests
    /// </summary>
    public class QueryCountryApiController : ApiController
    {
        private readonly QueryCountryController _queryController = new QueryCountryController();

        /// <summary>
        /// Get list of all available countries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("gexp/countries")]
        public HttpResponseMessage GetListOfCountries()
        {
            var response = new List<CountryModel>();
            try
            {
                response = _queryController.GetListOfCountries();
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get country resource by name
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("gexp/country")]
        public HttpResponseMessage GetCountry(string countryName)
        {
            var response = new CountryModel();
            try
            {
                if (String.IsNullOrEmpty(countryName))
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                else
                {
                    response = _queryController.GetCountry(countryName);
                    if (response != null)
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    else
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
