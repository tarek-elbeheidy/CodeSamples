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
    public class UniversityMainCountry
    {
        public static List<Entities.UniversityMainCountry> GetAll()
        {
            Logging.GetInstance().Debug("Entering method UniversityMainCountry.GetAll");
            List<Entities.UniversityMainCountry> universityMainCountry = new List<Entities.UniversityMainCountry>();
            try
            {
                SPListItemCollection universityMainCountryItemsCollection = BusinessHelper.GetLookupData(Constants.UniversityMainCountry);

                if (universityMainCountryItemsCollection!=null && universityMainCountryItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in universityMainCountryItemsCollection)
                    {
                        universityMainCountry.Add(new Entities.UniversityMainCountry() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method UniversityMainCountry.GetAll");
            }
            return universityMainCountry;
        }
    }
}
