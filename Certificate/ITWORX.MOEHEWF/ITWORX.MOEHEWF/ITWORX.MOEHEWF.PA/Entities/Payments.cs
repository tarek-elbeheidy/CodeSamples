using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.PA.Entities
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
        
    }
}
