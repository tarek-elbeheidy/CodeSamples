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
    public class EntityNeedsEquivalency
    {
        public static List<Entities.EntityNeedsEquivalency> GetAll()
        {
            Logging.GetInstance().Debug("Entering method EntityNeedsEquivalency.GetAll");
            List<Entities.EntityNeedsEquivalency> entityNeedsEquivalency = new List<Entities.EntityNeedsEquivalency>();
            try
            {
                SPListItemCollection entityNeedsEquivalencyItemsCollection = BusinessHelper.GetLookupData(Constants.EntityNeedsEquivalency);

                if (entityNeedsEquivalencyItemsCollection !=null && entityNeedsEquivalencyItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in entityNeedsEquivalencyItemsCollection)
                    {
                        entityNeedsEquivalency.Add(new Entities.EntityNeedsEquivalency() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method EntityNeedsEquivalency.GetAll");
            }
            return entityNeedsEquivalency;
        }
    }
}
