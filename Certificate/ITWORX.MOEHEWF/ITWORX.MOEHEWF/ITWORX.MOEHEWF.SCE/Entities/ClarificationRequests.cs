using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.SCE.Entities
{
   public class ClarificationRequests
    {

        private string _RequestNumber;
        public string RequestNumber
        {
            get
            {
                return _RequestNumber;
            }
            set
            {
                _RequestNumber = value;
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
        private string _ID;
        public string ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        private string _AttachmentURL;
        public string AttachmentURL
        {
            get
            {
                return _AttachmentURL;
            }
            set
            {
                _AttachmentURL = value;
            }
        }
        private string _AssignedTo;
        public string AssignedTo
        {
            get
            {
                return _AssignedTo;
            }
            set
            {
                _AssignedTo = value;
            }
        }
        private bool _IsRequestClosed;
        public bool IsRequestClosed
        {
            get
            {
                return _IsRequestClosed;
            }
            set
            {
                _IsRequestClosed = value;
            }
        }
        private string _FileName;
        public string FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                _FileName = value;
            }
        }
        private string _ClarRequested;
        public string ClarRequested
        {
            get
            {
                return _ClarRequested;
            }
            set
            {
                _ClarRequested = value;
            }
        }
        private DateTime _ClarRequestedDate;
        public DateTime ClarRequestedDate
        {
            get
            {
                return _ClarRequestedDate;
            }
            set
            {
                _ClarRequestedDate = value;
            }
        }
    
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

    
        private DateTime _RecievedDate;
        public DateTime RecievedDate
        {
            get
            {
                return _RecievedDate;
            }
            set
            {
                _RecievedDate = value;
            }
        }

        private string _Country;
        public string Country
        {
            get
            {
                return _Country;
            }
            set
            {
                _Country = value;
            }
        }

       

        private string _Nationality;
        public string Nationality
        {
            get
            {
                return _Nationality;
            }
            set
            {
                _Nationality = value;
            }
        }

        private string _FinalDecision;
        public string FinalDecision
        {
            get
            {
                return _FinalDecision;
            }
            set
            {
                _FinalDecision = value;
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

        private string _QatariID;
        public string QatariID
        {
            get
            {
                return _QatariID;
            }
            set
            {
                _QatariID = value;
            }
        }
        public string QID { get; set; }
        private string _ApplicantName;
        public string ApplicantName
        {
            get
            {
                return _ApplicantName;
            }
            set
            {
                _ApplicantName = value;
            }
        }

       
        private string _ReqReceiveDate;
        public string ReqReceiveDate
        {
            get
            {
                return _ReqReceiveDate;
            }
            set
            {
                _ReqReceiveDate = value;
            }
        }

        private DateTime _RejectionDate;
        public DateTime RejectionDate
        {
            get
            {
                return _RejectionDate;
            }
            set
            {
                _RejectionDate = value;
            }
        }

        private string _RejectionReason;
        public string RejectionReason
        {
            get
            {
                return _RejectionReason;
            }
            set
            {
                _RejectionReason = value;
            }
        }

        private string _RejectedFrom;
        public string RejectedFrom
        {
            get
            {
                return _RejectedFrom;
            }
            set
            {
                _RejectedFrom = value;
            }
        }
        public string DelayedDays { get; set; }
        public DateTime ActionDate { get; set; }

      
        public string ArabicName { get; set; }

        public string EnglishName { get; set; }

        public string ProgramType { get; set; }

     
        public string RequestSender { get; set; }
       
        public string RequestedClarification { get; set; }
        public string ClarificationReply { get; set; }
        public DateTime ReplyDate { get; set; }
        public DateTime RequestClarificationDate { get; set; }
        private string _ApplicantMobileNumber;
        public string ApplicantMobileNumber
        {
            get
            {
                return _ApplicantMobileNumber;
            }
            set
            {
                _ApplicantMobileNumber = value;
            }
        }
        private string _CertificateHolderQatarID;
        public string CertificateHolderQatarID
        {
            get
            {
                return _CertificateHolderQatarID;
            }
            set
            {
                _CertificateHolderQatarID = value;
            }
        }


        private string _StudentNameAccToCert;
        public string StudentNameAccToCert
        {
            get
            {
                return _StudentNameAccToCert;
            }
            set
            {
                _StudentNameAccToCert = value;
            }
        }
        private string _SchoolLastGrade;
        public string SchoolLastGrade
        {
            get
            {
                return _SchoolLastGrade;
            }
            set
            {
                _SchoolLastGrade = value;
            }
        }

        private string _ClarificationReason;
        public string ClarificationReason
        {
            get
            {
                return _ClarificationReason;
            }
            set
            {
                _ClarificationReason = value;
            }
        }
      

        
    }
}
