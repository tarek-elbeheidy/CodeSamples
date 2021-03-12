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
   public class University
    {
        public static List<Entities.University> GetAll()
        {
            Logging.GetInstance().Debug("Entering method University.GetAll");
            List<Entities.University> university = new List<Entities.University>();
            try
            {
                SPListItemCollection universityItemsCollection = BusinessHelper.GetLookupData(Constants.University);

                if (universityItemsCollection != null && universityItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in universityItemsCollection)
                    {
                        university.Add(new Entities.University() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method University.GetAll");
            }
            return university;
        }
        public static List<Entities.University> GetUniversityByYearAndCountry(int year, int countryId, int LCID)
        {
            Logging.GetInstance().Debug("Entering method University.GetUniversityByYearAndCountry");
            List<Entities.University> university = new List<Entities.University>();
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList universityList = web.Lists[Constants.University];
                            if (universityList == null)
                                throw new Exception();

                            string query = string.Empty;
                            string orderField = string.Empty;

                            if (LCID == 1033)
                            {
                                orderField = "Title";
                            }
                            else
                            {
                                orderField = "TitleAr";
                            }
                                
                            if (year==0)
                            {
                                query = @"<Where><Eq><FieldRef Name='CountryOfStudy' LookupId='TRUE' /><Value Type='Lookup'>" + countryId + "</Value></Eq></Where><OrderBy><FieldRef Name='" + orderField + "' Ascending='True' /></OrderBy>";

                            }
                            else
                            {
                                query = @"<Where><And><Eq><FieldRef Name='Year' /><Value Type='Number'>" + year + @"</Value></Eq>
                                <Eq><FieldRef Name='CountryOfStudy' LookupId='TRUE' /><Value Type='Lookup'>" + countryId + "</Value></Eq></And></Where><OrderBy><FieldRef Name='" + orderField + "' Ascending='True' /></OrderBy>";

                            }

                            SPQuery spquery = Common.Utilities.BusinessHelper.GetQueryObject(query,"",true, 3000);

                            SPListItemCollection universityItemsCollection = universityList.GetItems(spquery);
                            if (universityItemsCollection != null && universityItemsCollection.Count > 0)
                            {
                                foreach (SPListItem item in universityItemsCollection)
                                {
                                    university.Add(new Entities.University() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
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

                Logging.GetInstance().Debug("Exiting method University.GetUniversityByYearAndCountry");
            }
            return university;
        }

        public static List<Entities.University> GetUniversityByCountryID(int countryId)
        {
            Logging.GetInstance().Debug("Entering method University.GetUniversityByCountryID");
            List<Entities.University> university = new List<Entities.University>();
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList universityList = web.Lists[Constants.University];
                            if (universityList == null)
                                throw new Exception();
                            SPQuery spquery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='CountryOfStudy' LookupId='TRUE' /><Value Type='Lookup'>" +
                                countryId + "</Value></Eq></Where>", "", true, 3000);

                            SPListItemCollection universityItemsCollection = universityList.GetItems(spquery);
                            if (universityItemsCollection != null && universityItemsCollection.Count > 0)
                            {
                                foreach (SPListItem item in universityItemsCollection)
                                {
                                    university.Add(new Entities.University() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
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

                Logging.GetInstance().Debug("Exiting method University.GetUniversityByCountryID");
            }
            return university;
        }




        public static List<Entities.University> GetDistinctUniversityByYearAndCountry(int year, int countryId, int LCID)
        {
 


                Logging.GetInstance().Debug("Entering method University.GetDistinctUniversityByYearAndCountry");
                List<Entities.University> university = new List<Entities.University>();
                try
                {

                    SPSecurity.RunWithElevatedPrivileges(delegate ()
                    {
                        using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                        {
                            using (SPWeb web = site.OpenWeb())
                            {
                                SPList universityList = web.Lists[Constants.University];
                                if (universityList == null)
                                    throw new Exception();

                                string query = string.Empty;
                                string orderField = string.Empty;
                                if (LCID == 1033)
                                {
                                    orderField = "Title";
                                }
                                else
                                {
                                    orderField = "TitleAr";
                                }

                                if (year == 0)
                                {
                                    query = @"<Where><Eq><FieldRef Name='CountryOfStudy' LookupId='TRUE' /><Value Type='Lookup'>" + countryId + "</Value></Eq></Where><OrderBy><FieldRef Name='" + orderField + "' Ascending='True' /></OrderBy>";

                                }
                                else
                                {
                                    query = @"<Where><And><Eq><FieldRef Name='Year' /><Value Type='Number'>" + year + @"</Value></Eq>
                                    <Eq><FieldRef Name='CountryOfStudy' LookupId='TRUE' /><Value Type='Lookup'>" + countryId + "</Value></Eq></And></Where><OrderBy><FieldRef Name='" + orderField + "' Ascending='True' /></OrderBy>";

                                }
                                SPQuery spquery = Common.Utilities.BusinessHelper.GetQueryObject(query, "", true, 3000);

                                SPListItemCollection universityItemsCollection = universityList.GetItems(spquery);
                                if (universityItemsCollection != null && universityItemsCollection.Count > 0)
                                {
                                    foreach (SPListItem item in universityItemsCollection)
                                    {
                                        university.Add(new Entities.University() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                                    }
                                }
                            }
                        }
                    });


                if (university != null && university.Count > 0)
                {
                    if (LCID == (int)MOEHE.Utilities.Language.English)
                        university = university.GroupBy(r => r.EnglishTitle).Select(r => r.FirstOrDefault()).ToList();
                    else
                        university = university.GroupBy(r => r.ArabicTitle).Select(r => r.FirstOrDefault()).ToList();
                }

            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                    Logging.GetInstance().Debug("Exiting method University.GetDistinctUniversityByYearAndCountry");
                }

            return university;
        }

        public static bool IsUniversityHEDD(int universityId)
        {
            Logging.GetInstance().Debug("Entering method University.IsUniversityHEDD");
            bool HEDD = false;

            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList universityList = web.Lists[Constants.University];
                            if (universityList == null)
                                throw new Exception();
                            SPListItem universityItem = universityList.GetItemById(universityId);
                            if (universityItem!=null)
                            {
                                HEDD = Convert.ToBoolean(universityItem["HEDD"]);
                                
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

                Logging.GetInstance().Debug("Exiting method University.IsUniversityHEDD");
            }
            return HEDD;
        }


        public static Entities.CountryOfStudy GetUniversityCountryByUniversityId(int universityId)
        {
            Logging.GetInstance().Debug("Entering method University.GetUniversityCountryByUniversityId");
            Entities.CountryOfStudy universityCountry = new Entities.CountryOfStudy();
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList universityList = web.Lists[Constants.University];
                            if (universityList == null)
                                throw new Exception();
                            SPListItem universityItem = universityList.GetItemById(universityId);
                            if (universityItem !=null)
                            {
                                universityCountry.ID = new SPFieldLookupValue(Convert.ToString(universityItem["CountryOfStudy"])).LookupId;
                                universityCountry.EnglishTitle = new SPFieldLookupValue(Convert.ToString(universityItem["CountryOfStudy"])).LookupValue;
                                universityCountry.ArabicTitle = new SPFieldLookupValue(Convert.ToString(universityItem["CountryOfStudyAr"])).LookupValue;
                                
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

                Logging.GetInstance().Debug("Exiting method University.GetUniversityCountryByUniversityId");
            }
            return universityCountry;
        }

        public static bool IsUniversityCHED(int universityId)
        {
            Logging.GetInstance().Debug("Entering method University.IsUniversityCHED");
            bool CHED = false;

            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList universityList = web.Lists[Constants.University];
                            if (universityList == null)
                                throw new Exception();
                            SPListItem universityItem = universityList.GetItemById(universityId);
                            if (universityItem != null)
                            {
                                CHED = Convert.ToBoolean(universityItem["CHED"]);

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

                Logging.GetInstance().Debug("Exiting method University.IsUniversityCHED");
            }
            return CHED;
        }

        public static bool IsUniversityOnline(int universityId)
        {
            Logging.GetInstance().Debug("Entering method University.IsUniversityOnline");
            bool IsOnline = false;

            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList universityList = web.Lists[Constants.University];
                            if (universityList == null)
                                throw new Exception();
                            SPListItem universityItem = universityList.GetItemById(universityId);
                            if (universityItem != null)
                            {
                                IsOnline = Convert.ToBoolean(universityItem["IsOnline"]);

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

                Logging.GetInstance().Debug("Exiting method University.IsUniversityOnline");
            }
            return IsOnline;
        }


        public static Entities.University GetUniversityItemById(int universityId)
        {
            Logging.GetInstance().Debug("Entering method University.GetUniversityItemById");
            Entities.University university = null;
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList universityList = web.Lists[Constants.University];
                        if (universityList == null)
                            throw new Exception();
                        SPListItem uniItem = universityList.GetItemById(universityId);
                        if (uniItem != null)
                        {
                            university = new Entities.University() {ArabicTitle=Convert.ToString(uniItem["TitleAr"]),
                               EnglishTitle=uniItem.Title,
                               HEDD=Convert.ToBoolean(uniItem["HEDD"]),
                               CHED= Convert.ToBoolean(uniItem["CHED"]),
                               SelectedID=uniItem.ID.ToString()
                            };
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

                Logging.GetInstance().Debug("Exiting method University.GetUniversityItemById");
            }
            return university;
        }


        /*
        public static List<Entities.University> GetUniversityWithCountryIdAndEmptyUniversityLists(int countryId)
        {
            Logging.GetInstance().Debug("Entering method University.GetUniversityWithCountryIdAndEmptyUniversityLists");
            List<Entities.University> universities = new List<Entities.University>();
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList university = web.Lists[Constants.University];
                            SPList universityLists= web.Lists[Constants.UniversityList];
                            if (university == null || universityLists==null)
                                throw new Exception();
                            SPQuery spquery = new SPQuery();

                            spquery.Joins = "<Join Type='INNER' ListAlias="+ universityLists+">" +
                                                          "<Eq>" +
                                                              "<FieldRef Name='University' RefType='Id'/>" +
                                                              "<FieldRef List="+universityLists+" Name='ID'/>" +
                                                          "</Eq>" +
                                                          "</Join>";

                            spquery.ProjectedFields =
                      "<Field Name='UniversityLists_University' Type='Lookup' "
                              + "List="+universityLists+ " ShowField='University'/>";
                            spquery.ViewFields=""

                            spquery.Query = @"<Where><Eq ><FieldRef Name = 'Country' LookupId='TRUE' /><Value Type = 'Lookup' >" + countryId + @"</Value ></Eq ></Where >";

                            SPListItemCollection universityItemsCollection = universityList.GetItems(spquery);
                            if (universityItemsCollection != null && universityItemsCollection.Count > 0)
                            {
                                foreach (SPListItem item in universityItemsCollection)
                                {
                                    university.Add(new Entities.University() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
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

                Logging.GetInstance().Debug("Exiting method University.GetUniversityWithCountryIdAndEmptyUniversityLists");
            }
            return university;
        }
        */
    }
}
