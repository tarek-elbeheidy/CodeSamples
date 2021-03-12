using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class StudyAttachmentTypes
    {
        public static List<Entities.StudyAttachmentTypes> GetAll()
        {
            Logging.GetInstance().Debug("Entering method StudyAttachmentTypes.GetAll");
            List<Entities.StudyAttachmentTypes> studyAttachTypes = new List<Entities.StudyAttachmentTypes>();
            try
            {
                //SPListItemCollection academicItemsCollection = BusinessHelper.GetLookupData(Constants.AcademicDegree);
                List<string> viewFields = new List<string>() { "ID", "Title", "TitleAr" };
                string siteUrl = SPContext.Current.Site.Url + HelperMethods.GetConfigurationValue("", Common.Utilities.Constants.Configuration, "PARelativeSiteUrl");
                SPListItemCollection studyAttachTypesItemsCollection = HelperMethods.GetLookupData(siteUrl, PA.Utilities.Constants.StudyAttachmentTypes, viewFields);

                if (studyAttachTypesItemsCollection != null && studyAttachTypesItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in studyAttachTypesItemsCollection)
                    {
                        studyAttachTypes.Add(new Entities.StudyAttachmentTypes() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method StudyAttachmentTypes.GetAll");
            }
            return studyAttachTypes;
        }
    }
}