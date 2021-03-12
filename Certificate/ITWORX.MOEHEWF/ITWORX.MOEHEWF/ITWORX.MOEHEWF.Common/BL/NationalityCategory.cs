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

        public static int GetNationalityCategoryID(string Value)
        {
            List<Entities.NationalityCategory> nationalityCategory = GetAll();
            foreach (Entities.NationalityCategory it in nationalityCategory)
            {
                if (it.EnglishTitle.Equals(Value) || it.ArabicTitle.Equals(Value))
                {
                    return it.ID;
                }
            }
            return -1;
        }
    }
}

