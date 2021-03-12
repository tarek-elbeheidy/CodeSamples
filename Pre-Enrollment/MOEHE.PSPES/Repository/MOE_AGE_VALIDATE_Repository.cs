using MOEHE.PSPES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Repository
{
    public class MOE_AGE_VALIDATE_Repository
    {
        #region Get Curriculum ID BY School Code added by VEER on 24 July 2018
        public static async Task<string> GetCurriculumIDBySchoolCode(string term, string schoolCode)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                string CURRICULUMID = string.Empty;
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetCurriculumIDBySchoolCode/{0}/{1}", term, schoolCode));
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        CURRICULUMID = await res.Content.ReadAsAsync<string>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }
                return CURRICULUMID;
            }
        }
        #endregion
        public static async Task<MOE_AGE_VALIDATE_Model> GetAgeValidateData(string term, string schoolCode, string schoolType, string schoolCurriculumID, string grade,string passedID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                MOE_AGE_VALIDATE_Model Age_validate_Model = new MOE_AGE_VALIDATE_Model();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetAgeValidate/{0}/{1}/{2}/{3}/{4}/{5}", new object[] { term, schoolCode, schoolType, schoolCurriculumID, grade,passedID }));
                    
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Age_validate_Model = await res.Content.ReadAsAsync<MOE_AGE_VALIDATE_Model>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return Age_validate_Model;
            }
        }

        public static async Task<List<MOE_AGE_VALIDATE_Model>> GetGradeList(string term, string schoolCode, string schoolType, string schoolCurriculumID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<MOE_AGE_VALIDATE_Model> Age_validate_Model = new List<MOE_AGE_VALIDATE_Model>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetGradeList/{0}/{1}/{2}/{3}", new object[] { term,schoolCode, schoolType, schoolCurriculumID}));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Age_validate_Model = await res.Content.ReadAsAsync<List<MOE_AGE_VALIDATE_Model>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return Age_validate_Model;
            }
        }

        public static async Task<List<MOE_AGE_VALIDATE_Model>> GetGradeListWithCurriculumOnly(string term, string schoolType, string schoolCurriculumID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<MOE_AGE_VALIDATE_Model> Age_validate_Model = new List<MOE_AGE_VALIDATE_Model>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetGradeListWithCurriculumOnly/{0}/{1}/{2}", new object[] { term, schoolType, schoolCurriculumID }));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Age_validate_Model = await res.Content.ReadAsAsync<List<MOE_AGE_VALIDATE_Model>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return Age_validate_Model;
            }
        }

        public static async Task<MOE_AGE_VALIDATE_Model> GetGradeAgeRange(string term, string schoolCode, string schoolType, string schoolCurriculumID, string GradeLevel)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                MOE_AGE_VALIDATE_Model Age_validate_Model = new MOE_AGE_VALIDATE_Model();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetGradeAgeRange/{0}/{1}/{2}/{3}/{4}", new object[] { term,schoolCode, schoolType, schoolCurriculumID, GradeLevel }));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Age_validate_Model = await res.Content.ReadAsAsync<MOE_AGE_VALIDATE_Model>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return Age_validate_Model;
            }
        }
    }
}
