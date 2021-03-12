using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHEWF.PA.Entities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using BLCommon = ITWORX.MOEHEWF.Common.BL;
using EntitiesCommon = ITWORX.MOEHEWF.Common.Entities;
using ITWORX.MOEHE.Integration.SMS;
using UtilitiesCommon = ITWORX.MOEHEWF.Common.Utilities;


namespace ITWORX.MOEHEWF.PA.BL
{
    public class Request
    {
        public static object ClarRequests { get; private set; }

        public static bool AddUpdateRequest(Entities.PARequest request, string WizardStepActive, int requestId, int requestStatusVal)
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

                            SPList requestsList = web.Lists[Utilities.Constants.PARequests];
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

                            if (request.ProgramType != null && !string.IsNullOrEmpty(request.ProgramType.SelectedID))
                            {
                                item["ProgramType"] = new SPFieldLookupValue(int.Parse(request.ProgramType.SelectedID), request.ProgramType.SelectedTitle);
                            }
                            if (request.ProgramCountry != null && !string.IsNullOrEmpty(request.ProgramCountry.SelectedID))
                            {
                                item["ProgramCountry"] = new SPFieldLookupValue(int.Parse(request.ProgramCountry.SelectedID), request.ProgramCountry.SelectedTitle);
                            }
                            if (request.ProgramUniversity != null && !string.IsNullOrEmpty(request.ProgramUniversity.SelectedID))
                            {
                                item["ProgramUniversity"] = new SPFieldLookupValue(int.Parse(request.ProgramUniversity.SelectedID), request.ProgramUniversity.SelectedTitle);
                            }
                            if (!string.IsNullOrEmpty(request.ProgramFaculty))
                            {
                                item["ProgramFaculty"] = request.ProgramFaculty;
                            }
                            if (request.ProgramStudySystem != null && !string.IsNullOrEmpty(request.ProgramStudySystem.SelectedID))
                            {
                                item["StudySystem"] = new SPFieldLookupValue(int.Parse(request.ProgramStudySystem.SelectedID), request.ProgramStudySystem.SelectedTitle);
                            }
                            if (request.ProgramStudyType != null && !string.IsNullOrEmpty(request.ProgramStudyType.SelectedID))
                            {
                                item["StudyType"] = new SPFieldLookupValue(int.Parse(request.ProgramStudyType.SelectedID), request.ProgramStudyType.SelectedTitle);
                            }
                            if (request.ProgramPeriod != null && !string.IsNullOrEmpty(request.ProgramPeriod))
                            {
                                item["ActualStudingPeriod"] = request.ProgramPeriod;
                            }
                            item["AcademicNumberOfYears"] = request.AcademicNumberOfYears;

                            if (!string.IsNullOrEmpty(request.CertificateDate))
                                item["CertificateDate"] = request.CertificateDate;

                            item["SchoolName"] = request.School;

                            if (request.HighestCertificate != null && !string.IsNullOrEmpty(request.HighestCertificate.SelectedID))
                            {
                                item["AcademicDegreeForEquivalence"] = new SPFieldLookupValue(int.Parse(request.HighestCertificate.SelectedID), request.HighestCertificate.SelectedTitle);
                            }

                            //this should be changed when changed when cascading
                            if (request.CountryOfStudy != null && !string.IsNullOrEmpty(request.CountryOfStudy.SelectedID))
                            {
                                item["CountryOfStudy"] = new SPFieldLookupValue(int.Parse(request.CountryOfStudy.SelectedID), request.CountryOfStudy.SelectedTitle);

                            }

