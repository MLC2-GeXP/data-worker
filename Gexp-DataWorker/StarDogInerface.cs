using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gexp_DataWorker.Helpers;
using VDS.RDF.Query;

namespace Gexp_DataWorker
{
    public class StarDogInerface
    {
        /// <summary>
        /// insert rdf
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="fromFile"></param>
        /// <returns></returns>
        public bool SaveRdf(string graph, bool fromFile)
        {
            var status = false;
            try
            {
                var ins = StarDogHelper.InsertGraph(null, graph, fromFile);
                status = ins;
            }
            catch (Exception)
            {
            }

            return status;
        }

        /// <summary>
        /// query graph
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SparqlResultSet GetItemsList(string query)
        {
            var queryResult = new SparqlResultSet();
            try
            {
                var queryResponse = StarDogHelper.Query(query);
                queryResult = queryResponse;
            }
            catch (Exception)
            {
            }

            return queryResult;
        }

        /// <summary>
        /// delete graph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="fromFile"></param>
        /// <returns></returns>
        public bool DeleteGrapth(string graph, bool fromFile)
        {
            bool status = false;
            try
            {
                var del = StarDogHelper.DeleteGraph(null, graph);
                status = del;
            }
            catch (Exception)
            {
            }
            return status;
        }
    }
}
