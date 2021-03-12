using ITWORX.MOEHEWF.Common.Utilities;
using ITWORX.MOEHEWF.ReassignReceptionistsTasks.Utilities;
using Microsoft.SharePoint;
using Nintex.Workflow.HumanApproval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.ReassignReceptionistsTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ReassignReceptionistsLogger.GetInstance().Debug("Entering Method Program.Main , Namespace ITWORX.MOEHEWF.ReassignReceptionistsTasks");
                using (SPSite site = new SPSite(Helper.GetConfigValue("siteURL")))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList ReceptionistList = web.Lists["ReceptionEmployeesVacations"];
                        if (ReceptionistList != null)
                        {
                            var ReceptionEmployeesQuery = new SPQuery()
                            {
                                Query = @"<Where><Eq>
                                        <FieldRef Name='IsVacation' />
                                        <Value Type='Boolean'>1</Value>
                                      </Eq></Where>"
                            };

                            var AttendedEmployeeQuery = new SPQuery()
                            {
                                Query = @"<OrderBy>
                                            <FieldRef Name='TasksCount' Ascending='True' /> 
                                        </OrderBy>
                                        <Where><Eq> 
                                            <FieldRef Name='IsVacation'/>
                                            <Value Type='Boolean'>0</Value> 
                                      </Eq></Where>"
                            };

                            var ReceptionistInVacationListItems = ReceptionistList.GetItems(ReceptionEmployeesQuery);


                            SPList RequestsList = web.Lists["Requests"];
                            if (RequestsList != null)
                            {
                                var RequestsQuery = new SPQuery()
                                {
                                    Query = @"<Where><And><Eq><FieldRef Name='RequestStatus' LookupId='TRUE' />" +
                                             "<Value Type='Lookup'>" + Convert.ToInt32(RequestStatus.UCESubmitted) + "</Value>" +
                                             "</Eq><In><FieldRef Name = 'EmployeeAssignedTo' /><Values>"
                                };
                                SPUser user = null;
                                foreach (SPListItem item in ReceptionistInVacationListItems)
                                {
                                    user = GetSPUserFromItem(item, "Employee");
                                    RequestsQuery.Query += @"<Value Type='Text'>" + user.LoginName + "</Value>";
                                }
                                RequestsQuery.Query += @"</Values></In></And></Where>";

                                var RequestsItems = RequestsList.GetItems(RequestsQuery);

                                foreach (SPListItem requestItem in RequestsItems)
                                {
                                    var AttendedEmployeeListItems = ReceptionistList.GetItems(AttendedEmployeeQuery);
                                    if (AttendedEmployeeListItems.Count > 0)
                                    {
                                        SPListItem delegateEmployee = AttendedEmployeeListItems[0];
                                        user = GetSPUserFromItem(delegateEmployee, "Employee");
                                        bool result = DelegateTask(requestItem.Tasks[0], user);
                                        if (result)
                                        {
                                            string oldEmp = requestItem["EmployeeAssignedTo"].ToString();
                                            requestItem["EmployeeAssignedTo"] = user.LoginName;
                                            requestItem.Update();
                                            delegateEmployee["TasksCount"] = (Convert.ToInt32(delegateEmployee["TasksCount"]) + 1).ToString();
                                            delegateEmployee.Update();
                                        }
                                    }
                                }
                                var requestItemGrouping = (from SPListItem item in RequestsItems
                                                           group item by item["EmployeeAssignedTo"]
                                                                into grp
                                                           select grp).ToList();
                                foreach (var requestItem in requestItemGrouping)
                                {  
                                    var ReceptionistInVacation= ReceptionistList.GetItems(new SPQuery()
                                    {
                                        Query = @"<Where><And>
                                            <Eq>
                                                <FieldRef Name='IsVacation' />
                                                <Value Type='Boolean'>1</Value>
                                            </Eq>
                                            <Eq>
                                                <FieldRef Name='Employee' />
                                                <Value Type='User'>" + requestItem.Key+"</Value></Eq></And></Where>"
                                    });
                                    SPListItem Employee = ReceptionistInVacation[0];
                                    Employee["TasksCount"] = (Convert.ToInt32(Employee["TasksCount"]) - requestItem.Count()).ToString();
                                    Employee.Update();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex){
                ReassignReceptionistsLogger.GetInstance().LogException(ex);
            }
            finally {
                ReassignReceptionistsLogger.GetInstance().Debug("Exit Method Program.Main , Namespace ITWORX.MOEHEWF.ReassignReceptionistsTasks");
            }
        }

        public static SPUser GetSPUserFromItem(SPListItem item, string fieldName)
        {
            ReassignReceptionistsLogger.GetInstance().Debug("Entering Method GetSPUserFromItem , Namespace ITWORX.MOEHEWF.ReassignReceptionistsTasks");
            SPFieldUser spfu = (SPFieldUser)item.Fields[fieldName];
            SPFieldUserValue user = (SPFieldUserValue)spfu.GetFieldValue(item[fieldName].ToString());
            ReassignReceptionistsLogger.GetInstance().Debug("Exit Method GetSPUserFromItem , Namespace ITWORX.MOEHEWF.ReassignReceptionistsTasks");
            return user.User;
        }
        public static bool DelegateTask(SPListItem taskItem, SPUser delegateToUser)
        {
            ReassignReceptionistsLogger.GetInstance().Debug("Entering Method GetSPUserFromItem , Namespace ITWORX.MOEHEWF.ReassignReceptionistsTasks");
            bool success = false;
            try
            {
                NintexTask nintexTask = NintexTask.RetrieveTask(taskItem.ID, taskItem.Web, taskItem.ParentList);
                Approver approver = nintexTask.Approvers.GetBySPId(taskItem.ID);
                success = Delegation.DelegateApprovalTask(true, approver, delegateToUser.LoginName, false, false, "Delegated note.", true);
            }
            catch (Exception ex)
            { 
                ReassignReceptionistsLogger.GetInstance().LogException(ex);
            }
            finally
            {
                ReassignReceptionistsLogger.GetInstance().Debug("Exit Method DelegateTask , Namespace ITWORX.MOEHEWF.ReassignReceptionistsTasks");
            }
            return success;
        }
    }
}
