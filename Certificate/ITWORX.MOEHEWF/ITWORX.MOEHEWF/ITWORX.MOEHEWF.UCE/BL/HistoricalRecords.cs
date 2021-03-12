using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Entities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.BL
{
    public class HistoricalRecords
    {
        public static List<Entities.SimilarRequest> GetRequestDetailsbyID(string reqID, int LCID)
        {
            List<Entities.SimilarRequest> requestDetails = new List<Entities.SimilarRequest>();
            try
            {
                Logging.GetInstance().Debug("Entering method HistoricalRecords.GetRequestDetailsbyID");
                using (SPWeb web = new SPSite(SPContext.Current.Site.Url).OpenWeb())
                {
                    string strViewFields = string.Empty;
                    if (LCID == (int)Language.English)
                    {
                        strViewFields = "<FieldRef Name='RequestNumber'/>"
                              + "<FieldRef Name='Applicants_QatarID'/>"
                              + "<FieldRef Name='Applicants_ApplicantName'/>"
                              + "<FieldRef Name='SubmitDate'/>"
                              + "<FieldRef Name='ID'/>"
                              + "<FieldRef Name='AcademicDegreeForEquivalence'/>";
                    }
                    else
                    {
                        strViewFields = "<FieldRef Name='RequestNumber'/>"
                             + "<FieldRef Name='Applicants_QatarID'/>"
                             + "<FieldRef Name='Applicants_ApplicantName'/>"
                             + "<FieldRef Name='SubmitDate'/>"
                             + "<FieldRef Name='ID'/>"
                             + "<FieldRef Name='AcademicDegreeForEquivalenceAr'/>";
                    }
                    SPQuery query = new SPQuery
                    {
                        Joins = "<Join Type='INNER' ListAlias='Applicants'>" +
                                  "<Eq>" +
                                  "<FieldRef Name='ApplicantID' RefType='Id'/>" +
                                  "<FieldRef List='Applicants' Name='ID'/>" +
                                  "</Eq>" +
                                  "</Join>",

                        ProjectedFields = "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                                            "List='Applicants' ShowField='ApplicantName'/>" +
                                            "<Field Name='Applicants_QatarID' Type='Lookup' " +
                                            "List='Applicants' ShowField='PersonalID'/>",

                        ViewFields = strViewFields,
                        Query = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Text'>" + reqID + "</Value></Eq></Where><OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>",
                    };
                    SPList customerList = web.Lists[Utilities.Constants.Requests];
                    SPListItemCollection items = customerList.GetItems(query);
                    Common.Entities.RequestStatus requestStatusItem = new Common.Entities.RequestStatus();
                    foreach (SPListItem item in items)
                    {
                        SimilarRequest Request = new SimilarRequest();
                        SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue((item["Applicants_QatarID"] != null) ? item["Applicants_QatarID"].ToString() : string.Empty);
                        Request.QatariID = ApplicantsQatarID.LookupValue;
                        SPFieldLookupValue ApplicantsApplicantName = new SPFieldLookupValue((item["Applicants_ApplicantName"] != null) ? item["Applicants_ApplicantName"].ToString() : string.Empty);
                        Request.ApplicantName = ApplicantsApplicantName.LookupValue;
                        Request.ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;
                        Request.RequestID = (item["RequestNumber"] != null) ? item["RequestNumber"].ToString() : string.Empty;
                        Request.SubmitDate = (item["SubmitDate"] != null) ? DateTime.Parse(item["SubmitDate"].ToString()) : DateTime.MinValue;

                        if (LCID == (int)Language.English)
                        {
                            SPFieldLookupValue AcademicDegree = new SPFieldLookupValue((item["AcademicDegreeForEquivalence"] != null) ? item["AcademicDegreeForEquivalence"].ToString() : string.Empty);
                            Request.AcademicDegree = AcademicDegree.LookupValue;
                        }
                        else
                        {
                            SPFieldLookupValue AcademicDegree = new SPFieldLookupValue((item["AcademicDegreeForEquivalenceAr"] != null) ? item["AcademicDegreeForEquivalenceAr"].ToString() : string.Empty);
                            Request.AcademicDegree = AcademicDegree.LookupValue;
                        }
                        requestDetails.Add(Request);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method HistoricalRecords.GetRequestDetailsbyID");
            }
            return requestDetails;
        }
    }
}
