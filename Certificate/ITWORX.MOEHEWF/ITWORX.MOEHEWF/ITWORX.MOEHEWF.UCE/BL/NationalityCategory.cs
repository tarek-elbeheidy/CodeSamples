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
   public class NationalityCategory
    {
        public static List<Entities.NationalityCategory> GetAll()
        {
            Logging.GetInstance().Debug("Entering method NationalityCategory.GetAll");
            List<Entities.NationalityCategory> nationalityCategory = new List<Entities.NationalityCategory>();
            try
            {
                SPListItemCollection nationalityCategoryItemsCollection = BusinessHelper.GetLookupData(Constants.NationalityCategory);

                if (nationalityCategoryItemsCollection != null && nationalityCategoryItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in nationalityCategoryItemsCollection)
                    {
                        nationalityCategory.Add(new Entities.NationalityCategory() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method NationalityCategory.GetAll");
            }
            return nationalityCategory;
        }
    }
}
