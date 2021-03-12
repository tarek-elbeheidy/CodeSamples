using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class MessageTemplateRepository
    {
        public static MessageTemplate GetBy(string MessageTemplateTitle, int MessageTemplateID )
        {
            MessageTemplate messageTemplate = new MessageTemplate();

            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                DB.Database.Connection.Open();

                messageTemplate = DB.MessageTemplates.Where(D => (D.ID == MessageTemplateID ||  D.TemplateTitle == MessageTemplateTitle)).Select(D => D).FirstOrDefault();






            }

            return messageTemplate;

        }

    }
}