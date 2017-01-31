using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly StarDogInerface _starDogInerface = new StarDogInerface();

        public string LoadFilesFromFolder(string folderPath)
        {
            var status = string.Empty;
            if (String.IsNullOrEmpty(folderPath))
                return status = "folder path must be provided.";

            DirectoryInfo di = new DirectoryInfo(folderPath);
            try
            {
                int nrFilesSucces = 0;
                int nrFilesError = 0;
                List<string> errorFilesListName = new List<string>();
                foreach (FileInfo file in di.GetFiles("*.rdf"))
                {
                    var st = _starDogInerface.SaveRdf(file.FullName, true);
                    if (st)
                        nrFilesSucces++;
                    else
                    {
                        nrFilesError++;
                        errorFilesListName.Add(file.Name + Environment.NewLine);
                    }
                }
                string formatedErrorFilesListName = null;
                foreach (var item in errorFilesListName)
                    formatedErrorFilesListName += item + " " + Environment.NewLine;
                
                
                status = String.Format("Uploaded successfully {0} , not uploaded {1}" + Environment.NewLine + "Error on file/s: {2}", nrFilesSucces, nrFilesError, formatedErrorFilesListName);
            }
            catch (Exception exception)
            {
                status = "There was an error: " + exception.Message;
            }

            return status;
        }
    }
}
