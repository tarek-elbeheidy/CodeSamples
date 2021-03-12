using System;
using System.Collections.Generic;
using System.Linq;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace ITWORX.MOEHEWF.UCE.Layouts.ITWORX.MOEHEWF.UCE
{
    public partial class ajax : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void BindbtnsCount(string GroupName)
        {
            try
            {
                Logging.GetInstance().Debug("Enter ajax.BindbtnsCount");
                ButtonCount requestsCount = new ButtonCount();
                int delayedDays = int.Parse(HelperMethods.GetConfigurationValue(SPContext.Current.Site.Url + Common.Utilities.Constants.HEWebUrl, Common.Utilities.Constants.Configuration, "DelayedDays"));
                //#region
                //List<Entities.SimilarRequest> userRequests=  BL.NewRequests.GetAllNewRequests("", this.LCID).ToList<Entities.SimilarRequest>();
                //switch(GroupName.ToLower())
                //{
                //    case "ProgramManagerGroupName.ToLower()":
                //        userRequests.Where(r => r.RequestStatusId == (int)Common.Utilities.RequestStatus.UCEProgramManagerReview);
                //        break;
                //}

                //#endregion
                requestsCount.NewRequests = BL.NewRequests.GetAllNewRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.NewRequests.GetNewQueryPerRole(GroupName), "Or", true)
               + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList().Where(x => (x.AssignedTo.ToLower().Equals(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == GroupName.ToLower()) && (string.IsNullOrEmpty(x.DelayedDays) || int.Parse(x.DelayedDays) < delayedDays)).ToList().Count();

                if (userInGroup(Common.Utilities.Constants.CulturalMissionBritainGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionAustraliaGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionFranceGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionUSAGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionCanadaGroupName) ||
                    userInGroup(Common.Utilities.Constants.CulturalMissionJordanGroupName) ||
                    userInGroup(Common.Utilities.Constants.HigherEducationalInstitutionsGroupName)) { }
                else
                {
                    requestsCount.ReturnedRequests = BL.ReturnedRequests.GetAllReturnedRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.ReturnedRequests.GetReturnedQueryPerRole(GroupName), "Or", true)
                  + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList().Where(x => (x.AssignedTo.ToLower().Equals(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == GroupName.ToLower()) && (string.IsNullOrEmpty(x.DelayedDays) || int.Parse(x.DelayedDays) < delayedDays)).ToList().Count();
                }

                requestsCount.LateRequests = BL.LateRequests.GetAllLateRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.LateRequests.GetLateQueryPerRole(), "And", true)
                + "<OrderBy><FieldRef Name='SubmitDate' Ascending='False' /></OrderBy>", LCID).ToList().Where(x => x.AssignedTo.ToLower().Equals(SPContext.Current.Web.CurrentUser.LoginName.ToLower()) || x.AssignedTo.ToLower() == GroupName.ToLower()).ToList().Count();

                if (userInGroup(Common.Utilities.Constants.ReceptionistGroupName) || userInGroup(Common.Utilities.Constants.ArabicProgEmployeeGroupName)
                    || userInGroup(Common.Utilities.Constants.EuropeanProgEmployeeGroupName))
                    requestsCount.ClarificationRequests = BL.ClarificationRequests.GetAllClarificationRequests(Common.Utilities.BusinessHelper.CreateCAMLQuery(BL.ClarificationRequests.GetClarQueryPerRole(GroupName), "And", true)
                   + "<OrderBy><FieldRef Name='ClarificationDate' Ascending='False' /></OrderBy>", LCID).ToList().Where(x => x.RequestSender.ToLower().Equals(SPContext.Current.Web.CurrentUser.Name.ToLower()) || x.RequestSender.ToLower() == GroupName.ToLower()).ToList().Count();
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit MyRequests.BindbtnsCount");
            }
        }
        private bool userInGroup(string GroupName)
        {
            bool exists = false;
            List<SPUser> users = HelperMethods.GetGroupUsers(GroupName);
            //List<SPUser> Europeancusers = HelperMethods.GetGroupUsers(Common.Utilities.Constants.EuropeanProgEmployeeGroupName);
            foreach (SPUser user in users)
            {
                if (user.LoginName == SPContext.Current.Web.CurrentUser.LoginName)
                    exists = true;
            }
            return exists;
        }

    }

    public class ButtonCount
    {
        public int NewRequests { get; set; }
        public int ReturnedRequests { get; set; }
        public int LateRequests { get; set; }
        public int ClarificationRequests { get; set; }
    }
}
