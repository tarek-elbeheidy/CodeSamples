using Microsoft.SharePoint;
using Nintex.Workflow.HumanApproval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWorx.MOEHEWF.Nintex.Actions
{
    public class NintexContext
    {
        // The Nintex Workflow representation of the task
        private NintexTask _task = null;
        // The Nintex Workflow representation of the approver
        private Approver _approver = null;
        // The SharePoint task list containing the workflow task
        private SPList _taskList = null;
        // The sharepoint task the user interacts with
        private SPListItem _spTaskItem = null;
        // The item the workflow is running on (null for site workflows)
        private SPListItem _item = null;
        // The item id of the item the workflow is running on
        private int _workflowItemId = -1;
        // The list id of the item the workflow is running on
        private Guid _workflowListId = Guid.Empty;

        private SPWeb _web;

        public SPWeb Web
        {
            get { return _web; }
            set { _web = value; }
        }

        public NintexTask Task
        {
            get { return _task; }
            set { _task = value; }
        }

        public bool TaskAuthorized { get; set; }

        public Approver Approver
        {
            get { return _approver; }
            set { _approver = value; }
        }

        public SPList TaskList
        {
            get { return _taskList; }
            set { _taskList = value; }
        }

        public SPListItem TaskItem
        {
            get { return _spTaskItem; }
            set { _spTaskItem = value; }
        }

        public SPListItem Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public int WorkflowItemId
        {
            get { return _workflowItemId; }
            set { _workflowItemId = value; }
        }

        public Guid WorkflowListId
        {
            get { return _workflowListId; }
            set { _workflowListId = value; }
        }

        public SPUser CurrentUser { get; set; }

    }
}
