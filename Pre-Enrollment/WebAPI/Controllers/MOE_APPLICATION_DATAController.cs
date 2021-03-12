using MOEHE.PSPES.WebAPI.Models;
using MOEHE.PSPES.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOEHE.PSPES.WebAPI.Controllers
{
    public class MOE_APPLICATION_DATAController : ApiController
    {
        // GET: api/MOE_APPLICATION_DATA
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MOE_APPLICATION_DATA/5
        [Route("api/GetCurrentWaitListNumber/{schoolYearID}/{schoolCode}/{Grade}")]
        public int Get(int schoolYearID, string schoolCode, string Grade)
        {
            int currentWaitListNumber = MOE_APPLICATION_DATA_Repository.GetCurrentWaitListNumber(schoolYearID,schoolCode,Grade);
            return currentWaitListNumber;//MOE_BIO_DATA_Repository.Get(id).Result;
        }

        // GET: api/MOE_APPLICATION_DATA/5
        [Route("api/CheckApplicationExist/{schoolYearID}/{schoolCode}/{Grade}/{studentQID}")]
        public bool Get(int schoolYearID, string schoolCode, string Grade, string studentQID)
        {
            bool applictionExist = MOE_APPLICATION_DATA_Repository.CheckApplicationExist(schoolYearID, schoolCode, Grade,studentQID);
            return applictionExist;//MOE_BIO_DATA_Repository.Get(id).Result;
        }

        [Route("api/GetAppliactionData/{ApplicantReference}")]
        public MOE_APPLICATION_DATA_Model Get(string ApplicantReference)
        {
            MOE_APPLICATION_DATA_Model mOE_Application_DATA = MOE_APPLICATION_DATA_Repository.GetBy(ApplicantReference);
            return mOE_Application_DATA;//MOE_BIO_DATA_Repository.Get(id).Result;
        }

        [Route("api/GetAppliactionDataByRefAndID/{ApplicantReference}/{QID}")]
        public MOE_APPLICATION_DATA_Model GetBYRefAndID(string ApplicantReference,string QID)
        {
            MOE_APPLICATION_DATA_Model mOE_Application_DATA = MOE_APPLICATION_DATA_Repository.GetByRefAndID(ApplicantReference,QID);
            return mOE_Application_DATA;//MOE_BIO_DATA_Repository.Get(id).Result;
        }

        public DBOperationResult Post(MOE_APPLICATION_DATA_Model applicationDataModel)
        {
            //note : this used for insert and update
            return MOE_APPLICATION_DATA_Repository.Insert(applicationDataModel);

        }

        // PUT: api/MOE_APPLICATION_DATA/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MOE_APPLICATION_DATA/5
        public void Delete(int id)
        {
        }

        #region To check finalized applicaiton added by Veer on 30 July
        [Route("api/FinalizedBySchool/{id}/{schoolcode}/{year}")]
        public MOE_APPLICATION_DATA_Model Get(string id, string schoolCode, int year)
        {
            return MOE_APPLICATION_DATA_Repository.FinalizedBySchool(id,schoolCode,year);
        }

        #endregion
    }
}
