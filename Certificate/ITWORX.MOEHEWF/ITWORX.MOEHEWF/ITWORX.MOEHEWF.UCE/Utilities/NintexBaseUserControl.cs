using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

using Nintex.Workflow;
using Nintex.Workflow.Common;
using Nintex.Workflow.HumanApproval;

using ITWORX.MOEHEWF.UCE.Utilities;

namespace Portiva.CustomTaskForm.ControlTemplates
{
    public enum TaskResult
    {
        Completed,
        Rejected,
        Invalid
    }

    public class NintexBaseUserControl : UserControl
    {
        public NintexContext CurrentContext { get; set; }
        /// <summary>
        /// Virtual save task
        /// </summary>
        /// <param name="taskCloseMode"></param>
        /// <returns></returns>
        public virtual TaskResult SaveTask() 
        {
            return TaskResult.Invalid;
        }
    }
}
