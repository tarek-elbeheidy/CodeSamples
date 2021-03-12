using Microsoft.SharePoint;
using MOEHE.PSPES.Models;
using MOEHE.PSPES.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace MOEHE.PSPES.Repository
{
    class SeatCapacityRepository
    {

        public static async Task<DBOperationResult> Insert(SeatCapacityModel seatCapModel)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/SeatCapacity/", seatCapModel);
                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        ReturnedResult = await res.Content.ReadAsAsync<DBOperationResult>();
                    }
                }
                catch
                {
                    //this mean service down (Server may be changed)
                    //MessageID will be Error

                }
                return ReturnedResult;
            }
        }





        public static async Task<List<SeatCapacityModel>> GetSeatCapacity(int Term, string schoolCode, string Grade, int seatDistrib, int PreEnrollSeats)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<SeatCapacityModel> Adc = new List<SeatCapacityModel>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetSeatCapacity/{0}/{1}/{2}/{3}/{4}", Term,schoolCode,Grade,seatDistrib, PreEnrollSeats));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Adc = await res.Content.ReadAsAsync<List<SeatCapacityModel>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return Adc;
            }
        }


        public static async Task<List<SeatCapacityModel>> CheckExistsSeatCapacity(int Term, string schoolCode, string Grade)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<SeatCapacityModel> Adc = new List<SeatCapacityModel>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/CheckExistsSeatCapacity/{0}/{1}/{2}", Term, schoolCode, Grade));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Adc = await res.Content.ReadAsAsync<List<SeatCapacityModel>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return Adc;
            }
        }



        public static async Task<List<SeatCapacityModel>> CheckNumberofGrades(int Term, string schoolCode)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<SeatCapacityModel> Adc = new List<SeatCapacityModel>();
                try
                {
                    
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/CheckNumberofGrades/{0}/{1}", Term, schoolCode));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        Adc = await res.Content.ReadAsAsync<List<SeatCapacityModel>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return Adc;
            }
        }



    }
}
