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
    public class NationalityList
    {
        public static NationalityItem GetNationalityByISOCode(string isoCode)
        {
            NationalityItem nationalityItem = new NationalityItem();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        Logging.GetInstance().Debug("Enter NationalityList.GetNationalityByISOCode");
                        try
                        {
                            SPList list = web.Lists[Utilities.Constants.Nationality];
                            SPQuery query = new SPQuery() { Query = @"<Where><Eq><FieldRef Name='ISOCode' /><Value Type='Text'>" + isoCode + "</Value></Eq></Where>" };
                            SPListItemCollection itemCol = list != null ? list.GetItems(query) : null;
                            SPListItem item = itemCol != null ? itemCol[0] : null;
                            nationalityItem.Id = item.ID;
                            nationalityItem.Title = item.Title;
                            nationalityItem.TitleAr = item["TitleAr"].ToString();
                            nationalityItem.ISOCode = item["ISOCode"].ToString();
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exit NationalityList.GetNationalityByISOCode");
                        }
                    }
                }
            });
            return nationalityItem;
        }

        public static NationalityItem GetNationalityById(int id)
        {
            NationalityItem nationalityItem = new NationalityItem();
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        Logging.GetInstance().Debug("Enter NationalityList.GetNationalityById");
                        try
                        {
                            SPList list = web.Lists[Utilities.Constants.Nationality];
                            SPListItem item = list != null ? list.GetItemById(id) : null;
                            if(item != null)
                            {
                                nationalityItem.Id = item.ID;
                                nationalityItem.Title = item.Title;
                                nationalityItem.TitleAr = item["TitleAr"].ToString();
                                nationalityItem.ISOCode = item["ISOCode"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.GetInstance().LogException(ex);
                        }
                        finally
                        {
                            Logging.GetInstance().Debug("Exit NationalityList.GetNationalityById");
                        }
                    }
                }
            });
            return nationalityItem;
        }
    }
}
