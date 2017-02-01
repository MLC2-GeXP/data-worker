using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
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
        private readonly QueryCategoryAndSubcategoryController _queryController =
            new QueryCategoryAndSubcategoryController();

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
        /// Delete subcategory indicator
        /// </summary>
        /// <param name="indicatorName"></param>
        /// <returns></returns>
        [AcceptVerbs("GET", "DELETE")]
        [AllowAnonymous]
        [Route("gexp/delete/indicator")]
        public HttpResponseMessage DeleteIndicator(string indicatorName)
        {
            var status = false;
            try
            {
                if (String.IsNullOrEmpty(indicatorName))
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                status = _queryController.DeleteIndicator(indicatorName);
                if (status)
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete subcategory
        /// </summary>
        /// <param name="subcategoryName"></param>
        /// <returns></returns>
        [AcceptVerbs("GET", "DELETE")]
        [AllowAnonymous]
        [Route("gexp/delete/subcategory")]
        public HttpResponseMessage DeleteSubcategory(string subcategoryName)
        {
            var status = false;
            try
            {
                if (String.IsNullOrEmpty(subcategoryName))
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                status = _queryController.DeleteSubcategory(subcategoryName);
                if (status)
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [AcceptVerbs("GET", "DELETE")]
        [AllowAnonymous]
        [Route("gexp/delete/category")]
        public HttpResponseMessage DeleteCategory(string category)
        {
            var status = false;
            try
            {
                if (String.IsNullOrEmpty(category))
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                status = _queryController.DeleteCategory(category);
                if (status)
                    return Request.CreateResponse(HttpStatusCode.OK);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
