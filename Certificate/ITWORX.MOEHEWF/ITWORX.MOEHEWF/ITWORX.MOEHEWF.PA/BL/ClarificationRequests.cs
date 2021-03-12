using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Entities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class PAClarificationRequests
    {
        public static ClarificationReqs GetClarRequest_ForReply(string ID)
        {
            ClarificationReqs ClarReq = new ClarificationReqs();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method PAClarificationRequests.GetClarRequest_ForReply");
                            SPList list = web.Lists[Utilities.Constants.PAClarificationRequests];
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
                            Logging.GetInstance().Debug("Exiting method PAClarificationRequests.GetClarRequestsData");
                        }
                    }
                }
            });
            return ClarReq;
        }

        public static ClarificationReqs GetClarificationbyID(string ID)
        {
            ClarificationReqs ClarReq = new ClarificationReqs();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method PAClarificationRequests.GetClarificationbyID");
                            SPList list = web.Lists[Utilities.Constants.PAClarificationRequests];
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
                            Logging.GetInstance().Debug("Exiting method PAClarificationRequests.GetClarificationbyID");
                        }
                    }
                }
            });
            return ClarReq;
        }

        public static List<ClarificationReqs> GetClarificationRequestbyReqID(string reqID)
        {
            List<ClarificationReqs> ClarReq = new List<ClarificationReqs>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method PAClarificationRequests.GetClarificationRequestbyReqID");
                            SPList list = web.Lists[Utilities.Constants.PAClarificationRequests];
                            SPQuery q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                reqID + "</Value></Eq></Where><OrderBy><FieldRef Name='ClarificationDate' Ascending='False' /></OrderBy>");

                            SPListItemCollection collListItems = list.GetItems(q);
                            if (collListItems.Count != 0)
                                foreach (SPListItem item in collListItems)
                                {
                                    SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                    ClarReq.Add(new ClarificationReqs()
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
                            Logging.GetInstance().Debug("Exiting method PAClarificationRequests.GetClarificationRequestbyReqID");
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
                            Logging.GetInstance().Debug("Entering method PAClarificationRequests.AddRequestClar");
                            SPList list = web.Lists[Utilities.Constants.PAClarificationRequests];
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
                            Logging.GetInstance().Debug("Exiting method PAClarificationRequests.AddRequestClar");
                        }
                    }
                }
            });
        }

        public static IEnumerable<ClarificationReqs> GetAllPAClarificationRequests(string strQuery, int LCID)
        {
            List<Entities.ClarificationReqs> PARequests = new List<Entities.ClarificationReqs>();
            try
            {
                using (SPWeb web = new SPSite(SPContext.Current.Site.Url).OpenWeb())
                {
                    string joins = @"<Join Type='INNER' ListAlias='PARequests'>
                                      <Eq>
                                        <FieldRef Name='RequestID' RefType='Id' />
                                        <FieldRef List='PARequests' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='LEFT' ListAlias='Certificates'>
                                      <Eq>
                                        <FieldRef List='PARequests' Name='AcademicDegreeForEquivalence' RefType='Id' />
                                        <FieldRef List='Certificates' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='LEFT' ListAlias='CountryOfStudy'>
                                      <Eq>
                                        <FieldRef List='PARequests' Name='CountryOfStudy' RefType='Id' />
                                        <FieldRef List='CountryOfStudy' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='LEFT' ListAlias='University'>
                                      <Eq>
                                        <FieldRef List='PARequests' Name='University' RefType='Id' />
                                        <FieldRef List='University' Name='Id' />
                                      </Eq>
                                   </Join><Join Type='INNER' ListAlias='Applicants'>
                                      <Eq>
                                        <FieldRef List='PARequests' Name='ApplicantID' RefType='Id' />
                                        <FieldRef List='Applicants' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='INNER' ListAlias='Nationality'>
                                      <Eq>
                                        <FieldRef List='Applicants' Name='Nationality' RefType='Id' />
                                        <FieldRef List='Nationality' Name='Id' />
                                      </Eq>
                                    </Join>";

                    string projectedFields =
                                          "<Field Name='PARequests_RequestNumber' Type='Lookup' "
                    + "List='PARequests' ShowField='RequestNumber'/>" 
                    + "<Field Name='PARequests_RequestID' Type='Lookup' "
                    + "List='PARequests' ShowField='ID'/>"
                    
                    + "<Field Name='PARequests_Faculty' Type='Lookup' "
                    + "List='PARequests' ShowField='Faculty'/>"
                    + "<Field Name='PARequests_FacultyAr' Type='Lookup' "
                    + "List='PARequests' ShowField='FacultyAr'/>"
                    + "<Field Name='Certificates_Title' Type='Lookup' "
                    + "List='Certificates' ShowField='Title'/>"
                       + "<Field Name='Certificates_TitleAr' Type='Lookup' "
                    + "List='Certificates' ShowField='TitleAr'/>"
                    + "<Field Name='CountryOfStudy_Title' Type='Lookup' "
                    + "List='CountryOfStudy' ShowField='Title'/>"
                    +"<Field Name='CountryOfStudy_TitleAr' Type='Lookup' "
                    + "List='CountryOfStudy' ShowField='TitleAr'/>"

                    + "<Field Name='University_Title' Type='Lookup' "
                    + "List='University' ShowField='Title'/>"
                    +"<Field Name='University_TitleAr' Type='Lookup' "
                    + "List='University' ShowField='TitleAr'/>" +
                       "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ApplicantName'/>" 
                    + "<Field Name='Applicants_ArabicName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ArabicName'/>" +
                                                "<Field Name='Applicants_EnglishName' Type='Lookup' " +
                                                "List='Applicants' ShowField='EnglishName'/>" 
                    + "<Field Name='Applicants_QatarID' Type='Lookup' "
                    + "List='Applicants' ShowField='PersonalID'/>"
                     +"<Field Name='Applicants_QID' Type='Lookup' " 
                     +"List='Applicants' ShowField='ID'/>" 
                    + "<Field Name='Nationality_Title' Type='Lookup' "
                    + "List='Nationality' ShowField='Title'/>"+
                     "<Field Name='Nationality_TitleAr' Type='Lookup' " +
                                            "List='Nationality' ShowField='TitleAr'/>" ;


                    string viewFields = string.Empty;
                    if (LCID == (int)Language.English)
                    {

                        viewFields = "<FieldRef Name='PARequests_RequestNumber'/>"
                    + "<FieldRef Name='Certificates_Title'/>"
                    + "<FieldRef Name='CountryOfStudy_Title'/>"
                   
                    + "<FieldRef Name='PARequests_Faculty'/>"
                    + "<FieldRef Name='PARequests_FacultyAr'/>"
                    + "<FieldRef Name='University_Title'/>"
                    + "<FieldRef Name='Applicants_ApplicantName'/>"
                     + "<FieldRef Name='Applicants_EnglishName'/>"
                    + "<FieldRef Name='Applicants_QatarID'/>"
                    + "<FieldRef Name='Nationality_Title'/>"
                    + "<FieldRef Name='RequestedClarification'/>"
                    + "<FieldRef Name='ClarificationDate'/>"
                    + "<FieldRef Name='ClarAssignedTo'/>"
                    + "<FieldRef Name='RequestSender'/>"
                    + "<FieldRef Name='ClarificationReply'/>"
                    + "<FieldRef Name='PARequests_RequestID'/>";
                    }
                    else
                    {

                        viewFields = "<FieldRef Name='PARequests_RequestNumber'/>"
                    + "<FieldRef Name='Certificates_TitleAr'/>"
                    + "<FieldRef Name='CountryOfStudy_TitleAr'/>"
                  
                    + "<FieldRef Name='PARequests_Faculty'/>"
                    + "<FieldRef Name='PARequests_FacultyAr'/>"
                    + "<FieldRef Name='University_TitleAr'/>"
                    + "<FieldRef Name='Applicants_ApplicantName'/>"
                     + "<FieldRef Name='Applicants_ArabicName'/>"
                    + "<FieldRef Name='Applicants_QatarID'/>"
                    + "<FieldRef Name='Nationality_TitleAr'/>"
                    + "<FieldRef Name='RequestedClarification'/>"
                    + "<FieldRef Name='ClarificationDate'/>"
                    + "<FieldRef Name='ClarAssignedTo'/>"
                    + "<FieldRef Name='RequestSender'/>"
                    + "<FieldRef Name='ClarificationReply'/>"
                    + "<FieldRef Name='PARequests_RequestID'/>";
                    }

                    string query = @"<Where><IsNull><FieldRef Name='ClarificationReply' /></IsNull></Where>";

                    SPList list = web.Lists[Utilities.Constants.PAClarificationRequests];

                    SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject(query, joins, projectedFields, viewFields);
                    SPListItemCollection items = list.GetItems(spQuery);
                    if (items != null && items.Count > 0)
                    {
                        foreach (SPListItem item in items)
                        {
                            ClarificationReqs req = new ClarificationReqs();
                            req.ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;
                            req.RequestedClarification = (item["RequestedClarification"] != null) ? item["RequestedClarification"].ToString() : string.Empty;
                            req.RequestClarificationDate = (item["ClarificationDate"] != null) ? DateTime.Parse(item["ClarificationDate"].ToString()) : DateTime.MinValue;
                            req.AssignedTo = (item["ClarAssignedTo"] != null) ? item["ClarAssignedTo"].ToString() : string.Empty;
                            req.RequestSender = (item["RequestSender"] != null) ? item["RequestSender"].ToString() : string.Empty;

                            if (LCID == (int)Language.English)
                            {
                                if (item["PARequests_Faculty"] != null)
                                {
                                    SPFieldLookupValue RequestID = new SPFieldLookupValue(item["PARequests_Faculty"].ToString());
                                    req.Faculty = RequestID.LookupValue;
                                }

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

                                if (item["Certificates_Title"] != null)
                                {
                                    SPFieldLookupValue AcademicDegree = new SPFieldLookupValue(item["Certificates_Title"].ToString());
                                    req.AcademicDegree = AcademicDegree.LookupValue;
                                }

                                if (item["University_Title"] != null)
                                {
                                    SPFieldLookupValue University = new SPFieldLookupValue(item["University_Title"].ToString());
                                    req.University = University.LookupValue;
                                }
                                //SPFieldLookupValue ProgramUniversity = new SPFieldLookupValue((item["ProgramUniversity_Title"] != null) ? item["ProgramUniversity_Title"].ToString() : string.Empty);
                                //if (ProgramUniversity.LookupValue != null)
                                //    req.ProgramUniversity = ProgramUniversity.LookupValue;
                                //else
                                //    req.ProgramUniversity = (item["PARequests_OtherPAUniversityOfStudy"] != null) ? item["PARequests_OtherPAUniversityOfStudy"].ToString() : string.Empty;

                                //if (item["ProgramCountry_Title"] != null)
                                //{
                                //    SPFieldLookupValue _CountryOfStudy = new SPFieldLookupValue(item["ProgramCountry_Title"].ToString());
                                //    req.ProgramCountry = _CountryOfStudy.LookupValue;
                                //}
                            }
                            else
                            {
                                if (item["PARequests_FacultyAr"] != null)
                                {
                                    SPFieldLookupValue RequestID = new SPFieldLookupValue(item["PARequests_FacultyAr"].ToString());
                                    req.Faculty = RequestID.LookupValue;
                                }
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

                                if (item["Certificates_TitleAr"] != null)
                                {
                                    SPFieldLookupValue AcademicDegree = new SPFieldLookupValue(item["Certificates_TitleAr"].ToString());
                                    req.AcademicDegree = AcademicDegree.LookupValue;
                                }

                                if (item["University_TitleAr"] != null)
                                {
                                    SPFieldLookupValue University = new SPFieldLookupValue(item["University_TitleAr"].ToString());
                                    req.University = University.LookupValue;
                                }
                                //SPFieldLookupValue ProgramUniversity = new SPFieldLookupValue((item["ProgramUniversity_TitleAr"] != null) ? item["ProgramUniversity_TitleAr"].ToString() : string.Empty);
                                //if (ProgramUniversity.LookupValue != null)
                                //    req.ProgramUniversity = ProgramUniversity.LookupValue;
                                //else
                                //    req.ProgramUniversity = (item["PARequests_OtherPAUniversityOfStudy"] != null) ? item["PARequests_OtherPAUniversityOfStudy"].ToString() : string.Empty;

                                //if (item["ProgramCountry_TitleAr"] != null)
                                //{
                                //    SPFieldLookupValue _CountryOfStudy = new SPFieldLookupValue(item["ProgramCountry_TitleAr"].ToString());
                                //    req.ProgramCountry = _CountryOfStudy.LookupValue;
                                //}
                            }

                            if (item["PARequests_RequestID"] != null)
                            {
                                SPFieldLookupValue RequestID = new SPFieldLookupValue(item["PARequests_RequestID"].ToString());
                                req.RequestID = RequestID.LookupValue;
                            }

                            if (item["Applicants_QatarID"] != null)
                            {
                                SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue(item["Applicants_QatarID"].ToString());
                                req.QatariID = ApplicantsQatarID.LookupValue;
                            }

                           

                           

                            if (item["PARequests_RequestNumber"] != null)
                            {
                                SPFieldLookupValue RequestNumber = new SPFieldLookupValue(item["PARequests_RequestNumber"].ToString());
                                req.RequestNumber = RequestNumber.LookupValue;
                            }




                            PARequests.Add(req);
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
                Logging.GetInstance().Debug("Exiting method PASearchSimilarRequests.GetAllPAClarificationRequests");
            }
            return PARequests;
        }

        public static List<string> GetClarQueryPerRole(string SPGroupName)
        {
            List<string> objColumns = new List<string>();
            string strQuery = string.Empty;

            //Program Employees Query
            if (Common.Utilities.Constants.ArabicProgEmployeeGroupName == SPGroupName || Common.Utilities.Constants.EuropeanProgEmployeeGroupName == SPGroupName)
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.PAEmployeeClarificationReplay);
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
                            Logging.GetInstance().Debug("Entering method Add_Clarification.AddClarRequestReply");
                            SPList list = web.Lists[Utilities.Constants.PAClarificationRequests];
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
                            Logging.GetInstance().Debug("Exiting method Add_Clarification.AddClarRequestReply");
                        }
                    }
                }
            });
        }

        public static ClarificationReqs GetClarificationReplybyReqID(string reqID)
        {

            Logging.GetInstance().Debug("Entering method ClarificationRequests.GetClarificationReplybyReqID");
            ClarificationReqs clarReply = null;
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {

                            SPList list = web.Lists[Utilities.Constants.PAClarificationRequests];
                            SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                reqID + "</Value></Eq><IsNull><FieldRef Name='ClarificationReply' /></IsNull></And></Where>");

                            SPListItemCollection replyItems = list.GetItems(spQuery);
                            if (replyItems != null && replyItems.Count != 0)
                            {
                                SPListItem replyItem = replyItems[0];
                                clarReply = new ClarificationReqs();
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