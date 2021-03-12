using MOEHE.PSPES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Repository
{
    public class MessageTemplateRepository
    {
        public static async Task<MessageTemplate> Getby(string MessageTemplateTitle, int MessageTemplateID)
        {

            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                MessageTemplate messageTemplate = new MessageTemplate();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetMessageTemplate/{0}/{1}", MessageTemplateTitle, MessageTemplateID));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        messageTemplate = await res.Content.ReadAsAsync<MessageTemplate>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return messageTemplate;
            }
        }

    }
}
