using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.SCE.Entities
{
   public class NewRequests
    {
        //private string _EntityNeedsEquivalency;
        //public string EntityNeedsEquivalency
        //{
        //    get
        //    {
        //        return _EntityNeedsEquivalency;
        //    }
        //    set
        //    {
        //        _EntityNeedsEquivalency = value;
        //    }
        //}

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
        }/*
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
        private string _EntityNeedsCertificate;
        public string EntityNeedsCertificate
        {
            get
            {
                return _EntityNeedsCertificate;
            }
            set
            {
                _EntityNeedsCertificate = value;
            }
        }
        private string _University;
        public string University
        {
            get
            {
                return _University;
            }
            set
            {
                _University = value;
            }
        }*/
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
        /*
        private string _Faculty;
        public string Faculty
        {
            get
            {
                return _Faculty;
            }
            set
            {
                _Faculty = value;
            }
        }*/
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
        /*
        private string _StudySystem;
        public string StudySystem
        {
            get
            {
                return _StudySystem;
            }
            set
            {
                _StudySystem = value;
            }
        }

        private string _StudyLanguage;
        public string StudyLanguage
        {
            get
            {
                return _StudyLanguage;
            }
            set
            {
                _StudyLanguage = value;
            }
        }

        private string _AcademicDegree;
        public string AcademicDegree
        {
            get
            {
                return _AcademicDegree;
            }
            set
            {
                _AcademicDegree = value;
            }
        }
        */
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

        private string _CertificateResource;
        public string CertificateResource
        {
            get
            {
                return _CertificateResource;
            }
            set
            {
                _CertificateResource = value;
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
        /*
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

        private string _StudyType;
        public string StudyType
        {
            get
            {
                return _StudyType;
            }
            set
            {
                _StudyType = value;
            }
        }
        */
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

    

        /*
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
        */
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
        /*
        private string _CertifNeedsEquivalency;
        public string CertifNeedsEquivalency
        {
            get
            {
                return _CertifNeedsEquivalency;
            }
            set
            {
                _CertifNeedsEquivalency = value;
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
        }*/
        
        private string _DelayedDays;
        public string DelayedDays
        {
            get
            {
                return _DelayedDays;
            }
            set
            {
                _DelayedDays = value;
            }
        }
        private DateTime _ActionDate;
        public DateTime ActionDate
        {
            get
            {
                return _ActionDate;
            }
            set
            {
                _ActionDate = value;
            }
        }

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
        /*
        private string _OrgBookReply;
        public string OrgBookReply
        {
            get
            {
                return _OrgBookReply;
            }
            set
            {
                _OrgBookReply = value;
            }
        }*/

        private string _ArabicName;
        public string ArabicName
        {
            get
            {
                return _ArabicName;
            }
            set
            {
                _ArabicName = value;
            }
        }

        private string _EnglishName;
        public string EnglishName
        {
            get
            {
                return _EnglishName;
            }
            set
            {
                _EnglishName = value;
            }
        }

      

        // public string ProgramType { get; set; }

    }
}
