using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;
using PDFMerge.Models;

namespace PDFMerge.Controllers
{
    public class HomeController : Controller
    {
        UsersContext db = new UsersContext();

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Merge()
        {
            //Merge Those Files
            List<MemoryStream> files = new List<MemoryStream>();
           
            files.Add(GetPdf(@"http://laggel.site90.com/PDF/Con1.pdf"));
            files.Add(GetPdf(@"http://laggel.site90.com/PDF/Con2.pdf"));
            files.Add(GetPdf(@"http://laggel.site90.com/PDF/Con3.pdf"));
            files.Add(GetPdf(@"http://laggel.site90.com/PDF/Tar1.1.pdf"));
            
            var archivo = Merge(files.ToArray());
            
            //string path = @"C:\Somefile.pdf";
            //WebClient client = new WebClient();
            Byte[] buffer = ObjectToByteArray(archivo); // client.DownloadData(path);

            if (buffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
            }

            return View();
        }

        public ActionResult MergeEdubook(int materia, int curso)
        {
            //Get the files name array
            var files = GetFiles(materia, curso);

            //Merge the PDFs
            var archivo = Merge(files.ToArray());
            
            //Convert to response
            var buffer = ObjectToByteArray(archivo); 

            if (buffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
            }

            return View();
        }

        public List<MemoryStream> GetFiles(int materia, int curso)
        {
            var files = new List<MemoryStream>();
            var site = "http://laggel.site90.com/PDF/";
            
            //Get The Content File
            files.Add(GetPdf(@String.Format("{0}{1}.{2}.pdf",files,materia,curso)));
            
            //Get The Homework file
            var cnt = db.Recursos.Where(x => x.MateriaId == materia &&
                                             x.CursoId == curso).Count();

            var rand = new Random().Next(cnt);

            files.Add(GetPdf(@String.Format("{0}{1}.{2}.{3}.pdf", files, materia, curso,rand)));
            
            //files.Add(GetPdf(@"http://laggel.site90.com/PDF/Con3.pdf"));
            //files.Add(GetPdf(@"http://laggel.site90.com/PDF/Tar1.1.pdf"));

            return files;
        }

        byte[] ObjectToByteArray(Object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var b = new BinaryFormatter();
                b.Serialize(ms, obj);
                return ms.ToArray();
            }
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
        static MemoryStream Merge(MemoryStream[] files)
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

            var stream = new MemoryStream();
            outputDocument.Save(stream);
            
            // Save the document...
            //outputDocument.Save(filename);
            // ...and start a viewer.
            //Process.Start(filename);

            return stream;
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



        public ActionResult Upload()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                FTP.uploadFileUsingFTP("ftp://laggel.site90.com/public_html/PDF/" + fileName, file, "a7673407", "Laggel007");
            }

            return View();
        }

        
    }
}
