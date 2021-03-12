using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.SCE.Entities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.SCE.BL
{
    public class NationalityCategoryList
    {
        public static NationalityCategoryItem GetNationalityCategoryById(int id)
        {
            NationalityCategoryItem nationalityCategoryItem = new NationalityCategoryItem();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        Logging.GetInstance().Debug("Enter NationalityCategoryList.GetNationalityCategoryById");
                        try
                        {
                            SPList list = web.Lists[Utilities.Constants.NationalityCategory];
                            SPListItem item = list != null ? list.GetItemById(id) : null;
                            if (item != null)
                            {
                                nationalityCategoryItem.Id = item.ID;
                                nationalityCategoryItem.Title = item.Title;
                                nationalityCategoryItem.TitleAr = item["TitleAr"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exit NationalityCategoryList.GetNationalityCategoryById");
                        }
                    }
                }
            });
            return nationalityCategoryItem;
        }
    }
}
