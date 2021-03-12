using Microsoft.SharePoint;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.SharePoint.Utilities;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHEWF.DeleteDocument.Utilities;

namespace ITWORX.MOEHEWF.DeleteDocument
{
    public class BusinessHelper
    {
        public static void DeleteDocuments(DateTime dateTime)
        {
            try
            {
                DeletedDocumentLogger.GetInstance().Debug("Entering Method DeleteDocuments, Namespace ITWORX.MOEHEWF.DeleteDocument");
                string webURl = Helper.GetConfigValue("WebUrl");
                string DocumentLibraries = HelperMethods.GetConfigurationValue(webURl, Helper.GetConfigValue("ConfigurationList"),
                    Helper.GetConfigValue("DocumentLibraries"));
                string[] Libraries = DocumentLibraries.Split(',');

                using (SPSite site = new SPSite(webURl))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        web.AllowUnsafeUpdates = true;
                        foreach (var library in Libraries)
                        {

                            SPList sPList = web.Lists.TryGetList(library);
                            if (sPList != null)
                            {
                                if (sPList.Items != null && sPList.Items.Count > 0)
                                {
                                    string fromHour = SPUtility.CreateISO8601DateTimeFromSystemDateTime(dateTime.AddHours(
                                        -1* Convert.ToInt32(HelperMethods.GetConfigurationValue(Helper.GetConfigValue("WebUrl"),
                                        Helper.GetConfigValue("ConfigurationList"),
                                        Helper.GetConfigValue("LimitedHoursToSavebeforeDeletion")))));
                                    SPQuery qry = new SPQuery();
                                    qry.Query = "<Where><And><Neq><FieldRef Name='DocumentStatus'/>" +
                                                  "<Value Type='Choice'>Saved</Value></Neq><Lt><FieldRef Name='Modified'/>" +
                                                                                                     "<Value Type='DateTime'IncludeTimeValue='True'>" + fromHour + "</Value></Lt></And></Where>";

                                    qry.ViewAttributes = "Scope=Recursive";// "<FieldRef Name='DocumentStatus'/>";//<FieldRef Name='Modified'/>";
                                    SPListItemCollection items = sPList.GetItems(qry);
                                    List<int> listId = new List<int>();
                                    foreach (SPListItem itm in items)
                                    {
                                        listId.Add(itm.ID);
                                    }

                                    foreach (int idToDelete in listId)
                                    {
                                        SPListItem ItemToDelete = sPList.GetItemById(idToDelete);
                                        ItemToDelete.Delete();
                                    }

                                }

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
               DeletedDocumentLogger.GetInstance().LogException(ex);
            }
            finally
            {
                DeletedDocumentLogger.GetInstance().Debug("Exit Method DeleteDocuments, Namespace ITWORX.MOEHEWF.DeleteDocument");
            }
        }

    }
    }
