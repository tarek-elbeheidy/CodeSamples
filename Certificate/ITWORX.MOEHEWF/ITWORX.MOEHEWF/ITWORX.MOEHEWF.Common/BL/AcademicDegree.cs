using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.Common.BL
{
    public class AcademicDegree
    {
        public static List<Entities.AcademicDegree> GetAll()
        {
            Logging.GetInstance().Debug("Entering method AcademicDegree.GetAll");
            List<Entities.AcademicDegree> academicDegree = new List<Entities.AcademicDegree>();
            try
            {
                SPListItemCollection academicItemsCollection = BusinessHelper.GetLookupData(Constants.AcademicDegree);

                if (academicItemsCollection != null && academicItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in academicItemsCollection)
                    {
                        academicDegree.Add(new Entities.AcademicDegree() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method AcademicDegree.GetAll");
            }
            return academicDegree;
        }
    }
}