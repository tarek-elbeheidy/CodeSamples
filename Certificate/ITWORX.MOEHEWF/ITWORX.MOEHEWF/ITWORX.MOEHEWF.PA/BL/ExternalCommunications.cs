using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Entities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class ExternalCommunications
    {
        public static List<ExternalComms> GetBooksbyRequestID(string reqID)
        {
            List<ExternalComms> ExtComms = new List<ExternalComms>();
            using (SPSite site = new SPSite(SPContext.Current.Site.Url))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    try
                    {
                        Logging.GetInstance().Debug("Entering method ExternalCommunications.GetBooks");
                        SPList list = web.Lists[Utilities.Constants.PAExternalCommunications];
                        var q = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Text'>" +
                            reqID + "</Value></Eq></Where>");

                        SPListItemCollection collListItems = list.GetItems(q);
                        if (collListItems.Count != 0)
                            foreach (SPListItem item in collListItems)
                            {
                                ExtComms.Add(new ExternalComms()
                                {
                                    //OrgEmail = item["OrgEmail"] == null ? string.Empty : item["OrgEmail"].ToString(),
                                    BookID = item["BookID"] == null ? string.Empty : item["BookID"].ToString(),
                                    BookDate = item["BookDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["BookDate"].ToString()).ToShortDateString(),
                                    BookAuthor = item["BookAuthor"] == null ? string.Empty : item["BookAuthor"].ToString(),
                                    BookSubject = item["BookSubject"] == null ? string.Empty : item["BookSubject"].ToString(),
                                    BookDirectedTo = item["DirectedTo"] == null ? string.Empty : item["DirectedTo"].ToString(),
                                    OrgReplyAddress = item["OrgReplyAddress"] == null ? string.Empty : item["OrgReplyAddress"].ToString(),
                                    BookText = item["BookText"] == null ? string.Empty : item["BookText"].ToString(),
                                    RequestID = item["RequestID"] == null ? string.Empty : item["RequestID"].ToString(),
                                    OrgReply = item["OrgReply"] == null ? string.Empty : item["OrgReply"].ToString(),
                                    OrgReplyBookNo = item["OrgReplyBookNo"] == null ? string.Empty : item["OrgReplyBookNo"].ToString(),
                                    OrgReplyBookSubject = item["OrgReplyBookSubject"] == null ? string.Empty : item["OrgReplyBookSubject"].ToString(),
                                    ID = item["ID"] == null ? string.Empty : item["ID"].ToString(),
                                    OrgReplyDate = item["OrgReplyDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["OrgReplyDate"].ToString()).ToShortDateString(),
                                });
                            }
                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(ex);
                    }
                    finally
                    {
                        Logging.GetInstance().Debug("Exiting method ExternalCommunications.GetBooks");
                    }
                }
            }

            return ExtComms;
        }

        public static ExternalComms GetBookDetailsbyID(string itemID)
        {
            ExternalComms ExtComms = new ExternalComms();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method ExternalCommunications.GetBookDetailsbyID");
                            SPList list = web.Lists[Utilities.Constants.PAExternalCommunications];
                            SPListItem item = list.GetItemById(Convert.ToInt32(itemID));
                            if (item != null)
                            {
                                ExtComms.BookID = item["BookID"] == null ? string.Empty : item["BookID"].ToString();
                                ExtComms.ID = item["ID"] == null ? string.Empty : item["ID"].ToString();
                                ExtComms.BookAuthor = item["BookAuthor"] == null ? string.Empty : item["BookAuthor"].ToString();
                                // ExtComms.OrgEmail = item["OrgEmail"] == null ? string.Empty : item["OrgEmail"].ToString();
                                ExtComms.BookDate = item["BookDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["BookDate"].ToString()).ToShortDateString();
                                ExtComms.BookSubject = item["BookSubject"] == null ? string.Empty : item["BookSubject"].ToString();
                                ExtComms.BookDirectedTo = item["DirectedTo"] == null ? string.Empty : item["DirectedTo"].ToString();
                                ExtComms.OrgReplyAddress = item["OrgReplyAddress"] == null ? string.Empty : item["OrgReplyAddress"].ToString();
                                ExtComms.BookText = item["BookText"] == null ? string.Empty : item["BookText"].ToString();
                                ExtComms.RequestID = item["RequestID"] == null ? string.Empty : item["RequestID"].ToString();
                                ExtComms.OrgReply = item["OrgReply"] == null ? string.Empty : item["OrgReply"].ToString();
                                ExtComms.OrgReplyBookNo = item["OrgReplyBookNo"] == null ? string.Empty : item["OrgReplyBookNo"].ToString();
                                ExtComms.OrgReplyBookSubject = item["OrgReplyBookSubject"] == null ? string.Empty : item["OrgReplyBookSubject"].ToString();
                                ExtComms.OrgReplyDate = item["OrgReplyDate"] == null ? DateTime.MinValue.ToString() : Convert.ToDateTime(item["OrgReplyDate"].ToString()).ToShortDateString();
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exiting method ExternalCommunications.GetBookDetailsbyID");
                        }
                    }
                }
            });
            return ExtComms;
        }

        public static void AddNewBook(string reqID, string BookID, string BookSubject, string BookDirectedTo, string OrgAddress, string OrgEmail, string BookText)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method ExternalCommunications.AddNewBook");
                            SPList list = web.Lists[Utilities.Constants.PAExternalCommunications];
                            SPListItem newitem = list.Items.Add();
                            newitem["RequestID"] = new SPFieldLookupValue(Convert.ToInt32(reqID), reqID);
                            newitem["BookID"] = SPHttpUtility.HtmlEncode(BookID);
                            newitem["BookDate"] = DateTime.Now.ToShortDateString();
                            newitem["BookAuthor"] = SPContext.Current.Web.CurrentUser.Name;
                            newitem["BookSubject"] = SPHttpUtility.HtmlEncode(BookSubject);
                            newitem["DirectedTo"] = SPHttpUtility.HtmlEncode(BookDirectedTo);
                            //newitem["OrgEmail"] = SPHttpUtility.HtmlEncode(OrgEmail);
                            newitem["OrgReplyAddress"] = SPHttpUtility.HtmlEncode(OrgAddress);
                            newitem["BookText"] = SPHttpUtility.HtmlEncode(BookText);
                            web.AllowUnsafeUpdates = true;
                            newitem.Update();
                            list.Update();
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                            //handle exception as the business require.
                        }
                        finally
                        { //dispose any objects that require dispose.
                            Logging.GetInstance().Debug("Exiting method ExternalCommunications.AddNewBook");
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                }
            });
        }

        public static void UpdateBookbyOrgReply(string id, string reply, string OrgbookNo)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        try
                        {
                            Logging.GetInstance().Debug("Entering method ExternalCommunications.UpdateBookbyOrgReply");

                            SPList list = web.Lists[Utilities.Constants.PAExternalCommunications];
                            SPListItem itemToUpdate = list.GetItemById(int.Parse(id));
                            itemToUpdate["OrgReply"] = SPHttpUtility.HtmlEncode(reply);
                            //itemToUpdate["OrgBookSubject"] = SPHttpUtility.HtmlEncode(orgBookSubject);
                            itemToUpdate["OrgBookNo"] = SPHttpUtility.HtmlEncode(OrgbookNo);
                            itemToUpdate["OrgReplyDate"] = DateTime.Now;
                            itemToUpdate["OrgReplyby"] = SPContext.Current.Web.CurrentUser.Name;
                            web.AllowUnsafeUpdates = true;
                            itemToUpdate.Update();
                            list.Update();
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                            //handle exception as the business require.
                        }
                        finally
                        { //dispose any objects that require dispose.
                            Logging.GetInstance().Debug("Exiting method ExternalCommunications.UpdateBookbyOrgReply");
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                }
            });
        }
    }
}