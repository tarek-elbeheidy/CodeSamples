using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.UCE.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.BL
{
   public class RequestTypes
    {
        public static List<Entities.RequestTypes> GetAll()
        {
            Logging.GetInstance().Debug("Entering method RequestTypes.GetAll");
            List<Entities.RequestTypes> requestTypes = new List<Entities.RequestTypes>();
            try
            {
                SPListItemCollection requestTypesItemsCollection = BusinessHelper.GetLookupData(Constants.RequestTypes);

                if (requestTypesItemsCollection !=null || requestTypesItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in requestTypesItemsCollection)
                    {
                        requestTypes.Add(new Entities.RequestTypes() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method RequestTypes.GetAll");
            }
            return requestTypes;
        }
        public static string GetPageLink(string requestTypeID)
        {
            Logging.GetInstance().Debug("Entering method RequestTypes.GetPageLink");
            string pageLink = string.Empty;
                try
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                        {
                            using (SPWeb web = site.OpenWeb())
                            {
                                SPList spList = web.Lists[Constants.RequestTypes];


                                if (spList == null)
                                    throw new Exception();

                                SPQuery spQuery = new SPQuery();
                                spQuery.Query = "< Where >< Eq >< FieldRef Name = 'ID' />< Value Type = 'Counter' >" + int.Parse(requestTypeID) + "</ Value ></ Eq ></ Where >";
                                spQuery.ViewFields = "<FieldRef Name='ID' /><FieldRef Name='PageLink' />";
                                spQuery.ViewFieldsOnly = true;
                                SPListItemCollection itemsCollection = spList.GetItems(spQuery);
                                if (itemsCollection!=null || itemsCollection.Count > 0)
                                {
                                    pageLink =Convert.ToString(itemsCollection[0]["PageLink"]);
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

                    Logging.GetInstance().Debug("Exiting method RequestTypes.GetPageLink");
                }

                return pageLink;
            }
            
            
    }
}
