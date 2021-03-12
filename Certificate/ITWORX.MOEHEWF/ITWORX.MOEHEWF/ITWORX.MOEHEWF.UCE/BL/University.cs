using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.BL
{
    public class UniversityLookup
    {
        public static bool ReturnIsUniversityHEDD(int universityId)
        {
            Logging.GetInstance().Debug("Entering BLL.Universtity.ReturnIsUniversityHEDD");
            bool HEDD = false;
            SPListItemCollection itemsCollection = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList spList = web.Lists[Utilities.Constants.University];

                            //What shall i do in this part
                            if (spList == null)
                                throw new Exception();

                            SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject(@"<Where>
      <Eq>
         <FieldRef Name = 'ID' />
         <Value Type = 'Counter' > "+universityId+@" </Value>
      </Eq>
   </Where> ");

                            itemsCollection = spList.GetItems(spQuery);

                            if (itemsCollection!=null && itemsCollection.Count>0)
                            {
                                HEDD = itemsCollection[0]["HEDD"] != null ? bool.Parse(itemsCollection[0]["HEDD"].ToString()) : false;
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
                Logging.GetInstance().Debug("Exit BLL.Universtity.ReturnIsUniversityHEDD");
            }

            return HEDD;




        }

    }
}
