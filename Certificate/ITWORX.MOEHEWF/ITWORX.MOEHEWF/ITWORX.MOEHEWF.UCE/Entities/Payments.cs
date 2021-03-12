using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.Entities
{
    public class Payments
    {
        private DateTime _ReceiptDate;
        public DateTime ReceiptDate
        {
            get
            {
                return _ReceiptDate;
            }
            set
            {
                _ReceiptDate = value;
            }
        }

        private string _ReceiptNumber;
        public string ReceiptNumber
        {
            get
            {
                return _ReceiptNumber;
            }
            set
            {
                _ReceiptNumber = value;
            }
        }

        private string _Statement;
        public string Statement
        {
            get
            {
                return _Statement;
            }
            set
            {
                _Statement = value;
            }
        }

        private string _CardType;
        public string CardType
        {
            get
            {
                return _CardType;
            }
            set
            {
                _CardType = value;
            }
        }

        private string _CardNumber;
        public string CardNumber
        {
            get
            {
                return _CardNumber;
            }
            set
            {
                _CardNumber = value;
            }
        }

        private string _Amount;
        public string Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                _Amount = value;
            }
        }
        private string _RequestID;
        public string RequestID
        {
            get
            {
                return _RequestID;
            }
            set
            {
                _RequestID = value;
            }
        }
        public string ID { get; set; }
      
        public string ResponseMessage { get; set; }
        public string AssignedTo { get; set; }
      
        public string ApplicantName { get; set; }
        public string RequestNumber { get; set; }
        public string QatariID { get; set; }
        public string Nationality { get; set; }
        public string AcademicDegree { get; set; }
        public string EntityNeedsEquivalency { get; set; }
        public string Country { get; set; }
        public string University { get; set; }
        public string Faculty { get; set; }
        private DateTime _SubmitDate;
        public DateTime SubmitDate
        {
            get
            {
                return _SubmitDate;
            }
            set
            {
                _SubmitDate = value;
            }
        }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        private string _NationCatgeory;
        public string NationCatgeory
        {
            get
            {
                return _NationCatgeory;
            }
            set
            {
                _NationCatgeory = value;
            }
        }
        private string _Specialization;
        public string Specialization
        {
            get
            {
                return _Specialization;
            }
            set
            {
                _Specialization = value;
            }
        }
        private int _RequestStatusId;
        public int RequestStatusId
        {
            get
            {
                return _RequestStatusId;
            }
            set
            {
                _RequestStatusId = value;
            }
        }
        private string _RequestStatus;
        public string RequestStatus
        {
            get
            {
                return _RequestStatus;
            }
            set
            {
                _RequestStatus = value;
            }
        }
        public string ReasonCode { get; set; }
    }
}
