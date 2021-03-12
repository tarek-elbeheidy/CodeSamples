using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class StudyLanguage
    {
        public static List<Entities.StudyLanguage> GetAll()
        {
            Logging.GetInstance().Debug("Entering method StudyLanguage.GetAll");
            List<Entities.StudyLanguage> studyLanguage = new List<Entities.StudyLanguage>();
            try
            {
                SPListItemCollection studyLanguageItemsCollection = BusinessHelper.GetLookupData(Constants.StudyLanguage);

                if (studyLanguageItemsCollection != null && studyLanguageItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in studyLanguageItemsCollection)
                    {
                        studyLanguage.Add(new Entities.StudyLanguage() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method StudyLanguage.GetAll");
            }
            return studyLanguage;
        }
    }
}