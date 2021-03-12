using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using Nintex.Workflow;
using Nintex.Workflow.Common;
using Nintex.Workflow.HumanApproval;

namespace ITWORX.MOEHEWF.UCE.Utilities
{
    public static class NintexHelper
    {
        public static NintexContext ParseRequest(int taskId, Guid taskListId)
        {

            SPWeb web = SPContext.Current.Site.RootWeb;
            NintexContext context = new NintexContext();
            SPSecurity.RunWithElevatedPrivileges(() =>
            {

                context = new NintexContext();
                context.Web = web;
                context.CurrentUser = web.CurrentUser;
                SPListCollection lists = context.Web.Lists;
                lists.ListsForCurrentUser = true;

                try
                {
                    context.TaskList = lists.GetList(taskListId, false);
                    context.TaskItem = context.TaskList.GetItemById(taskId);
                }
                catch (SPException)
                {
                    throw new SPException(string.Format("Cannot find a task list with id {0}.", taskListId));
                }
                catch (ArgumentException)
                {
                    throw new SPException(string.Format("Cannot find task with id {0}.", taskListId));
                }

                // Retrieve the Nintex Workflow object model representation of the task
                context.Task = NintexTask.RetrieveTask(taskId, context.Web, context.TaskList);

                // Retrieve the Nintex Workflow object model representation of the approver for this SharePoint task item id.
                // A Nintex Workflow task can have multiple approvers, and each approver will have a separated SharePoint task item.
                context.Approver = context.Task.Approvers.GetBySPId(taskId);

                if (context.Approver == null)
                {
                    throw new SPException(string.Format("Cannot find an approver for SharePoint task item {0}.", taskId));
                }

                NWListWorkflowContext listWorkflowContext = context.Task.WFContext as NWListWorkflowContext;

                context.WorkflowListId = listWorkflowContext.ListID;
                SPList list = context.Web.Lists[listWorkflowContext.ListID];

                try
                {
                    context.WorkflowItemId = listWorkflowContext.ItemID;
                    context.Item = list.GetItemById(listWorkflowContext.ItemID);
                }
                catch
                {
                    // do not error the page just because we cannot get hold of the item
                }
            });
            return context;

        }
        /// <summary>
        /// Complete the current task
        /// </summary>
        /// <param name="CurrentContext"></param>
        public static void CompleteTask(NintexContext context)
        {
            //SetTaskStatus(context, Outcome.Approved);
        }
        public static void ContinueTask(NintexContext context, string key, string Comment,string listName,string itemID)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList requestList = web.Lists.TryGetList(listName);
                        if(requestList!=null)
                        {
                            SPListItem item = requestList.GetItemById(int.Parse(itemID));
                            if(item!=null)
                            {
                                item["EmployeeAssignedTo"] = string.Empty;
                                item["DelayedDays"] = string.Empty;
                                item["ActionDate"] = DateTime.Now;
                                item.Update();
                            }
                        }

                        int Outcome = Convert.ToInt32(HelperMethods.GetConfigurationValue(SPContext.Current.Site.RootWeb.Url, "NintexConfiguredOutcomes", key));
                        SetTaskStatus(context, Outcome, Comment);
                    }
                }
            });
        }
        /// <summary>
        /// Complete the current task
        /// </summary>
        /// <param name="CurrentContext"></param>
        public static void RejectTask(NintexContext context)
        {
            ///SetTaskStatus(context, Outcome.Rejected);
        }
        /// <summary>
        /// Complete the current task
        /// </summary>
        /// <param name="CurrentContext"></param>
        private static void SetTaskStatus(NintexContext context, int outcome, string Comment)
        {
            SPWeb web = null;
            if (!AttemptLockTask(context))
                return;
            try
            {
                // string webUrl = context.TaskItem.Web.Url;
                string siteUrl = context.TaskItem.Web.Site.Url;

                Guid listGuid = context.TaskItem.ParentList.ID;
                int itemID = context.TaskItem.ID;

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    Guid commentsFieldId = NWSharePointObjects.FieldComments;

                    Guid decisionFieldId = NWSharePointObjects.FieldDecision;

                    Guid ApprovalOutcome = NWSharePointObjects.FieldApprovalOutcome;

                    Guid ApproverTaskID = NWSharePointObjects.FieldApproverTaskID;

                    using (web = new SPSite(siteUrl).OpenWeb())
                    {
                        web.AllowUnsafeUpdates = true;
                        SPList list = web.Lists[listGuid];
                        if (list != null)
                        {
                            NintexTask approvalTask = NintexTask.RetrieveTask(itemID, web, web.Lists[listGuid]);

                            SPListItem item = list.GetItemById(itemID); 

                            if (context.Approver != null && approvalTask.ApprovalStatus == Outcome.Pending)
                            {
                                item[ApproverTaskID] = context.Approver.ApproverID;

                                item[decisionFieldId] = outcome;

                                item[commentsFieldId] = Comment;

                                item[ApprovalOutcome] = outcome;

                                item.Update();

                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                //Unlock
                Logging.GetInstance().LogException(ex);
                context.Approver.UpdateTaskLocked(false);
            }
            finally
            {
                web.AllowUnsafeUpdates = false;
                Logging.GetInstance().Debug("Exiting method NintexHelper.SetTaskStatus");
            }
        }
        /// <summary>
        /// Try to lock a task
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool AttemptLockTask(NintexContext context)
        {
            if (TaskLocker.AttemptTaskLock(context.Approver, context.Item, context.WorkflowListId, context.WorkflowItemId))
            {
                return true;
            }
            else
            {
                throw new SPException("This task is locked by another user. Please try again.");
            }
        }
    }
}
