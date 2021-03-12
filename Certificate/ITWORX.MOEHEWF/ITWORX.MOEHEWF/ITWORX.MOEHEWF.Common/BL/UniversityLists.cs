using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.BL
{
   public class UniversityLists
    {
        public static string GetUniversityListByUniversityId(int universityId, int year)
        {
            Logging.GetInstance().Debug("Entering method UniversityLists.GetUniversityListByUniversityId");
            string universityLists = string.Empty;
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList universityList = web.Lists[Constants.UniversityList];
                            if (universityList == null)
                                throw new Exception();

                            string query = string.Empty;
                            if (year == 0)
                            {

                                query = @"<Where><Eq><FieldRef Name = 'University'  LookupId='TRUE'/><Value Type = 'Lookup'>" + universityId + @"</Value></Eq></Where>
                                 <OrderBy><FieldRef Name='Year' Ascending='True' /></OrderBy>";

                            }
                            else

                            {
                                query = @"<Where><And><Eq><FieldRef Name = 'University'  LookupId='TRUE'/><Value Type = 'Lookup'>" + universityId + @"</Value></Eq>
                            <Geq><FieldRef Name = 'Year'/><Value Type = 'Number'>" + year + @"</Value></Geq></And></Where>
                            <OrderBy><FieldRef Name='Year' Ascending='True' /></OrderBy>";

                            }

                            SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject(query);
                            SPListItemCollection universityItems = universityList.GetItems(spQuery);

                            if (universityItems != null && universityItems.Count > 0)
                            {
                                universityLists = Convert.ToString(SPContext.Current.Web.Language == 1033 ? universityItems[0]["Title"] : universityItems[0]["TitleAr"]);

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

                Logging.GetInstance().Debug("Exiting method UniversityLists.GetUniversityListByUniversityId");
            }
            return universityLists;
        }
        public static List<Entities.University> GetUniversityWithEmptyUniversityListsAndCountryId(int countryId)
        {
            Logging.GetInstance().Debug("Entering method UniversityLists.GetUniversityWithEmptyUniversityListsAndCountryId");
            List<Entities.University> universities = new List<Entities.University>();
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList universityList = web.Lists[Constants.UniversityList];
                            if (universityList == null)
                                throw new Exception();
                            SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><And><IsNull><FieldRef Name='Title' /></IsNull><IsNull><FieldRef Name='TitleAr' /></IsNull></And></Where>"
                            ,"<FieldRef Name='University' /><FieldRef Name='UniversityAr' />");

                            SPListItemCollection universityItems = universityList.GetItems(spQuery);

                            if (universityItems != null && universityItems.Count > 0)
                            {
                                foreach(SPListItem item in universityItems )
                                {
                                    Entities.CountryOfStudy country = University.GetUniversityCountryByUniversityId(new SPFieldLookupValue(Convert.ToString(item["University"])).LookupId);
                                    if (country != null)
                                    {

                                        universities.Add(new Entities.University() { ID = new SPFieldLookupValue(Convert.ToString(item["University"])).LookupId,
                                            EnglishTitle = new SPFieldLookupValue(Convert.ToString(item["University"])).LookupValue,
                                            ArabicTitle = new SPFieldLookupValue(Convert.ToString(item["UniversityAr"])).LookupValue,
                                            CountryOfStudy = country
                                        });
                                    }
                                }
                               
                                if (universities!=null && universities.Count>0)
                                {
                                    universities = universities.FindAll(u => u.CountryOfStudy.ID == countryId);
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

                Logging.GetInstance().Debug("Exiting method UniversityLists.GetUniversityWithEmptyUniversityListsAndCountryId");
            }
            return universities;
        }
    }
}
