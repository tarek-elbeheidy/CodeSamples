
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.SharePoint;
using System.Web;

using Microsoft.SharePoint.Utilities;
using iTextSharp.text.html;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Word;
using BarcodeLib;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHEWF.Common.Utilities;



namespace ITWORX.MOEHEWF.SCE.BL
{
    public class SCERequestAttachments
    {
        

        public static void ConvertHTMLToPDF(string htmlString, string requestID, RequestStatus currentRequestStatus)
        {

            System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
            MemoryStream output = GeneratePDF(htmlString);


            //Calling Response
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Cache.SetCacheability(HttpCacheability.Private);
            Response.ContentType = "application /pdf";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.pdf", "FinalDecision"));
            Response.BinaryWrite(output.ToArray());







        }




        public static MemoryStream GeneratePDF(string htmlString)
        {
           

            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdfBytes = htmlToPdf.GeneratePdf(htmlString);
            MemoryStream output = new MemoryStream(pdfBytes);

            return output;

        }

        public static MemoryStream GetPDFFile(string htmlString,string requestNumber, RequestStatus currentRequestStatus)
        {
           
            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdfBytes = htmlToPdf.GeneratePdf(htmlString);
            MemoryStream output = new MemoryStream(pdfBytes);

            return output;

        }

      


        public static void ConvertWordToPDF(string formatedText)
        {
            Microsoft.Office.Interop.Word.Document wordDocument;
            Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();
            wordDocument = appWord.Documents.Open(@"D:\desktop\xxxxxx.docx");
            wordDocument.ExportAsFixedFormat(@"D:\desktop\DocTo.pdf", WdExportFormat.wdExportFormatPDF);

        }


        private static string GetAttachmentUrls(SPListItem oItem, bool IsDraft)
        {

            string path = string.Empty;

            try

            {
                if (IsDraft)
                {
                    path = SPUrlUtility.CombineUrl(oItem.Attachments.UrlPrefix, oItem.Attachments[0]);

                }
                else
                {
                    SPUrlUtility.CombineUrl(oItem.Attachments.UrlPrefix, oItem.Attachments[1]);
                }

                //path = (from string file in oItem.Attachments
                //        orderby file
                //        select SPUrlUtility.CombineUrl(oItem.Attachments.UrlPrefix, file)).FirstOrDefault();
                return path;
            }
            catch
            {
                return string.Empty;
            }

        }

        public static void ViewAttachments(string ListName, bool IsDraft, int RequestID, HttpResponse Response)
        {

            SPSecurity.RunWithElevatedPrivileges(() =>
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    { 
                        web.AllowUnsafeUpdates = true;
                        SPList list = web.Lists[ListName];
                        SPListItemCollection collListItems = null;
                        var camelQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name ='ID'/><Value Type = 'Number'>" +
                            RequestID.ToString() + "</Value></Eq></Where>");

                        collListItems = list.GetItems(camelQuery);

                        if (collListItems != null)
                        {
                            string attachments = GetAttachmentUrls(collListItems[0], true);

                            if (attachments != string.Empty)
                            {
                                WebClient client = new WebClient();
                                client.Credentials= CredentialCache.DefaultCredentials;
                                Byte[] buffer = client.DownloadData(attachments);
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("content-length", buffer.Length.ToString());
                                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.pdf", "Certificate"));
                                Response.BinaryWrite(buffer);
                                Response.End();
                            }

                        }
                    }
                }
            });

        }

        public  static string GenerateBarcodeImage(string barcodeNumber)
        {
            BarcodeLib.TYPE type = BarcodeLib.TYPE.UNSPECIFIED;
            type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.IncludeLabel = true;
            b.LabelFont = new System.Drawing.Font(b.LabelFont.Name, 15f);
           // b.ForeColor = System.Drawing.Color.Gray;
            System.Drawing.Image img = b.Encode(type, barcodeNumber, 400, 50);
            byte[] ImgeByte = b.Encoded_Image_Bytes;
            string base64String = Convert.ToBase64String(ImgeByte, 0, ImgeByte.Length);
            return base64String;

        }

        public static string GenerateImage(string path)
        {

           
            string base64String = string.Empty;
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                   
                    base64String = Convert.ToBase64String(imageBytes);


                }
            }


            return base64String;

        }

    }
}
