using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.PA.Utilities;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;

namespace ITWORX.MOEHEWF.PA.BL
{
    public class Faculty
    {
        public static List<Entities.Faculty> GetAll()
        {
            Logging.GetInstance().Debug("Entering method Faculty.GetAll");
            List<Entities.Faculty> faculty = new List<Entities.Faculty>();
            try
            {
                SPListItemCollection facultyItemsCollection = BusinessHelper.GetLookupData(Constants.Faculty);

                if (facultyItemsCollection != null && facultyItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in facultyItemsCollection)
                    {
                        faculty.Add(new Entities.Faculty() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exiting method Faculty.GetAll");
            }
            return faculty;
        }

        public static List<Entities.Faculty> GetFacultyByUniversityId(int universityId)
        {
            Logging.GetInstance().Debug("Entering method Faculty.GetFacultyByUniversityId");
            List<Entities.Faculty> faculties = new List<Entities.Faculty>();
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList facultyList = web.Lists[Constants.Faculty];
                            if (facultyList == null)
                                throw new Exception();
                            SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name = 'University'  LookupId='TRUE'/><Value Type = 'Lookup'>" +
                                universityId + @"</Value></Eq></Where>");

                            SPListItemCollection facultyItems = facultyList.GetItems(spQuery);

                            if (facultyItems != null && facultyItems.Count > 0)
                            {
                                foreach (SPListItem fac in facultyItems)
                                {
                                    faculties.Add(new Entities.Faculty() { ID = fac.ID, EnglishTitle = Convert.ToString(fac["Title"]), ArabicTitle = Convert.ToString(fac["TitleAr"]) });
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
                Logging.GetInstance().Debug("Exiting method Faculty.GetFacultyByUniversityId");
            }
            return faculties;
        }
    }
}