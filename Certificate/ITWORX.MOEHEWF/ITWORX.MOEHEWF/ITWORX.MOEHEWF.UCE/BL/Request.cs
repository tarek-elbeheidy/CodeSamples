using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHEWF.UCE.Entities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using BLCommon = ITWORX.MOEHEWF.Common.BL;
using EntitiesCommon = ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHE.Integration.SMS;
using UtilitiesCommon = ITWORX.MOEHEWF.Common.Utilities;
using System.Web;


namespace ITWORX.MOEHEWF.UCE.BL
{
    public class Request
    {
        public static object ClarRequests { get; private set; }

        public static bool AddUpdateRequest(Entities.Request request, string WizardStepActive, int requestId, int requestStatusVal)
        {
            Logging.GetInstance().Debug("Entering method Request.AddUpdateRequest");
            SPWeb web = null;
            bool updated = false;
            try
            {


                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {

                            SPList requestsList = web.Lists[Utilities.Constants.Requests];
                            if (requestsList == null)
                                throw new Exception();
                            web.AllowUnsafeUpdates = true;
                            SPListItem item = null;
                            if (requestId > 0)
                            {
                                item = GetRequestItemByID(requestId);
                            }
                            else
                            {
                                item = requestsList.AddItem();
                            }
                            //string needsClarItemBeforeEdit = string.Empty;
                            //if (requestStatusVal== (int)Common.Utilities.RequestStatus.UCEDraftForClarification)
                            //{
                            //    needsClarItemBeforeEdit = item.Xml;
                            //}

                            if (request.AcademicDegree != null && !string.IsNullOrEmpty(request.AcademicDegree.SelectedID))
                            {
                                item["AcademicDegree"] = new SPFieldLookupValue(int.Parse(request.AcademicDegree.SelectedID), request.AcademicDegree.SelectedTitle);
                            }
                            if (request.AcademicDegreeForEquivalence != null && !string.IsNullOrEmpty(request.AcademicDegreeForEquivalence.SelectedID))
                            {
                                item["AcademicDegreeForEquivalence"] = new SPFieldLookupValue(int.Parse(request.AcademicDegreeForEquivalence.SelectedID), request.AcademicDegreeForEquivalence.SelectedTitle);
                            }
                            item["CertificateThroughScholarship"] = request.CertificateThroughScholarship;

                            //user control
                            if (!string.IsNullOrEmpty(request.EntityProvidingStudy))
                            {
                                item["EntityProvidingStudy"] = request.EntityProvidingStudy;
                            }

                            if (WizardStepActive != string.Empty)
                                item["WizardActiveStep"] = WizardStepActive;
                            item["CampusStudy"] = request.CampusStudy;
                            item["PlaceOfStudy"] = !string.IsNullOrEmpty(request.PlaceOfStudy) ? SPHttpUtility.HtmlEncode(request.PlaceOfStudy) : string.Empty;
                            item["CampusExam"] = request.CampusExam;
                            item["PlaceOfExam"] = !string.IsNullOrEmpty(request.PlaceOfExam) ? SPHttpUtility.HtmlEncode(request.PlaceOfExam) : string.Empty;
                            if (request.StudyLanguage != null && !string.IsNullOrEmpty(request.StudyLanguage.SelectedID))
                            {
                                item["StudyLanguage"] = new SPFieldLookupValue(int.Parse(request.StudyLanguage.SelectedID), request.StudyLanguage.SelectedTitle);
                            }
                            if (request.StudySystem != null && !string.IsNullOrEmpty(request.StudySystem.SelectedID))
                            {
                                item["StudySystem"] = new SPFieldLookupValue(int.Parse(request.StudySystem.SelectedID), request.StudySystem.SelectedTitle);
                            }
                            else
                            {
                                item["StudySystem"] = null;
                            }
                            if (request.StudyStartDate != DateTime.MinValue)
                            {
                                item["StudyStartDate"] = request.StudyStartDate;
                            }
                            if (request.StudyGraduationDate != DateTime.MinValue && request.StudyGraduationDate != DateTime.MinValue)
                            {
                                item["StudyGraduationDate"] = request.StudyGraduationDate;
                            }
                            else
                            {
                                item["StudyGraduationDate"] = null;
                            }
                            if (request.AcademicProgramPeriod != 0)
                            {
                                item["AcademicProgramPeriod"] = SPHttpUtility.HtmlEncode(request.AcademicProgramPeriod);
                            }
                            if (request.ActualStudingPeriod != 0)
                            {
                                item["ActualStudingPeriod"] = SPHttpUtility.HtmlEncode(request.ActualStudingPeriod);
                            }
                            if (request.NumberOfHoursGained != 0)
                            {
                                item["NumberOfHoursGained"] = SPHttpUtility.HtmlEncode(request.NumberOfHoursGained);
                            }
                            if (!string.IsNullOrEmpty(request.GPA))
                            {
                                item["GPA"] = SPHttpUtility.HtmlEncode(request.GPA);
                            }
                            if (request.NumberOfOnlineHours != 0)
                            {
                                item["NumberOfOnlineHours"] = SPHttpUtility.HtmlEncode(request.NumberOfOnlineHours);
                            }
                            item["PercentageOfOnlineHours"] = SPHttpUtility.HtmlEncode(request.PercentageOfOnlineHours);
                            //item["IsThereComprehensiveExam"] = request.IsThereComprehensiveExam;
                            //item["IsThereAcceptanceExam"] = request.IsThereAcceptanceExam;

                            //this should be changed when changed when cascading
                            if (request.CountryOfStudy != null)
                            {
                                if (!string.IsNullOrEmpty(request.CountryOfStudy.SelectedID))
                                {
                                    item["CountryOfStudy"] = new SPFieldLookupValue(int.Parse(request.CountryOfStudy.SelectedID), request.CountryOfStudy.SelectedTitle);
                                    item["OtherCountryOfStudy"] = string.Empty;
                                }
                                else
                                {
                                    item["CountryOfStudy"] = string.Empty;
                                    item["OtherCountryOfStudy"] = request.CountryOfStudy.SelectedTitle;
                                }
                            }

                            if (!string.IsNullOrEmpty(request.UniversityNotFoundInList) || request.University != null)
                            {
                                if (!string.IsNullOrEmpty(request.University.SelectedID))
                                {
                                    item["University"] = new SPFieldLookupValue(int.Parse(request.University.SelectedID), request.University.SelectedTitle);
                                    item["UniversityNotFoundInList"] = string.Empty;
                                }
                                else if (!string.IsNullOrEmpty(request.UniversityNotFoundInList))
                                {
                                    item["University"] = string.Empty;
                                    item["UniversityNotFoundInList"] = request.UniversityNotFoundInList;// request.University.SelectedTitle;
                                }
                            }

                            if (!string.IsNullOrEmpty(request.UniversityList))
                            {
                                item["UniversityList"] = request.UniversityList;
                            }
                            if (!string.IsNullOrEmpty(request.Faculty))
                            {
                                item["Faculty"] =
                                item["FacultyAr"] =
                                SPHttpUtility.HtmlEncode(request.Faculty);
                            }

                            if (!string.IsNullOrEmpty(request.SpecializationNotFoundInList) || request.Specialization != null)
                            {
                                if (!string.IsNullOrEmpty(request.SpecializationNotFoundInList))
                                {
                                    item["SpecializationNotFoundInList"] = SPHttpUtility.HtmlEncode(request.SpecializationNotFoundInList);
                                }
                                else
                                {
                                    item["SpecializationNotFoundInList"] = string.Empty;
                                }
                                if (request.Specialization != null && !string.IsNullOrEmpty(request.Specialization.SelectedID))
                                {
                                    item["Specialization"] = new SPFieldLookupValue(int.Parse(request.Specialization.SelectedID), request.Specialization.SelectedTitle);
                                }
                                else
                                {

                                    item["Specialization"] = string.Empty;
                                }

                            }

                            if (!string.IsNullOrEmpty(request.NewUniversityHeadquarter) || request.UniversityMainHeadquarter != null)
                            {
                                if (!string.IsNullOrEmpty(request.NewUniversityHeadquarter))
                                {
                                    item["NewUniversityHeadquarter"] = SPHttpUtility.HtmlEncode(request.NewUniversityHeadquarter);
                                }
                                else
                                {
                                    item["NewUniversityHeadquarter"] = string.Empty;
                                }
                                if (!string.IsNullOrEmpty(request.UniversityMainHeadquarter.SelectedID))
                                {
                                    item["UniversityMainHeadquarter"] = new SPFieldLookupValue(int.Parse(request.UniversityMainHeadquarter.SelectedID), request.UniversityMainHeadquarter.SelectedTitle);
                                }
                                else
                                {

                                    item["UniversityMainHeadquarter"] = string.Empty;
                                }

                            }
                            if (!string.IsNullOrEmpty(request.UniversityAddress))
                            {
                                item["UniversityAddress"] = SPHttpUtility.HtmlEncode(request.UniversityAddress);
                            }
                            if (!string.IsNullOrEmpty(request.UniversityEmail))
                            {
                                item["UniversityEmail"] = SPHttpUtility.HtmlEncode(request.UniversityEmail);
                            }
                            item["HavePAOrNot"] = request.HavePAOrNot;
                            item["WorkingOrNot"] = request.WorkingOrNot;
                            item["UniversityType"] = request.UniversityType;
                            if (request.WorkingOrNot == true)
                            {

                                if (!string.IsNullOrEmpty(request.OtherEntityWorkingFor) || request.EntityWorkingFor != null)
                                {
                                    if (!string.IsNullOrEmpty(request.OtherEntityWorkingFor))
                                    {
                                        item["OtherEntityWorkingFor"] = SPHttpUtility.HtmlEncode(request.OtherEntityWorkingFor);
                                    }
                                    else
                                    {
                                        item["OtherEntityWorkingFor"] = string.Empty;
                                    }
                                    if (!string.IsNullOrEmpty(request.EntityWorkingFor.SelectedID))
                                    {
                                        item["EntityWorkingFor"] = new SPFieldLookupValue(int.Parse(request.EntityWorkingFor.SelectedID), request.EntityWorkingFor.SelectedTitle);
                                    }
                                    else
                                    {

                                        item["EntityWorkingFor"] = string.Empty;
                                    }

                                }



                                //if (!string.IsNullOrEmpty(request.Occupation))
                                //{
                                //    item["Occupation"] = request.Occupation;
                                //} 

                                //Ahmed
                                //item["EntityNeedsEquivalency"] = string.Empty;
                                //item["OtherEntityNeedsEquivalency"] = string.Empty;
                                if (!string.IsNullOrEmpty(request.OtherEntityNeedsEquivalency) || request.EntityNeedsEquivalency != null)
                                {
                                    if (!string.IsNullOrEmpty(request.OtherEntityNeedsEquivalency))
                                    {
                                        item["OtherEntityNeedsEquivalency"] = SPHttpUtility.HtmlEncode(request.OtherEntityNeedsEquivalency);
                                    }
                                    else
                                    {
                                        item["OtherEntityNeedsEquivalency"] = string.Empty;
                                    }
                                    if (!string.IsNullOrEmpty(request.EntityNeedsEquivalency.SelectedID))
                                    {
                                        item["EntityNeedsEquivalency"] = new SPFieldLookupValue(int.Parse(request.EntityNeedsEquivalency.SelectedID), request.EntityNeedsEquivalency.SelectedTitle);
                                    }
                                    else
                                    {

                                        item["EntityNeedsEquivalency"] = string.Empty;
                                    }

                                }

                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(request.OtherEntityNeedsEquivalency) || request.EntityNeedsEquivalency != null)
                                {
                                    if (!string.IsNullOrEmpty(request.OtherEntityNeedsEquivalency))
                                    {
                                        item["OtherEntityNeedsEquivalency"] = SPHttpUtility.HtmlEncode(request.OtherEntityNeedsEquivalency);
                                    }
                                    else
                                    {
                                        item["OtherEntityNeedsEquivalency"] = string.Empty;
                                    }
                                    if (!string.IsNullOrEmpty(request.EntityNeedsEquivalency.SelectedID))
                                    {
                                        item["EntityNeedsEquivalency"] = new SPFieldLookupValue(int.Parse(request.EntityNeedsEquivalency.SelectedID), request.EntityNeedsEquivalency.SelectedTitle);
                                    }
                                    else
                                    {

                                        item["EntityNeedsEquivalency"] = string.Empty;
                                    }

                                }
                                item["EntityWorkingFor"] = string.Empty;
                                item["OtherEntityWorkingFor"] = string.Empty;
                                item["Occupation"] = string.Empty;
                                item["OtherOccupation"] = string.Empty;
                                item["HiringDate"] = null;
                                item["OccupationPhone"] = string.Empty;


                            }
                            if (request.IncomingNumber != 0)
                            {
                                item["IncomingNumber"] = SPHttpUtility.HtmlEncode(request.IncomingNumber);
                            }
                            if (request.BookDate != DateTime.MinValue && request.BookDate != DateTime.MinValue)
                            {
                                item["BookDate"] = request.BookDate;
                            }
                            if (!string.IsNullOrEmpty(request.BarCode))
                            {
                                item["BarCode"] = SPHttpUtility.HtmlEncode(request.BarCode);
                            }
                            item["LoginName"] = request.LoginName;
                            item["RequestType"] = new SPFieldLookupValue((int)RequestType.CertificateEquivalency, ((int)RequestType.CertificateEquivalency).ToString()); // "Certificate Equivalency"/* Constants.RequestStatus.RequestStatus.UCESubmitted*/);

                            Common.Entities.RequestStatus requestStatus = new Common.Entities.RequestStatus();// GetRequestStatusById(int requestStatusId)
                            if (requestStatusVal == (int)Common.Utilities.RequestStatus.UCESubmitted)
                            {
                                if (request.SubmitDate != DateTime.MinValue)
                                {
                                    item["SubmitDate"] = request.SubmitDate;
                                }
                                if (request.ActionDate != DateTime.MinValue)
                                {
                                    item["ActionDate"] = request.ActionDate;
                                }
                                //item["EmployeeAssignedTo"] = "Receptionists";
                                item["RequestStatus"] = new SPFieldLookupValue((int)Common.Utilities.RequestStatus.UCESubmitted, ((int)Common.Utilities.RequestStatus.UCESubmitted).ToString());
                                requestStatus = Common.BL.RequestStatus.GetRequestStatusById((int)Common.Utilities.RequestStatus.UCESubmitted);
                                if (!string.IsNullOrEmpty(requestStatus.FinalDecisionEn) || !string.IsNullOrEmpty(requestStatus.FinalDecisionAr))
                                {
                                    item["FinalDecision"] = requestStatus.FinalDecisionEn;
                                    item["FinalDecisionAr"] = requestStatus.FinalDecisionAr;
                                }
                            }
                            else if (requestStatusVal == (int)Common.Utilities.RequestStatus.UCEDraft)
                            {

                                item["RequestStatus"] = new SPFieldLookupValue((int)Common.Utilities.RequestStatus.UCEDraft, ((int)Common.Utilities.RequestStatus.UCEDraft).ToString());/*Constants.RequestStatus.UCEDraft*/
                                requestStatus = Common.BL.RequestStatus.GetRequestStatusById((int)Common.Utilities.RequestStatus.UCEDraft);
                                if (!string.IsNullOrEmpty(requestStatus.FinalDecisionEn) || !string.IsNullOrEmpty(requestStatus.FinalDecisionAr))
                                {
                                    item["FinalDecision"] = requestStatus.FinalDecisionEn;
                                    item["FinalDecisionAr"] = requestStatus.FinalDecisionAr;
                                }
                            }
                            else
                            {

                                item["RequestStatus"] = new SPFieldLookupValue((int)Common.Utilities.RequestStatus.UCEDraftForClarification, ((int)Common.Utilities.RequestStatus.UCEDraftForClarification).ToString());/*Constants.RequestStatus.UCEDraft*/
                                requestStatus = Common.BL.RequestStatus.GetRequestStatusById((int)Common.Utilities.RequestStatus.UCEDraftForClarification);
                                if (!string.IsNullOrEmpty(requestStatus.FinalDecisionEn) || !string.IsNullOrEmpty(requestStatus.FinalDecisionAr))
                                {
                                    item["FinalDecision"] = requestStatus.FinalDecisionEn;
                                    item["FinalDecisionAr"] = requestStatus.FinalDecisionAr;
                                }
                            }

                            item.Update();
                            string requestNum = generateRequestNumber(item.ID);

                            item["RequestNumber"] = requestNum;
                            HttpContext.Current.Session["RequestNumber"] = requestNum;
                            //if(item.Xml.Get == old xml)
                            //error and return

                            //set status
                            item.Update();
                            updated = true;
                            request.ID = item.ID;

                            //if (needsClarItemBeforeEdit.Equals(item.Xml))
                            //{
                            //    updated = false;
                            //}

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
                web.AllowUnsafeUpdates = false;
                Logging.GetInstance().Debug("Exiting method Request.AddUpdateRequest");
            }
            return updated;
        }
        public static SPListItem GetRequestItemByID(int requestId)
        {
            Logging.GetInstance().Debug("Entering method Request.GetRequestItemByNumber");
            SPListItem item = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            string userName = SPContext.Current.Web.CurrentUser.LoginName.ToLower();
                            SPList requestsList = web.Lists[Utilities.Constants.Requests];
                            if (requestsList == null)
                                throw new Exception();


                            SPQuery reqQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" +
                                requestId + "</Value></Eq></Where>");