                            if (!string.IsNullOrEmpty(request.UniversityNotFoundInList) || request.University != null)
                            {
                                if (!string.IsNullOrEmpty(request.UniversityNotFoundInList))
                                {
                                    item["UniversityNotFoundInList"] = SPHttpUtility.HtmlEncode(request.UniversityNotFoundInList);
                                }
                                else
                                {
                                    item["UniversityNotFoundInList"] = string.Empty;
                                }
                                if (request.University != null && !string.IsNullOrEmpty(request.University.SelectedID))
                                {
                                    item["University"] = new SPFieldLookupValue(int.Parse(request.University.SelectedID), request.University.SelectedTitle);
                                }
                                else
                                {

                                    item["University"] = string.Empty;
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

                            if (!string.IsNullOrEmpty(request.ProgramSpecializationNotFoundInList) || request.ProgramSpecialization != null)
                            {
                                if (!string.IsNullOrEmpty(request.ProgramSpecializationNotFoundInList))
                                {
                                    item["ProgramSpecializationNotFoundInList"] = SPHttpUtility.HtmlEncode(request.ProgramSpecializationNotFoundInList);
                                }
                                else
                                {
                                    item["ProgramSpecializationNotFoundInList"] = string.Empty;
                                }
                                if (request.ProgramSpecialization != null && !string.IsNullOrEmpty(request.ProgramSpecialization.SelectedID))
                                {
                                    item["ProgramSpecialization"] = new SPFieldLookupValue(int.Parse(request.ProgramSpecialization.SelectedID), request.ProgramSpecialization.SelectedTitle);
                                }
                                else
                                {

                                    item["ProgramSpecialization"] = string.Empty;
                                }

                            }
                            item["JoinedOtherUniversities"] = request.JoinedOtherUniversities;
                            if (WizardStepActive != string.Empty)
                                item["WizardActiveStep"] = WizardStepActive;
                            item["WorkingOrNot"] = request.WorkingOrNot;
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

                                item["Occupation"] = SPHttpUtility.HtmlEncode(request.Occupation);

                                if (request.HiringDate > DateTime.MinValue)
                                {
                                    item["HiringDate"] = request.HiringDate;
                                }

                                item["OccupationPhone"] = SPHttpUtility.HtmlEncode(request.OccupationPhone);

                            }
                            else
                            {
                                item["EntityWorkingFor"] = string.Empty;
                                item["OtherEntityWorkingFor"] = string.Empty;
                                item["Occupation"] = string.Empty;
                                item["OtherOccupation"] = string.Empty;
                                item["HiringDate"] = null;
                                item["OccupationPhone"] = string.Empty;


                            }

                            item["LoginName"] = request.LoginName;
                            item["RequestType"] = new SPFieldLookupValue((int)RequestType.PriorApproval, ((int)RequestType.PriorApproval).ToString()); // "Certificate Equivalency"/* Constants.RequestStatus.RequestStatus.PASubmitted*/);
                            //item["ApplicantID"] = new SPFieldLookupValue(request.ApplicantID.ID, request.ApplicantID.ID.ToString());

                            Common.Entities.RequestStatus requestStatus = new Common.Entities.RequestStatus();// GetRequestStatusById(int requestStatusId)
                            if (requestStatusVal == (int)Common.Utilities.RequestStatus.PASubmitted)
                            {
                                if (request.SubmitDate != DateTime.MinValue)
                                    item["SubmitDate"] = request.SubmitDate;

                                if (request.ActionDate != DateTime.MinValue)
                                    item["ActionDate"] = request.ActionDate;

                                //item["EmployeeAssignedTo"] = "Receptionists";
                                item["RequestStatus"] = new SPFieldLookupValue((int)Common.Utilities.RequestStatus.PASubmitted, ((int)Common.Utilities.RequestStatus.PASubmitted).ToString());
                                requestStatus = Common.BL.RequestStatus.GetRequestStatusById((int)Common.Utilities.RequestStatus.PASubmitted);
                                if (!string.IsNullOrEmpty(requestStatus.FinalDecisionEn) || !string.IsNullOrEmpty(requestStatus.FinalDecisionAr))
                                {
                                    item["FinalDecision"] = requestStatus.FinalDecisionEn;
                                    item["FinalDecisionAr"] = requestStatus.FinalDecisionAr;
                                }
                            }
                            else if (requestStatusVal == (int)Common.Utilities.RequestStatus.PADraft)
                            {

                                item["RequestStatus"] = new SPFieldLookupValue((int)Common.Utilities.RequestStatus.PADraft, ((int)Common.Utilities.RequestStatus.PADraft).ToString());/*Constants.RequestStatus.PADraft*/
                                requestStatus = Common.BL.RequestStatus.GetRequestStatusById((int)Common.Utilities.RequestStatus.PADraft);
                                if (!string.IsNullOrEmpty(requestStatus.FinalDecisionEn) || !string.IsNullOrEmpty(requestStatus.FinalDecisionAr))
                                {
                                    item["FinalDecision"] = requestStatus.FinalDecisionEn;
                                    item["FinalDecisionAr"] = requestStatus.FinalDecisionAr;
                                }
                            }
                            else
                            {

                                item["RequestStatus"] = new SPFieldLookupValue((int)Common.Utilities.RequestStatus.PADraftForClarification, ((int)Common.Utilities.RequestStatus.PADraftForClarification).ToString());/*Constants.RequestStatus.PADraft*/
                                requestStatus = Common.BL.RequestStatus.GetRequestStatusById((int)Common.Utilities.RequestStatus.PADraftForClarification);
                                if (!string.IsNullOrEmpty(requestStatus.FinalDecisionEn) || !string.IsNullOrEmpty(requestStatus.FinalDecisionAr))
                                {
                                    item["FinalDecision"] = requestStatus.FinalDecisionEn;
                                    item["FinalDecisionAr"] = requestStatus.FinalDecisionAr;
                                }
                            }

                            if (request.AcademicStartDate != DateTime.MinValue)
                                item["AcademicStartDate"] = request.AcademicStartDate;

                            if (request.AcademicEndDate != DateTime.MinValue)
                                item["AcademicEndDate"] = request.AcademicEndDate;


                            item.Update();
                            item["RequestNumber"] = generateRequestNumber(item.ID);

                            item.Update();
                            updated = true;
                            if (updated)
                            {
                                request.ID = item.ID;
                            }
                            else
                            {
                                request.ID = 0;
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
                            SPList requestsList = web.Lists[Utilities.Constants.PARequests];
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
                            SPList requestsList = web.Lists[Utilities.Constants.PARequests];
                           
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
                Logging.GetInstance().LogException(new Exception("Request ID: " + requestId + " Exception Message: "));
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Request.GetRequestItemByNumber");
            }
            return item;
        }
        public static Entities.PARequest GetRequestByNumber(int requestId, int LCID)
        {
            Logging.GetInstance().Debug("Entering method Request.GetRequestByNumber");
            Entities.PARequest request = null;
            try
            {

                SPListItem requestItem = GetRequestItemByID(requestId);
                if (requestItem != null)
                {
                    request = new Entities.PARequest();
                    request.ID = requestItem.ID;
                    request.RequestNumber = Convert.ToString(requestItem["RequestNumber"]);
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
                        request.ApplicantID = new Common.Entities.Applicants()
                        {
                            ID = new SPFieldLookupValue(requestItem["ApplicantID"].ToString()).LookupId
                        };

                    }
                    if (requestItem["RequestStatus"] != null)
                    {
                        request.RequestStatus = new Common.Entities.RequestStatus()
                        {

                            Code = new SPFieldLookupValue(requestItem["RequestStatus"].ToString()).LookupId.ToString()
                        };
                    }
                    if (requestItem["AcademicDegreeForEquivalence"] != null)
                    {
                        request.HighestCertificate = new Entities.Certificates()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["AcademicDegreeForEquivalence"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["AcademicDegreeForEquivalence"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["AcademicDegreeForEquivalenceAr"].ToString()).LookupValue
                        };
                    }
                    if (requestItem["CertificateDate"]!=null)
                    {
                        request.CertificateDate = requestItem["CertificateDate"].ToString();
                    }

                    if (requestItem["CountryOfStudy"] != null)
                    {
                        request.CountryOfStudy = new CountryOfStudy()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["CountryOfStudy"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["CountryOfStudy"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["CountryOfStudyAr"].ToString()).LookupValue
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

                        request.UniversityNotFoundInList = Convert.ToString(requestItem["UniversityNotFoundInList"]);
                    }

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

                    if (requestItem["ProgramSpecialization"] != null)
                    {

                        request.ProgramSpecialization = new Entities.Specialization()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["ProgramSpecialization"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["ProgramSpecialization"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["ProgramSpecializationAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["ProgramSpecializationNotFoundInList"] != null)
                    {

                        request.ProgramSpecializationNotFoundInList = Convert.ToString(requestItem["ProgramSpecializationNotFoundInList"]);
                    }

                    if (Convert.ToDateTime(requestItem["AcademicStartDate"]) != DateTime.MinValue)
                    {
                        request.AcademicStartDate = Convert.ToDateTime(requestItem["AcademicStartDate"]);
                    }
                    if (Convert.ToDateTime(requestItem["AcademicEndDate"]) != DateTime.MinValue)
                    {
                        request.AcademicEndDate = Convert.ToDateTime(requestItem["AcademicEndDate"]);
                    }

                    if (requestItem["AcademicNumberOfYears"] != null)
                    { 
                    request.AcademicNumberOfYears = int.Parse(Convert.ToString(requestItem["AcademicNumberOfYears"]));
                    }

                    if (requestItem["ProgramUniversity"] != null)
                    {
                        request.ProgramUniversity = new University()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["ProgramUniversity"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["ProgramUniversity"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["ProgramUniversityAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["OtherPAUniversityOfStudy"] != null)
                    {
                        request.ProgramUniversity = new University()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = requestItem["OtherPAUniversityOfStudy"].ToString()
                        };
                    }
                    if (requestItem["ProgramCountry"] != null)
                    {
                        request.ProgramCountry = new CountryOfStudy()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["ProgramCountry"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["ProgramCountry"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["ProgramCountryAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["OtherPACountryOfStudy"] != null)
                    {
                        request.ProgramCountry = new CountryOfStudy()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = requestItem["OtherPACountryOfStudy"].ToString()
                        };
                    }

                    if (requestItem["ProgramFaculty"] != null)
                    {
                        request.ProgramFaculty = Convert.ToString(requestItem["ProgramFaculty"]);
                    }
                    if (requestItem["SchoolName"] != null)
                    {
                        request.School = Convert.ToString(requestItem["SchoolName"]);
                    }
                    if (requestItem["ProgramType"] != null)
                    {
                        request.ProgramType = new Entities.AcademicDegree()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["ProgramType"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["ProgramType"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["ProgramTypeAr"].ToString()).LookupValue
                        };
                    }
                    if (requestItem["StudySystem"] != null)
                    {
                        request.ProgramStudySystem = new Entities.StudySystem()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["StudySystem"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["StudySystem"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["StudySystemAr"].ToString()).LookupValue
                        };
                    }
                    request.JoinedOtherUniversities = Convert.ToBoolean(requestItem["JoinedOtherUniversities"]);
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

                        if (requestItem["Occupation"] != null)
                        {

                            request.Occupation = Convert.ToString(requestItem["Occupation"]);
                        }
                        else if (requestItem["OtherOccupation"] != null)
                        {

                            request.OtherOccupation = Convert.ToString(requestItem["OtherOccupation"]);
                        }
                        //if (requestItem["Occupation"] != null)
                        //{

                        //    request.Occupation = new Entities.Occupation()
                        //    {
                        //        SelectedID = new SPFieldLookupValue(requestItem["Occupation"].ToString()).LookupId.ToString(),
                        //        SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["Occupation"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["OccupationAr"].ToString()).LookupValue
                        //    };
                        //}
                        //else if (requestItem["OtherOccupation"] != null)
                        //{

                        //    request.OtherOccupation = Convert.ToString(requestItem["OtherOccupation"]);
                        //}

                        if (Convert.ToDateTime(requestItem["HiringDate"]) != DateTime.MinValue)
                        {
                            request.HiringDate = Convert.ToDateTime(requestItem["HiringDate"]);
                        }
                        if (requestItem["OccupationPhone"] != null)
                        {
                            request.OccupationPhone = Convert.ToString(requestItem["OccupationPhone"]);
                        }

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
                    if(requestItem["EmployeeAssignedTo"]!=null)
                    {
                        request.AssignedTo = Convert.ToString(requestItem["EmployeeAssignedTo"]);
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
        public static List<PARequest> GetRequestActiveStep(int requestId)
        {
            List<PARequest> Request = new List<PARequest>();
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
                            SPList requestsList = web.Lists[Utilities.Constants.PARequests];
                            if (requestsList == null)
                                throw new Exception();
                            SPQuery reqQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='LoginName' /><Value Type='Text'>" +
                                userName + "</Value></Eq><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + requestId + "</Value></Eq></And></Where>");

                            SPListItemCollection requestItemCollection = requestsList.GetItems(reqQuery);
                            if (requestItemCollection.Count != 0)
                                foreach (SPListItem item in requestItemCollection)
                                {
                                    Request.Add(new PARequest()
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
        public static Entities.PARequest GetApplicantFromRequest(int requestId, int LCID)
        {
            Logging.GetInstance().Debug("Entering method PARequest.GetApplicantFromRequest");
            Entities.PARequest request = null;
            try
            {
                SPListItem requestItem = Reviewer_GetRequestItemByID(requestId);
                if (requestItem != null)
                {
                    request = new Entities.PARequest();
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
                Logging.GetInstance().Debug("Exiting method PARequest.GetApplicantFromRequest");
            }
            return request;
        }
        public static Entities.PARequest Reviewer_GetRequestByNumber(int requestId, int LCID)
        {
            Logging.GetInstance().Debug("Entering method Request.GetRequestByNumber");
            Entities.PARequest request = null;
            try
            {
              
                SPListItem requestItem = Reviewer_GetRequestItemByID(requestId);
                if (requestItem != null)
                {
                    request = new Entities.PARequest();
                    request.ID = requestItem.ID;
                    request.RequestNumber = Convert.ToString(requestItem["RequestNumber"]);
                    if (Convert.ToDateTime(requestItem["SubmitDate"]) != DateTime.MinValue)
                    {
                        request.SubmitDate = Convert.ToDateTime(requestItem["SubmitDate"]);
                    }

                    if (Convert.ToDateTime(requestItem["Created"]) != DateTime.MinValue)
                    {
                        request.RequestCreationDate = Convert.ToDateTime(requestItem["Created"]);
                    }
                    request.JoinedOtherUniversities = Convert.ToBoolean(requestItem["JoinedOtherUniversities"]);
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

                        if (requestItem["Occupation"] != null)
                        {

                            request.Occupation = Convert.ToString(requestItem["Occupation"]);
                        } 
                    }
                    if (requestItem["AcademicDegreeForEquivalence"] != null)
                    {
                        request.HighestCertificate = new Entities.Certificates()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["AcademicDegreeForEquivalence"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["AcademicDegreeForEquivalence"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["AcademicDegreeForEquivalenceAr"].ToString()).LookupValue
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
                  

                    if (requestItem["CertificateDate"]!=null)
                    {
                        request.CertificateDate =requestItem["CertificateDate"].ToString();
                    }

                    if (requestItem["SchoolName"] != null)
                    {
                        request.School = Convert.ToString(requestItem["SchoolName"]);
                    }

                    if (requestItem["University"] != null)
                    {

                        request.University = new University()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["University"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["University"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["UniversityAr"].ToString()).LookupValue
                        };
                    }


                    if (requestItem["UniversityList"] != null)
                    {

                        request.UniversityList = (LCID == (int)Language.English) ? Convert.ToString(requestItem["UniversityList"]).Split(',')[0] : Convert.ToString(requestItem["UniversityList"]).Split(',')[1];
                    }


                    if (requestItem["ProgramType"] != null)
                    {
                        request.ProgramType = new Entities.AcademicDegree()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["ProgramType"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["ProgramType"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["ProgramTypeAr"].ToString()).LookupValue
                        };
                    }

                    if (requestItem["ProgramUniversity"] != null)
                    {
                        request.ProgramUniversity = new University()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["ProgramUniversity"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["ProgramUniversity"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["ProgramUniversityAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["OtherPAUniversityOfStudy"] != null)
                    {
                        request.ProgramUniversity = new University()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = requestItem["OtherPAUniversityOfStudy"].ToString()
                        };
                    }
                    if (requestItem["ProgramCountry"] != null)
                    {
                        request.ProgramCountry = new CountryOfStudy()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["ProgramCountry"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["ProgramCountry"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["ProgramCountryAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["OtherPACountryOfStudy"] != null)
                    {
                        request.ProgramCountry = new CountryOfStudy()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = requestItem["OtherPACountryOfStudy"].ToString()
                        };
                    }
                    if (requestItem["ProgramFaculty"] != null)
                    {
                        request.ProgramFaculty = Convert.ToString(requestItem["ProgramFaculty"]);
                    }
                    if (requestItem["UniversityNotFoundInList"] != null)
                    {
                        request.UniversityNotFoundInList = Convert.ToString(requestItem["UniversityNotFoundInList"]);
                    }
                    if (requestItem["Faculty"] != null)
                    {
                        request.Faculty = Convert.ToString(requestItem["Faculty"]);
                    }
                    if (requestItem["FacultyNotFoundInList"] != null)
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


                    if (requestItem["ProgramSpecialization"] != null)
                    {

                        request.ProgramSpecialization = new Entities.Specialization()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["ProgramSpecialization"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["ProgramSpecialization"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["ProgramSpecializationAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["ProgramSpecializationNotFoundInList"] != null)
                    {
                        request.ProgramSpecializationNotFoundInList = Convert.ToString(requestItem["ProgramSpecializationNotFoundInList"]);
                    }


                    if (requestItem["StudySystem"] != null)
                    {
                        request.ProgramStudySystem = new Entities.StudySystem()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["StudySystem"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["StudySystem"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["StudySystemAr"].ToString()).LookupValue
                        };
                    }

                    if (requestItem["StudyType"] != null)
                    {
                        request.ProgramStudyType = new Entities.StudyType()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["StudyType"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["StudyType"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["StudyTypeAr"].ToString()).LookupValue
                        };
                    }

                    if (requestItem["ActualStudingPeriod"] != null)
                    {
                        request.ProgramPeriod = Convert.ToString(requestItem["ActualStudingPeriod"]);
                    }
                    if (requestItem["AcademicStartDate"] != null && Convert.ToDateTime(requestItem["AcademicStartDate"]) != DateTime.MinValue)
                    {
                        request.AcademicStartDate = Convert.ToDateTime(requestItem["AcademicStartDate"]);
                    }
                    if (requestItem["AcademicEndDate"] != null && Convert.ToDateTime(requestItem["AcademicEndDate"]) != DateTime.MinValue)
                    {
                        request.AcademicEndDate = Convert.ToDateTime(requestItem["AcademicEndDate"]);
                    }
                    if (requestItem["AcademicNumberOfYears"] != null)
                    {
                        request.AcademicNumberOfYears = int.Parse(Convert.ToString(requestItem["AcademicNumberOfYears"]));
                    }
                    if (requestItem["LoginName"] != null)
                    {
                        request.LoginName = Convert.ToString(requestItem["LoginName"]);
                    }
                    if (requestItem["Year"] != null)
                    {
                        request.Year = int.Parse(Convert.ToString(requestItem["Year"]));
                    }

                    request.ApplicantID = new Common.Entities.Applicants()
                    {
                        ID = new SPFieldLookupValue(requestItem["ApplicantID"].ToString()).LookupId
                    };

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
        public static Entities.PARequest GetUniversityRequestData(int requestId, int LCID)
        {
            Logging.GetInstance().Debug("Entering method Request.GetUniversityRequestData");
            Entities.PARequest request = null;
            try
            {

                SPListItem requestItem = GetRequestItemByID(requestId);
                if (requestItem != null)
                {
                    request = new Entities.PARequest();
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

                    if (requestItem["Program Country"] != null)
                    {
                        request.ProgramCountry = new CountryOfStudy()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["Program Country"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["Program Country"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["ProgramCountryAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["OtherPACountryOfStudy"] != null)
                    {
                        request.ProgramCountry = new CountryOfStudy()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = requestItem["OtherPACountryOfStudy"].ToString()
                        };
                    }

                    if (requestItem["ProgramUniversity"] != null)
                    {

                        request.ProgramUniversity = new University()
                        {
                            SelectedID = new SPFieldLookupValue(requestItem["ProgramUniversity"].ToString()).LookupId.ToString(),
                            SelectedTitle = (LCID == (int)Language.English) ? new SPFieldLookupValue(requestItem["ProgramUniversity"].ToString()).LookupValue : new SPFieldLookupValue(requestItem["ProgramUniversityAr"].ToString()).LookupValue
                        };
                    }
                    else if (requestItem["OtherPAUniversityOfStudy"] != null)
                    {
                        request.ProgramUniversity = new University()
                        {
                            SelectedID = string.Empty,
                            SelectedTitle = requestItem["OtherPAUniversityOfStudy"].ToString()
                        };
                    }
                    else if (requestItem["UniversityNotFoundInList"] != null)
                    {

                        request.UniversityNotFoundInList = Convert.ToString(requestItem["UniversityNotFoundInList"]);
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

                        request.UniversityNotFoundInList = Convert.ToString(requestItem["UniversityNotFoundInList"]);
                    }

                    if (requestItem["ApplicantID"] != null)
                    {
                        request.ApplicantID = new Applicants()
                        {
                            ID = new SPFieldLookupValue(requestItem["ApplicantID"].ToString()).LookupId
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
                            SPQuery requestQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + requestId + "</Value></Eq></Where>");

                            SPList requestList = web.Lists[Utilities.Constants.PARequests];
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
        public static int AddCheckUniversityDetails(Entities.PARequest request)
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

                            SPList requestsList = web.Lists[Utilities.Constants.PARequests];
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


                            if (request.ProgramCountry != null
                            && !string.IsNullOrEmpty(request.ProgramCountry.SelectedID))
                            { 
                                requestItem["Program Country"] = new SPFieldLookupValue(int.Parse(request.ProgramCountry.SelectedID), request.ProgramCountry.SelectedTitle);
                                requestItem["OtherPACountryOfStudy"] = string.Empty;
                            }
                            else
                            {
                                requestItem["Program Country"] = string.Empty;
                                requestItem["OtherPACountryOfStudy"] = request.ProgramCountry.SelectedTitle;
                            }
                            if (request.ProgramUniversity != null
                            && !string.IsNullOrEmpty(request.ProgramUniversity.SelectedID))
                            {
                                requestItem["ProgramUniversity"] = new SPFieldLookupValue(int.Parse(request.ProgramUniversity.SelectedID), request.ProgramUniversity.SelectedTitle);
                                if (!string.IsNullOrEmpty(request.UniversityList))
                                {
                                    requestItem["UniversityList"] = request.UniversityList;
                                }
                                requestItem["UniversityNotFoundInList"] = string.Empty;
                                requestItem["OtherPAUniversityOfStudy"] = string.Empty;
                            }
                            else
                            {
                                requestItem["ProgramUniversity"] = string.Empty;
                                requestItem["UniversityList"] = string.Empty;
                                requestItem["OtherPAUniversityOfStudy"] = request.ProgramUniversity.SelectedTitle;
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
        public static bool UpdateRejection(Entities.PARequest request)
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

                            SPList requestsList = web.Lists[Utilities.Constants.PARequests];

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
                        List<SimilarRequest> PANewRequests = BL.PANewRequests.GetAllPANewRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.PANewRequests.GetNewQueryPerRole(SPGroupName), "Or", true)
                            + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();
                        PANewRequests = PANewRequests.Where(x => (x.AssignedTo.ToLower().Contains(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == SPGroupName.ToLower()) && (string.IsNullOrEmpty(x.DelayedDays) || int.Parse(x.DelayedDays) < delayedDays)).ToList();
                        List<SimilarRequest> RejectedRequests = BL.ReturnedRequests.GetAllReturnedRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.ReturnedRequests.GetReturnedQueryPerRole(SPGroupName), "Or", true)
                            + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();
                        RejectedRequests = RejectedRequests.Where(x => (x.AssignedTo.ToLower().Contains(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == SPGroupName.ToLower()) && (string.IsNullOrEmpty(x.DelayedDays) || int.Parse(x.DelayedDays) < delayedDays)).ToList();

                        List<ClarificationReqs> ClarRequests = BL.PAClarificationRequests.GetAllPAClarificationRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.PAClarificationRequests.GetClarQueryPerRole(SPGroupName), "Or", true)
                           + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList();
                        RejectedRequests = RejectedRequests.Where(x => x.AssignedTo.ToLower().Contains(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == SPGroupName.ToLower()).ToList();


                        if (LateRequests.Count > 0)
                            objColumns.Add(new Common.Entities.RequestsChart()
                            {
                                RequestCount = LateRequests.Count,
                                RequestType = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "LateRequests", (uint)LCID)
                            });
                        if (PANewRequests.Count > 0)
                            objColumns.Add(new Common.Entities.RequestsChart()
                            {
                                RequestCount = PANewRequests.Count,
                                RequestType = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "PANewRequests", (uint)LCID)
                            });

                        if (RejectedRequests.Count > 0)
                            objColumns.Add(new Common.Entities.RequestsChart()
                            {
                                RequestCount = RejectedRequests.Count,
                                RequestType = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "ReturnedRequests", (uint)LCID)
                            });

                        if (ClarRequests.Count > 0)
                            objColumns.Add(new Common.Entities.RequestsChart()
                            {
                                RequestCount = ClarRequests.Count,
                                RequestType = HelperMethods.LocalizedText("ITWORX_MOEHEWF_PA", "PAClarificationRequests", (uint)LCID)
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
            string errorCode = "0";
            List<Entities.SimilarRequest> Requests = new List<Entities.SimilarRequest>();
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method Request.GetRequestsListing");
                        SPList customerList = web.Lists[Utilities.Constants.PARequests];
                        errorCode = "1";
                        SPListItemCollection items = customerList.GetItems(query);
                        errorCode = "1.1";
                        Common.Entities.RequestStatus requestStatusItem = new Common.Entities.RequestStatus();
                        foreach (SPListItem item in items)
                        {

                            errorCode = "1.2 for each "+ item.ID;

                            if (item["RequestStatusId"] == null)
                            {
                                Logging.GetInstance().LogException(new Exception("Item Status is Null or empty"));
                                continue;
                            }

                            requestStatusItem = Common.BL.RequestStatus.GetRequestStatusById(new SPFieldLookupValue(item["RequestStatusId"].ToString()).LookupId);
                            errorCode = "1.3";
                            SimilarRequest Request = new SimilarRequest();
                            SPFieldLookupValue ApplicantsQatarID = new SPFieldLookupValue((item["Applicants_QatarID"] != null) ? item["Applicants_QatarID"].ToString() : string.Empty);
                            Request.QatariID = ApplicantsQatarID.LookupValue;
                            errorCode = "1.4";
                            Request.DelayedDays = (item["DelayedDays"] != null) ? item["DelayedDays"].ToString() : string.Empty;
                            errorCode = "1.5";
                            Request.AssignedTo = (item["EmployeeAssignedTo"] != null) ? item["EmployeeAssignedTo"].ToString() : string.Empty;
                            Request.ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;
                            Request.RequestNumber = (item["RequestNumber"] != null) ? item["RequestNumber"].ToString() : string.Empty;
                            errorCode = "1.6";
                            if (requestStatusItem.Code == Common.Utilities.RequestStatus.PAEmployeeClarificationReplay.ToString())
                            {
                                errorCode = "1.7";
                                if (LCID == (int)Language.English)
                                {
                                    errorCode = "1.8";
                                    SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                                    Request.RejectedFrom = ApplicantsEnglishName.LookupValue;
                                }
                                else
                                {
                                    errorCode = "1.9";
                                    SPFieldLookupValue ApplicantsArabicName = new SPFieldLookupValue((item["Applicants_ArabicName"] != null) ? item["Applicants_ArabicName"].ToString() : string.Empty);
                                    Request.RejectedFrom = ApplicantsArabicName.LookupValue;
                                }
                                errorCode = "2.0";
                            }
                            else
                            {
                                errorCode = "2.1";
                                Request.RejectedFrom = (item["RejectedFrom"] != null) ? item["RejectedFrom"].ToString() : string.Empty;
                            }

                            errorCode = "2.2";
                            Request.ProgramFaculty = (item["ProgramFaculty"] != null) ? item["ProgramFaculty"].ToString() : string.Empty;
                            Request.RejectionReason = (item["RejectionReason"] != null) ? item["RejectionReason"].ToString() : string.Empty;
                            Request.RejectionDate = (item["RejectionDate"] != null) ? DateTime.Parse(item["RejectionDate"].ToString()) : DateTime.MinValue;
                            Request.SubmitDate = (item["SubmitDate"] != null) ? DateTime.Parse(item["SubmitDate"].ToString()) : DateTime.MinValue;
                            errorCode = "2.3";
                            SPFieldLookupValue StatusId = new SPFieldLookupValue((item["RequestStatusId"] != null) ? item["RequestStatusId"].ToString() : string.Empty);
                            errorCode = "2.4";
                            Request.RequestStatusId = StatusId.LookupId;
                            if ((Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEClosedByAcceptance) || (Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEClosedByRejection))
                                Request.IsRequestClosed = true;
                            else
                                Request.IsRequestClosed = false;
                            errorCode = "2.5";
                            SPFieldLookupValue ApplicantsID = new SPFieldLookupValue((item["Applicants_QID"] != null) ? item["Applicants_QID"].ToString() : string.Empty);
                            Request.QID = ApplicantsID.LookupValue;
                            errorCode = "2.6";
                            if (LCID == (int)Language.English)
                            {
                                errorCode = "2.7";
                                SPFieldLookupValue NationalityTitle = new SPFieldLookupValue((item["Nationality_Title"] != null) ? item["Nationality_Title"].ToString() : string.Empty);
                                Request.Nationality = NationalityTitle.LookupValue;
                                SPFieldLookupValue RequestStatus = new SPFieldLookupValue((item["RequestStatus"] != null) ? item["RequestStatus"].ToString() : string.Empty);
                                Request.RequestStatus = RequestStatus.LookupValue;
                                errorCode = "2.8";
                                Request.RequestStatus = requestStatusItem.ReviewerDescriptionEn;
                                SPFieldLookupValue ApplicantsEnglishName = new SPFieldLookupValue((item["Applicants_EnglishName"] != null) ? item["Applicants_EnglishName"].ToString() : string.Empty);
                                Request.ApplicantName = ApplicantsEnglishName.LookupValue;
                                errorCode = "2.9";
                                SPFieldLookupValue ProgramUniversity = new SPFieldLookupValue((item["ProgramUniversity"] != null) ? item["ProgramUniversity"].ToString() : string.Empty);
                                if (ProgramUniversity.LookupValue != null)
                                    Request.ProgramUniversity = ProgramUniversity.LookupValue;
                                else
                                    Request.ProgramUniversity = (item["OtherPAUniversityOfStudy"] != null) ? item["OtherPAUniversityOfStudy"].ToString() : string.Empty;

                                errorCode = "3.0";
                                if (item["CountryOfStudy"] != null)
                                {
                                    errorCode = "3.1";
                                    SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue(item["CountryOfStudy"].ToString());
                                    Request.Country = CountryOfStudy.LookupValue;
                                    errorCode = "3.2";
                                }
                                if (item["ProgramCountry"] != null)
                                {
                                    errorCode = "3.3";
                                    SPFieldLookupValue _CountryOfStudy = new SPFieldLookupValue(item["ProgramCountry"].ToString());
                                    Request.ProgramCountry = _CountryOfStudy.LookupValue;
                                    errorCode = "3.4";
                                }
                                if (item["Faculty"] != null)
                                {
                                    errorCode = "3.5";
                                    Request.Faculty = item["Faculty"].ToString();
                                    errorCode = "3.6";
                                }
                            }
                            else
                            {
                                SPFieldLookupValue NationalityTitleAr = new SPFieldLookupValue((item["Nationality_TitleAr"] != null) ? item["Nationality_TitleAr"].ToString() : string.Empty);
                                Request.Nationality = NationalityTitleAr.LookupValue;
                                errorCode = "3.7";
                                SPFieldLookupValue RequestStatus = new SPFieldLookupValue((item["RequestStatusAr"] != null) ? item["RequestStatusAr"].ToString() : string.Empty);
                                Request.RequestStatus = RequestStatus.LookupValue;
 
                                Request.RequestStatus = requestStatusItem.ReviewerDescriptionAr;
                                errorCode = "3.8";

                                SPFieldLookupValue ApplicantsArabicName = new SPFieldLookupValue((item["Applicants_ArabicName"] != null) ? item["Applicants_ArabicName"].ToString() : string.Empty);
                                Request.ApplicantName = ApplicantsArabicName.LookupValue;
                                errorCode = "3.9";
                                SPFieldLookupValue University = new SPFieldLookupValue((item["UniversityAr"] != null) ? item["UniversityAr"].ToString() : string.Empty);
                                if (University.LookupValue != null)
                                    Request.University = University.LookupValue;
                                else
                                    Request.University = (item["UniversityNotFoundInList"] != null) ? item["UniversityNotFoundInList"].ToString() : string.Empty;

                                errorCode = "4.0";
                                SPFieldLookupValue ProgramUniversity = new SPFieldLookupValue((item["ProgramUniversityAr"] != null) ? item["ProgramUniversityAr"].ToString() : string.Empty);
                                if (ProgramUniversity.LookupValue != null)
                                    Request.ProgramUniversity = ProgramUniversity.LookupValue;
                                else
                                    Request.ProgramUniversity = (item["OtherPAUniversityOfStudy"] != null) ? item["OtherPAUniversityOfStudy"].ToString() : string.Empty;
                                errorCode = "4.1";

                                if (item["CountryOfStudyAr"] != null)
                                {
                                    SPFieldLookupValue CountryOfStudy = new SPFieldLookupValue(item["CountryOfStudyAr"].ToString());
                                    Request.Country = CountryOfStudy.LookupValue;
                                    errorCode = "4.2";
                                }
                                if (item["ProgramCountryAr"] != null)
                                {
                                    SPFieldLookupValue _CountryOfStudy = new SPFieldLookupValue(item["ProgramCountryAr"].ToString());
                                    Request.ProgramCountry = _CountryOfStudy.LookupValue;
                                    errorCode = "4.3";
                                }

                                if (item["FacultyAr"] != null)
                                {
                                    Request.Faculty = item["FacultyAr"].ToString();
                                    errorCode = "4.4";
                                }
                            }
                            Requests.Add(Request);
                            errorCode = "4.5";
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(new Exception("Error Code: " + errorCode));
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

        public static List<Entities.SimilarRequest> GetRequestsListing(SPQuery query)
        {
            string errorCode = "0";
            List<Entities.SimilarRequest> Requests = new List<Entities.SimilarRequest>();
            using (SPSite site = new SPSite(SPContext.Current.Site.ID))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method Request.GetRequestsListing");
                        SPList customerList = web.Lists[Utilities.Constants.PARequests];
                        errorCode = "1";
                        SPListItemCollection items = customerList.GetItems(query);
                        errorCode = "1.1";
                        Common.Entities.RequestStatus requestStatusItem = new Common.Entities.RequestStatus();
                        foreach (SPListItem item in items)
                        {

                            errorCode = "1.2 for each " + item.ID;

                            if (item["RequestStatusId"] == null)
                            {
                                Logging.GetInstance().LogException(new Exception("Item Status is Null or empty"));
                                continue;
                            }

                            requestStatusItem = Common.BL.RequestStatus.GetRequestStatusById(new SPFieldLookupValue(item["RequestStatusId"].ToString()).LookupId);
                            errorCode = "1.3";
                            SimilarRequest Request = new SimilarRequest();
                          
                            Request.DelayedDays = (item["DelayedDays"] != null) ? item["DelayedDays"].ToString() : string.Empty;
                            errorCode = "1.5";
                            Request.AssignedTo = (item["EmployeeAssignedTo"] != null) ? item["EmployeeAssignedTo"].ToString() : string.Empty;
                            Request.ID = (item["ID"] != null) ? item["ID"].ToString() : string.Empty;
                            Request.RequestNumber = (item["RequestNumber"] != null) ? item["RequestNumber"].ToString() : string.Empty;
                            errorCode = "1.6";
                          
                            Request.RejectionDate = (item["RejectionDate"] != null) ? DateTime.Parse(item["RejectionDate"].ToString()) : DateTime.MinValue;
                            Request.SubmitDate = (item["SubmitDate"] != null) ? DateTime.Parse(item["SubmitDate"].ToString()) : DateTime.MinValue;
                            errorCode = "2.3";
                            SPFieldLookupValue StatusId = new SPFieldLookupValue((item["RequestStatusId"] != null) ? item["RequestStatusId"].ToString() : string.Empty);
                            errorCode = "2.4";
                            Request.RequestStatusId = StatusId.LookupId;
                            if ((Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEClosedByAcceptance) || (Request.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEClosedByRejection))
                                Request.IsRequestClosed = true;
                            else
                                Request.IsRequestClosed = false;
                            errorCode = "2.5";
                          
                        
                            Requests.Add(Request);
                            errorCode = "4.5";
                        }
                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(new Exception("Error Code: " + errorCode));
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
                            SPList requestsList = web.Lists[Utilities.Constants.PARequests];
                            SPListItem item = requestsList.GetItemById(requestID);
                            if (item != null)
                            {
                                if (item["RequestStatus"] != null)
                                {
                                    SPFieldLookupValue statusValue = new SPFieldLookupValue(item["RequestStatus"].ToString());
                                    if (statusValue.LookupId ==(int) Common.Utilities.RequestStatus.PADraftForClarification)
                                    {
                                        item["RequestStatus"] = new SPFieldLookupValue((int)Common.Utilities.RequestStatus.PAEmployeeClarificationReplay, ((int)Common.Utilities.RequestStatus.PAEmployeeClarificationReplay).ToString());
                                       

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
                    if (!string.IsNullOrEmpty(universityID))
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
    }
}
