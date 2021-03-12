using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.UCE.BL
{
    public class DelegationDocuments
    {
        public static Entities.DelegationDocuments GetDocumentByDelegationId(int itemId, SPWeb web)
        {
            Logging.GetInstance().Debug("Enter DelegationDocuments.GetDocumentByDelegationId");

            Entities.DelegationDocuments delegationDocuments = new Entities.DelegationDocuments();
            try
            {
                SPList delegationDocumentsLibrary = web.Lists[Utilities.Constants.DelegationDocuments];
                if (delegationDocumentsLibrary == null)
                    throw new Exception();

                SPQuery documentsQuery = Common.Utilities.BusinessHelper.GetQueryObject(@"<Where><Eq>
         <FieldRef Name='DelegationTemplateID' LookupId='TRUE' />
         <Value Type='Lookup'>" + itemId + "</Value></Eq></Where>");

                SPListItemCollection delegatesCollection = delegationDocumentsLibrary.GetItems(documentsQuery);
                if (delegatesCollection !=null && delegatesCollection.Count > 0)
                {

                    delegationDocuments.ID = delegatesCollection[0].ID;
                    delegationDocuments.DelegationDocument = delegatesCollection[0].File;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exit DelegationDocuments.GetDocumentByDelegationId");
            }
            return delegationDocuments;

        }

        public static void UploadDelegationDocumentFile(FileUpload fileUpload, int itemId, string requestNumber)
        {
            Logging.GetInstance().Debug("Entering DelegationDocuments.UploadFile");
            SPWeb web = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {

                            SPList delegationList = web.Lists[Utilities.Constants.DelegationDocuments];
                            if (delegationList == null)
                                throw new Exception();
                            SPFolder delegationDocumentsLibrary = delegationList.RootFolder;

                            web.AllowUnsafeUpdates = true;


                            // Upload document

                            SPFile spfile = delegationDocumentsLibrary.Files.Add(fileUpload.PostedFile.FileName, fileUpload.PostedFile.InputStream, true);

                            // Commit 
                            delegationDocumentsLibrary.Update();

                            SPListItem delegationItem = delegationList.Items[spfile.UniqueId];
                            if (delegationItem != null)
                            {
                                delegationItem["DelegationTemplateID"] = new SPFieldLookupValue(itemId, Convert.ToString(itemId));
                                delegationItem["RequestID"] = new SPFieldLookupValue(int.Parse(requestNumber), requestNumber);
                                delegationItem.Update();
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
                Logging.GetInstance().Debug("Exit DelegationDocuments.UploadFile");
            }
        }

        public static List<Entities.DelegationDocuments> GetGelegatesDocuments(int requestNumber, int LCID)
        {
            Logging.GetInstance().Debug("Entering DelegationDocuments.GetGelegatesDocuments");
            List<Entities.DelegationDocuments> delegationDocumentsData = new List<Entities.DelegationDocuments>();
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList delegationDocuments = web.Lists[Utilities.Constants.DelegationDocuments];
                            SPList delegationTemplates = web.Lists[Common.Utilities.Constants.DelegationTemplate];
                            if (delegationDocuments == null || delegationTemplates == null)
                                throw new Exception();

                            SPQuery documentsQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" + 
                                requestNumber + "</Value></Eq></Where>");

                            SPQuery templatesQuery = Common.Utilities.BusinessHelper.GetQueryObject(string.Empty, "<FieldRef Name='ID' /><FieldRef Name='Title' /><FieldRef Name='TitleAr' />");

                            SPListItemCollection delegateCollection = delegationDocuments.GetItems(documentsQuery);
                            SPListItemCollection templateCollection = delegationTemplates.GetItems(templatesQuery);

                            if ((delegateCollection != null && delegateCollection.Count > 0 ) && (templateCollection != null || templateCollection.Count > 0))
                            {

                                delegationDocumentsData = (from SPListItem item in delegateCollection
                                                           select new Entities.DelegationDocuments
                                                           {
                                                               ID = item.ID,
                                                               FileName = item.File.Name,
                                                               DelegationTempFileName = (from SPListItem tempItem in templateCollection
                                                                                         where tempItem.ID == new SPFieldLookupValue(item["DelegationTemplateID"].ToString()).LookupId
                                                                                         select (LCID == (int)Language.English) ?
                                                                                       Convert.ToString(tempItem["Title"]) : Convert.ToString(tempItem["TitleAr"])).SingleOrDefault()

                                                           }).ToList();
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

                Logging.GetInstance().Debug("Exit DelegationDocuments.GetGelegatesDocuments");
            }
            return delegationDocumentsData;
        }

        public static void DeleteDelegationDocuments(int requestNumber)
        {
            Logging.GetInstance().Debug("Entering DelegationDocuments.DeleteDelegationDocuments");
            SPWeb web = null;
           
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (web = site.OpenWeb())
                        {
                            SPQuery delegationQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" +
                                requestNumber + "</Value></Eq></Where>");

                            SPList delegationDocumentist = web.Lists[Utilities.Constants.DelegationDocuments];
                            SPListItemCollection delegationCollection = delegationDocumentist.GetItems(delegationQuery);
                            web.AllowUnsafeUpdates = true;
                            if (delegationCollection !=null && delegationCollection.Count > 0)
                            {
                                foreach (SPListItem item in delegationCollection)
                                {
                                    delegationDocumentist.Items.DeleteItemById(item.ID);
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
                Logging.GetInstance().Debug("Exit DelegationDocuments.DeleteDelegationDocuments");
            }

        }
    }
    }
