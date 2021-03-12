using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using System.Web;
using System.Data;
using System.ComponentModel;
using System.Threading;
using System.Globalization;

namespace MOEHE.PSPES.Repository
{
    class Utility
    {
        /// <summary>
        /// we use this method to get the connection to the WebAPI service
        /// </summary>
        /// <returns></returns>
        public bool isWindowClosed { get; set; }
        public static HttpClient GetHttpClientConnection()
        {
            string serviceURL = ConfigurationManager.AppSettings["WebAPIURL"]; 
           //HttpClient cons = new HttpClient() { BaseAddress = new Uri("http://localhost:17802/") };
           HttpClient cons = new HttpClient() { BaseAddress = new Uri(serviceURL) };


            cons.DefaultRequestHeaders.Accept.Clear();
            cons.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return cons;
        }


        public static HttpClient GetSMSHttpClientConnection()
        {
            string hodhodServiceURL = ConfigurationManager.AppSettings["HodHodAPIURL"];
            HttpClient cons = new HttpClient() { BaseAddress = new Uri(hodhodServiceURL) };


            cons.DefaultRequestHeaders.Accept.Clear();
            cons.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return cons;
        }
        public static string GenerateOTP()
        {
            string[] allowChar = { "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            string OTP = string.Empty;
            string temp = string.Empty;
            Random rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                int p = rand.Next(0, allowChar.Length);
                temp = allowChar[rand.Next(0, allowChar.Length)];
                OTP += temp;
            }
            return OTP;
        }

        public static bool MOETERM_CURRENT(string schoolCode)
        {
            bool isClosed = false;
            try
            {
                var term = TermRepository.GetTerms();
            }
            catch (Exception ex)
            {

            }
            return isClosed;
        }
        public static bool MOETERM_NEXT(string schoolCode)
        {
            bool isClosed = false;
            try
            {
                var term = TermRepository.GetTerms();
            }
            catch (Exception ex)
            {

            }
            return isClosed;
        }
        public static bool MOETERM_LAST(string schoolCode)
        {
            bool isClosed = false;
            try
            {
                var term = TermRepository.GetTerms();
            }
            catch (Exception ex)
            {

            }
            return isClosed;
        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            DataTable table = new DataTable();
            try
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                foreach (PropertyDescriptor prop in properties)
                    if (prop.Name != "EntityState" && prop.Name != "EntityKey")
                    {
                        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    }
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                        if (prop.Name != "EntityState" && prop.Name != "EntityKey")
                        {
                            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                        }
                    table.Rows.Add(row);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return table;
        }
        public static void ExportToExcel(DataTable table, string filename)
        {
            try
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                //HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");

                HttpContext.Current.Response.Charset = "UTF-8";
                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;

                HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
                HttpContext.Current.Response.Write("<BR><BR><BR>");

                HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");

                int columnscount = table.Columns.Count;

                for (int j = 0; j < columnscount; j++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write("<B>");
                    HttpContext.Current.Response.Write(table.Columns[j].ColumnName.ToString());
                    HttpContext.Current.Response.Write("</B>");
                    HttpContext.Current.Response.Write("</Td>");
                }
                HttpContext.Current.Response.Write("</TR>");
                foreach (DataRow row in table.Rows)
                {
                    HttpContext.Current.Response.Write("<TR>");
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write(row[i].ToString());
                        HttpContext.Current.Response.Write("</Td>");
                    }

                    HttpContext.Current.Response.Write("</TR>");
                }
                HttpContext.Current.Response.Write("<TR>");
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(table.Rows.Count.ToString());
                HttpContext.Current.Response.Write("</TR>");
                HttpContext.Current.Response.Write("</Table>");
                HttpContext.Current.Response.Write("</font>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.Response.End();
            }
            catch (ThreadAbortException) { }
            catch (Exception){}
        }

        public static StringBuilder ExportToPDF(DataTable table)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                sb.Append("<HTML><HEAD>PRE-ENROLLMENT DOCUMENT</HEAD><BODY>");
                sb.Append("<font style='font-size:10.0pt; font-family:Calibri;'>");
                sb.Append("<BR><BR><BR>");
                sb.Append("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
                int columnscount = table.Columns.Count;
                for (int j = 0; j < columnscount; j++)
                {
                    sb.Append("<Td><B>");
                    sb.Append(table.Columns[j].ColumnName.ToString());
                    sb.Append("</B></Td>");
                }
                sb.Append("</TR>");
                foreach (DataRow row in table.Rows)
                {
                    sb.Append("<TR>");
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        sb.Append("<Td>");
                        sb.Append(row[i].ToString());
                        sb.Append("</Td>");
                    }

                    sb.Append("</TR>");
                }
                sb.Append("<TR>");
                sb.Append("<Td>");
                sb.Append(table.Rows.Count.ToString());
                sb.Append("</TR>");
                sb.Append("</Table>");
                sb.Append("</font>");
                sb.Append("</BODY></HTML>");
            }
            catch (Exception ex)
            {
                throw;
            }
            return sb;
        }
        public static DateTime ConvertDateTime(string Date)
        {
            DateTime date = new DateTime();
            try
            {
                string CurrentPattern = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
                string[] Split = new string[] { "-", "/", @"\", "." };
                string[] Patternvalue = CurrentPattern.Split(Split, StringSplitOptions.None);
                string[] DateSplit = Date.Split(Split, StringSplitOptions.None);
                string NewDate = "";
                if (Patternvalue[0].ToLower().Contains("d") == true && Patternvalue[1].ToLower().Contains("m") == true && Patternvalue[2].ToLower().Contains("y") == true)
                {
                    NewDate = DateSplit[1] + "/" + DateSplit[0] + "/" + DateSplit[2];
                }
                else if (Patternvalue[0].ToLower().Contains("m") == true && Patternvalue[1].ToLower().Contains("d") == true && Patternvalue[2].ToLower().Contains("y") == true)
                {
                    NewDate = DateSplit[0] + "/" + DateSplit[1] + "/" + DateSplit[2];
                }
                else if (Patternvalue[0].ToLower().Contains("y") == true && Patternvalue[1].ToLower().Contains("m") == true && Patternvalue[2].ToLower().Contains("d") == true)
                {
                    NewDate = DateSplit[2] + "/" + DateSplit[0] + "/" + DateSplit[1];
                }
                else if (Patternvalue[0].ToLower().Contains("y") == true && Patternvalue[1].ToLower().Contains("d") == true && Patternvalue[2].ToLower().Contains("m") == true)
                {
                    NewDate = DateSplit[2] + "/" + DateSplit[1] + "/" + DateSplit[0];
                }
                date = DateTime.Parse(NewDate, Thread.CurrentThread.CurrentCulture);
            }
            catch (Exception ex)
            {

            }
            return date;
        }
    }
}
