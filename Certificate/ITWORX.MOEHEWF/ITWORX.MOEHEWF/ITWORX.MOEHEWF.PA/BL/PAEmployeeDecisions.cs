using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class PAEmployeeDecisions
    {
        public static List<Entities.PAEmployeeDecisions> GetAllByGroupName(string spGroupName)
        {
            Logging.GetInstance().Debug("Entering method PAEmployeeProcedures.GetAll");
            List<Entities.PAEmployeeDecisions> PAEmployeeDecisions = new List<Entities.PAEmployeeDecisions>();
            try
            {
                SPListItemCollection PAItemsCollection = BusinessHelper.PAEmployeeByGroupName(Constants.PAEmployeeDecisions, spGroupName);

                if (PAItemsCollection != null && PAItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in PAItemsCollection)
                    {
                        PAEmployeeDecisions.Add(new Entities.PAEmployeeDecisions() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title, SPGroupName = Convert.ToString(item["SPGroupName"]) });
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
            return PAEmployeeDecisions;
        }
    }
}