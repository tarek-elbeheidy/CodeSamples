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
   public class UCEEmployeeProcedures
    {
        public static List<Entities.UCEEmployeeProcedures> GetAllByGroupName(string spGroupName)
        {
            Logging.GetInstance().Debug("Entering method UCEEmployeeProcedures.GetAll");
            List<Entities.UCEEmployeeProcedures> uCEEmployeeProcedures = new List<Entities.UCEEmployeeProcedures>();
            try
            {
                SPListItemCollection uCEItemsCollection = BusinessHelper.UCEEmployeeByGroupName(Constants.UCEEmployeeProcedures,spGroupName);

                if (uCEItemsCollection != null && uCEItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in uCEItemsCollection)
                    {
                        uCEEmployeeProcedures.Add(new Entities.UCEEmployeeProcedures() { ID = Convert.ToInt32(item["OrderNo"].ToString()), ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title,SPGroupName= Convert.ToString(item["SPGroupName"]) });
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
            return uCEEmployeeProcedures;
        }
    }
}
