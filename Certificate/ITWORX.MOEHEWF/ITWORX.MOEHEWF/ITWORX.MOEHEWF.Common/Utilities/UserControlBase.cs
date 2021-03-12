using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace ITWORX.MOEHEWF.Common.Utilities
{
    public abstract class UserControlBase : UserControl
    {
        private int lCID = 0;
        public int LCID
        {
            get => lCID;
            private set => lCID = value;
        }


        /// <summary>
        /// Overrides the OnInit method.
        /// </summary>
        /// <param name="e">EventArgs for the event.</param>
        protected override void OnInit(EventArgs e)
        {
            if (Page.IsPostBack)
            {
                SPUtility.ValidateFormDigest();
            }

            base.OnInit(e);
            LCID = System.Threading.Thread.CurrentThread.CurrentUICulture.LCID;
            Logging.GetInstance().SetParameterInLoggerContext("Username", Context.User.Identity.Name);
        }


        #region Detecting Refresh
        private bool _refreshState;
        private bool _isRefresh;

        public bool IsRefresh
        {
            get { return _isRefresh; }
        }

        protected override void LoadViewState(object savedState)
        {
            object[] allStates = (object[])savedState;
            base.LoadViewState(allStates[0]);
            _refreshState = (bool)allStates[1];
            if (Session["__ISREFRESH"] != null)
            {
                _isRefresh = _refreshState == (bool)Session["__ISREFRESH"];
            }
        }
        protected override object SaveViewState()
        {
            Session["__ISREFRESH"] = _refreshState;
            object[] allStates = new object[2];
            allStates[0] = base.SaveViewState();
            allStates[1] = !_refreshState;
            return allStates;
        }
        #endregion


    }
}
