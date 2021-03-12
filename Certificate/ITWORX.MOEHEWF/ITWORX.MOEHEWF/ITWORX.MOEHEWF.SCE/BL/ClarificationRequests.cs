using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.SCE.BL
{
    public static class ClarificationRequests
    {
        public static Entities.ClarificationRequests GetClarRequest_ForReply(string ID)
        {
            Entities.ClarificationRequests ClarReq = new Entities.ClarificationRequests();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method ClarificationRequests.GetClarRequest_ForReply");
                            SPList list = web.Lists[Utilities.Constants.SCEClarificationsRequests];
                            SPListItem item = list.GetItemById(Convert.ToInt32(ID));
                            if (item != null)
                            {
                                ClarReq.RequestedClarification = (item["RequestedClarification"] != null) ? item["RequestedClarification"].ToString() : string.Empty;
                                ClarReq.ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                            //handle exception as the business require.
                        }
                        finally
                        {
                            //dispose any objects that require dispose.
                            Logging.GetInstance().Debug("Exiting method ClarificationRequests.GetClarRequest_ForReply");
                        }
                    }
                }
            });
            return ClarReq;
        }

        public static Entities.ClarificationRequests GetClarificationbyID(string ID)
        {
            Entities.ClarificationRequests ClarReq = new Entities.ClarificationRequests();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method ClarificationRequests.GetClarificationbyID");
                            SPList list = web.Lists[Utilities.Constants.SCEClarificationsRequests];
                            SPListItem item = list.GetItemById(Convert.ToInt32(ID));
                            if (item != null)
                            {
                                SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                ClarReq.RequestedClarification = (item["RequestedClarification"] != null) ? item["RequestedClarification"].ToString() : string.Empty;
                                ClarReq.ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;
                                ClarReq.AssignedTo = (item["ClarAssignedTo"] != null) ? item["ClarAssignedTo"].ToString() : string.Empty;
                                ClarReq.ClarificationReply = (item["ClarificationReply"] != null) ? item["ClarificationReply"].ToString() : string.Empty;
                                ClarReq.ReplyDate = item["ReplyDate"] == null ? DateTime.MinValue : DateTime.Parse(item["ReplyDate"].ToString());
                                ClarReq.RequestClarificationDate = item["ClarificationDate"] == null ? DateTime.MinValue : DateTime.Parse(item["ClarificationDate"].ToString());
                                ClarReq.RequestID = RequestID.LookupValue;
                                ClarReq.RequestSender = (item["RequestSender"] != null) ? item["RequestSender"].ToString() : string.Empty;
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                            //handle exception as the business require.
                        }
                        finally
                        {
                            //dispose any objects that require dispose.
                            Logging.GetInstance().Debug("Exiting method ClarificationRequests.GetClarificationbyID");
                        }
                    }
                }
            });
            return ClarReq;
        }

        public static List<Entities.ClarificationRequests> GetClarificationRequestbyReqID(string reqID)
        {
            List<Entities.ClarificationRequests> ClarReq = new List<Entities.ClarificationRequests>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method ClarificationRequests.GetClarificationRequestbyReqID");
                            SPList list = web.Lists[Utilities.Constants.SCEClarificationsRequests];
                            SPQuery q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                reqID + "</Value></Eq></Where><OrderBy><FieldRef Name='ClarificationDate' Ascending='False' /></OrderBy>");

                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                    ClarReq.Add(new Entities.ClarificationRequests()
                                    {
                                        RequestID = RequestID.LookupValue,
                                        RequestClarificationDate = (item["ClarificationDate"] != null) ? DateTime.Parse(item["ClarificationDate"].ToString()) : DateTime.MinValue,
                                        ReplyDate = (item["ReplyDate"] != null) ? DateTime.Parse(item["ReplyDate"].ToString()) : DateTime.MinValue,
                                        ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty,
                                        RequestedClarification = (item["RequestedClarification"] != null) ? item["RequestedClarification"].ToString() : string.Empty,
                                        AssignedTo = (item["ClarAssignedTo"] != null) ? item["ClarAssignedTo"].ToString() : string.Empty,
                                        RequestSender = (item["RequestSender"] != null) ? item["RequestSender"].ToString() : string.Empty,
                                        ClarificationReply = (item["ClarificationReply"] != null) ? item["ClarificationReply"].ToString() : string.Empty,
                                    });
                                }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method ClarificationRequests.GetClarificationRequestbyReqID");
                        }
                    }
                }
            });
            return ClarReq;
        }

        public static void AddRequestClar(string ClarificationRequested, string reqID)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method ClarificationRequests.AddRequestClar");
                            SPList list = web.Lists[Utilities.Constants.SCEClarificationsRequests];
                            web.AllowUnsafeUpdates = true;
                            SPListItem listItem = list.Items.Add();
                            listItem["RequestID"] = new SPFieldLookupValue(Convert.ToInt32(reqID), reqID);
                            listItem["RequestedClarification"] = SPHttpUtility.HtmlEncode(ClarificationRequested);
                            listItem["ClarificationDate"] = DateTime.Now;
                            listItem["RequestSender"] = SPContext.Current.Web.CurrentUser.Name;
                            listItem.Update();
                            list.Update();
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                            //handle exception as the business require.
                        }
                        finally
                        {
                            web.AllowUnsafeUpdates = false;
                            //dispose any objects that require dispose.
                            Logging.GetInstance().Debug("Exiting method ClarificationRequests.AddRequestClar");
                        }
                    }
                }
            });
        }

        public static IEnumerable<Entities.ClarificationRequests> GetAllClarificationRequests(/*string strQuery,*/ int LCID, string SPGroupName)
        {
            List<Entities.ClarificationRequests> SCERequests = new List<Entities.ClarificationRequests>();
            try
            {
                using (SPWeb web = new SPSite(SPContext.Current.Site.Url).OpenWeb())
                {
                    string joins = @"<Join Type='INNER' ListAlias='SCERequests'>
                                      <Eq>
                                        <FieldRef Name='RequestID' RefType='Id' />
                                        <FieldRef List='SCERequests' Name='Id' />
                                      </Eq>
                                    </Join>
    <Join Type='LEFT' ListAlias='RequestStatus'>
                                      <Eq>
                                        <FieldRef List='SCERequests' Name='RequestStatus' RefType='Id' />
                                        <FieldRef List='RequestStatus' Name='Id' />
                                      </Eq>
                                    </Join>
<Join Type='LEFT' ListAlias='CountryOfStudy'>
                                      <Eq>
                                        <FieldRef List='SCERequests' Name='CertificateResource' RefType='Id' />
                                        <FieldRef List='CountryOfStudy' Name='Id' />
                                      </Eq>
                                    </Join>" +
 "<Join Type='LEFT' ListAlias='ScholasticLevel'>" +
                                      "<Eq>" +
                                        "<FieldRef  List='SCERequests' Name='LastScholasticLevel' RefType='Id' />" +
                                        "<FieldRef List='ScholasticLevel' Name='Id' />" +
                                      "</Eq></Join>" +
"<Join Type='LEFT' ListAlias='Applicants'>" +
                                      "<Eq>" +
                                        "<FieldRef List='SCERequests' Name='ApplicantID' RefType='Id' />" +
                                        "<FieldRef List='Applicants' Name='Id' />" +
                                      "</Eq>" +
                                    "</Join><Join Type='INNER' ListAlias='Nationality'>" +
                                      "<Eq>" +
                                       "<FieldRef List='SCERequests' Name='StdNationality' RefType='Id' />" +
                                        "<FieldRef List='Nationality' Name='Id' />" +
                                      "</Eq>" +
                                    "</Join>";
                                 //"<Join Type='LEFT' ListAlias='ClarificationReasons'>" +
                                 //     "<Eq>" +
                                 //       "<FieldRef  Name='ClarificationReason' RefType='Id' />" +
                                 //       "<FieldRef List='ClarificationReasons' Name='Id' />" +
                                 //     "</Eq></Join>";

                    string projectedFields =
                    //    "<Field Name='ClarificationReasons_Title' Type='Lookup' "
                    //+ "List='ClarificationReasons' ShowField='Title'/>" +
                    //"<Field Name='ClarificationReasons_TitleAr' Type='Lookup' "
                    //+ "List='ClarificationReasons' ShowField='TitleAr'/>" +
                                          "<Field Name='SCERequests_RequestNumber' Type='Lookup' "
                    + "List='SCERequests' ShowField='RequestNumber'/>"
                   + "<Field Name='SCERequests_RecievedDate' Type='Lookup' "
                    + "List='SCERequests' ShowField='RecievedDate'/>"
                    + "<Field Name='SCERequests_ID' Type='Lookup' "
                    + "List='SCERequests' ShowField='ID'/>"
                    + "<Field Name='SCERequests_OtherCertificateResource' Type='Lookup' "
                    + "List='SCERequests' ShowField='OtherCertificateResource'/>"

                   
                    + "<Field Name='SCERequests_StdPrintedName' Type='Lookup' "
                    + "List='SCERequests' ShowField='StdPrintedName'/>"


                     + "<Field Name='SCERequests_StdQatarID' Type='Lookup' "
                    + "List='SCERequests' ShowField='StdQatarID'/>" +
                     "<Field Name='SCERequests_EmployeeAssignedTo' Type='Lookup' "
                    + "List='SCERequests' ShowField='EmployeeAssignedTo'/>"


                    + "<Field Name='SCERequests_ReturnedBy' Type='Lookup' "
                    + "List='SCERequests' ShowField='ReturnedBy'/>"

                     + "<Field Name='RequestStatus_Code' Type='Lookup' "
                    + "List='RequestStatus' ShowField='Code'/>"

                     +
                      "<Field Name='SCERequests_MobileNumber' Type='Lookup' "
                        + "List='SCERequests' ShowField='MobileNumber'/>"


                    + "<Field Name='CountryOfStudy_Title' Type='Lookup' "
                    + "List='CountryOfStudy' ShowField='Title'/>"
                    + "<Field Name='CountryOfStudy_TitleAr' Type='Lookup' "
                    + "List='CountryOfStudy' ShowField='TitleAr'/>" +

                       "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ApplicantName'/>"
                    + "<Field Name='Applicants_ArabicName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ArabicName'/>" +
                                                "<Field Name='Applicants_EnglishName' Type='Lookup' " +
                                                "List='Applicants' ShowField='EnglishName'/>"
                    + "<Field Name='Applicants_QatarID' Type='Lookup' "
                    + "List='Applicants' ShowField='PersonalID'/>"
                     + "<Field Name='Applicants_QID' Type='Lookup' "
                     + "List='Applicants' ShowField='ID'/>" +
                      "<Field Name='Applicants_MobileNumber' Type='Lookup' "
                        + "List='Applicants' ShowField='MobileNumber'/>"

                    + "<Field Name='Nationality_Title' Type='Lookup' "
                    + "List='Nationality' ShowField='Title'/>" +
                     "<Field Name='Nationality_TitleAr' Type='Lookup' " +
                                            "List='Nationality' ShowField='TitleAr'/>"

                                              + "<Field Name='ScholasticLevel_Title' Type='Lookup' "
                    + "List='ScholasticLevel' ShowField='Title'/>" +
                     "<Field Name='ScholasticLevel_TitleAr' Type='Lookup' " +
                                            "List='ScholasticLevel' ShowField='TitleAr'/>";
                                            
                    


                    string viewFields = string.Empty;
                    if (LCID == (int)Language.English)
                    {

                        viewFields = "<FieldRef Name='SCERequests_RequestNumber'/>"
                    + "<FieldRef Name='CountryOfStudy_Title'/>"
                    + "<FieldRef Name='Applicants_ApplicantName'/>"
                     + "<FieldRef Name='Applicants_EnglishName'/>"
                    + "<FieldRef Name='Applicants_QatarID/>"
                    + "<FieldRef Name='Applicants_MobileNumber'/>"
                    + "<FieldRef Name='Nationality_Title'/>"
                    
                    + "<FieldRef Name='ClarificationDate'/>"
                   
                    + "<FieldRef Name='Sender'/>"+
                     "<FieldRef Name='ClarificationReason'/>"
                     + "<FieldRef Name='OtherClarificationReason'/>"

                    + "<FieldRef Name='SCERequests_ID'/>"
                        + "<FieldRef Name='SCERequests_StdPrintedName'/>"
                         + "<FieldRef Name='SCERequests_StdQatarID'/>"

                        
                            + "<FieldRef Name='SCERequests_RecievedDate'/>"

                             + "<FieldRef Name= 'SCERequests_OtherCertificateResource' />"
                             + "<FieldRef Name= 'SCERequests_EmployeeAssignedTo' />"
                              + "<FieldRef Name= 'SCERequests_MobileNumber' />"

                             + "<FieldRef Name= 'SCERequests_ReturnedBy' />"
                             + "<FieldRef Name= 'RequestStatus_Code' />"
                               + "<FieldRef Name= 'ScholasticLevel_Title' />";
                            

                        
                    }
                    else
                    {

                        viewFields = "<FieldRef Name='SCERequests_RequestNumber'/>"

                   + "<FieldRef Name='CountryOfStudy_TitleAr'/>"

                    + "<FieldRef Name='Applicants_ApplicantName'/>"
                     + "<FieldRef Name='Applicants_ArabicName'/>"
                    + "<FieldRef Name='Applicants_QatarID'/>"
                    + "<FieldRef Name='Applicants_MobileNumber'/>"
                    + "<FieldRef Name='Nationality_TitleAr'/>"
                  
                    + "<FieldRef Name='ClarificationDate'/>" 
                     +"<FieldRef Name='Sender'/>" +
                      "<FieldRef Name='OtherClarificationReason'/>"
                   + "<FieldRef Name='ClarificationReasonAr'/>"
                    + "<FieldRef Name='SCERequests_ID'/>"
                     + "<FieldRef Name='SCERequests_StdPrintedName'/>"
                     + "<FieldRef Name='SCERequests_StdQatarID'/>"
                          + "<FieldRef Name='SCERequests_PrevSchool'/>"
                           + "<FieldRef Name='SCERequests_RecievedDate'/>"

                             + "<FieldRef Name= 'SCERequests_OtherCertificateResource' />"
                             + "<FieldRef Name= 'SCERequests_EmployeeAssignedTo' />"
                             + "<FieldRef Name= 'SCERequests_ReturnedBy' />"
                              + "<FieldRef Name= 'SCERequests_MobileNumber' />"
                              + "<FieldRef Name= 'RequestStatus_Code' />"
                               + "<FieldRef Name= 'ScholasticLevel_TitleAr' />";

                    }
                   



                    SPUser user =web.EnsureUser(SPContext.Current.Web.CurrentUser.LoginName);//This will add user to site if not already added.


                    //string query = @"<Where><And><IsNull><FieldRef Name='ClarificationReply' /></IsNull>
                    //   <And><Eq><FieldRef Name='Sender' LookupId='True'  /><Value Type='User'>" + user.ID + "</Value></Eq>"+
                    //"<And><Eq><FieldRef Name='RequestStatus_Code' /><Value Type='Text'>"+Common.Utilities.RequestStatus.SCEEmployeeNeedsClarification+
                    //"</Value></Eq><Or><Eq><FieldRef Name='SCERequests_EmployeeAssignedTo' /><Value Type='Text'>"+ SPContext.Current.Web.CurrentUser.LoginName +
                    //"</Value></Eq><Eq><FieldRef Name='SCERequests_EmployeeAssignedTo' /><Value Type='Text'>"+ SPGroupName.Trim() + "</Value></Eq></Or></And></And></And></Where>" +

                    //"<OrderBy><FieldRef Name='ClarificationDate' Ascending='False' /></OrderBy>";

                    string query = @"<Where><And><And><And><IsNull><FieldRef Name='ClarificationReply' /></IsNull>
            <Eq><FieldRef Name='Sender' LookupId='True' /><Value Type='User'>"+ user.ID+
            "</Value></Eq></And><Eq><FieldRef Name='RequestStatus_Code' /><Value Type='Text'>"+Common.Utilities.RequestStatus.SCEEmployeeNeedsClarification +
                        "</Value></Eq></And><Or><Eq><FieldRef Name='SCERequests_ReturnedBy' /><Value Type='Text'>" + SPContext.Current.Web.CurrentUser.LoginName +
                        "</Value></Eq><Eq><FieldRef Name='SCERequests_ReturnedBy' /><Value Type='Text'>" + SPGroupName.Trim()+"</Value></Eq>" +
                        "</Or></And></Where><OrderBy><FieldRef Name='SCERequests_RecievedDate' Ascending='False' /></OrderBy>";

                    SPList list = web.Lists[Utilities.Constants.SCEClarificationsRequests];

                    SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject(query, joins, projectedFields, viewFields);
                    SPListItemCollection items = list.GetItems(spQuery);
                    if (items != null && items.Count > 0)
                    {
                        foreach (SPListItem item in items)
                        {
                           
                            Entities.ClarificationRequests req = new Entities.ClarificationRequests();
                            req.ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;
                          
                            req.RequestClarificationDate = (item["ClarificationDate"] != null) ? DateTime.Parse(item["ClarificationDate"].ToString()) : DateTime.MinValue;
                           
                            SPFieldLookupValue ApplicantsMobileNumber = new SPFieldLookupValue((item["SCERequests_MobileNumber"] != null) ? item["SCERequests_MobileNumber"].ToString() : string.Empty);
                            req.ApplicantMobileNumber = ApplicantsMobileNumber.LookupValue;


                            SPFieldLookupValue RecievedDate = new SPFieldLookupValue((item["SCERequests_RecievedDate"] != null) ? item["SCERequests_RecievedDate"].ToString() : string.Empty);
                            req.RecievedDate = DateTime.Parse(RecievedDate.LookupValue);

                                SPFieldLookupValue StudentAccToCert = new SPFieldLookupValue((item["SCERequests_StdPrintedName"] != null) ? item["SCERequests_StdPrintedName"].ToString() : string.Empty);
                                req.StudentNameAccToCert = StudentAccToCert.LookupValue;

                            
                               
                            SPFieldLookupValue CertHolderID = new SPFieldLookupValue((item["SCERequests_StdQatarID"] != null) ? item["SCERequests_StdQatarID"].ToString() : string.Empty);
                            req.CertificateHolderQatarID = CertHolderID.LookupValue;

                            
                            if (item["SCERequests_ID"] != null)
                            {
                                SPFieldLookupValue RequestID = new SPFieldLookupValue(item["SCERequests_ID"].ToString());
                                req.RequestID = RequestID.LookupValue;
                            }

                            if (item["Applicants_QatarID"] != null)
                            {
                                SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue(item["Applicants_QatarID"].ToString());
                                req.QatariID = ApplicantsQatarID.LookupValue;
                            }

                            if (item["SCERequests_RequestNumber"] != null)
                            {
                                SPFieldLookupValue RequestNumber = new SPFieldLookupValue(item["SCERequests_RequestNumber"].ToString());
                                req.RequestNumber = RequestNumber.LookupValue;
                            }



                            if (LCID == (int)Language.English)
                            {
                                
                                SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                                req.ApplicantName = ApplicantsEnglishName.LookupValue;
                                if (item["Nationality_Title"] != null)
                                {
                                    SPFieldLookupValue NationalityTitle = new SPFieldLookupValue(item["Nationality_Title"].ToString());
                                    req.Nationality = NationalityTitle.LookupValue;
                                }


                                if (item["CountryOfStudy_Title"] != null)
                                {
                                    SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue(item["CountryOfStudy_Title"].ToString());
                                    req.Country = CountryOfStudy.LookupValue;
                                }
                                else if (item["SCERequests_OtherCertificateResource"] != null)
                                {
                                    SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue(item["SCERequests_OtherCertificateResource"].ToString());
                                    req.Country = CountryOfStudy.LookupValue;
                                }
                                if (item["ClarificationReason"] != null)
                                {
                                    SPFieldLookupValue ClarificationReason = new SPFieldLookupValue(item["ClarificationReason"].ToString());
                                    req.ClarificationReason = ClarificationReason.LookupValue;
                                }
                                else if (item["OtherClarificationReason"]!=null)
                                {
                                    req.ClarificationReason = Convert.ToString(item["OtherClarificationReason"]);
                                }
                                if (item["ScholasticLevel_Title"] != null)
                                {
                                    SPFieldLookupValue SchoolLastGrade = new SPFieldLookupValue(item["ScholasticLevel_Title"].ToString());
                                    req.SchoolLastGrade = SchoolLastGrade.LookupValue;
                                }


                            }
                            else
                            {
                                

                                SPFieldLookupValue ApplicantsArabicName = new SPFieldLookupValue((item["Applicants_ArabicName"] != null) ? item["Applicants_ArabicName"].ToString() : string.Empty);
                                req.ApplicantName = ApplicantsArabicName.LookupValue;
                                if (item["Nationality_TitleAr"] != null)
                                {
                                    SPFieldLookupValue NationalityTitle = new SPFieldLookupValue(item["Nationality_TitleAr"].ToString());
                                    req.Nationality = NationalityTitle.LookupValue;
                                }

                                if (item["CountryOfStudy_TitleAr"] != null)
                                {
                                    SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue(item["CountryOfStudy_TitleAr"].ToString());
                                    req.Country = CountryOfStudy.LookupValue;
                                }
                                else if (item["SCERequests_OtherCertificateResource"] != null)
                                {
                                    SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue(item["SCERequests_OtherCertificateResource"].ToString());
                                    req.Country = CountryOfStudy.LookupValue;
                                }
                                if (item["ClarificationReasonAr"] != null)
                                {
                                    SPFieldLookupValue ClarificationReason = new SPFieldLookupValue(item["ClarificationReasonAr"].ToString());
                                    req.ClarificationReason = ClarificationReason.LookupValue;
                                }
                                else if (item["OtherClarificationReason"] != null)
                                {
                                   
                                    req.ClarificationReason = Convert.ToString(item["OtherClarificationReason"]); 
                                }
                                if (item["ScholasticLevel_TitleAr"] != null)
                                {
                                    SPFieldLookupValue SchoolLastGrade = new SPFieldLookupValue(item["ScholasticLevel_TitleAr"].ToString());
                                    req.SchoolLastGrade = SchoolLastGrade.LookupValue;
                                }
                            }

                            



                            SCERequests.Add(req);
                        }
                    }
                }

                //To remove the requests that are created by employees 

            
                List<int> deletedId = new List<int>();
                if (SCERequests.Count != 0)
                {
                    using (SCEContextDataContext ctx = new SCEContextDataContext(SPContext.Current.Site.RootWeb.Url))
                    {
                        SCERequestsListFieldsContentType req = null;
                        foreach (var request in SCERequests)

                        {
                            req = ctx.SCERequestsList.ScopeToFolder("", true).Where(r => r.Id == int.Parse(request.RequestID)).FirstOrDefault();
                            if (req != null && req.IsEmployee == IsEmployee.Yes)
                            {

                                deletedId.Add(int.Parse(request.RequestID));


                            }
                        }


                        if (deletedId.Count > 0)
                        {
                            foreach (int id in deletedId)
                            {
                                SCERequests.Remove(SCERequests.Where(r => int.Parse(r.RequestID) == id).First());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method ClarificationRequests.GetAllClarificationRequests");
            }
            return SCERequests;
        }
        public static string GetClarificationQuery (string SPGroupName)
        {
            Logging.GetInstance().Debug("Enter ClarificationRequests.GetClarificationQuery");
            string clarQuery = string.Empty;
            try
            {
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit ClarificationRequests.GetClarificationQuery");
            }
            return clarQuery;
          
        }
        public static List<string> GetClarQueryPerRole(string SPGroupName)
        {
            List<string> objColumns = new List<string>();
            string strQuery = string.Empty;

            //Equivalence Employees Query
            if (Common.Utilities.Constants.SCEEquivalenceEmployeesGroupName == SPGroupName )
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.SCEEmployeeNeedsClarification);
            }

          

            return objColumns;
        }

        public static void AddClarRequestReply(int ID, string Reply)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method ClarificationRequests.AddClarRequestReply");
                            SPList list = web.Lists[Utilities.Constants.SCEClarificationsRequests];
                            SPListItem item = list.GetItemById(ID);
                            item["ClarificationReply"] = SPHttpUtility.HtmlEncode(Reply);
                            item["ReplyDate"] = DateTime.Now;
                            web.AllowUnsafeUpdates = true;
                            item.Update();
                            list.Update();
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            web.AllowUnsafeUpdates = false;
                            Logging.GetInstance().Debug("Exiting method ClarificationRequests.AddClarRequestReply");
                        }
                    }
                }
            });
        }

        public static Entities.ClarificationRequests GetClarificationReplybyReqID(string reqID)
        {

            Logging.GetInstance().Debug("Entering method ClarificationRequests.GetClarificationReplybyReqID");
            Entities.ClarificationRequests clarReply = null;
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {

                            SPList list = web.Lists[Utilities.Constants.SCERequests];
                            SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                reqID + "</Value></Eq><IsNull><FieldRef Name='ClarificationReply' /></IsNull></And></Where>");

                            SPListItemCollection replyItems = list.GetItems(spQuery);
                            if (replyItems != null && replyItems.Count != 0)
                            {
                                SPListItem replyItem = replyItems[0];
                                clarReply = new Entities.ClarificationRequests();
                                clarReply.ClarificationReply = Convert.ToString(replyItem["ClarificationReply"]);
                                clarReply.ID = replyItem.ID.ToString();

                            }

                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);

            }
            finally
            {


                Logging.GetInstance().Debug("Exiting method ClarificationRequests.GetClarificationReplybyReqID");


            }
            return clarReply;
        }
    }
}
