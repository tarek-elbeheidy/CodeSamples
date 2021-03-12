using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class EntityProvidingStudy
    {
        public static List<Entities.EntityProvidingStudy> GetAll()
        {
            Logging.GetInstance().Debug("Entering method EntityProvidingStudy.GetAll");
            List<Entities.EntityProvidingStudy> entityProvidingStudy = new List<Entities.EntityProvidingStudy>();
            try
            {
                SPListItemCollection entityItemsCollection = BusinessHelper.GetLookupData(Constants.EntityProvidingStudy);

                if (entityItemsCollection != null && entityItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in entityItemsCollection)
                    {
                        entityProvidingStudy.Add(new Entities.EntityProvidingStudy() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method EntityProvidingStudy.GetAll");
            }
            return entityProvidingStudy;
        }
    }
}