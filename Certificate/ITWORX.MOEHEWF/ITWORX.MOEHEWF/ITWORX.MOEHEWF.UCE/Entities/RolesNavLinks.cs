using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.UCE.Entities
{
   public class RolesNavLinks
    {

        private string _PageLink;
        public string PageLink
        {
            get
            {
                return _PageLink;
            }
            set
            {
                _PageLink = value;
            }
        }

        private string _PageTitle;
        public string PageTitle
        {
            get
            {
                return _PageTitle;
            }
            set
            {
                _PageTitle = value;
            }
        }

        private string _Active;
        public string Active
        {
            get
            {
                return _Active;
            }
            set
            {
                _Active = value;
            }
        }

        private string _Order;
        public string Order
        {
            get
            {
                return _Order;
            }
            set
            {
                _Order = value;
            }
        }

        private string _GroupName;
        public string GroupName
        {
            get
            {
                return _GroupName;
            }
            set
            {
                _GroupName = value;
            }
        }
    }
}
