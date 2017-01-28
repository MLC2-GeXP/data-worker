using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gexp_DataWorker.Controller;
using Gexp_DataWorker.Helpers;

namespace Gexp_DataWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            #region MainActivity

            Console.WriteLine(@"Insert rdf folder path: ");
            bool write = true;
            var folderPath = String.Empty;
            while (write)
            {
                folderPath = Console.ReadLine();
                if(string.IsNullOrEmpty(folderPath))
                    Console.WriteLine(@"Please provide a valid path.");
                else
                {
                    DirectoryInfo dir = new DirectoryInfo(folderPath);
                    if(!dir.Exists)
                        Console.WriteLine(@"Folder path dose not exist.");
                    else
                    {
                        Console.WriteLine(@"please wait! loading");
                        var spinner = new Spinner(1, 3);

                        spinner.Start();
                        var loadFile = new LoadFile();
                        var status = loadFile.LoadFilesFromFolder(folderPath);
                        spinner.Stop();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(status);
                    }
                }
                Console.WriteLine(@"==========================================" + Environment.NewLine);
                Console.WriteLine(@"Doriti sa introduceti un alt text? y/n");
                var y = Console.ReadKey();
                write = y.KeyChar == 121;
            }
            #endregion


            #region for test's

            /* var quer = new QueryDataController();
            quer.GetListOfItems("", "", "", "", "","");*/

            //StarDogInerface star = new StarDogInerface();            

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

            /* var statusDel = star.DeleteGrapth(Properties.Resources.rdf4test, true);
            if (statusDel)
                Console.WriteLine("deleted");
            else
                Console.WriteLine("error");*/

            #endregion

            #endregion

            Console.ReadKey();
        }
    }
}
