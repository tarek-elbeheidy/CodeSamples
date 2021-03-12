using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class EntityWorkingFor
    {
        public static List<Entities.EntityWorkingFor> GetAll()
        {
            Logging.GetInstance().Debug("Entering method EntityWorkingFor.GetAll");
            List<Entities.EntityWorkingFor> entityWorkingFor = new List<Entities.EntityWorkingFor>();
            try
            {
                SPListItemCollection entityWorkingForItemsCollection = BusinessHelper.GetLookupData(Constants.EntityWorkingFor);

                if (entityWorkingForItemsCollection != null && entityWorkingForItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in entityWorkingForItemsCollection)
                    {
                        entityWorkingFor.Add(new Entities.EntityWorkingFor() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method EntityWorkingFor.GetAll");
            }
            return entityWorkingFor;
        }
    }
}