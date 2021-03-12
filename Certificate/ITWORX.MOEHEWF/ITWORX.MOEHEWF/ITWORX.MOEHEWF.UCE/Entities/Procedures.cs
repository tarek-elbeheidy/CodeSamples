using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.Entities
{
    public class Procedures
    {
        
       private string _headManagerName;
        public string HeadManagerName
        {
            get
            {
                return _headManagerName;
            }
            set
            {
                _headManagerName = value;
            }
        }
        private string _bookDate;
        public string BookDate
        {
            get
            {
                return _bookDate;
            }
            set
            {
                _bookDate = value;
            }
        }

        private string _bookNum;
        public string BookNum
        {
            get
            {
                return _bookNum;
            }
            set
            {
                _bookNum = value;
            }
        }

        private string _occupationName;
        public string OccupationName
        {
            get
            {
                return _occupationName;
            }
            set
            {
                _occupationName = value;
            }
        }

        private string _DecisionForPrint;
        public string DecisionForPrint
        {
            get
            {
                return _DecisionForPrint;
            }
            set
            {
                _DecisionForPrint = value;
            }
        }

        private string _ProcedureComments;
        public string ProcedureComments
        {
            get
            {
                return _ProcedureComments;
            }
            set
            {
                _ProcedureComments = value;
            }
        }
        private string _RecommendationStatus;
        public string RecommendationStatus
        {
            get
            {
                return _RecommendationStatus;
            }
            set
            {
                _RecommendationStatus = value;
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
        private string _EmpAssignedTo;
        public string EmpAssignedTo
        {
            get
            {
                return _EmpAssignedTo;
            }
            set
            {
                _EmpAssignedTo = value;
            }
        }
        private string _ProcedureCreatedby;
        public string ProcedureCreatedby
        {
            get
            {
                return _ProcedureCreatedby;
            }
            set
            {
                _ProcedureCreatedby = value;
            }
        }
        private string _ProcedureDate;
        public string ProcedureDate
        {
            get
            {
                return _ProcedureDate;
            }
            set
            {
                _ProcedureDate = value;
            }
        }
        private string _Procedure;
        public string Procedure
        {
            get
            {
                return _Procedure;
            }
            set
            {
                _Procedure = value;
            }
        }
        private string _Opinion;
        public string Opinion
        {
            get
            {
                return _Opinion;
            }
            set
            {
                _Opinion = value;
            }
        }
        private string _LinkFilename;
        public string LinkFilename
        {
            get
            {
                return _LinkFilename;
            }
            set
            {
                _LinkFilename = value;
            }
        }
        private string _FNameExt;
        public string FNameExt
        {
            get
            {
                return _FNameExt;
            }
            set
            {
                _FNameExt = value;
            }
        }
        private string _Filename;
        public string Filename
        {
            get
            {
                return _Filename;
            }
            set
            {
                _Filename = value;
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
        private string _Editor;
        public string Editor
        {
            get
            {
                return _Editor;
            }
            set
            {
                _Editor = value;
            }
        }
        private DateTime _Modified;
        public DateTime Modified
        {
            get
            {
                return _Modified;
            }
            set
            {
                _Modified = value;
            }
        }
        private string _NumberOfHoursGained;

        public string NumberOfHoursGained
        {
            get
            {
                return _NumberOfHoursGained;
            }
            set
            {
                _NumberOfHoursGained = value;
            }
        }
        private string _NumberOfOnlineHours;

        public string NumberOfOnlineHours
        {
            get
            {
                return _NumberOfOnlineHours;
            }
            set
            {
                _NumberOfOnlineHours = value;
            }
        }
        private string _PercentageOfOnlineHours;

        public string PercentageOfOnlineHours
        {
            get
            {
                return _PercentageOfOnlineHours;
            }
            set
            {
                _PercentageOfOnlineHours = value;
            }
        }
        private string _OrdinaryOrOwners;

        public string OrdinaryOrOwners
        {
            get
            {
                return _OrdinaryOrOwners;
            }
            set
            {
                _OrdinaryOrOwners = value;
            }
        }

        public string HavePA { get; set; }

        public string TypeUniversity { get; set; }
        public string HasException { get; set; }
        public string ExceptionFrom { get; set; }
        public string SirValue { get; set; }
        public string RespectedValue { get; set; }
    }
}
