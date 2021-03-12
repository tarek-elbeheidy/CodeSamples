using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.Utilities
{
    public class BusinessHelper
    {
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

                            SPQuery spQuery = GetQueryObject(string.Empty, "<FieldRef Name='ID' /><FieldRef Name='Title' /><FieldRef Name='TitleAr' />");

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SPQuery GetQueryObject(string query, string joins, string projectedFields, string viewFields)
        {
            SPQuery spQuery = GetQueryObject(query, viewFields);
            spQuery.Joins = joins;
            spQuery.ProjectedFields = projectedFields;

            return spQuery;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SPQuery GetQueryObject(string query, string viewFields = "", bool isRecursive = true, uint rowLimit = 300)
        {
            SPQuery spQuery = new SPQuery();
            spQuery.Query = query;
            spQuery.RowLimit = rowLimit;

            if (!string.IsNullOrEmpty(viewFields))
            {
                spQuery.ViewFields = viewFields;
                spQuery.ViewFieldsOnly = true;
            }

            if (isRecursive)
            {
                spQuery.ViewAttributes = "Scope=\"Recursive\"";
            }

            return spQuery;
        }
        public static SPListItemCollection GetLookupDataOrdered(string listName, int LCID)
        {
            Logging.GetInstance().Debug("Entering BusinessHelper.GetLookupDataOrdered");
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
                            string orderField = string.Empty;
                            if (LCID == (uint)Language.English)
                            {
                                orderField = "Title";
                            }
                            else
                            {
                                orderField = "TitleAr";
                            }

                            SPQuery spQuery = GetQueryObject("<OrderBy><FieldRef Name='" + orderField + "' Ascending='True' /></OrderBy>",
                            "<FieldRef Name='ID' /><FieldRef Name='Title' /><FieldRef Name='TitleAr' />");

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
                Logging.GetInstance().Debug("Exit BusinessHelper.GetLookupDataOrdered");
            }
            return itemsCollection;
        }

        public static SPFolder CreateFolderInternal(SPList list, SPFolder parentFolder, string folderUrl)
        {
            var folderNames = folderUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var folderName = folderNames[0];

            var curFolder =
                parentFolder.SubFolders.Cast<SPFolder>()
                            .FirstOrDefault(
                                f =>
                                System.String.Compare(f.Name, folderName, System.StringComparison.OrdinalIgnoreCase) ==
                                0);
            if (curFolder == null)
            {
                SPSecurity.RunWithElevatedPrivileges(() =>
                {
                    var folderItem = list.Items.Add(parentFolder.ServerRelativeUrl, SPFileSystemObjectType.Folder, folderName);
                    folderItem.SystemUpdate();
                    curFolder = folderItem.Folder;
                });
            }
            if (folderNames.Length > 1)
            {
                var subFolderUrl = string.Join("/", folderNames, 1, folderNames.Length - 1);
                return CreateFolderInternal(list, curFolder, subFolderUrl);
            }
            return curFolder;
        }

        public static string CreateCAMLQuery(List<string> parameters, string orAndCondition, bool isIncludeWhereClause)
        {
            StringBuilder sb = new StringBuilder();
            if (parameters.Count == 0)
            {
                AppendEQ(sb, "all");
            }
            int j = 0;
            for (int i = 0; i < parameters.Count; i++)
            {
                if (!string.IsNullOrEmpty(parameters[i].Split(';')[3]))
                {
                    AppendEQ(sb, parameters[i]);

                    if (i > 0 && j > 0)
                    {
                        sb.Insert(0, "<" + orAndCondition + ">");
                        sb.Append("</" + orAndCondition + ">");
                    }
                    j++;
                }
            }
            if (isIncludeWhereClause)
            {
                sb.Insert(0, "<Where>");
                sb.Append("</Where>");
            }
            return sb.ToString();
        }

        public static void AppendEQ(StringBuilder sb, string value)
        {
            string[] field = value.Split(';');
            sb.AppendFormat("<{0}>", field[2].ToString());
            sb.AppendFormat("<FieldRef Name='{0}'/>", field[0].ToString());
            //if()IncludeTimeValue='FALSE'
            sb.AppendFormat("<Value Type='{0}'>{1}</Value>", field[1].ToString(), field[3].ToString());
            //else
            sb.AppendFormat("</{0}>", field[2].ToString());
        }
        public static SPFieldLookupValue GetLookupFieldFromValue(string lookupValue, string lookupSourceColumn, string lookupSourceList)

        {
            SPFieldLookupValue value = null;
            using (SPSite site = new SPSite(SPContext.Current.Site.Url))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    Logging.GetInstance().Debug("Enter NewRequests.lnk_CheckOut_Click");
                    try
                    {

                        SPQuery query = GetQueryObject("<Where><Eq><FieldRef Name='" + lookupSourceColumn + "'/><Value Type='Counter'>" +
                            lookupValue + "</Value></Eq></Where>");

                        SPListItemCollection listItems = web.Lists[lookupSourceList].GetItems(query);

                        if (listItems.Count > 0)
                            value = new SPFieldLookupValue(listItems[0].ID, listItems[0].Fields["ApplicantDescriptionEn"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Logging.GetInstance().LogException(ex);
                    }
                    finally
                    {
                        Logging.GetInstance().Debug("Exit NewRequests.lnk_CheckOut_Click");
                    }
                }

            }
            return value;
        }

 
    }
}
