using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class PAEmployeeProcedures
    {
        public static List<Entities.PAEmployeeProcedures> GetAllByGroupName(string spGroupName)
        {
            Logging.GetInstance().Debug("Entering method PAEmployeeProcedures.GetAll");
            List<Entities.PAEmployeeProcedures> PAEmployeeProcedures = new List<Entities.PAEmployeeProcedures>();
            try
            {
                SPListItemCollection PAItemsCollection = BusinessHelper.PAEmployeeByGroupName(Constants.PAEmployeeProcedures, spGroupName);

                if (PAItemsCollection != null && PAItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in PAItemsCollection)
                    {
                        PAEmployeeProcedures.Add(new Entities.PAEmployeeProcedures() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title, SPGroupName = Convert.ToString(item["SPGroupName"]) });
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method PAEmployeeProcedures.GetAll");
            }
            return PAEmployeeProcedures;
        }
    }
}