using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.Entities
{
   public class HistoricalRecord
    {
        private string _Comments;
        public string Comments
        {
            get
            {
                return _Comments;
            }
            set
            {
                _Comments = value;
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
        private string _AuthorityTask;
        public string AuthorityTask
        {
            get
            {
                return _AuthorityTask;
            }
            set
            {
                _AuthorityTask = value;
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
        private string _ExecutedAction;
        public string ExecutedAction
        {
            get
            {
                return _ExecutedAction;
            }
            set
            {
                _ExecutedAction = value;
            }
        }
        private string _Executor;
        public string Executor
        {
            get
            {
                return _Executor;
            }
            set
            {
            
                _Executor = value;
            }
        }
        private string _ApplicantCanView;
        public string ApplicantCanView
        {
            get
            {
                return _ApplicantCanView;
            }
            set
            {

                _ApplicantCanView = value;
            }
        }
    }
}
