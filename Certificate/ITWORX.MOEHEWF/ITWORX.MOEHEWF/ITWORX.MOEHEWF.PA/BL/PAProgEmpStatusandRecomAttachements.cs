using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.PA.BL
{
   public class PAProgEmpStatusandRecomAttachements
    {
        public static List<Entities.PAProgEmpStatusandRecomAttachements> GetProgEmpStatusandRecomAttachements(string requestNumber,string documentStatus)
        {
            Logging.GetInstance().Debug("Entering PAProgEmpStatusandRecomAttachements.GetProgEmpStatusandRecomAttachements");
            List<Entities.PAProgEmpStatusandRecomAttachements> progEmpStatusandRecomAttachements = new List<Entities.PAProgEmpStatusandRecomAttachements>();
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList StatusandRecomAttachementsList = web.Lists[Constants.PAProgEmpStatusandRecomAttachements];
                            if (StatusandRecomAttachementsList == null)
                                throw new Exception();

                            SPQuery statusandRecomAttachementsQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><Eq><FieldRef Name='RequestID' /><Value Type='Lookup'>" + requestNumber + "</Value></Eq><Eq><FieldRef Name='DocumentStatus' /><Value Type='Choice'>" + documentStatus + "</Value></Eq></And></Where>");

                            SPListItemCollection StatusandRecomAttachementsCollection = StatusandRecomAttachementsList.GetItems(statusandRecomAttachementsQuery);

                            if (StatusandRecomAttachementsCollection != null && StatusandRecomAttachementsCollection.Count > 0)
                            {
                                 
                                foreach (SPListItem item in StatusandRecomAttachementsCollection)
                                {
                                    SPFieldLookupValue RequestID = new SPFieldLookupValue((item["RequestID"] != null) ? item["RequestID"].ToString() : string.Empty);
                                    progEmpStatusandRecomAttachements.Add(new Entities.PAProgEmpStatusandRecomAttachements()
                                    {
                                        RequestID = RequestID.LookupValue,
                                        ID = item["ID"] == null ? string.Empty : item["ID"].ToString(),
                                        Group = item["Group"] == null ? string.Empty : item["Group"].ToString(),

                                    });
                                }
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit PAProgEmpStatusandRecomAttachements.GetProgEmpStatusandRecomAttachements");
            }
            return progEmpStatusandRecomAttachements;
        }
    }
}
