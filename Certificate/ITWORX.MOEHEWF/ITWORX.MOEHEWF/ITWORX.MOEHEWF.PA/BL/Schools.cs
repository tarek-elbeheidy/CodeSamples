using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class Schools
    {
        public static List<Entities.Schools> GetAll()
        {
            Logging.GetInstance().Debug("Entering method Schools.GetAll");
            List<Entities.Schools> Schools = new List<Entities.Schools>();
            try
            {
                SPListItemCollection SchoolsItemsCollection = BusinessHelper.GetLookupData(Utilities.Constants.Schools);

                if (SchoolsItemsCollection != null && SchoolsItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in SchoolsItemsCollection)
                    {
                        Schools.Add(new Entities.Schools() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method Schools.GetAll");
            }
            return Schools;
        }
    }
}
