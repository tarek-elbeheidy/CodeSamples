using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Entities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ITWORX.MOEHEWF.PA.Utilities
{
    public static class BusinessHelper
    {

        public static SPListItemCollection PAEmployeeByGroupName(string listName ,string spGroupName)
        {
            Logging.GetInstance().Debug("Entering BusinessHelper.PAEmployeeByGroupName");
            SPListItemCollection itemsCollection = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList spList = web.Lists[listName];

                            //What shall i do in this part
                            if (spList == null)
                                throw new Exception();

                            SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='SPGroupName' /><Value Type='Text'>" + spGroupName +
                                "</Value></Eq></Where>");
                                            
                            itemsCollection = spList.GetItems(spQuery);

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
                Logging.GetInstance().Debug("Exit BusinessHelper.PAEmployeeByGroupName");
            }
            return itemsCollection;
        }
        public static SPListItemCollection GetLookupData(string listName)
        {
            Logging.GetInstance().Debug("Entering BusinessHelper.GetLookupData");
            SPListItemCollection itemsCollection = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList spList = web.Lists[listName];

                            //What shall i do in this part
                            if (spList == null)
                                throw new Exception();

                            SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject(string.Empty, "<FieldRef Name='ID' /><FieldRef Name='Title' /><FieldRef Name='TitleAr' />");
                            itemsCollection = spList.GetItems(spQuery);



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
                Logging.GetInstance().Debug("Exit BusinessHelper.GetLookupData");
            }
            return itemsCollection;
        }
        public static void DownloadFile(string SiteUrl, int ItemID, string spListName)
        {
            Logging.GetInstance().Debug("Entering BusinessHelper.DownloadFile"); 
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SiteUrl))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {

                            string strContentType = "";
                            SPList library = web.Lists[spListName];
                            SPListItem item = library.GetItemById(ItemID);
                            SPFile file = item.File;
                            string filename = file.Name;

                            switch (item["File_x0020_Type"].ToString())
                            {
                                case "txt":
                                    strContentType = "text/plain";
                                    break;
                                case "htm":
                                    strContentType = "text/html";
                                    break;
                                case "html":
                                    strContentType = "text/html";
                                    break;
                                case "rtf":
                                    strContentType = "text/richtext";
                                    break;
                                case "jpg":
                                    strContentType = "image/jpeg";
                                    break;

                                case "jpeg":
                                    strContentType = "image/jpeg";
                                    break;
                                case "gif":
                                    strContentType = "image/gif";
                                    break;
                                case "bmp":
                                    strContentType = "image/bmp";
                                    break;
                                case "mpg":
                                    strContentType = "video/mpeg";
                                    break;
                                case "mpeg":
                                    strContentType = "video/mpeg";
                                    break;
                                case "avi":
                                    strContentType = "video/avi";
                                    break;
                                case "pdf":
                                    strContentType = "application/pdf";
                                    break;
                                case "doc":
                                    strContentType = "application/msword";
                                    break;
                                case "dot":
                                    strContentType = "application/msword";
                                    break;
                                case "csv":
                                    strContentType = "application/vnd.msexcel";
                                    break;
                                case ".xls":
                                    strContentType = "application/vnd.msexcel";
                                    break;
                                case ".xlt":
                                    strContentType = "application/vnd.msexcel";
                                    break;
                                default:
                                    strContentType = "application/octet-stream";
                                    break;
                            }
                            HttpResponse Response = HttpContext.Current.Response;
                            Response.ClearContent();
                            Response.ClearHeaders();
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename.ToString());
                            // content length(File Size). This lets the browser know how much data is being transfered 
                            Response.AppendHeader("Content-Length", file.Length.ToString());
                            Response.ContentType = strContentType;

                            //Check that the client is connected and has not closed the connection after the request
                            if (Response.IsClientConnected)
                            {
                                Response.BinaryWrite(file.OpenBinary());
                            }
                            Response.Flush();
                            Response.Close();
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

                Logging.GetInstance().Debug("Exit BusinessHelper.DownloadFile");
            }

        }
        public static string GetstrViewFields(int LCID)
        {
            string strViewFields = string.Empty;
            if (LCID == (int)Language.English)
            {
                strViewFields = "<FieldRef Name='RequestNumber'/>"
                      + "<FieldRef Name='Applicants_QatarID'/>"
                       + "<FieldRef Name='Applicants_QID'/>"
                      + "<FieldRef Name='Applicants_ApplicantName'/>"
                      + "<FieldRef Name='Applicants_EnglishName'/>"
                      + "<FieldRef Name='SubmitDate'/>"
                      + "<FieldRef Name='Nationality_Title'/>"
                      + "<FieldRef Name='AcademicDegree'/>"
                      + "<FieldRef Name='CountryOfStudy'/>"
                      + "<FieldRef Name='University'/>"
                      + "<FieldRef Name='ID'/>"
                      + "<FieldRef Name='Faculty'/>"
                      + "<FieldRef Name='Specialization'/>"
                      + "<FieldRef Name='RequestStatus'/>"
                      + "<FieldRef Name='EntityNeedsEquivalency'/>"
                      + "<FieldRef Name='RequestStatusId'/>"
                     + "<FieldRef Name='EmployeeAssignedTo'/>"
                     + "<FieldRef Name='DelayedDays'/>"
                      + "<FieldRef Name='HighestCertificate'/>"
                          + "<FieldRef Name='RejectionDate'/>"
                        + "<FieldRef Name='RejectionReason'/>"
                        + "<FieldRef Name='UniversityNotFoundInList'/>"
                        + "<FieldRef Name='ProgramCountry'/>"
                        + "<FieldRef Name='ProgramUniversity'/>"
                         + "<FieldRef Name='ProgramFaculty'/>"
                        + "<FieldRef Name='OtherPAUniversityOfStudy'/>"
                        + "<FieldRef Name='RejectedFrom'/>";
            }
            else
            {
                strViewFields = "<FieldRef Name='RequestNumber'/>"
                     + "<FieldRef Name='Applicants_QatarID'/>"
                      + "<FieldRef Name='Applicants_QID'/>"
                     + "<FieldRef Name='Applicants_ApplicantName'/>"
                      + "<FieldRef Name='Applicants_ArabicName'/>"
                     + "<FieldRef Name='SubmitDate'/>"
                     + "<FieldRef Name='Nationality_TitleAr'/>"
                     + "<FieldRef Name='AcademicDegreeAr'/>"
                     + "<FieldRef Name='CountryOfStudyAr'/>"
                     + "<FieldRef Name='UniversityAr'/>"
                     + "<FieldRef Name='ID'/>"
                     + "<FieldRef Name='FacultyAr'/>"
                     + "<FieldRef Name='RequestStatusAr'/>"
                     + "<FieldRef Name='EmployeeAssignedTo'/>"
                     + "<FieldRef Name='SpecializationAr'/>"
                     + "<FieldRef Name='RequestStatusId'/>"
                     + "<FieldRef Name='EntityNeedsEquivalencyAr'/>"
                     + "<FieldRef Name='DelayedDays'/>"
                     + "<FieldRef Name='HighestCertificateAr'/>"
                     + "<FieldRef Name='RejectionDate'/>"
                        + "<FieldRef Name='RejectionReason'/>"
                        + "<FieldRef Name='UniversityNotFoundInList'/>"
                         + "<FieldRef Name='ProgramCountryAr'/>"
                        + "<FieldRef Name='ProgramUniversityAr'/>"
                         + "<FieldRef Name='ProgramFaculty'/>"
                        + "<FieldRef Name='OtherPAUniversityOfStudy'/>"
                + "<FieldRef Name='RejectedFrom'/>";
            }
            return strViewFields;
        }

        public static string GetstrViewFields()
        {
            string strViewFields = string.Empty;
           
                strViewFields = "<FieldRef Name='RequestNumber'/>"
                      + "<FieldRef Name='Applicants_QatarID'/>"
                       + "<FieldRef Name='Applicants_QID'/>"
                      + "<FieldRef Name='Applicants_ApplicantName'/>"
                      + "<FieldRef Name='Applicants_EnglishName'/>"
                      + "<FieldRef Name='SubmitDate'/>"
                      + "<FieldRef Name='Nationality_Title'/>"
                      + "<FieldRef Name='AcademicDegree'/>"
                      + "<FieldRef Name='CountryOfStudy'/>"
                      + "<FieldRef Name='University'/>"
                      + "<FieldRef Name='ID'/>"
                      + "<FieldRef Name='Faculty'/>"
                      + "<FieldRef Name='Specialization'/>"
                      + "<FieldRef Name='RequestStatus'/>"
                      + "<FieldRef Name='EntityNeedsEquivalency'/>"
                      + "<FieldRef Name='RequestStatusId'/>"
                     + "<FieldRef Name='EmployeeAssignedTo'/>"
                     + "<FieldRef Name='DelayedDays'/>"
                      + "<FieldRef Name='HighestCertificate'/>"
                          + "<FieldRef Name='RejectionDate'/>"
                        + "<FieldRef Name='RejectionReason'/>"
                        + "<FieldRef Name='UniversityNotFoundInList'/>"
                        + "<FieldRef Name='ProgramCountry'/>"
                        + "<FieldRef Name='ProgramUniversity'/>"
                         + "<FieldRef Name='ProgramFaculty'/>"
                        + "<FieldRef Name='OtherPAUniversityOfStudy'/>"
                        + "<FieldRef Name='RejectedFrom'/>";
           
            return strViewFields;
        }
        public static void UpdateRequestStatus(int statusID, string reqID,string AssignedTo)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method BusinessHelper.UpdateRequestStatus");
                            SPList list = web.Lists[Utilities.Constants.PARequests];
                            SPListItem itemToUpdate = list.GetItemById(int.Parse(reqID));
                            itemToUpdate["RequestStatus"] = new SPFieldLookupValue(statusID, statusID.ToString());
                            if (!string.IsNullOrEmpty(AssignedTo))
                            {
                                itemToUpdate["EmployeeAssignedTo"] = AssignedTo;
                            }
                            web.AllowUnsafeUpdates = true;
                            itemToUpdate.Update();
                            list.Update();
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            web.AllowUnsafeUpdates = false;
                            Logging.GetInstance().Debug("Exiting method BusinessHelper.UpdateRequestStatus");
                        }
                    }
                }
            });
        }
        public static void UpdateOrgBookReply(string reqID)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method BusinessHelper.UpdateOrgBookReply");
                            SPList list = web.Lists[Utilities.Constants.PARequests];
                            SPListItem itemToUpdate = list.GetItemById(int.Parse(reqID));
                            itemToUpdate["OrgBookReply"] = "1";
                            web.AllowUnsafeUpdates = true;
                            itemToUpdate.Update();
                            list.Update();
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            web.AllowUnsafeUpdates = false;
                            Logging.GetInstance().Debug("Exiting method BusinessHelper.UpdateOrgBookReply");
                        }
                    }
                }
            });
        }
        public static bool GetAssignee(string ReqID,string spGroupName=null)
        {
            bool IsAssignedtoLoggedIn = false;
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            SimilarRequest req = new SimilarRequest();
                            Logging.GetInstance().Debug("Entering method PAClarificationRequests.GetClarificationRequestbyReqID");
                            SPList list = web.Lists[Utilities.Constants.PARequests];
                            SPListItem item = list.GetItemById(int.Parse(ReqID));
                            if (item != null)
                            {
                                req.AssignedTo = (item["EmployeeAssignedTo"] != null) ? item["EmployeeAssignedTo"].ToString() : string.Empty;
                                if (req.AssignedTo.ToLower().Contains(SPContext.Current.Web.CurrentUser.LoginName.ToLower())||(spGroupName!=null&&req.AssignedTo.ToLower()==spGroupName.ToLower())
                                || HelperMethods.InGroup(req.AssignedTo))
                                    IsAssignedtoLoggedIn = true;
                                else
                                    IsAssignedtoLoggedIn = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method ClarRequestsListing.GetAssignee");
                        }
                    }
                }
            });
            return IsAssignedtoLoggedIn;
        }
    }
}
