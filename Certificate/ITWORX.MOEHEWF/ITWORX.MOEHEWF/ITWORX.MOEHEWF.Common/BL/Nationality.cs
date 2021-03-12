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
  public class Nationality
    {
        public static List<Entities.Nationality> GetAll()
        {
            Logging.GetInstance().Debug("Entering method Nationality.GetAll");
            List<Entities.Nationality> nationality = new List<Entities.Nationality>();
            try
            {
                SPListItemCollection nationalityItemsCollection = BusinessHelper.GetLookupData(Constants.Nationality);

                if (nationalityItemsCollection != null && nationalityItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in nationalityItemsCollection)
                    {
                        nationality.Add(new Entities.Nationality() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method Nationality.GetAll");
            }
            return nationality;
        }
        public static int GetNationalityID(string Value)
        {
            List<Entities.Nationality> nationality=GetAll();
            foreach (Entities.Nationality it in nationality)
            {
                if (it.EnglishTitle.Equals(Value) || it.ArabicTitle.Equals(Value))
                {
                    return it.ID;
                }
            }
            return -1;
        }

        public static List<Entities.Nationality> GetOrderedNationality()
        {
            Logging.GetInstance().Debug("Entering method Nationality.GetOrderedNationality");
            List<Entities.Nationality> nationality = new List<Entities.Nationality>();
            try
            {
                SPListItemCollection nationalityItemsCollection = BusinessHelper.GetLookupData(Constants.Nationality);
               
                if (nationalityItemsCollection != null && nationalityItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in nationalityItemsCollection)
                    {
                        nationality.Add(new Entities.Nationality() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                    if (SPContext.Current.Web.Language == 1033)
                    {
                        nationality.OrderBy(n => n.EnglishTitle);
                    }
                    else
                    {
                        nationality.OrderBy(n => n.ArabicTitle);
                    }
                   
                   
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method Nationality.GetOrderedNationality");
            }
            return nationality;
        }

        public static SPListItem GetNationalityISOCode(string ISOCode)
        {
            Logging.GetInstance().Debug("Entering method Nationality.GetNationalityISOCode");
            SPListItem item = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList requestsList = web.Lists[Utilities.Constants.Nationality];
                            if (requestsList == null)
                                throw new Exception();


                            SPQuery reqQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='ISOCode' /><Value Type='Text'>" +
                                ISOCode + "</Value></Eq></Where>");

                            SPListItemCollection requestItemCollection = requestsList.GetItems(reqQuery);
                            if (requestItemCollection != null && requestItemCollection.Count > 0)
                            {
                                item = requestItemCollection[0];
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
                Logging.GetInstance().Debug("Exiting method Nationality.GetNationalityISOCode");
            }
            return item;
        }
    }

}
