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
   public class AttachmentType
    {
        public static List<Entities.AttachmentType> GetAll()
        {
            Logging.GetInstance().Debug("Entering method AttachmentType.GetAll");
            List<Entities.AttachmentType> attachmentType = new List<Entities.AttachmentType>();
            try
            {
                SPListItemCollection attachmentTypeItemsCollection = BusinessHelper.GetLookupData(Constants.AttachmentType);

                if (attachmentTypeItemsCollection != null && attachmentTypeItemsCollection.Count > 0)
                {
                    foreach (SPListItem item in attachmentTypeItemsCollection)
                    {
                        attachmentType.Add(new Entities.AttachmentType() { ID = item.ID, ArabicTitle = Convert.ToString(item["TitleAr"]), EnglishTitle = item.Title });
                    }
                }
            }

            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {

                Logging.GetInstance().Debug("Exiting method AttachmentType.GetAll");
            }
            return attachmentType;
        }
    }

}
