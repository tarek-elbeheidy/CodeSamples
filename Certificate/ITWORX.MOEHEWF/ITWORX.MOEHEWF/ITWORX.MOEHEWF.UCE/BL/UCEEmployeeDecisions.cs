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
    public class UCEEmployeeDecisions
    {
        public static List<Entities.UCEEmployeeDecisions> GetAllByGroupName(string spGroupName)
        {
            Logging.GetInstance().Debug("Entering method UCEEmployeeProcedures.GetAll");
            List<Entities.UCEEmployeeDecisions> uCEEmployeeDecisions = new List<Entities.UCEEmployeeDecisions>();
            try
            {
                SPListItemCollection uCEItemsCollection = BusinessHelper.UCEEmployeeByGroupName(Constants.UCEEmployeeDecisions, spGroupName);

                if (uCEItemsCollection != null && uCEItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in uCEItemsCollection)
                    {
                        uCEEmployeeDecisions.Add(new Entities.UCEEmployeeDecisions() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title, SPGroupName = Convert.ToString(item["SPGroupName"]) });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method UCEEmployeeProcedures.GetAll");
            }
            return uCEEmployeeDecisions;
        }
    }
}
