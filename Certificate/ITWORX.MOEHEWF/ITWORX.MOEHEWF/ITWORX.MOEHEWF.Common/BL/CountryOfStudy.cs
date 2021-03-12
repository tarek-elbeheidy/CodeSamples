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
   public class CountryOfStudy
    {
        public static List<Entities.CountryOfStudy> GetAll(int LCID)
        {
            Logging.GetInstance().Debug("Entering method CountryOfStudy.GetAll");
            List<Entities.CountryOfStudy> countryOfStudy = new List<Entities.CountryOfStudy>();
            try
            {
                SPListItemCollection countryItemsCollection = BusinessHelper.GetLookupDataOrdered(Constants.CountryOfStudy,LCID);

                if (countryItemsCollection != null && countryItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in countryItemsCollection)
                    {
                        countryOfStudy.Add(new Entities.CountryOfStudy() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method CountryOfStudy.GetAll");
            }
            return countryOfStudy;
        }


        public static List<Entities.CountryOfStudy> GetAllByViewPA(int LCID)
        {
            Logging.GetInstance().Debug("Entering method CountryOfStudy.GetAll");
            List<Entities.CountryOfStudy> countryOfStudy = new List<Entities.CountryOfStudy>();
            try
            {
                SPListItemCollection countryItemsCollection = GetLookupDataOrdered(Constants.CountryOfStudy, LCID ,"ViewPA");

                if (countryItemsCollection != null && countryItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in countryItemsCollection)
                    {
                        countryOfStudy.Add(new Entities.CountryOfStudy() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method CountryOfStudy.GetAll");
            }
            return countryOfStudy;
        }

        public static List<Entities.CountryOfStudy> GetAllByViewUCE(int LCID)
        {
            Logging.GetInstance().Debug("Entering method CountryOfStudy.GetAll");
            List<Entities.CountryOfStudy> countryOfStudy = new List<Entities.CountryOfStudy>();
            try
            {
                 
                SPListItemCollection countryItemsCollection = GetLookupDataOrdered(Constants.CountryOfStudy, LCID,"ViewUCE");

                if (countryItemsCollection != null && countryItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in countryItemsCollection)
                    {
                        countryOfStudy.Add(new Entities.CountryOfStudy() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method CountryOfStudy.GetAll");
            }
            return countryOfStudy;
        }

        public static SPListItemCollection GetLookupDataOrdered(string listName, int LCID,string viewBy)
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
                            if (LCID == 1033)
                            {
                                orderField = "Title";
                            }
                            else
                            {
                                orderField = "TitleAr";
                            }

                            SPQuery spQuery = BusinessHelper.GetQueryObject("<OrderBy><FieldRef Name='" + orderField + "' Ascending='True' /></OrderBy><Where><Eq><FieldRef Name='"+viewBy+"' /><Value Type='Boolean'>1</Value></Eq></Where>",
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

    }

}