                            SPListItemCollection requestItemCollection = requestsList.GetItems(reqQuery);
                            if (requestItemCollection != null && requestItemCollection.Count > 0)
                            {
                                item = requestItemCollection[0];
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
                Logging.GetInstance().Debug("Exiting method Request.GetRequestItemByNumber");
            }
            return item;
        }
        public static List<Entities.Request> GetRequestActiveStep(int requestId)
        {
            List<Entities.Request> Request = new List<Entities.Request>();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method Request.GetRequestActiveStep");
                            string userName = SPContext.Current.Web.CurrentUser.LoginName.ToLower();
                            SPList requestsList = web.Lists[Utilities.Constants.Requests];
                            if (requestsList == null)
                                throw new Exception();
                            SPQuery reqQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='LoginName' /><Value Type='Text'>" +
                            userName + "</Value></Eq><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + requestId + "</Value></Eq></And></Where>");

                            SPListItemCollection requestItemCollection = requestsList.GetItems(reqQuery);
                            if (requestItemCollection.Count != 0)
                                foreach (SPListItem item in requestItemCollection)
                                {
                                    Request.Add(new Entities.Request()
                                    {
                                        WizardActiveStep = (item["WizardActiveStep"] != null) ? item["WizardActiveStep"].ToString() : string.Empty,
                                    });
                                }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method Request.GetRequestActiveStep");
                        }
                    }
                }
            });
            return Request;
        }


        public static SPListItem Reviewer_GetRequestItemByID(int requestId)
        {
            Logging.GetInstance().Debug("Entering method Request.GetRequestItemByNumber");
            SPListItem item = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            string userName = SPContext.Current.Web.CurrentUser.LoginName.ToLower();
                            SPList requestsList = web.Lists[Utilities.Constants.Requests];
                            if (requestsList == null)
                                throw new Exception();


                            SPQuery reqQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" +
                                requestId + "</Value></Eq></Where>");

                            SPListItemCollection requestItemCollection = requestsList.GetItems(reqQuery);
                            if (requestItemCollection != null && requestItemCollection.Count > 0)
                            {
                                item = requestItemCollection[0];
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
                Logging.GetInstance().Debug("Exiting method Request.GetRequestItemByNumber");
            }
            return item;
        }
        public static Entities.Request GetRequestByNumber(int requestId, int LCID)
        {
            Logging.GetInstance().Debug("Entering method Request.GetRequestByNumber");
            Entities.Request request = null;
            try
            {

                SPListItem requestItem = GetRequestItemByID(requestId);
                if (requestItem != null)
                {
                    request = new Entities.Request();
                    request.ID = requestItem.ID;
                    if (requestItem["RequestNumber"] != null)
                    {
                        request.RequestNumber = Convert.ToString(requestItem["RequestNumber"]);
                    }
                    if (Convert.ToDateTime(requestItem["SubmitDate"]) != DateTime.MinValue)
                    {
                        request.SubmitDate = Convert.ToDateTime(requestItem["SubmitDate"]);
                    }
                    if (Convert.ToDateTime(requestItem["Created"]) != DateTime.MinValue)
                    {
                        request.RequestCreationDate = Convert.ToDateTime(requestItem["Created"]);
                    }
                    if (requestItem["ApplicantID"] != null)
                    {
                        request.ApplicantID = new Applicants()
                        {
                            ID = new SPFieldLookupValue(requestItem["ApplicantID"].ToString()).LookupId
                        };
                    }
                    if (requestItem["CountryOfStudy"] != null)
                    {
                        request.CountryOfStudy = new CountryOfStudy()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["CountryOfStudy"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["CountryOfStudy"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["CountryOfStudyAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["OtherCountryOfStudy"] != null)
                    {
                        request.CountryOfStudy = new CountryOfStudy()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = requestItem["OtherCountryOfStudy"].ToString()
                        };
                    }

                    if (requestItem["University"] != null)
                    {

                        request.University = new University()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["University"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["University"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["UniversityAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["UniversityNotFoundInList"] != null)
                    {
                        request.University = new University()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = requestItem["UniversityNotFoundInList"].ToString()
                        };
                        request.UniversityNotFoundInList = Convert.ToString(requestItem["UniversityNotFoundInList"]);
                    }
                    //else if (requestItem["UniversityNotFoundInList"] != null)
                    //{

                    //    request.UniversityNotFoundInList = Convert.ToString(requestItem["UniversityNotFoundInList"]);
                    //}
                    if (requestItem["AcademicDegree"] != null)
                    {
                        request.AcademicDegree = new Entities.AcademicDegree()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["AcademicDegree"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["AcademicDegree"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["AcademicDegreeAr"].ToString()).LookupValue
                        };
                    }
                    if (requestItem["AcademicDegreeForEquivalence"] != null)
                    {
                        request.AcademicDegreeForEquivalence = new Entities.AcademicDegree()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["AcademicDegreeForEquivalence"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["AcademicDegreeForEquivalence"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["AcademicDegreeForEquivalenceAr"].ToString()).LookupValue
                        };
                    }
                    if (requestItem["RequestStatus"] != null)
                    {
                        request.RequestStatus = new Common.Entities.RequestStatus()
                        {
                            Code = new SPFieldLookupValue(requestItem["RequestStatus"].ToString()).LookupId.ToString()
                        };
                    }
                    if (requestItem["CertificateThroughScholarship"] != null)
                    {
                        request.CertificateThroughScholarship = Convert.ToBoolean(requestItem["CertificateThroughScholarship"]);
                    }



                    //handle this section in edit mode
                    if (requestItem["EntityProvidingStudy"] != null)
                    {
                        request.EntityProvidingStudy = Convert.ToString(requestItem["EntityProvidingStudy"]);
                    }

                    request.CampusStudy = Convert.ToBoolean(requestItem["CampusStudy"]);

                    if (requestItem["PlaceOfStudy"] != null)
                    {

                        request.PlaceOfStudy = Convert.ToString(requestItem["PlaceOfStudy"]);
                    }

                    request.CampusExam = Convert.ToBoolean(requestItem["CampusExam"]);

                    if (requestItem["PlaceOfExam"] != null)
                    {

                        request.PlaceOfExam = Convert.ToString(requestItem["PlaceOfExam"]);
                    }

                    //if (requestItem["Faculty"] != null)
                    //{

                    //    request.Faculty = new Entities.Faculty()
                    //    {
                    //        SelectedID = new SPFieldLookupValue(requestItem["Faculty"].ToString()).LookupId.ToString(),
                    //        SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["Faculty"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["FacultyAr"].ToString()).LookupValue
                    //    };
                    //} 
                    if (requestItem["Faculty"] != null)
                    {

                        request.Faculty = Convert.ToString(requestItem["Faculty"]);
                    }
                    else if (requestItem["FacultyNotFoundInList"] != null)
                    {

                        request.FacultyNotFoundInList = Convert.ToString(requestItem["FacultyNotFoundInList"]);
                    }

                    //Add notes to the request
                    if (requestItem["Note"] != null)
                    {

                        request.Note = Convert.ToString(requestItem["Note"]);
                    }
                    else
                    {
                        request.Note = string.Empty;
                    }


                    if (requestItem["Specialization"] != null)
                    {

                        request.Specialization = new Entities.Specialization()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["Specialization"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["Specialization"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["SpecializationAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["SpecializationNotFoundInList"] != null)
                    {

                        request.SpecializationNotFoundInList = Convert.ToString(requestItem["SpecializationNotFoundInList"]);
                    }

                    if (requestItem["UniversityMainHeadquarter"] != null)
                    {

                        request.UniversityMainHeadquarter = new Entities.UniversityMainCountry()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["UniversityMainHeadquarter"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["UniversityMainHeadquarter"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["UniversityMainHeadquarterAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["NewUniversityHeadquarter"] != null)
                    {

                        request.NewUniversityHeadquarter = Convert.ToString(requestItem["NewUniversityHeadquarter"]);
                    }


                    if (requestItem["StudyLanguage"] != null)
                    {
                        request.StudyLanguage = new Entities.StudyLanguage()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["StudyLanguage"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["StudyLanguage"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["StudyLanguageAr"].ToString()).LookupValue
                        };
                    }
                    //if (requestItem["StudyType"] != null)
                    //{
                    //    request.StudyType = new Entities.StudyType()
                    //    {
                    //        SelectedID = new SPFieldLookupValue(requestItem["StudyType"].ToString()).LookupId.ToString(),
                    //        SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["StudyType"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["StudyTypeAr"].ToString()).LookupValue
                    //    };
                    //}
                    if (requestItem["StudySystem"] != null)
                    {
                        request.StudySystem = new Entities.StudySystem()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["StudySystem"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["StudySystem"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["StudySystemAr"].ToString()).LookupValue
                        };

                    }
                    if (requestItem["StudyStartDate"] != null)
                    {
                        request.StudyStartDate = Convert.ToDateTime(requestItem["StudyStartDate"]);
                    }
                    if (requestItem["StudyGraduationDate"] != null)
                    {
                        request.StudyGraduationDate = Convert.ToDateTime(requestItem["StudyGraduationDate"]);
                    }
                    if (requestItem["AcademicProgramPeriod"] != null)
                    {
                        request.AcademicProgramPeriod = int.Parse(Convert.ToString(requestItem["AcademicProgramPeriod"]));
                    }
                    if (requestItem["ActualStudingPeriod"] != null)
                    {
                        request.ActualStudingPeriod = int.Parse(Convert.ToString(requestItem["ActualStudingPeriod"]));
                    }
                    if (requestItem["NumberOfHoursGained"] != null)
                    {
                        request.NumberOfHoursGained = int.Parse(Convert.ToString(requestItem["NumberOfHoursGained"]));
                    }
                    if (requestItem["GPA"] != null)
                    {
                        request.GPA = Convert.ToString(requestItem["GPA"]);
                    }
                    if (requestItem["NumberOfOnlineHours"] != null)
                    {
                        request.NumberOfOnlineHours = int.Parse(Convert.ToString(requestItem["NumberOfOnlineHours"]));
                    }
                    if (requestItem["PercentageOfOnlineHours"] != null)
                    {
                        request.PercentageOfOnlineHours = Convert.ToString(requestItem["PercentageOfOnlineHours"]);
                    }
                    //if (requestItem["IsThereComprehensiveExam"] != null)
                    //{
                    //    request.IsThereComprehensiveExam = Convert.ToBoolean(requestItem["IsThereComprehensiveExam"]);
                    //}
                    //if (requestItem["IsThereAcceptanceExam"] != null)
                    //{
                    //    request.IsThereAcceptanceExam = Convert.ToBoolean(requestItem["IsThereAcceptanceExam"]);
                    //}
                    if (requestItem["UniversityAddress"] != null)
                    {
                        request.UniversityAddress = Convert.ToString(requestItem["UniversityAddress"]);
                    }
                    if (requestItem["UniversityEmail"] != null)
                    {
                        request.UniversityEmail = Convert.ToString(requestItem["UniversityEmail"]);
                    }
                    if (requestItem["WorkingOrNot"] != null)
                    {
                        request.WorkingOrNot = Convert.ToBoolean(requestItem["WorkingOrNot"]);


                        if (Convert.ToBoolean(requestItem["WorkingOrNot"]) == true)
                        {
                            // set drop of occupation and entity working for

                            if (requestItem["EntityWorkingFor"] != null)
                            {

                                request.EntityWorkingFor = new Entities.EntityWorkingFor()
                                {
                                    SelectedID = new SPFieldLookupValue(requestItem["EntityWorkingFor"].ToString()).LookupId.ToString(),
                                    SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["EntityWorkingFor"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["EntityWorkingForAr"].ToString()).LookupValue
                                };
                            }
                            else if (requestItem["OtherEntityWorkingFor"] != null)
                            {

                                request.OtherEntityWorkingFor = Convert.ToString(requestItem["OtherEntityWorkingFor"]);
                            }

                            //if (requestItem["Occupation"] != null)
                            //{

                            //    request.Occupation = Convert.ToString(requestItem["Occupation"]);
                            //request.Occupation = new Entities.Occupation()
                            //{
                            //    SelectedID = new SPFieldLookupValue(requestItem["Occupation"].ToString()).LookupId.ToString(),
                            //    SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["Occupation"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["OccupationAr"].ToString()).LookupValue
                            //};
                            // }
                            //else if (requestItem["OtherOccupation"] != null)
                            //{

                            //    request.OtherOccupation = Convert.ToString(requestItem["OtherOccupation"]);
                            //}


                            //request.HiringDate = Convert.ToDateTime(requestItem["HiringDate"]);
                            //request.OccupationPhone = Convert.ToString(requestItem["OccupationPhone"]);

                            //Ahmed
                            if (requestItem["EntityNeedsEquivalency"] != null)
                            {

                                request.EntityNeedsEquivalency = new Entities.EntityNeedsEquivalency()
                                {
                                    SelectedID = new SPFieldLookupValue(requestItem["EntityNeedsEquivalency"].ToString()).LookupId.ToString(),
                                    SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["EntityNeedsEquivalency"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["EntityNeedsEquivalencyAr"].ToString()).LookupValue
                                };
                            }
                            else if (requestItem["OtherEntityNeedsEquivalency"] != null)
                            {

                                request.OtherEntityNeedsEquivalency = Convert.ToString(requestItem["OtherEntityNeedsEquivalency"]);
                            }

                        }
                        else
                        {
                            if (requestItem["EntityNeedsEquivalency"] != null)
                            {

                                request.EntityNeedsEquivalency = new Entities.EntityNeedsEquivalency()
                                {
                                    SelectedID = new SPFieldLookupValue(requestItem["EntityNeedsEquivalency"].ToString()).LookupId.ToString(),
                                    SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["EntityNeedsEquivalency"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["EntityNeedsEquivalencyAr"].ToString()).LookupValue
                                };
                            }
                            else if (requestItem["OtherEntityNeedsEquivalency"] != null)
                            {

                                request.OtherEntityNeedsEquivalency = Convert.ToString(requestItem["OtherEntityNeedsEquivalency"]);
                            }

                        }
                    }
                    if (requestItem["IncomingNumber"] != null)
                    {
                        request.IncomingNumber = int.Parse(Convert.ToString(requestItem["IncomingNumber"]));
                    }
                    if (requestItem["BookDate"] != null)
                    {
                        request.BookDate = Convert.ToDateTime(requestItem["BookDate"]);
                    }
                    if (requestItem["BarCode"] != null)
                    {
                        request.BarCode = Convert.ToString(requestItem["BarCode"]);
                    }
                    if (requestItem["LoginName"] != null)
                    {
                        request.LoginName = Convert.ToString(requestItem["LoginName"]);
                    }
                    if (requestItem["Year"] != null)
                    {
                        request.Year = int.Parse(Convert.ToString(requestItem["Year"]));
                    }
                    if (requestItem["UniversityList"] != null)
                    {
                        request.UniversityList = Convert.ToString(requestItem["UniversityList"]);
                    }
                    if (requestItem["UniversityType"] != null)
                    {
                        request.UniversityType = Convert.ToString(requestItem["UniversityType"]);
                    }

                    if (requestItem["HavePAOrNot"] != null)
                    {
                        request.HavePAOrNot = Convert.ToBoolean(requestItem["HavePAOrNot"]);

                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Request.GetRequestByNumber");
            }
            return request;
        }
        public static Entities.Request GetApplicantFromRequest(int requestId, int LCID)
        {
            Logging.GetInstance().Debug("Entering method Request.GetApplicantFromRequest");
            Entities.Request request = null;
            try
            {
                SPListItem requestItem = Reviewer_GetRequestItemByID(requestId);
                if (requestItem != null)
                {
                    request = new Entities.Request();
                    if (requestItem["ApplicantID"] != null)
                    {
                        request.ApplicantID = new Applicants()
                        {
                            ID = new SPFieldLookupValue(requestItem["ApplicantID"].ToString()).LookupId
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Request.GetApplicantFromRequest");
            }
            return request;
        }
        public static Entities.Request Reviewer_GetRequestByNumber(int requestId, int LCID)
        {
            Logging.GetInstance().Debug("Entering method Request.GetRequestByNumber");
            Entities.Request request = null;
            try
            {

                SPListItem requestItem = Reviewer_GetRequestItemByID(requestId);
                if (requestItem != null)
                {

                    request = new Entities.Request();
                    request.ID = requestItem.ID;
                    if (requestItem["RequestNumber"] != null)
                    {
                        request.RequestNumber = Convert.ToString(requestItem["RequestNumber"]);
                    }
                    if (Convert.ToDateTime(requestItem["SubmitDate"]) != DateTime.MinValue)
                    {
                        request.SubmitDate = Convert.ToDateTime(requestItem["SubmitDate"]);
                    }
                    if (Convert.ToDateTime(requestItem["Created"]) != DateTime.MinValue)
                    {
                        request.RequestCreationDate = Convert.ToDateTime(requestItem["Created"]);
                    }
                    if (requestItem["ApplicantID"] != null)
                    {
                        request.ApplicantID = new Applicants()
                        {
                            ID = new SPFieldLookupValue(requestItem["ApplicantID"].ToString()).LookupId
                        };
                    }
                    if (requestItem["AcademicDegree"] != null)
                    {
                        request.AcademicDegree = new Entities.AcademicDegree()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["AcademicDegree"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["AcademicDegree"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["AcademicDegreeAr"].ToString()).LookupValue
                        };
                    }
                    if (requestItem["AcademicDegreeForEquivalence"] != null)
                    {
                        request.AcademicDegreeForEquivalence = new Entities.AcademicDegree()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["AcademicDegreeForEquivalence"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["AcademicDegreeForEquivalence"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["AcademicDegreeForEquivalenceAr"].ToString()).LookupValue
                        };
                    }
                    if (requestItem["RequestStatus"] != null)
                    {
                        request.RequestStatus = new Common.Entities.RequestStatus()
                        {
                            Code = new SPFieldLookupValue(requestItem["RequestStatus"].ToString()).LookupId.ToString()
                        };
                    }
                    if (requestItem["CertificateThroughScholarship"] != null)
                    {
                        request.CertificateThroughScholarship = Convert.ToBoolean(requestItem["CertificateThroughScholarship"]);
                    }



                    //handle this section in edit mode
                    if (requestItem["EntityProvidingStudy"] != null)
                    {
                        request.EntityProvidingStudy = Convert.ToString(requestItem["EntityProvidingStudy"]);

                        //request.EntityProvidingStudy = new Entities.EntityProvidingStudy()
                        //{
                        //    SelectedID = new SPFieldLookupValue(requestItem["EntityProvidingStudy"].ToString()).LookupId.ToString(),
                        //    SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["EntityProvidingStudy"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["EntityProvidingStudyAr"].ToString()).LookupValue
                        //};
                    }
                    //else if (requestItem["OtherEntity"] != null)
                    //{

                    //    request.OtherEntity = Convert.ToString(requestItem["OtherEntity"]);
                    //}

                    request.CampusStudy = Convert.ToBoolean(requestItem["CampusStudy"]);

                    if (requestItem["PlaceOfStudy"] != null)
                    {

                        request.PlaceOfStudy = Convert.ToString(requestItem["PlaceOfStudy"]);
                    }

                    request.CampusExam = Convert.ToBoolean(requestItem["CampusExam"]);

                    if (requestItem["PlaceOfExam"] != null)
                    {

                        request.PlaceOfExam = Convert.ToString(requestItem["PlaceOfExam"]);
                    }

                    if (requestItem["CountryOfStudy"] != null)
                    {
                        request.CountryOfStudy = new CountryOfStudy()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["CountryOfStudy"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["CountryOfStudy"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["CountryOfStudyAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["OtherCountryOfStudy"] != null)
                    {
                        request.CountryOfStudy = new CountryOfStudy()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = requestItem["OtherCountryOfStudy"].ToString()
                        };
                    }

                    if (requestItem["University"] != null)
                    {

                        request.University = new University()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["University"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["University"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["UniversityAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["UniversityNotFoundInList"] != null)
                    {
                        request.University = new University()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = requestItem["UniversityNotFoundInList"].ToString()
                        };
                        request.UniversityNotFoundInList = Convert.ToString(requestItem["UniversityNotFoundInList"]);
                    }
                    //else if (requestItem["UniversityNotFoundInList"] != null)
                    //{

                    //    request.UniversityNotFoundInList = Convert.ToString(requestItem["UniversityNotFoundInList"]);
                    //}


                    //if (requestItem["Faculty"] != null)
                    //{

                    //    request.Faculty = new Entities.Faculty()
                    //    {
                    //        SelectedID = new SPFieldLookupValue(requestItem["Faculty"].ToString()).LookupId.ToString(),
                    //        SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["Faculty"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["FacultyAr"].ToString()).LookupValue
                    //    };
                    //}
                    if (requestItem["Faculty"] != null)
                    {

                        request.Faculty = Convert.ToString(requestItem["Faculty"]);
                    }
                    else if (requestItem["FacultyNotFoundInList"] != null)
                    {

                        request.FacultyNotFoundInList = Convert.ToString(requestItem["FacultyNotFoundInList"]);
                    }


                    if (requestItem["Specialization"] != null)
                    {

                        request.Specialization = new Entities.Specialization()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["Specialization"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["Specialization"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["SpecializationAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["SpecializationNotFoundInList"] != null)
                    {

                        request.SpecializationNotFoundInList = Convert.ToString(requestItem["SpecializationNotFoundInList"]);
                    }

                    if (requestItem["UniversityMainHeadquarter"] != null)
                    {

                        request.UniversityMainHeadquarter = new Entities.UniversityMainCountry()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["UniversityMainHeadquarter"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["UniversityMainHeadquarter"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["UniversityMainHeadquarterAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["NewUniversityHeadquarter"] != null)
                    {

                        request.NewUniversityHeadquarter = Convert.ToString(requestItem["NewUniversityHeadquarter"]);
                    }


                    if (requestItem["StudyLanguage"] != null)
                    {
                        request.StudyLanguage = new Entities.StudyLanguage()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["StudyLanguage"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["StudyLanguage"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["StudyLanguageAr"].ToString()).LookupValue
                        };
                    }
                    //if (requestItem["StudyType"] != null)
                    //{
                    //    request.StudyType = new Entities.StudyType()
                    //    {
                    //        SelectedID = new SPFieldLookupValue(requestItem["StudyType"].ToString()).LookupId.ToString(),
                    //        SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["StudyType"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["StudyTypeAr"].ToString()).LookupValue
                    //    };
                    //}
                    if (requestItem["StudySystem"] != null)
                    {
                        request.StudySystem = new Entities.StudySystem()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["StudySystem"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["StudySystem"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["StudySystemAr"].ToString()).LookupValue
                        };

                    }
                    if (requestItem["StudyStartDate"] != null)
                    {
                        request.StudyStartDate = Convert.ToDateTime(requestItem["StudyStartDate"]);
                    }
                    if (requestItem["StudyGraduationDate"] != null)
                    {
                        request.StudyGraduationDate = Convert.ToDateTime(requestItem["StudyGraduationDate"]);
                    }
                    if (requestItem["AcademicProgramPeriod"] != null)
                    {
                        request.AcademicProgramPeriod = int.Parse(Convert.ToString(requestItem["AcademicProgramPeriod"]));
                    }
                    if (requestItem["ActualStudingPeriod"] != null)
                    {
                        request.ActualStudingPeriod = int.Parse(Convert.ToString(requestItem["ActualStudingPeriod"]));
                    }
                    if (requestItem["NumberOfHoursGained"] != null)
                    {
                        request.NumberOfHoursGained = int.Parse(Convert.ToString(requestItem["NumberOfHoursGained"]));
                    }
                    if (requestItem["GPA"] != null)
                    {
                        request.GPA = Convert.ToString(requestItem["GPA"]);
                    }
                    if (requestItem["NumberOfOnlineHours"] != null)
                    {
                        request.NumberOfOnlineHours = int.Parse(Convert.ToString(requestItem["NumberOfOnlineHours"]));
                    }
                    if (requestItem["PercentageOfOnlineHours"] != null)
                    {
                        request.PercentageOfOnlineHours = Convert.ToString(requestItem["PercentageOfOnlineHours"]);
                    }
                    //if (requestItem["IsThereComprehensiveExam"] != null)
                    //{
                    //    request.IsThereComprehensiveExam = Convert.ToBoolean(requestItem["IsThereComprehensiveExam"]);
                    //}
                    //if (requestItem["IsThereAcceptanceExam"] != null)
                    //{
                    //    request.IsThereAcceptanceExam = Convert.ToBoolean(requestItem["IsThereAcceptanceExam"]);
                    //}
                    if (requestItem["UniversityAddress"] != null)
                    {
                        request.UniversityAddress = Convert.ToString(requestItem["UniversityAddress"]);
                    }
                    if (requestItem["UniversityEmail"] != null)
                    {
                        request.UniversityEmail = Convert.ToString(requestItem["UniversityEmail"]);
                    }
                    if (requestItem["WorkingOrNot"] != null)
                    {
                        request.WorkingOrNot = Convert.ToBoolean(requestItem["WorkingOrNot"]);


                        if (Convert.ToBoolean(requestItem["WorkingOrNot"]) == true)
                        {
                            // set drop of occupation and entity working for

                            if (requestItem["EntityWorkingFor"] != null)
                            {

                                request.EntityWorkingFor = new Entities.EntityWorkingFor()
                                {
                                    SelectedID = new SPFieldLookupValue(requestItem["EntityWorkingFor"].ToString()).LookupId.ToString(),
                                    SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["EntityWorkingFor"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["EntityWorkingForAr"].ToString()).LookupValue
                                };
                            }
                            else if (requestItem["OtherEntityWorkingFor"] != null)
                            {

                                request.OtherEntityWorkingFor = Convert.ToString(requestItem["OtherEntityWorkingFor"]);
                            }

                            //if (requestItem["Occupation"] != null)
                            //{ 
                            //    request.Occupation = Convert.ToString(requestItem["Occupation"]);
                            //} 
                            //Ahmed
                            if (requestItem["EntityNeedsEquivalency"] != null)
                            {

                                request.EntityNeedsEquivalency = new Entities.EntityNeedsEquivalency()
                                {
                                    SelectedID = new SPFieldLookupValue(requestItem["EntityNeedsEquivalency"].ToString()).LookupId.ToString(),
                                    SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["EntityNeedsEquivalency"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["EntityNeedsEquivalencyAr"].ToString()).LookupValue
                                };
                            }
                            else if (requestItem["OtherEntityNeedsEquivalency"] != null)
                            {

                                request.OtherEntityNeedsEquivalency = Convert.ToString(requestItem["OtherEntityNeedsEquivalency"]);
                            }

                        }
                        else
                        {
                            if (requestItem["EntityNeedsEquivalency"] != null)
                            {

                                request.EntityNeedsEquivalency = new Entities.EntityNeedsEquivalency()
                                {
                                    SelectedID = new SPFieldLookupValue(requestItem["EntityNeedsEquivalency"].ToString()).LookupId.ToString(),
                                    SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["EntityNeedsEquivalency"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["EntityNeedsEquivalencyAr"].ToString()).LookupValue
                                };
                            }
                            else if (requestItem["OtherEntityNeedsEquivalency"] != null)
                            {

                                request.OtherEntityNeedsEquivalency = Convert.ToString(requestItem["OtherEntityNeedsEquivalency"]);
                            }

                        }
                    }
                    if (requestItem["IncomingNumber"] != null)
                    {
                        request.IncomingNumber = int.Parse(Convert.ToString(requestItem["IncomingNumber"]));
                    }
                    if (requestItem["BookDate"] != null)
                    {
                        request.BookDate = Convert.ToDateTime(requestItem["BookDate"]);
                    }
                    if (requestItem["BarCode"] != null)
                    {
                        request.BarCode = Convert.ToString(requestItem["BarCode"]);
                    }
                    if (requestItem["LoginName"] != null)
                    {
                        request.LoginName = Convert.ToString(requestItem["LoginName"]);
                    }
                    if (requestItem["Year"] != null)
                    {
                        request.Year = int.Parse(Convert.ToString(requestItem["Year"]));
                    }
                    if (requestItem["UniversityList"] != null)
                    {

                        request.UniversityList = (LCID == (int)Language.English) ? Convert.ToString(requestItem["UniversityList"]).Split(',')[0] : Convert.ToString(requestItem["UniversityList"]).Split(',')[1];
                    }
                    if (requestItem["HavePAOrNot"] != null)
                    {
                        request.HavePAOrNot = Convert.ToBoolean(requestItem["HavePAOrNot"]);

                    }
                    if (requestItem["UniversityType"] != null)
                    {
                        request.UniversityType = Convert.ToString(requestItem["UniversityType"]);
                    }
                    if (requestItem["UniversityType"] != null)
                    {
                        request.UniversityType = Convert.ToString(requestItem["UniversityType"]);
                    }

                    if (requestItem["HavePAOrNot"] != null)
                    {
                        request.HavePAOrNot = Convert.ToBoolean(requestItem["HavePAOrNot"]);

                    }

                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Request.GetRequestByNumber");
            }
            return request;
        }
        public static Entities.Request GetUniversityRequestData(int requestId, int LCID)
        {
            Logging.GetInstance().Debug("Entering method Request.GetUniversityRequestData");
            Entities.Request request = null;
            try
            {

                SPListItem requestItem = GetRequestItemByID(requestId);
                if (requestItem != null)
                {
                    request = new Entities.Request();
                    request.ID = requestItem.ID;
                    request.RequestNumber = Convert.ToString(requestItem["RequestNumber"]);
                    if (Convert.ToDateTime(requestItem["Created"]) != DateTime.MinValue)
                    {
                        request.RequestCreationDate = Convert.ToDateTime(requestItem["Created"]);
                    }

                    if (requestItem["Year"] != null)
                    {
                        request.Year = int.Parse(Convert.ToString(requestItem["Year"]));
                    }
                    if (requestItem["CountryOfStudy"] != null)
                    {
                        request.CountryOfStudy = new CountryOfStudy()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["CountryOfStudy"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["CountryOfStudy"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["CountryOfStudyAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["OtherCountryOfStudy"] != null)
                    {
                        request.CountryOfStudy = new CountryOfStudy()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = requestItem["OtherCountryOfStudy"].ToString()
                        };
                    }

                    if (requestItem["UniversityMainHeadquarter"] != null)
                    {
                        request.UniversityMainHeadquarter = new Entities.UniversityMainCountry()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["UniversityMainHeadquarter"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["UniversityMainHeadquarter"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["UniversityMainHeadquarterAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["NewUniversityHeadquarter"] != null)
                    {
                        request.UniversityMainHeadquarter = new Entities.UniversityMainCountry()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = requestItem["NewUniversityHeadquarter"].ToString()
                        };
                    }
                    if (requestItem["University"] != null)
                    {

                        request.University = new University()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["University"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["University"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["UniversityAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["UniversityNotFoundInList"] != null)
                    {
                        request.University = new University()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = requestItem["UniversityNotFoundInList"].ToString()
                        };
                        request.UniversityNotFoundInList = Convert.ToString(requestItem["UniversityNotFoundInList"]);
                    }
                    //else if (requestItem["UniversityNotFoundInList"] != null)
                    //{

                    //    request.UniversityNotFoundInList = Convert.ToString(requestItem["UniversityNotFoundInList"]);
                    //}



                    if (requestItem["ApplicantID"] != null)
                    {
                        request.ApplicantID = new Applicants()
                        {
                            ID = new SPFieldLookupValue(requestItem["ApplicantID"].ToString()).LookupId
                        };
                    }

                    if (requestItem["AcademicDegree"] != null)
                    {
                        request.AcademicDegree = new Entities.AcademicDegree()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["AcademicDegree"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["AcademicDegree"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["AcademicDegreeAr"].ToString()).LookupValue,
                            ArabicTitle = new SPFieldLookupValue(requestItem["AcademicDegreeAr"].ToString()).LookupValue,
                            EnglishTitle = new SPFieldLookupValue(requestItem["AcademicDegree"].ToString()).LookupValue
                        };
                    }
                    request.UniversityList = Convert.ToString(requestItem["UniversityList"]);
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit Request.GetUniversityRequestData");
            }
            return request;
        }

        public static void DeleteRequest(int requestId)
        {
            Logging.GetInstance().Debug("Entering Request.DeleteRequest");
            SPWeb web = null;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPQuery requestQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" +
                            requestId + "</Value></Eq></Where>");

                            SPList requestList = web.Lists[Utilities.Constants.Requests];
                            SPListItemCollection requestCollection = requestList.GetItems(requestQuery);
                            web.AllowUnsafeUpdates = true;
                            if (requestCollection != null && requestCollection.Count > 0)
                            {
                                requestList.Items.DeleteItemById(requestCollection[0].ID);

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
                web.AllowUnsafeUpdates = false;
                Logging.GetInstance().Debug("Exit Request.DeleteRequest");
            }

        }
        public static int AddCheckUniversityDetails(Entities.Request request)
        {
            Logging.GetInstance().Debug("Entering method Request.AddCheckUniversityDetails");
            SPWeb web = null;
            int requestId = 0;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPList requestsList = web.Lists[Utilities.Constants.Requests];
                            if (requestsList == null)
                                throw new Exception();
                            SPListItem requestItem = null;
                            web.AllowUnsafeUpdates = true;
                            if (request.ID > 0)
                            {
                                requestItem = GetRequestItemByID(request.ID);
                            }
                            else
                            {
                                requestItem = requestsList.AddItem();
                            }

                            if (request.CountryOfStudy != null && !string.IsNullOrEmpty(request.CountryOfStudy.SelectedID))
                            {
                                requestItem["CountryOfStudy"] = new SPFieldLookupValue(int.Parse(request.CountryOfStudy.SelectedID), request.CountryOfStudy.SelectedTitle);
                                requestItem["UniversityMainHeadquarter"] = new SPFieldLookupValue(int.Parse(request.CountryOfStudy.SelectedID), request.CountryOfStudy.SelectedTitle);
                                requestItem["OtherCountryOfStudy"] = string.Empty;
                            }
                            else
                            {
                                requestItem["CountryOfStudy"] = string.Empty;
                                requestItem["OtherCountryOfStudy"] = request.CountryOfStudy.SelectedTitle;
                                requestItem["NewUniversityHeadquarter"] = request.CountryOfStudy.SelectedTitle;
                            }

                            if (request.University != null && !string.IsNullOrEmpty(request.University.SelectedID))
                            {
                                requestItem["University"] = new SPFieldLookupValue(int.Parse(request.University.SelectedID), request.University.SelectedTitle);
                                if (!string.IsNullOrEmpty(request.UniversityList))
                                {
                                    requestItem["UniversityList"] = request.UniversityList;
                                }
                                requestItem["UniversityNotFoundInList"] = string.Empty;
                                requestItem["UniversityNotFoundInList"] = string.Empty;
                            }
                            else
                            {
                                requestItem["University"] = string.Empty;
                                requestItem["UniversityList"] = request.UniversityList;
                                requestItem["UniversityNotFoundInList"] = request.University.SelectedTitle;
                            }
                            requestItem["Year"] = request.Year;
                            requestItem["ApplicantID"] = new SPFieldLookupValue(request.ApplicantID.ID, request.ApplicantID.ID.ToString());
                            requestItem["LoginName"] = request.LoginName;

                            requestItem.Update();
                            requestId = requestItem.ID;
                            requestItem["RequestNumber"] = generateRequestNumber(requestItem.ID);
                            requestItem.Update();
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
                web.AllowUnsafeUpdates = false;
                Logging.GetInstance().Debug("Exit Request.AddCheckUniversityDetails");
            }
            return requestId;

        }
        public static List<object> GetRequestTypesCount(int LCID, string SPGroupName)
        {
            List<object> objColumns = new List<object>();
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method Request.GetRequestTypesCount");
                        web.AllowUnsafeUpdates = true;
                        int delayedDays = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "PADelayedDays"));

                        List<SimilarRequest> LateRequests = BL.LateRequests.GetAllLateRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.LateRequests.GetLateQueryPerRole(), "And", true)
                            + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();
                        LateRequests = LateRequests.Where(x => x.AssignedTo.ToLower().Contains(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == SPGroupName.ToLower()).ToList();
                        List<SimilarRequest> NewRequests = BL.NewRequests.GetAllNewRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.NewRequests.GetNewQueryPerRole(SPGroupName), "Or", true)
                            + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();
                        NewRequests = NewRequests.Where(x => (x.AssignedTo.ToLower().Contains(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == SPGroupName.ToLower()) && (string.IsNullOrEmpty(x.DelayedDays) || int.Parse(x.DelayedDays) < delayedDays)).ToList();
                        List<SimilarRequest> RejectedRequests = BL.ReturnedRequests.GetAllReturnedRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.ReturnedRequests.GetReturnedQueryPerRole(SPGroupName), "Or", true)
                            + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();
                        RejectedRequests = RejectedRequests.Where(x => (x.AssignedTo.ToLower().Contains(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == SPGroupName.ToLower()) && (string.IsNullOrEmpty(x.DelayedDays) || int.Parse(x.DelayedDays) < delayedDays)).ToList();

                        List<ClarificationReqs> ClarRequests = BL.ClarificationRequests.GetAllClarificationRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.ClarificationRequests.GetClarQueryPerRole(SPGroupName), "Or", true)
                           + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();
                        ClarRequests = ClarRequests.Where(x => x.AssignedTo.ToLower().Contains(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == SPGroupName.ToLower()).ToList();


                        if (LateRequests.Count > 0)
                            objColumns.Add(new Common.Entities.RequestsChart()
                            {
                                RequestCount = LateRequests.Count,
                                RequestType = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "LateRequests", (uint)LCID)
                            });
                        if (NewRequests.Count > 0)
                            objColumns.Add(new Common.Entities.RequestsChart()
                            {
                                RequestCount = NewRequests.Count,
                                RequestType = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "NewRequests", (uint)LCID)
                            });

                        if (RejectedRequests.Count > 0)
                            objColumns.Add(new Common.Entities.RequestsChart()
                            {
                                RequestCount = RejectedRequests.Count,
                                RequestType = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ReturnedRequests", (uint)LCID)
                            });

                        if (ClarRequests.Count > 0)
                            objColumns.Add(new Common.Entities.RequestsChart()
                            {
                                RequestCount = ClarRequests.Count,
                                RequestType = HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", "ClarificationRequests", (uint)LCID)
                            });
                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(ex);
                    }
                    finally
                    {
                        web.AllowUnsafeUpdates = false;
                        Logging.GetInstance().Debug("Exiting method Request.GetRequestTypesCount");
                    }
                    return objColumns;
                }
            }
        }


        public static List<Entities.SimilarRequest> GetRequestsListing(SPQuery query, int LCID)
        {
            List<Entities.SimilarRequest> Requests = new List<Entities.SimilarRequest>();
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method Request.GetRequestsListing");
                        SPList customerList = web.Lists[Utilities.Constants.Requests];
                        SPListItemCollection items = customerList.GetItems(query);
                        Common.Entities.RequestStatus requestStatusItem = new Common.Entities.RequestStatus();
                        foreach (SPListItem item in items)
                        {
                            if (string.IsNullOrEmpty(Convert.ToString(item["RequestStatusId"])))
                            {
                                Logging.GetInstance().LogException(new Exception("Item Status is Null or empty"));
                                continue;
                            }
                            requestStatusItem = Common.BL.RequestStatus.GetRequestStatusById(new SPFieldLookupValue(item["RequestStatusId"].ToString()).LookupId);
                            SimilarRequest Request = new SimilarRequest();
                            SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue((item["Applicants_QatarID"] != null) ? item["Applicants_QatarID"].ToString() : string.Empty);
                            Request.QatariID = ApplicantsQatarID.LookupValue;

                            Request.DelayedDays = (item["DelayedDays"] != null) ? item["DelayedDays"].ToString() : string.Empty;
                            Request.AssignedTo = (item["EmployeeAssignedTo"] != null) ? item["EmployeeAssignedTo"].ToString() : string.Empty;
                            Request.ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;
                            Request.RequestNumber = (item["RequestNumber"] != null) ? item["RequestNumber"].ToString() : string.Empty;

                            if (requestStatusItem.Code == Common.Utilities.RequestStatus.UCProgramEmployeeClarificationReply.ToString())
                            {

                                if (LCID == (int)Language.English)
                                {
                                    SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                                    Request.RejectedFrom = ApplicantsEnglishName.LookupValue;
                                }
                                else
                                {
                                    SPFieldLookupValue ApplicantsArabicName = new SPFieldLookupValue((item["Applicants_ArabicName"] != null) ? item["Applicants_ArabicName"].ToString() : string.Empty);
                                    Request.RejectedFrom = ApplicantsArabicName.LookupValue;
                                }
                            }
                            else
                            {
                                Request.RejectedFrom = (item["RejectedFrom"] != null) ? item["RejectedFrom"].ToString() : string.Empty;
                            }

                            Request.RejectionReason = (item["RejectionReason"] != null) ? item["RejectionReason"].ToString() : string.Empty;
                            Request.RejectionDate = (item["RejectionDate"] != null) ? DateTime.Parse(item["RejectionDate"].ToString()) : DateTime.MinValue;
                            Request.SubmitDate = (item["SubmitDate"] != null) ? DateTime.Parse(item["SubmitDate"].ToString()) : DateTime.MinValue;
                            SPFieldLookupValue StatusId = new SPFieldLookupValue((item["RequestStatusId"] != null) ? item["RequestStatusId"].ToString() : string.Empty);
                            Request.RequestStatusId = StatusId.LookupId;
                            if ((Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEClosedByAcceptance) || (Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEClosedByRejection))
                                Request.IsRequestClosed = true;
                            else
                                Request.IsRequestClosed = false;


                            if (item["Note"] != null)
                            {

                                Request.Note = Convert.ToString(item["Note"]);
                            }
                            else
                            {
                                Request.Note = string.Empty;
                            }

                            if (LCID == (int)Language.English)
                            {
                                SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                                Request.ApplicantName = ApplicantsEnglishName.LookupValue;
                                SPFieldLookupValue NationalityTitle = new SPFieldLookupValue((item["Nationality_Title"] != null) ? item["Nationality_Title"].ToString() : string.Empty);
                                Request.Nationality = NationalityTitle.LookupValue;
                                SPFieldLookupValue RequestStatus = new SPFieldLookupValue((item["RequestStatus"] != null) ? item["RequestStatus"].ToString() : string.Empty);
                                Request.RequestStatus = RequestStatus.LookupValue;
                                SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue((item["CountryOfStudy"] != null) ? item["CountryOfStudy"].ToString() : string.Empty);
                                Request.Country = CountryOfStudy.LookupValue;
                                SPFieldLookupValue University = new SPFieldLookupValue((item["University"] != null) ? item["University"].ToString() : string.Empty);

                                if (University.LookupValue != null)
                                {
                                    Request.University = University.LookupValue;
                                    Request.UniversityId = University.LookupId;
                                }

                                else
                                {
                                    Request.University = (item["UniversityNotFoundInList"] != null) ? item["UniversityNotFoundInList"].ToString() : string.Empty;
                                    Request.UniversityId = 0;
                                }
                                SPFieldLookupValue Specialization = new SPFieldLookupValue((item["Specialization"] != null) ? item["Specialization"].ToString() : string.Empty);
                                Request.Specialization = Specialization.LookupValue;

                                SPFieldLookupValue EntityNeedsEquivalency = new SPFieldLookupValue((item["EntityNeedsEquivalency"] != null) ? item["EntityNeedsEquivalency"].ToString() : string.Empty);

                                if (EntityNeedsEquivalency.LookupValue != null)
                                    Request.EntityNeedsEquivalency = EntityNeedsEquivalency.LookupValue;
                                else
                                    Request.EntityNeedsEquivalency = (item["OtherEntityNeedsEquivalency"] != null) ? item["OtherEntityNeedsEquivalency"].ToString() : string.Empty;


                                SPFieldLookupValue AcademicDegree = new SPFieldLookupValue((item["AcademicDegreeForEquivalence"] != null) ? item["AcademicDegreeForEquivalence"].ToString() : string.Empty);
                                Request.AcademicDegree = AcademicDegree.LookupValue;
                                Request.RequestStatus = requestStatusItem.ReviewerDescriptionEn;
                                Request.Faculty = (item["Faculty"] != null) ? item["Faculty"].ToString() : string.Empty;
                            }
                            else
                            {
                                SPFieldLookupValue ApplicantsArabicName = new SPFieldLookupValue((item["Applicants_ArabicName"] != null) ? item["Applicants_ArabicName"].ToString() : string.Empty);
                                Request.ApplicantName = ApplicantsArabicName.LookupValue;
                                SPFieldLookupValue NationalityTitleAr = new SPFieldLookupValue((item["Nationality_TitleAr"] != null) ? item["Nationality_TitleAr"].ToString() : string.Empty);
                                Request.Nationality = NationalityTitleAr.LookupValue;
                                SPFieldLookupValue RequestStatus = new SPFieldLookupValue((item["RequestStatusAr"] != null) ? item["RequestStatusAr"].ToString() : string.Empty);
                                Request.RequestStatus = RequestStatus.LookupValue;
                                SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue((item["CountryOfStudyAr"] != null) ? item["CountryOfStudyAr"].ToString() : string.Empty);
                                Request.Country = CountryOfStudy.LookupValue;
                                SPFieldLookupValue University = new SPFieldLookupValue((item["UniversityAr"] != null) ? item["UniversityAr"].ToString() : string.Empty);
                                if (University.LookupValue != null)
                                {
                                    Request.University = University.LookupValue;
                                    Request.UniversityId = University.LookupId;
                                }

                                else
                                {
                                    Request.University = (item["UniversityNotFoundInList"] != null) ? item["UniversityNotFoundInList"].ToString() : string.Empty;
                                    Request.UniversityId = 0;
                                }

                                SPFieldLookupValue Specialization = new SPFieldLookupValue((item["SpecializationAr"] != null) ? item["SpecializationAr"].ToString() : string.Empty);
                                Request.Specialization = Specialization.LookupValue;
                                SPFieldLookupValue EntityNeedsEquivalency = new SPFieldLookupValue((item["EntityNeedsEquivalencyAr"] != null) ? item["EntityNeedsEquivalencyAr"].ToString() : string.Empty);
                                //Request.EntityNeedsEquivalency = EntityNeedsEquivalency.LookupValue;
                                if (EntityNeedsEquivalency.LookupValue != null)
                                    Request.EntityNeedsEquivalency = EntityNeedsEquivalency.LookupValue;
                                else
                                    Request.EntityNeedsEquivalency = (item["OtherEntityNeedsEquivalency"] != null) ? item["OtherEntityNeedsEquivalency"].ToString() : string.Empty;


                                SPFieldLookupValue AcademicDegree = new SPFieldLookupValue((item["AcademicDegreeForEquivalenceAr"] != null) ? item["AcademicDegreeForEquivalenceAr"].ToString() : string.Empty);
                                Request.AcademicDegree = AcademicDegree.LookupValue;
                                Request.RequestStatus = requestStatusItem.ReviewerDescriptionAr;
                                Request.Faculty = (item["FacultyAr"] != null) ? item["FacultyAr"].ToString() : string.Empty;
                            }
                            Requests.Add(Request);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(ex);
                    }
                    finally
                    {
                        web.AllowUnsafeUpdates = false;
                        Logging.GetInstance().Debug("Exit Request.GetRequestsListing");
                    }
                    return Requests;
                }
            }
        }

        public static List<Entities.SimilarRequest> GetRequestsListingCount(SPQuery query)
        {
            List<Entities.SimilarRequest> Requests = new List<Entities.SimilarRequest>();
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method Request.GetRequestsListing");
                        SPList customerList = web.Lists[Utilities.Constants.Requests];
                        SPListItemCollection items = customerList.GetItems(query);
                        Common.Entities.RequestStatus requestStatusItem = new Common.Entities.RequestStatus();
                        foreach (SPListItem item in items)
                        {
                            if (string.IsNullOrEmpty(Convert.ToString(item["RequestStatusId"])))
                            {
                                Logging.GetInstance().LogException(new Exception("Item Status is Null or empty"));
                                continue;
                            }
                            //requestStatusItem = Common.BL.RequestStatus.GetRequestStatusById(new SPFieldLookupValue(item["RequestStatusId"].ToString()).LookupId);
                            SimilarRequest Request = new SimilarRequest();

                            Request.DelayedDays = (item["DelayedDays"] != null) ? item["DelayedDays"].ToString() : string.Empty;
                            Request.AssignedTo = (item["EmployeeAssignedTo"] != null) ? item["EmployeeAssignedTo"].ToString() : string.Empty;
                            Request.ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;
                            Request.RequestNumber = (item["RequestNumber"] != null) ? item["RequestNumber"].ToString() : string.Empty;
                            SPFieldLookupValue StatusId = new SPFieldLookupValue((item["RequestStatusId"] != null) ? item["RequestStatusId"].ToString() : string.Empty);
                            Request.RequestStatusId = StatusId.LookupId;
                            if ((Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEClosedByAcceptance) || (Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEClosedByRejection))
                                Request.IsRequestClosed = true;
                            else
                                Request.IsRequestClosed = false;
                            if (item["ApplicantID"] != null)
                            {
                                SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue(item["ApplicantID"].ToString());
                                Request.ApplicantName = ApplicantsEnglishName.LookupValue;
                            }
                            Requests.Add(Request);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(ex);
                    }
                    finally
                    {
                        web.AllowUnsafeUpdates = false;
                        Logging.GetInstance().Debug("Exit Request.GetRequestsListing");
                    }
                    return Requests;
                }
            }
        }
        public static int GetRequestsCount(SPQuery query)
        {
            int requestsCount = 0;
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method Request.GetRequestsListing");
                        SPList customerList = web.Lists[Utilities.Constants.Requests];
                        requestsCount = customerList.GetItems(query).Count;

                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(ex);
                    }
                    finally
                    {
                        Logging.GetInstance().Debug("Exit Request.GetRequestsListing");
                    }
                    return requestsCount;
                }
            }
        }

        private static string generateRequestNumber(long ID)
        {
            Logging.GetInstance().Debug("Entering method Request.generateRequestNumber");
            string requestNumber = string.Empty;
            try
            {
                string RequestNoPrefix = HelperMethods.GetConfigurationValue(SPContext.Current.Site.RootWeb.Url, Utilities.Constants.ConfigurationList, Utilities.Constants.RequestNoPrefix);
                string PaddingTotalRequest = HelperMethods.GetConfigurationValue(SPContext.Current.Site.RootWeb.Url, Utilities.Constants.ConfigurationList, Utilities.Constants.PaddingTotalRequest);
                requestNumber = RequestNoPrefix + ID.ToString(PaddingTotalRequest);
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method Request.generateRequestNumber");
            }
            return requestNumber;
        }

        public static void UpdateNeedsClarificationRequestStatus(int requestID)
        {
            Logging.GetInstance().Debug("Entering method Request.UpdateNeedsClarificationRequestStatus");

            SPWeb web = null;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;
                            SPList requestsList = web.Lists[Utilities.Constants.Requests];
                            SPListItem item = requestsList.GetItemById(requestID);
                            if (item != null)
                            {
                                if (item["RequestStatus"] != null)
                                {
                                    SPFieldLookupValue statusValue = new SPFieldLookupValue(item["RequestStatus"].ToString());
                                    if (statusValue.LookupId == (int)Common.Utilities.RequestStatus.UCEDraftForClarification)
                                    {
                                        item["RequestStatus"] = new SPFieldLookupValue((int)Common.Utilities.RequestStatus.UCProgramEmployeeClarificationReply, ((int)Common.Utilities.RequestStatus.UCProgramEmployeeClarificationReply).ToString());


                                        item.Update();

                                    }
                                }
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
                web.AllowUnsafeUpdates = false;
                Logging.GetInstance().Debug("Exiting method Request.UpdateNeedsClarificationRequestStatus");
            }

        }
        public static bool UpdateRejection(Entities.Request request)
        {
            try
            {
                SPWeb web = null;
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {

                            SPList requestsList = web.Lists[Utilities.Constants.Requests];

                            SPListItem listItem = requestsList.Items.GetItemById(request.ID);
                            if (listItem != null)
                            {
                                web.AllowUnsafeUpdates = true;
                                listItem["RejectedFrom"] = request.RejectedFrom;
                                listItem["RejectionReason"] = request.RejectionReason;
                                listItem["RejectionDate"] = request.RejectionDate;

                                listItem.Update();
                                web.AllowUnsafeUpdates = false;
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
                Logging.GetInstance().Debug("Exit Request.UpdateRejection");
            }
            return true;
        }

        public static void notifyApplicant(int applicantID, string universityID, string mailBody, string smsBody, string mailSubject)
        {
            try
            {
                Logging.GetInstance().Debug("Entering method Add_FinalDecision.notifyUser");
                EntitiesCommon.Applicants sessionApplicant = BLCommon.Applicants.GetApplicantByID(applicantID, (int)SPContext.Current.Web.Language);
                //send email
                string SMTPServer = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + UtilitiesCommon.Constants.HEWebUrl, UtilitiesCommon.Constants.Configuration, "SMTPServer");
                string SMTPServerPort = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + UtilitiesCommon.Constants.HEWebUrl, UtilitiesCommon.Constants.Configuration, "SMTPServerPort");
                string SMTPFromAddress = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + UtilitiesCommon.Constants.HEWebUrl, UtilitiesCommon.Constants.Configuration, "SMTPFromAddress");
                string SMTPFromDisplayName = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + UtilitiesCommon.Constants.HEWebUrl, UtilitiesCommon.Constants.Configuration, "SMTPFromDisplayName");
                string SMTPUserName = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + UtilitiesCommon.Constants.HEWebUrl, UtilitiesCommon.Constants.Configuration, "SMTPUserName");
                string SMTPPassword = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + UtilitiesCommon.Constants.HEWebUrl, UtilitiesCommon.Constants.Configuration, "SMTPPassword");


                if (!(string.IsNullOrEmpty(mailBody) || string.IsNullOrEmpty(mailSubject)))
                {
                    HelperMethods.SendNotificationEmail(mailBody, mailSubject, SMTPFromAddress, SMTPFromDisplayName, sessionApplicant.ApplicantEmail, SMTPServer, SMTPServerPort, SMTPUserName, SMTPPassword, true, new List<System.Net.Mail.Attachment>());
                }
                //Send SMS here

                string smsEnabledConfig = HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + UtilitiesCommon.Constants.HEWebUrl, UtilitiesCommon.Constants.Configuration, "SMSUCEEnabled");

                var SMSUCEEnabled = false;

                if (!string.IsNullOrEmpty(smsEnabledConfig))
                {
                    bool.TryParse(smsEnabledConfig, out SMSUCEEnabled);
                }

                if (SMSUCEEnabled)
                {

                    if (!string.IsNullOrEmpty(smsBody))
                    {
                        Texting.SendSMS(sessionApplicant.MobileNumber, smsBody);
                    }
                    if (!string.IsNullOrEmpty(universityID) && universityID != "0")
                    {
                        bool IsUniversityHEDD = BLCommon.University.IsUniversityHEDD(int.Parse(universityID));
                        if (IsUniversityHEDD)
                        {
                            EntitiesCommon.Notifications smsNotificationsHEDD = BLCommon.Notifications.GetSubmittedNotification((int)UtilitiesCommon.NotificationType.SMS, 0);
                            if (smsNotificationsHEDD != null)
                            {
                                //Send SMS here
                                Texting.SendSMS(sessionApplicant.MobileNumber, string.Format(smsBody, sessionApplicant.ApplicantName, sessionApplicant.ApplicantName));
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
                Logging.GetInstance().Debug("Exiting method AssistSecretaryProceduresWPUserControl.notifyUser");
            }
        }

        public static void UpdateRequestNote(int requestId, string Note)
        {
            Logging.GetInstance().Debug("Entering method Request.UpdateRequestNote");
            SPWeb web = null;
            bool updated = false;
            try
            {


                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPList requestsList = web.Lists[Utilities.Constants.Requests];
                            if (requestsList == null)
                                throw new Exception("Requests list is null");
                            web.AllowUnsafeUpdates = true;
                            SPListItem item = null;
                            if (requestId > 0)
                            {
                                item = GetRequestItemByID(requestId);
                                item["Note"] = Note;
                                item.Update();
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
                Logging.GetInstance().Debug("Exit method Request.UpdateRequestNote");

            }
        }

    }
}
