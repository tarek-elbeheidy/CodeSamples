using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Entities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.UCE.BL
{
    public class ClarificationRequests
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
                            Logging.GetInstance().Debug("Entering method ClarificationRequests.GetClarRequest_ForReply");
                            SPList list = web.Lists[Utilities.Constants.ClarificationRequests];
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
                            Logging.GetInstance().Debug("Exiting method ClarificationRequests.GetClarRequestsData");
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
                            Logging.GetInstance().Debug("Entering method ClarificationRequests.GetClarificationbyID");
                            SPList list = web.Lists[Utilities.Constants.ClarificationRequests];
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
                            Logging.GetInstance().Debug("Entering method ClarificationRequests.GetClarificationRequestbyReqID");
                            SPList list = web.Lists[Utilities.Constants.ClarificationRequests];
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
                            //handle exception as the business require.
                        }
                        finally
                        {
                            //dispose any objects that require dispose.
                            Logging.GetInstance().Debug("Exiting method ClarificationRequests.GetClarificationRequestbyReqID");
                        }
                    }
                }
            });
            return ClarReq;
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
                        SPList list = web.Lists[Utilities.Constants.ClarificationRequests];

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
                            SPList list = web.Lists[Utilities.Constants.ClarificationRequests];
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

        public static IEnumerable<ClarificationReqs> GetAllClarificationRequests(string strQuery, int LCID)
        {
            List<Entities.ClarificationReqs> Requests = new List<Entities.ClarificationReqs>();
            try
            {
                using (SPWeb web = new SPSite(SPContext.Current.Site.Url).OpenWeb())
                {
                    string joins = @"<Join Type='INNER' ListAlias='Requests'>
                                      <Eq>
                                        <FieldRef Name='RequestID' RefType='Id' />
                                        <FieldRef List='Requests' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='LEFT' ListAlias='AcademicDegree'>
                                      <Eq>
                                        <FieldRef List='Requests' Name='AcademicDegreeForEquivalence' RefType='Id' />
                                        <FieldRef List='AcademicDegree' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='LEFT' ListAlias='CountryOfStudy'>
                                      <Eq>
                                        <FieldRef List='Requests' Name='CountryOfStudy' RefType='Id' />
                                        <FieldRef List='CountryOfStudy' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='LEFT' ListAlias='University'>
                                      <Eq>
                                        <FieldRef List='Requests' Name='University' RefType='Id' />
                                        <FieldRef List='University' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='LEFT' ListAlias='EntityNeedsEquivalency'>
                                      <Eq>
                                        <FieldRef List='Requests' Name='EntityNeedsEquivalency' RefType='Id' />
                                        <FieldRef List='EntityNeedsEquivalency' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='INNER' ListAlias='Applicants'>
                                      <Eq>
                                        <FieldRef List='Requests' Name='ApplicantID' RefType='Id' />
                                        <FieldRef List='Applicants' Name='Id' />
                                      </Eq>
                                    </Join><Join Type='INNER' ListAlias='Nationality'>
                                      <Eq>
                                        <FieldRef List='Applicants' Name='Nationality' RefType='Id' />
                                        <FieldRef List='Nationality' Name='Id' />
                                      </Eq>
                                    </Join>";

                    string projectedFields =
                                          "<Field Name='Requests_RequestNumber' Type='Lookup' "
                    + "List='Requests' ShowField='RequestNumber'/>"
                    + "<Field Name='Requests_UniversityNotFoundInList' Type='Lookup' "
                    + "List='Requests' ShowField='UniversityNotFoundInList'/>"
                     + "<Field Name='Requests_RequestID' Type='Lookup' "
                    + "List='Requests' ShowField='ID'/>"
                    + "<Field Name='AcademicDegree_Title' Type='Lookup' "
                    + "List='AcademicDegree' ShowField='Title'/>"
                    + "<Field Name='CountryOfStudy_Title' Type='Lookup' "
                    + "List='CountryOfStudy' ShowField='Title'/>"
                    + "<Field Name='University_Title' Type='Lookup' "
                    + "List='University' ShowField='Title'/>"
                       /* + "<Field Name='Faculty_Title' Type='Lookup' "
                        + "List='Faculty' ShowField='Title'/>"*/
                       + "<Field Name='Requests_Faculty' Type='Lookup' "
                    + "List='Requests' ShowField='Faculty'/>"
                    + "<Field Name='Requests_FacultyAr' Type='Lookup' "
                    + "List='Requests' ShowField='FacultyAr'/>"
                    + "<Field Name='EntityNeedsEquivalency_Title' Type='Lookup'"
                    + "List='EntityNeedsEquivalency' ShowField='Title'/>" +

                    "<Field Name='Applicants_ApplicantName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ApplicantName'/>"

                    + "<Field Name='Applicants_ArabicName' Type='Lookup' " +
                                                "List='Applicants' ShowField='ArabicName'/>" +
                                                "<Field Name='Applicants_EnglishName' Type='Lookup' " +
                                                "List='Applicants' ShowField='EnglishName'/>"
                    + "<Field Name='Applicants_QatarID' Type='Lookup' "
                    + "List='Applicants' ShowField='PersonalID'/>"
                    + "<Field Name='Nationality_Title' Type='Lookup' "
                    + "List='Nationality' ShowField='Title'/>";

                    string viewFields = string.Empty;
                    if (LCID == (int)Language.English)
                    {
                        viewFields = "<FieldRef Name='Requests_RequestNumber'/>"
                             + "<FieldRef Name='Requests_UniversityNotFoundInList'/>"
                    + "<FieldRef Name='AcademicDegree_Title'/>"
                    + "<FieldRef Name='CountryOfStudy_Title'/>"
                    + "<FieldRef Name='University_Title'/>"
                      /*+ "<FieldRef Name='Faculty_Title'/>"*/
                      + "<FieldRef Name='Requests_Faculty'/>"
                    + "<FieldRef Name='Requests_FacultyAr'/>"
                    + "<FieldRef Name='EntityNeedsEquivalency_Title'/>"
                    + "<FieldRef Name='Applicants_ApplicantName'/>"
                     + "<FieldRef Name='Applicants_EnglishName'/>"
                    + "<FieldRef Name='Applicants_QatarID'/>"
                    + "<FieldRef Name='Nationality_Title'/>"
                    + "<FieldRef Name='RequestedClarification'/>"
                    + "<FieldRef Name='ClarificationDate'/>"
                    + "<FieldRef Name='ClarAssignedTo'/>"
                    + "<FieldRef Name='RequestSender'/>"
                    + "<FieldRef Name='Requests_RequestID'/>";
                    }
                    else
                    {
                        viewFields = "<FieldRef Name='Requests_RequestNumber'/>"
                            + "<FieldRef Name='Requests_UniversityNotFoundInList'/>"
               + "<FieldRef Name='AcademicDegree_Title'/>"
               + "<FieldRef Name='CountryOfStudy_Title'/>"
               + "<FieldRef Name='University_Title'/>"
               /* + "<FieldRef Name='Faculty_Title'/>"*/
               + "<FieldRef Name='Requests_Faculty'/>"
                    + "<FieldRef Name='Requests_FacultyAr'/>"
               + "<FieldRef Name='EntityNeedsEquivalency_Title'/>"
               + "<FieldRef Name='Applicants_ApplicantName'/>"
                + "<FieldRef Name='Applicants_ArabicName'/>"
               + "<FieldRef Name='Applicants_QatarID'/>"
               + "<FieldRef Name='Nationality_Title'/>"
               + "<FieldRef Name='RequestedClarification'/>"
               + "<FieldRef Name='ClarificationDate'/>"
               + "<FieldRef Name='ClarAssignedTo'/>"
               + "<FieldRef Name='RequestSender'/>"
               + "<FieldRef Name='Requests_RequestID'/>";
                    }

                    string query = @"<Where><IsNull><FieldRef Name='ClarificationReply' /></IsNull></Where>";

                    SPList list = web.Lists[Utilities.Constants.ClarificationRequests];

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

                            SPFieldLookupValue RequestID = new SPFieldLookupValue((item["Requests_RequestID"] != null) ? item["Requests_RequestID"].ToString() : string.Empty);
                            req.RequestID = RequestID.LookupValue;

                            SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue(item["Applicants_QatarID"].ToString());
                            req.QatariID = ApplicantsQatarID.LookupValue;

                            if (LCID == (int)Language.English)
                            {
                                SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                                req.ApplicantName = ApplicantsEnglishName.LookupValue;
                                if (item["Requests_Faculty"] != null)
                                {
                                    SPFieldLookupValue faculty = new SPFieldLookupValue(item["Requests_Faculty"].ToString());
                                    req.Faculty = faculty.LookupValue;
                                }
                            }
                            else
                            {
                                SPFieldLookupValue ApplicantsArabicName = new SPFieldLookupValue((item["Applicants_ArabicName"] != null) ? item["Applicants_ArabicName"].ToString() : string.Empty);
                                req.ApplicantName = ApplicantsArabicName.LookupValue;

                                if (item["Requests_FacultyAr"] != null)
                                {
                                    SPFieldLookupValue faculty = new SPFieldLookupValue(item["Requests_FacultyAr"].ToString());
                                    req.Faculty = faculty.LookupValue;
                                }
                            }

                            SPFieldLookupValue NationalityTitle = new SPFieldLookupValue(item["Nationality_Title"].ToString());
                            req.Nationality = NationalityTitle.LookupValue;

                            SPFieldLookupValue RequestNumber = new SPFieldLookupValue(item["Requests_RequestNumber"].ToString());
                            req.RequestNumber = RequestNumber.LookupValue;

                            SPFieldLookupValue EntityNeedsEquivalency = new SPFieldLookupValue((item["EntityNeedsEquivalency_Title"] != null) ? item["EntityNeedsEquivalency_Title"].ToString() : string.Empty);
                            req.EntityNeedsEquivalency = EntityNeedsEquivalency.LookupValue;

                            SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue((item["CountryOfStudy_Title"] != null) ? item["CountryOfStudy_Title"].ToString() : string.Empty);
                            req.Country = CountryOfStudy.LookupValue;

                            SPFieldLookupValue AcademicDegree = new SPFieldLookupValue((item["AcademicDegree_Title"] != null) ? item["AcademicDegree_Title"].ToString() : string.Empty);
                            req.AcademicDegree = AcademicDegree.LookupValue;

                            SPFieldLookupValue University = new SPFieldLookupValue((item["University_Title"] != null) ? item["University_Title"].ToString() : string.Empty);
                            SPFieldLookupValue otherUniversity = new SPFieldLookupValue((item["Requests_UniversityNotFoundInList"] != null) ? item["Requests_UniversityNotFoundInList"].ToString() : string.Empty);
                            if (University.LookupValue != null)
                                req.University = University.LookupValue;
                            else
                                req.University = otherUniversity.LookupValue;


                            //SPFieldLookupValue Faculty = new SPFieldLookupValue(item["Requests_Faculty"].ToString());
                            //req.Faculty = Faculty.LookupValue;

                            Requests.Add(req);
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
                Logging.GetInstance().Debug("Exiting method SearchSimilarRequests.GetAllClarificationRequests");
            }
            return Requests;
        }
        public static IEnumerable<ClarificationReqs> GetAllClarificationRequests(string strQuery)
        {
            List<Entities.ClarificationReqs> Requests = new List<Entities.ClarificationReqs>();
            try
            {
                using (SPWeb web = new SPSite(SPContext.Current.Site.Url).OpenWeb())
                {



                    string query = @"<Where><IsNull><FieldRef Name='ClarificationReply' /></IsNull></Where>";

                    SPList list = web.Lists[Utilities.Constants.ClarificationRequests];

                    SPQuery spQuery = new SPQuery { Query = query + strQuery };

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

                            SPFieldLookupValue RequestID = new SPFieldLookupValue((item["Requests_RequestID"] != null) ? item["Requests_RequestID"].ToString() : string.Empty);
                            req.RequestID = RequestID.LookupValue;



                            //SPFieldLookupValue Faculty = new SPFieldLookupValue(item["Requests_Faculty"].ToString());
                            //req.Faculty = Faculty.LookupValue;

                            Requests.Add(req);
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
                Logging.GetInstance().Debug("Exiting method SearchSimilarRequests.GetAllClarificationRequests");
            }
            return Requests;
        }
        public static List<string> GetClarQueryPerRole(string SPGroupName)
        {
            List<string> objColumns = new List<string>();
            string strQuery = string.Empty;

            //Program Employees Query
            if (Common.Utilities.Constants.ArabicProgEmployeeGroupName == SPGroupName || Common.Utilities.Constants.EuropeanProgEmployeeGroupName == SPGroupName)
            {
                objColumns.Add("RequestStatusId;Lookup;Eq;" + (int)Common.Utilities.RequestStatus.UCEProgramEmployeeNeedsClarification);
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
                            SPList list = web.Lists[Utilities.Constants.ClarificationRequests];
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
    }
}