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
    public class RequestPhase
    {
        public static List<Entities.RequestPhase> GetAll()
        {
            Logging.GetInstance().Debug("Entering method RequestPhase.GetAll");
            List<Entities.RequestPhase> requestPhase = new List<Entities.RequestPhase>();
            try
            {
                SPListItemCollection requestPhaseItemsCollection = BusinessHelper.GetLookupData(Constants.RequestPhase);

                if (requestPhaseItemsCollection != null && requestPhaseItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in requestPhaseItemsCollection)
                    {
                        requestPhase.Add(new Entities.RequestPhase() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method RequestPhase.GetAll");
            }
            return requestPhase;
        }
    }
}
