using MOEHE.PSPES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Repository
{
   public class SendSMS
    {
        //int BulkID = 0;
        //var BulkReturnedValue = MessageBulkRepository.Insert(new MessageBulkModel { UserID = long.Parse(ViewState["QIDUserID"].ToString()), CreateDate = DateTime.Now, IsBulkSet = false, IsCompleted = false }).Result;
        //            if (BulkReturnedValue[1] == Constants.New)
        //            {

        //                BulkID = BulkReturnedValue[0];

        //            }

        //string TextBody = string.Format("{0}  ", MessageText);

        //var task = MessageRepository.Insert(new MessageDetailsModel
        //{
        //    UserID = long.Parse(ViewState["QIDUserID"].ToString()),
        //    Title = "Title",
        //    MobileNumber = "",
        //    PriorityID = 1,

        //    MessageID = "",
        //    IsBulk = true,
        //    SenderCode = ViewState["DepartmentID"].ToString(),
        //    BulkID = BulkID,
        //    ContactSourceID = Constants.ManualContactSource,
        //    TextBody = string.Format("{0}  ", MessageText)
        //});

        //string MessageID = task.Result;
        //MessageDetailsModel messageDetails = new MessageDetailsModel
        //{
        //    UserID = long.Parse(ViewState["QIDUserID"].ToString()),
        //    Title = "Title",
        //    MobileNumber = item,
        //    PriorityID = 1,

        //    MessageID = MessageID,
        //    IsBulk = true,
        //    SenderCode = ViewState["DepartmentID"].ToString(),
        //    IsBulkSet = false,
        //    BulkID = BulkID,
        //    ContactSourceID = Constants.ManualContactSource,
        //    TextBody = string.Format("{0}  ", MessageText)
        //};


        //var task = MessageRepository.SendSMS(messageDetails);

        public static  string  SendMessage(string title, string mobileNumber, string txtMessageBody)
        {
            string MessageStatus = "";
            try
            {
                // txtPreEnrollmentSchool.Text += "start";

                int BulkID = 0;
                //Commented by Veer on 17 July 2018 - START
                //var BulkReturnedValue = Repository.SendSMS.InsertBulk(new MessageBulkModel { UserID = long.Parse("28135610324"), CreateDate = DateTime.Now, IsBulkSet = false, IsCompleted = false }).Result;
                //Commented by Veer on 17 July 2018 - END
                //if (BulkReturnedValue[1] == Constants.New)
                //{
                //foreach (var item in BulkReturnedValue)
                //{
                //    txtPreEnrollmentSchool.Text += item;

                //}

                //Commented by Veer on 17 July 2018 - START
                //BulkID = int.Parse(BulkReturnedValue[0]);
                //Commented by Veer on 17 July 2018 - END

                //}

                string textBody = string.Format("{0}  ", "test message");
                // txtPreEnrollmentSchool.Text += "before insert message";
                var task = Repository.SendSMS.Insert(new MessageDetailsModel
                {
                    UserID = long.Parse("28135610324"),
                    Title = title,
                    MobileNumber = mobileNumber,
                    PriorityID = 1,

                    MessageID = "",
                    IsBulk = true,
                    SenderCode = "11500",
                    BulkID = BulkID,
                    ContactSourceID = 1,
                    TextBody = txtMessageBody
                });

                string MessageID = task.Result;
                //txtPreEnrollmentSchool.Text += MessageID;
                MessageDetailsModel messageDetails = new MessageDetailsModel
                {
                    UserID = long.Parse("28135610324"),
                    Title = title,
                    MobileNumber = mobileNumber,
                    PriorityID = 1,

                    MessageID = MessageID,
                    IsBulk = true,
                    SenderCode = "11500",
                    IsBulkSet = false,
                    BulkID = BulkID,
                    ContactSourceID = 1,
                    TextBody = txtMessageBody
                };


                var messagetask = Repository.SendSMS.Send(messageDetails).Result;
                 MessageStatus = messagetask;
                //txtPreEnrollmentSchool.Text += "after send";
            }
            catch (Exception ex)
            {
                //txtPreEnrollmentSchool.Text += ex.Message;
            }
            return MessageStatus;
        }


        public static async Task<List<string>> InsertBulk(MessageBulkModel messageBulkModel)
        {
            using (HttpClient cons = Utility.GetSMSHttpClientConnection())
            {
                List<string> ReturnedResult = new List<string>();
                try
                {
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/Bulk/", messageBulkModel);
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        ReturnedResult = await res.Content.ReadAsAsync<List<string>>();
                    }
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    //this mean service down (Server may be changed)
                    //MessageID will be Error

                }
                return ReturnedResult;
            }
        }


        public static async Task<string> Send(MessageDetailsModel messageDetails)
        {

            using (HttpClient cons = Utility.GetSMSHttpClientConnection())
            {
                string StatusCode = "";
                try
                {
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/HodhodPushSMS/", messageDetails);
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        StatusCode = await res.Content.ReadAsAsync<string>();
                    }
                }
                catch
                {
                    //this mean service down (Server may be changed)

                }
                return StatusCode;
            }

        }

        public static async Task<string> Insert(MessageDetailsModel messageDetails)
        {
            using (HttpClient cons = Utility.GetSMSHttpClientConnection())
            {
                string MessageID = "";
                try
                {
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/Messages/", messageDetails);
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        MessageID = await res.Content.ReadAsAsync<string>();
                    }
                }
                catch
                {
                    //this mean service down (Server may be changed)
                    //MessageID will be Error

                }
                return MessageID;
            }
        }


    }
}
