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
   public class Nationality
    {
        public static List<Entities.Nationality> GetAll()
        {
            Logging.GetInstance().Debug("Entering method Nationality.GetAll");
            List<Entities.Nationality> nationality = new List<Entities.Nationality>();
            try
            {
                SPListItemCollection nationalityItemsCollection = BusinessHelper.GetLookupData(Constants.Nationality);

                if (nationalityItemsCollection !=null && nationalityItemsCollection.Count > 0)
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

        public static SPListItem GetNationalityByISOCode(string iSOCode)
        {
            Logging.GetInstance().Debug("Entering method Nationality.GetNationalityByISOCode");
            SPListItem item = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList nationalityList = web.Lists[Utilities.Constants.Nationality];
                            if (nationalityList == null)
                                throw new Exception();


                            SPQuery reqQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='ISOCode' /><Value Type='Text'>" +
                                iSOCode + "</Value></Eq></Where>");

                            SPListItemCollection nationalityItemCollection = nationalityList.GetItems(reqQuery);
                            if (nationalityItemCollection != null && nationalityItemCollection.Count > 0)
                            {
                                item = nationalityItemCollection[0];
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
                Logging.GetInstance().Debug("Exiting method Nationality.GetNationalityByISOCode");
            }
            return item;
        }
    }
}
