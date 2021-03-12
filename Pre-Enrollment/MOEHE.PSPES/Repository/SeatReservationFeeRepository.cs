using MOEHE.PSPES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOEHE.PSPES.Repository
{
   public  class SeatReservationFeeRepository
    {

        public static async Task<List<SeatReservationFee>> Getby(string ApplicationReference)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<SeatReservationFee> seatReservationFeeModel = new List<SeatReservationFee>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetSeatReservationFee/{0}", ApplicationReference));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        seatReservationFeeModel = await res.Content.ReadAsAsync<List<SeatReservationFee>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return seatReservationFeeModel;
            }
        }

        public static async Task<List<SeatReservationFee>> GetbyRefAndID(string ApplicationReference,string QID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<SeatReservationFee> seatReservationFeeModel = new List<SeatReservationFee>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetSeatReservationFeeByRefAndID/{0}/{1}", ApplicationReference,QID));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        seatReservationFeeModel = await res.Content.ReadAsAsync<List<SeatReservationFee>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return seatReservationFeeModel;
            }
        }

        public static async Task<List<SeatReservationFee>> CheckConfirmedApplications(string QID)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                List<SeatReservationFee> seatReservationFeeModel = new List<SeatReservationFee>();
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/CheckConfirmedApplications/{0}", QID));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        seatReservationFeeModel = await res.Content.ReadAsAsync<List<SeatReservationFee>>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return seatReservationFeeModel;
            }
        }


        public static async Task<int> GetCountOfApplientApplications(int? Term, string SchoolCode, string Grade)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                int ApplicationCount = 0;
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetSeatReservationFee/{0}/{1}/{2}", Term,SchoolCode,Grade));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        ApplicationCount = await res.Content.ReadAsAsync<int>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return ApplicationCount;
            }
        }



        public static async Task<DBOperationResult> Insert(SeatReservationFeeModel  seatReservationFeeModel)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {
                DBOperationResult ReturnedResult = new DBOperationResult();
                try
                {
                    HttpResponseMessage res = await cons.PostAsJsonAsync("api/SeatReservationFees/", seatReservationFeeModel);
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



        public static async Task<int> GetAllConfirmedAppCount(int? Term, string SchoolCode)
        {
            using (HttpClient cons = Utility.GetHttpClientConnection())
            {

                int ApplicationCount = 0;
                try
                {
                    HttpResponseMessage res = await cons.GetAsync(string.Format("api/GetSeatReservationFee/{0}/{1}", Term, SchoolCode));

                    res.EnsureSuccessStatusCode();
                    if (res.IsSuccessStatusCode)
                    {
                        ApplicationCount = await res.Content.ReadAsAsync<int>();
                    }
                }
                catch (Exception ex)
                {
                    //this mean service down (Server may be changed)
                }

                return ApplicationCount;
            }
        }


    }
}
