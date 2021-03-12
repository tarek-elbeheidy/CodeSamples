using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.BL
{
    public class StatementOrganizations
    {
        public static List<Entities.StatementOrganizations> GetAll()
        {
            Logging.GetInstance().Debug("Entering method StatementOrganizations.GetAll");
            List<Entities.StatementOrganizations> statementOrganizations = new List<Entities.StatementOrganizations>();
            try
            {
                SPListItemCollection itemsCollection = GetLookupData(Constants.StatementOrganizations);

                if (itemsCollection != null && itemsCollection.Count > 0)
                {
                    foreach (SPListItem item in itemsCollection)
                    {
                        statementOrganizations.Add(new Entities.StatementOrganizations() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title, SPGroupName = Convert.ToString(item["SPGroupName"]) });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method UCEEmployeeProcedures.GetAll");
            }
            return statementOrganizations;
        }

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

                            SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject(string.Empty, "<FieldRef Name='ID' /><FieldRef Name='Title' /><FieldRef Name='TitleAr' /><FieldRef Name='SPGroupName' />");
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


        public static SPListItem GetStatementOrganizationByID(int Id)
        {
            Logging.GetInstance().Debug("Entering method StatementOrganizations.GetStatementOrganizationByID");
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
                            SPList statementOrganizations = web.Lists[Utilities.Constants.StatementOrganizations];
                            if (statementOrganizations == null)
                                throw new Exception();


                            SPQuery reqQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" +
                                Id + "</Value></Eq></Where>");
                            SPListItemCollection itemCollection = statementOrganizations.GetItems(reqQuery);
                            if (itemCollection != null && itemCollection.Count > 0)
                            {
                              item= itemCollection[0];
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
                Logging.GetInstance().Debug("Exiting method StatementOrganizations.GetStatementOrganizationByID");
            }
            return item;
        }
    }
}
