using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.BL
{
    public class TermsAndConditions
    {
        //Get terms and conitions per request type id 
        public static Entities.TermsAndConditions GetTermsConditionsByRequestType (int requestType, uint LCID)
        {

            Logging.GetInstance().Debug("Entering method TermsAndConditions.GetTermsConditionsByRequestType");
            Entities.TermsAndConditions termsAndConditions = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                using (SPSite site = new SPSite(SPContext.Current.Site.Url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList termsAndConditionsList = web.Lists[Utilities.Constants.TermsandConditions];
                            if (termsAndConditionsList == null)
                            {
                                Exception ex = new Exception("Terms and Conditions List is not found");
                                Logging.GetInstance().LogException(ex);
                            }

                            SPQuery termsQuery = Utilities.BusinessHelper.GetQueryObject("<Where><Eq><FieldRef Name='RequestType' LookupId='TRUE' /><Value Type='Lookup'>" + requestType + "</Value></Eq></Where>");
                        SPListItemCollection termsCollection = termsAndConditionsList.GetItems(termsQuery);

                        if (termsCollection != null && termsCollection.Count != 0)
                        {
                            SPListItem termItem = termsCollection[0];
                            termsAndConditions = new Entities.TermsAndConditions();
                            termsAndConditions.Description = LCID == (uint)Language.English ? Convert.ToString(termItem["DescriptionEn"]) : Convert.ToString(termItem["DescriptionAr"]);

                            if (termItem.Attachments != null && termItem.Attachments.Count > 0)
                            {
                              termsAndConditions.TermsAttachmentsList = new List<Entities.TermsAttachments>();


                                foreach (string attachment in termItem.Attachments)
                                    {
                                        bool isArabic = (ContainsArabicCharacter(attachment) && LCID !=(uint)Language.English);
                                        bool isEnglish = (!ContainsArabicCharacter(attachment) && LCID==(uint)Language.English);
                                        if (isArabic || isEnglish)
                                        {
                                            termsAndConditions.TermsAttachmentsList.Add(
                                            new Entities.TermsAttachments()
                                            {
                                                FileName = Path.GetFileNameWithoutExtension(attachment),
                                                FileURL = SPUrlUtility.CombineUrl(termItem.Attachments.UrlPrefix, attachment)
                                            });
                                        }
                                    }

                                        
                                    }
                              
                                }


                            }
                        }
                   // }
                });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }


            finally
            {

                Logging.GetInstance().Debug("Exiting method TermsAndConditions.GetTermsConditionsByRequestType");
            }

            return termsAndConditions;
            

        }

        //check if sting contains Arabic charachers
        static bool  ContainsArabicCharacter(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            for (int i = 1; i < bytes.Length; i += 2)
                if (bytes[i] == 6)  //0x06** is arabic code page
                    return true;
            return false;
        }
    }
}
