using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class MOE_WITHDRAWAL_REASON_Repository
    {
        public static List<MOE_WITHDRAWAL_REASON_Model> GetWithdrawalReasons()
        {
            try
            {
                List<MOE_WITHDRAWAL_REASON_Model> oModels = new List<MOE_WITHDRAWAL_REASON_Model>();
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    oModels = DB.MOE_WITHDRAWAL_REASON.Select(W => new MOE_WITHDRAWAL_REASON_Model {
                        ID = W.ID,
                        MOE_NAME_ENGLISH = W.MOE_NAME_ENGLISH,
                        MOE_NAME_ARABIC = W.MOE_NAME_ARABIC
                    }).ToList();
                }
                return oModels;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}