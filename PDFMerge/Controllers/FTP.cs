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

namespace PDFMerge.Controllers
{
    public static class FTP
    {
        public static void uploadFileUsingFTP(string CompleteFTPPath,
                                      HttpPostedFileBase File,
                                      string UName = "",
                                      string PWD = "")
        {
            //Create a FTP Request Object and Specfiy a Complete Path
            FtpWebRequest reqObj = (FtpWebRequest)FtpWebRequest.Create(CompleteFTPPath);

            //Call A FileUpload Method of FTP Request Object
            reqObj.Method = WebRequestMethods.Ftp.UploadFile;

            //If you want to access Resourse Protected You need to give User Name      and PWD
            reqObj.Credentials = new NetworkCredential(UName, PWD);

            //FileStream object read file from Local Drive
            var streamObj = File.InputStream;// System.IO.File.OpenRead(CompleteLocalPath);

            //Store File in Buffer
            var buffer = new Byte[streamObj.Length];

            //Read File from Buffer
            streamObj.Read(buffer, 0, buffer.Length);

            //Close FileStream Object Set its Value to nothing
            streamObj.Close();
            streamObj = null;

            //Upload File to ftp://localHost/ set its object to nothing
            reqObj.GetRequestStream().Write(buffer, 0, buffer.Length);
            reqObj = null;

        }

    }
}