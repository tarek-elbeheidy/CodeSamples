using MOEHE.PSPES.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOEHE.PSPES.WebAPI.Repository
{
    public class MOE_WITHDRAWAL_DATA_Repository
    {
        public static DBOperationResult Insert(MOE_WITHDRAWAL_DATA_Model oModel)
        {
            DBOperationResult results = new DBOperationResult();

            if (!IsItemExists(oModel.MOE_REFERENCE_ID))
            {
                decimal QID = decimal.Parse(oModel.MOE_NATIONAL_ID);
                string RefId = oModel.MOE_REFERENCE_ID;
                string schoolCode = oModel.MOE_SCHOOL_CODE;
                int moe_term = 0;
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    //UPDATE MOE_APPPLICATION_DATA table
                    //Update isActive : False, isFinalized: in App Data table
                    MOE_Application_DATA oAppData = DB.MOE_Application_DATA.Where(D => D.NATIONAL_ID == QID && D.MOE_APPL_REF_NBR == RefId && D.MOE_SCHOOL_CODE == schoolCode && D.IsActive == true && D.IsApplicationFinalized == true).FirstOrDefault();
                    oAppData.IsActive = false;
                    oAppData.IsApplicationFinalized = false;
                    oAppData.Comment = "Changed isActive : False, isFinalized: False based on email from Withdrawal Application on " + DateTime.Now;
                    moe_term = (int)oAppData.MOE_TERM;
                    //Update Seat Reservation table to Changed NationalID.
                    SeatReservationFee oSeatsReserve = DB.SeatReservationFees.Where(D => D.ApplicantReferenceNumber == RefId && D.NationalID == QID).FirstOrDefault();
                    if (oSeatsReserve !=null && oSeatsReserve.NationalID !=null)
                    {
                    oSeatsReserve.NationalID = 100;
                    oSeatsReserve.Comments = "Changed NationalID from :" + QID + " to 100 from withdrawal application on " + DateTime.Now;

                    }
                  
                    //Adding withdrawal application item
                    MOE_WITHDRAWAL_DATA oData = new MOE_WITHDRAWAL_DATA
                    {
                        MOE_SCHOOL_CODE = oModel.MOE_SCHOOL_CODE,
                        MOE_NATIONAL_ID = oModel.MOE_NATIONAL_ID,
                        MOE_REFERENCE_ID = oModel.MOE_REFERENCE_ID,
                        MOE_WITHDRAWAL_REQUESTDATE = oModel.MOE_WITHDRAWAL_REQUESTDATE,
                        MOE_WITHDRAWAL_DATE = oModel.MOE_WITHDRAWAL_DATE,
                        MOE_WITHDRAWAL_REASONID = oModel.MOE_WITHDRAWAL_REASONID,
                        MOE_ATTACHMENT = oModel.MOE_ATTACHMENT,
                        MOE_ACTION_DATE = DateTime.Now,
                        MOE_LOGIN_ID = oModel.MOE_LOGIN_ID,
                        MOE_COMMENTS = oModel.MOE_COMMENTS,
                        MOE_TERM = moe_term
                    };
                    DB.MOE_WITHDRAWAL_DATA.Add(oData);
                    DB.SaveChanges();
                }
            }
            return results;
        }

        public static bool IsItemExists(string RefID)
        {
            bool isExists = false;
            using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
            {
                MOE_WITHDRAWAL_DATA oData = DB.MOE_WITHDRAWAL_DATA.Where(W => W.MOE_REFERENCE_ID == RefID).FirstOrDefault();
                if (oData != null)
                    isExists = true;
            }
            return isExists;
        }

        public static MOE_WITHDRAWAL_DATA_Model GetWithdrawalByRefId(string RefId)
        {
            MOE_WITHDRAWAL_DATA_Model oModel = new MOE_WITHDRAWAL_DATA_Model();
            try
            {
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    oModel = DB.MOE_WITHDRAWAL_DATA.Where(W => W.MOE_REFERENCE_ID == RefId).Select(W => new MOE_WITHDRAWAL_DATA_Model{
                        MOE_SCHOOL_CODE = W.MOE_SCHOOL_CODE,
                        MOE_NATIONAL_ID = W.MOE_NATIONAL_ID,
                        MOE_REFERENCE_ID = W.MOE_REFERENCE_ID,
                        MOE_WITHDRAWAL_REQUESTDATE = W.MOE_WITHDRAWAL_REQUESTDATE,
                        MOE_WITHDRAWAL_DATE = W.MOE_WITHDRAWAL_DATE,
                        MOE_WITHDRAWAL_REASONID = W.MOE_WITHDRAWAL_REASONID,
                        MOE_ATTACHMENT = W.MOE_ATTACHMENT,
                        MOE_ACTION_DATE = W.MOE_ACTION_DATE,
                        MOE_LOGIN_ID = W.MOE_LOGIN_ID,
                        MOE_COMMENTS = W.MOE_COMMENTS,
                        MOE_TERM = W.MOE_TERM.Value
                    }).FirstOrDefault();
                    return oModel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static MOE_WITHDRAWAL_DATA_Model GetWithdrawalByQId(string QID)
        {
            MOE_WITHDRAWAL_DATA_Model oModel = new MOE_WITHDRAWAL_DATA_Model();
            try
            {
                decimal QIDDec = decimal.Parse(QID);
                using (PrivateScoolsPreEnrollmentEntities222 DB = new PrivateScoolsPreEnrollmentEntities222())
                {
                    oModel = DB.MOE_WITHDRAWAL_DATA.Where(W => W.MOE_NATIONAL_ID == QID).Select(W => new MOE_WITHDRAWAL_DATA_Model{
                        MOE_SCHOOL_CODE = W.MOE_SCHOOL_CODE,
                        MOE_NATIONAL_ID = W.MOE_NATIONAL_ID,
                        MOE_REFERENCE_ID = W.MOE_REFERENCE_ID,
                        MOE_WITHDRAWAL_REQUESTDATE = W.MOE_WITHDRAWAL_REQUESTDATE,
                        MOE_WITHDRAWAL_DATE = W.MOE_WITHDRAWAL_DATE,
                        MOE_WITHDRAWAL_REASONID = W.MOE_WITHDRAWAL_REASONID,
                        MOE_ATTACHMENT = W.MOE_ATTACHMENT,
                        MOE_ACTION_DATE = W.MOE_ACTION_DATE,
                        MOE_LOGIN_ID = W.MOE_LOGIN_ID,
                        MOE_COMMENTS = W.MOE_COMMENTS,
                        MOE_TERM = W.MOE_TERM.Value
                    }).FirstOrDefault();
                    return oModel;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}