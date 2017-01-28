using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Gexp_DataWorker.Controller;
using Gexp_DataWorker.Models;

namespace Gexp_DataWorker_API.Controllers
{
    /// <summary>
    /// Controller for category and subcategory requests
    /// </summary>
    public class QueryCategoryApiController : ApiController
    {
        private readonly QueryCategoryAndSubcategoryController _queryController = new QueryCategoryAndSubcategoryController();

        /// <summary>
        /// Get list of categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("gexp/categories")]
        public HttpResponseMessage GetListOfCategories()
        {
            var response = new List<CategoryModel>();
            try
            {
                response = _queryController.GetListOfCategory();
                if (response != null)
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get list of categories and their subcategory
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("gexp/categoriesAndsubcategories")]
        public HttpResponseMessage GetListOfCategoriesWithSubcategories()
        {
            var response = new List<CategoryAndSubcategoryModel>();
            try
            {
                response = _queryController.GetListOfCategoryWihtSubCategory();
                if(response != null)
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
