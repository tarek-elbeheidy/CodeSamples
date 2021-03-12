using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;
using ITWORX.MOEHE.Utilities.Logging;
using System.Web.Script.Services;
using System.Web.Services;
using ITWORX.MOEHEWF.Common.Entities;
using System.Web.Script.Serialization;
using System.Web;

namespace ITWORX.MOEHEWF.UCE.Layouts.ITWORX.MOEHEWF.UCE
{
    public partial class CascadingDropdowns : UnsecuredLayoutsPageBase
    {
       

        protected override bool AllowAnonymousAccess { get { return true; } }
        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);
            LCID = System.Threading.Thread.CurrentThread.CurrentUICulture.LCID;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetUniversities(int year, int countryId,int LCID)
        {
            Logging.GetInstance().Debug("Entering method CascadingDropdowns.GetUniversities");
            string jsonObject = string.Empty;
            List<University> university = new List<University>();
            try
            {


                var sp = SPContext.Current;
                university =Common.BL.University.GetUniversityByYearAndCountry(year,countryId,LCID);

                JavaScriptSerializer jss = new JavaScriptSerializer();
                jss.MaxJsonLength = Int32.MaxValue;
                jsonObject = jss.Serialize(university);
               
                
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method CascadingDropdowns.GetUniversities");
            }
            //return university;
            return jsonObject;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<University> GetUniversitiesFromAutoComplete(int year, int countryId, int LCID,string prefix)
        {
            Logging.GetInstance().Debug("Entering method CascadingDropdowns.GetUniversitiesFromAutoComplete");
           
            List<University> university = new List<University>();
            try
            {
                var sp = SPContext.Current;
                university = Common.BL.University.GetUniversityByYearAndCountry(year, countryId,LCID);
                
                university = LCID == 1033? university.FindAll(u => u.EnglishTitle.ToLower().StartsWith(prefix.ToLower()))
                        : university.FindAll(u => u.ArabicTitle.ToLower().StartsWith(prefix.ToLower()));

            }
          
           
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method CascadingDropdowns.GetUniversitiesFromAutoComplete");
            }
            //return university;
            return university;

        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetUniversityList( int universityId, int year)
        {
            Logging.GetInstance().Debug("Entering method CascadingDropdowns.GetUniversityList");
            string universityList = string.Empty;
            try
            {
                var sp = SPContext.Current;
               universityList = Common.BL.UniversityLists.GetUniversityListByUniversityId(universityId, year);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method CascadingDropdowns.GetUniversityList");
            }
            return universityList;


        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string DeleteOrganizationalAttachment(int working)
        {
            Logging.GetInstance().Debug("Entering method CascadingDropdowns.DeleteOrganizationalAttachment");
            string universityList = string.Empty;
            try
            {
                var sp = SPContext.Current;
                int requestId = 0;
                if ( HttpContext.Current.Session["EditRequestId"] != null)
                {
                    requestId = int.Parse(HttpContext.Current.Session["EditRequestId"].ToString());
                }
                else if (HttpContext.Current.Session["NewRequestId"] != null)
                {
                    requestId = int.Parse(HttpContext.Current.Session["NewRequestId"].ToString());
                }
                if (working==0)
                {
                    BL.OrganizationlLettersAttachments.DeleteOrganizationalAttachmentsByGroupAndRequestID(requestId, "CopyOfOrganizationlLetter");
                }
                else
                {
                    BL.OrganizationlLettersAttachments.DeleteOrganizationalAttachmentsByGroupAndRequestID(requestId, "NotWorkingCopyOfOrganizationlLetter");
                }
           
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method CascadingDropdowns.DeleteOrganizationalAttachment");
            }
            return universityList;


        }
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public static void SetFileUploadDrodownText(string value)
        //{
        //    Logging.GetInstance().Debug("Entering method SetFileUploadCategory.SetFileUploadDrodownText");

        //    try
        //    {

        //        HttpContext.Current.Session["FileCategory"] = value;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logging.GetInstance().LogException(ex);
        //    }
        //    finally
        //    {

        //        Logging.GetInstance().Debug("Exiting method SetFileUploadCategory.SetFileUploadDrodownText");
        //    }


        //}
    }
}