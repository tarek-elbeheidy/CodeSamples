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
   public class StudySystem
    {
        public static List<Entities.StudySystem> GetAll()
        {
            Logging.GetInstance().Debug("Entering method StudySystem.GetAll");
            List<Entities.StudySystem> studySystem = new List<Entities.StudySystem>();
            try
            {
                SPListItemCollection studySystemItemsCollection = BusinessHelper.GetLookupData(Constants.StudySystem);

                if (studySystemItemsCollection != null && studySystemItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in studySystemItemsCollection)
                    {
                        studySystem.Add(new Entities.StudySystem() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method StudySystem.GetAll");
            }
            return studySystem;
        }
    }
}
