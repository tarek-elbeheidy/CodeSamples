using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;
using ITWORX.MOEHE.Utilities.Logging;
using System.Web.Script.Services;
using System.Web.Services;
using ITWORX.MOEHEWF.Common.Entities;

namespace ITWORX.MOEHEWF.PA.Layouts.ITWORX.MOEHEWF.PA
{
    public partial class CascadingDropdowns : UnsecuredLayoutsPageBase
    {
        private static int lCID = 0;
        public static int LCID
        {
            get => lCID;
            private set => lCID = value;
        }
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
        public static List<University> GetUniversities(int year, int countryId)
        {
            Logging.GetInstance().Debug("Entering method CascadingDropdowns.GetUniversities");
            List<University> university = new List<University>();
            try
            {
                var sp = SPContext.Current;
                university =Common.BL.University.GetUniversityByYearAndCountry(year,countryId, LCID);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method CascadingDropdowns.GetUniversities");
            }
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

    }
}