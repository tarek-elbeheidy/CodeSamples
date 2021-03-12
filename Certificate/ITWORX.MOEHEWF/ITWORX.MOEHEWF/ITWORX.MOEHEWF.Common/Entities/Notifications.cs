using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.Entities
{
    public class Notifications
    {
        public string Subject { set; get; }
        public string Body { set; get; }
        public RequestStatus RequestStatusId { set; get; }
        public bool Type { set; get; }
    }
}
