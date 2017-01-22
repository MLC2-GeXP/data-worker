using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gexp_DataWorker.Helpers;

namespace Gexp_DataWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            StarDogInerface star = new StarDogInerface();            

            #region insert

            /*var statusInser = star.SaveRdf(Properties.Resources.rdf4test);
            if(statusInser)
                Console.WriteLine("inserted");
            else
                Console.WriteLine("error");*/

            #endregion

            #region query

            /*var query = "select distinct ?p ?label where { ?s ?p ?o . OPTIONAL { ?p rdfs:label ?label }}";
            var resItems = star.GetItemsList(query);

            foreach (var result in resItems.Results)
            {
                Console.WriteLine(result);
                Console.WriteLine("===================================================");
            }*/

            #endregion

            #region delete

            var statusDel = star.DeleteGrapth(Properties.Resources.rdf4test, true);
            if (statusDel)
                Console.WriteLine("deleted");
            else
                Console.WriteLine("error");

            #endregion

            Console.ReadKey();
        }
    }
}
