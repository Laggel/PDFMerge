using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PDFMerge.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Merge()
        {
            List<MemoryStream> files = new List<MemoryStream>();
           
            files.Add(GetPdf(@"http://laggel.site90.com/PDF/Con1.pdf"));
            files.Add(GetPdf(@"http://laggel.site90.com/PDF/Con2.pdf"));
            files.Add(GetPdf(@"http://laggel.site90.com/PDF/Con3.pdf"));
            
            var OutPut = @"C:\Users\Simetri\Dropbox\UNIBE\2013-1\Negocios Electronicos\SLN - Random PDF Merge\Documentos\Result.pdf";

            Merge(files.ToArray(), OutPut);

            return View();
        }

        static MemoryStream GetPdf(string strPDF)
        { 
            
            byte[] buffer = new byte[4096];

            var wr = WebRequest.Create(strPDF);
                
            var response = wr.GetResponse();
            var responseStream = response.GetResponseStream();
            
            var memoryStream = new MemoryStream();
            int count = 0;
            do
            {
                count = responseStream.Read(buffer, 0, buffer.Length);
                memoryStream.Write(buffer, 0, count);
            } while (count != 0);

            return memoryStream;
        }

        /// <summary>
        /// Imports all pages from a list of documents.
        /// </summary>
        static void Merge(MemoryStream[] files, string filename)
        {
            // Open the output document
            PdfDocument outputDocument = new PdfDocument();

            // Iterate files
            foreach (var file in files)
            {
                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                // Iterate pages
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    // Get the page from the external document...
                    PdfPage page = inputDocument.Pages[idx];
                    // ...and add it to the output document.
                    outputDocument.AddPage(page);
                }
            }

            // Save the document...
            outputDocument.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }


        /// <summary>
        /// Imports all pages from a list of documents.
        /// </summary>
        static void Merge(string[] files, string filename)
        {
            // Open the output document
            PdfDocument outputDocument = new PdfDocument();
            
            // Iterate files
            foreach (string file in files)
            {
                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);
                
                // Iterate pages
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    // Get the page from the external document...
                    PdfPage page = inputDocument.Pages[idx];
                    // ...and add it to the output document.
                    outputDocument.AddPage(page);
                }
            }
            
            // Save the document...
            outputDocument.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
