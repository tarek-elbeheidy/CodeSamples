using ITWORX.MOEHE.Utilities;
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
    public class AcademicDegree
    {
        public static List<Entities.AcademicDegree> GetAll()
        {
            Logging.GetInstance().Debug("Entering method AcademicDegree.GetAll");
            List<Entities.AcademicDegree> academicDegree = new List<Entities.AcademicDegree>();
            try
            {
                //SPListItemCollection academicItemsCollection = BusinessHelper.GetLookupData(Constants.AcademicDegree);
                List<string> viewFields = new List<string>() { "ID","Title", "TitleAr" };
                string siteUrl = SPContext.Current.Site.Url + HelperMethods.GetConfigurationValue("", Common.Utilities.Constants.Configuration, "UCERelativeSiteUrl");
                SPListItemCollection academicItemsCollection = HelperMethods.GetLookupData(siteUrl, UCE.Utilities.Constants.AcademicDegree, viewFields);

                if (academicItemsCollection!=null && academicItemsCollection.Count > 0)
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
