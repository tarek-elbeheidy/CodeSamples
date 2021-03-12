using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
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
    }
}