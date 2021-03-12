using MOEHE.PSPES.WebAPI.Models;
using MOEHE.PSPES.WebAPI.Repository;
using RestSharp;
//using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MOEHE.PSPES.WebAPI.Controllers
{
    public class MOE_BIO_DATAController : ApiController
    {
        // GET: api/MOE_BIO_DATA
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/MOE_BIO_DATA/5
        [Route ("api/GetBioData/{id}")]
        public MOE_BIO_DATA_Model Get(string id)
        {
            MOE_BIO_DATA_Model model = MOE_BIO_DATA_Repository.Get(id);
            return model;//MOE_BIO_DATA_Repository.Get(id).Result;
        }
        //[Route("api/Test")]
        //public MOE_BIO_DATA_Model Get()
        //{
        //    IRestResponse response = MOE_BIO_DATA_Repository.TestCall();
        //    dynamic bio  =  Newtonsoft.Json.Linq.JObject.Parse(response.Content.ToString());


        //    MOE_BIO_DATA_Model Bio_Model = new MOE_BIO_DATA_Model() {
        //        NATIONAL_ID = bio.QatarId,
        //        MOE_ENGLISH_NAME = bio.EnglishName,
        //        MOE_ARABIC_NAME = bio.ArabicName,
        //        MOE_DOB = bio.DOB,
        //        MOE_GENDER = bio.Gender,
        //        MOE_COUNTRY_ENGLISH_NAME = bio.CountryNameEn,
        //        MOE_COUNTRY_ARABIC_NAME = bio.CountryNameAr,
        //        MOE_COUNTRY_CODE = bio.CountryCode,
        //        MOE_STATUS = bio.Status,
        //        MOE_STATUS_DATE = bio.StatusDate,
        //        MOE_LAST_UPDATED_DATE = bio.LastUpdatedDate
        //    };



        //    return Bio_Model;
        //}

        // POST: api/MOE_BIO_DATA
        [Route("api/InsertBioData/")]
        public DBOperationResult Post(MOE_BIO_DATA_Model bio_model)
        {
            return MOE_BIO_DATA_Repository.Insert(bio_model);
        }

        // PUT: api/MOE_BIO_DATA/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MOE_BIO_DATA/5
        public void Delete(int id)
        {
        }
    }
}
