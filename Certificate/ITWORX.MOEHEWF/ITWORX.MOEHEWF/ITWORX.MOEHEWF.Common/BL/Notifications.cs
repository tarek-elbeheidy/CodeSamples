using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITWORX.MOEHEWF.Common.BL
{
    public class Notifications
    {
        public static Entities.Notifications GetSubmittedNotification(int notificationType, int requestStatusId)
        {
            Entities.Notifications notifications = null;
            Logging.GetInstance().Debug("Entering method Notifications.GetSubmittedNotification");
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList notificationsList = web.Lists.TryGetList(Constants.Notifications);
                            if (notificationsList != null)
                            {
                                string query = string.Empty;
                                if (requestStatusId == 0) // Zero means that the requestStatusId lookup field value is null in the notifications list
                                {
                                    if (notificationType == (int)NotificationType.Email)
                                    {
                                        query = "<Where><And><IsNull><FieldRef Name='RequestStatusId' /></IsNull><Eq><FieldRef Name='Type' /><Value Type='Boolean'>" + (int)NotificationType.Email + "</Value></Eq></And></Where>";
                                    }
                                    else
                                    {
                                        query = "<Where><And><IsNull><FieldRef Name='RequestStatusId' /></IsNull><Eq><FieldRef Name='Type' /><Value Type='Boolean'>" + (int)NotificationType.SMS + "</Value></Eq></And></Where>";
                                    }
                                }
                                else
                                {
                                    if (notificationType == (int)NotificationType.Email)
                                    {
                                        query = "<Where><And><Eq><FieldRef Name='RequestStatusId'  LookupId='TRUE' /><Value Type='Lookup' >" + requestStatusId + "</Value></Eq><Eq><FieldRef Name='Type' /><Value Type='Boolean'>" + (int)NotificationType.Email + "</Value></Eq></And></Where>";
                                    }
                                    else
                                    {
                                        query = "<Where><And><Eq><FieldRef Name='RequestStatusId'  LookupId='TRUE' /><Value Type='Lookup'>" + requestStatusId + "</Value></Eq><Eq><FieldRef Name='Type' /><Value Type='Boolean'>" + (int)NotificationType.SMS + "</Value></Eq></And></Where>";
                                    }
                                }

                                SPQuery sPQuery = Common.Utilities.BusinessHelper.GetQueryObject(query);

                                SPListItemCollection notificationItems = notificationsList.GetItems(sPQuery);
                                if (notificationItems != null && notificationItems.Count > 0)
                                {
                                    notifications = new Entities.Notifications();
                                    notifications.Subject = Convert.ToString(notificationItems[0]["Subject"]);
                                    notifications.Body = Convert.ToString(notificationItems[0]["Body"]);
                                }
                            }
                            else
                            {
                                Logging.GetInstance().Debug("Notifications list doesn't exist");
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
                Logging.GetInstance().Debug("Exiting method Notifications.GetSubmittedNotification");
            }

            return notifications;
        }

        public static Entities.Notifications GetSCENotifications(int notificationType, int requestStatusId)
        {
            Entities.Notifications notifications = null;
            Logging.GetInstance().Debug("Entering method Notifications.GetSCENotification");
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList notificationsList = web.Lists.TryGetList(Constants.SCENotifications);
                            if (notificationsList != null)
                            {
                                string query = string.Empty;
                                if (requestStatusId == 0) // Zero means that the requestStatusId lookup field value is null in the notifications list
                                {
                                    if (notificationType == (int)NotificationType.Email)
                                    {
                                        query = "<Where><And><IsNull><FieldRef Name='RequestStatusId' /></IsNull><Eq><FieldRef Name='Type' /><Value Type='Boolean'>" + (int)NotificationType.Email + "</Value></Eq></And></Where>";
                                    }
                                    else
                                    {
                                        query = "<Where><And><IsNull><FieldRef Name='RequestStatusId' /></IsNull><Eq><FieldRef Name='Type' /><Value Type='Boolean'>" + (int)NotificationType.SMS + "</Value></Eq></And></Where>";
                                    }
                                }
                                else
                                {
                                    if (notificationType == (int)NotificationType.Email)
                                    {
                                        query = "<Where><And><Eq><FieldRef Name='RequestStatusId'  LookupId='TRUE' /><Value Type='Lookup' >" + requestStatusId + "</Value></Eq><Eq><FieldRef Name='Type' /><Value Type='Boolean'>" + (int)NotificationType.Email + "</Value></Eq></And></Where>";
                                    }
                                    else
                                    {
                                        query = "<Where><And><Eq><FieldRef Name='RequestStatusId'  LookupId='TRUE' /><Value Type='Lookup'>" + requestStatusId + "</Value></Eq><Eq><FieldRef Name='Type' /><Value Type='Boolean'>" + (int)NotificationType.SMS + "</Value></Eq></And></Where>";
                                    }
                                }

                                SPQuery sPQuery = Common.Utilities.BusinessHelper.GetQueryObject(query);

                                SPListItemCollection notificationItems = notificationsList.GetItems(sPQuery);
                                if (notificationItems != null && notificationItems.Count > 0)
                                {
                                    notifications = new Entities.Notifications();
                                    notifications.Subject = Convert.ToString(notificationItems[0]["Subject"]);
                                    notifications.Body = Convert.ToString(notificationItems[0]["Body"]);
                                }
                            }
                            else
                            {
                                Logging.GetInstance().Debug("Notifications list doesn't exist");
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
                Logging.GetInstance().Debug("Exiting method Notifications.GetSCENotification");
            }

            return notifications;

        }

        public static List<string> SendMailToGroup(string groupName)
        {
            List<string> mails = null;
            Logging.GetInstance().Debug("Entering method Notifications.SendMailToGroup");
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            if (web.Groups.OfType<SPGroup>().Where(g => g.Name == groupName).Count() > 0)
                            {
                                SPGroup group = web.SiteGroups.GetByName(groupName);
                                SPUserCollection userCollection = group.Users;
                                if (userCollection.Count > 0)
                                { 
                                    foreach (SPUser user in userCollection)
                                    {
                                    
                                        if (!string.IsNullOrEmpty(user.Email))
                                            mails.Add(user.Email);
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
                Logging.GetInstance().Debug("Exiting method Notifications.SendMailToGroup");
            }
            return mails;
        }
    }
}