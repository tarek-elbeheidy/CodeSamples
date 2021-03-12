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
  public  class Occupation
    {
        public static List<Entities.Occupation> GetAll()
        {
            Logging.GetInstance().Debug("Entering method Occupation.GetAll");
            List<Entities.Occupation> occupation = new List<Entities.Occupation>();
            try
            {
                SPListItemCollection occupationItemsCollection = BusinessHelper.GetLookupData(Constants.Occupation);

                if (occupationItemsCollection !=null && occupationItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in occupationItemsCollection)
                    {
                        occupation.Add(new Entities.Occupation() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method Occupation.GetAll");
            }
            return occupation;
        }
    }
}
