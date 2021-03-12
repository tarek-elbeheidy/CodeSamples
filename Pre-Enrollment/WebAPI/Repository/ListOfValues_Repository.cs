using MOEHE.PSPES.WebAPI.Models;
using MOEHE.PSPES.WebAPI.Repository.NSIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class ListOfValues_Repository
    {
        public static List<ListOfValues_Model> GetListOfValues(string codesetID)
        {




            List<ListOfValues_Model> listOfValues_Models = new List<ListOfValues_Model>();
            try
            {
                listOfValues_Models = NSIS_Helper.GetListOfValues(codesetID);

            }
            catch (Exception ex)
            {

                string s = ex.Message;

            }


            return listOfValues_Models;
        }
        }
}