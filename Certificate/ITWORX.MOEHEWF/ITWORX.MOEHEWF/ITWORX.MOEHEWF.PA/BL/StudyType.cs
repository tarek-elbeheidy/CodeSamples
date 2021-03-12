using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class StudyType
    {
        public static List<Entities.StudyType> GetAll()
        {
            Logging.GetInstance().Debug("Entering method StudyType.GetAll");
            List<Entities.StudyType> studyType = new List<Entities.StudyType>();
            try
            {
                SPListItemCollection studyTypeItemsCollection = BusinessHelper.GetLookupData(Constants.StudyType);

                if (studyTypeItemsCollection != null && studyTypeItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in studyTypeItemsCollection)
                    {
                        studyType.Add(new Entities.StudyType() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method StudyType.GetAll");
            }
            return studyType;
        }
    }
}