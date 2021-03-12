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
    public class Specialization
    {
        public static List<Entities.Specialization> GetAll()
        {
            Logging.GetInstance().Debug("Entering method Specialization.GetAll");
            List<Entities.Specialization> specialization = new List<Entities.Specialization>();
            try
            {
                SPListItemCollection specializationItemsCollection = BusinessHelper.GetLookupData(Constants.Specialization);

                if (specializationItemsCollection!=null && specializationItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in specializationItemsCollection)
                    {
                        specialization.Add(new Entities.Specialization() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method Specialization.GetAll");
            }
            return specialization;
        }

        public static List<Entities.Specialization> GetSpecializationByFacultyId(int facultyId)
        {
            Logging.GetInstance().Debug("Entering method Faculty.GetSpecializationByFacultyId");
            List<Entities.Specialization> specialization = new List<Entities.Specialization>();
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList specializationList = web.Lists[Constants.Specialization];
                            if (specializationList == null)
                                throw new Exception();
                            SPQuery spQuery = Common.Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name = 'Faculty'  LookupId='TRUE'/><Value Type = 'Lookup'>" +
                                facultyId + @"</Value></Eq></Where>");


                            SPListItemCollection specializationItems = specializationList.GetItems(spQuery);

                            if (specializationItems != null && specializationItems.Count > 0)
                            {
                                foreach (SPListItem spec in specializationItems)
                                {
                                    specialization.Add(new Entities.Specialization() { ID = spec.ID, EnglishTitle = Convert.ToString(spec["Title"]), ArabicTitle = Convert.ToString(spec["TitleAr"]) });
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

                Logging.GetInstance().Debug("Exiting method Faculty.GetSpecializationByFacultyId");
            }
            return specialization;
        }
    }
}
