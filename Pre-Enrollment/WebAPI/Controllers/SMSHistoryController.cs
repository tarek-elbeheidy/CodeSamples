using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOEHE.PSPES.WebAPI.Models;
using MOEHE.PSPES.WebAPI.Repository;

namespace MOEHE.PSPES.WebAPI.Controllers
{
    public class SMSHistoryController : ApiController
    {

        [Route("api/GetSMSHistory/{AppRefNum}/{MsgType}/{MobileNumber}/{QID}")]
        [HttpGet]
        public List<SMSHistoryModel> Get(string AppRefNum, string MsgType,string MobileNumber,string QID)
        {
            List<SMSHistoryModel> list = SMSHistoryRepository.Get(AppRefNum,MsgType,MobileNumber, QID)   ;
            return list;
        }

        [Route("api/GetSMSHistory/{AppRefNum}/{MsgType}/{MessageTitle}/{MobileNumber}/{QID}")]
        [HttpGet]
        public List<SMSHistoryModel> Get(string AppRefNum, string MsgType,string MessageTitle,string MobileNumber,string QID)
        {
            List<SMSHistoryModel> list = SMSHistoryRepository.Get(AppRefNum, MsgType, MessageTitle,MobileNumber,QID);
            return list;
        }

        // PUT: api/AddedCapacity/5
        public DBOperationResult Post(SMSHistoryModel smsHistModel)
        {
            
            return SMSHistoryRepository.Insert(smsHistModel);

        }
    }
}