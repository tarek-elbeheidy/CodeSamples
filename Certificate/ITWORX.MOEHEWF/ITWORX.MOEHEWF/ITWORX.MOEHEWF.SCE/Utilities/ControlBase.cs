using ITWORX.MOEHE.Utilities.Logging;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITWORX.MOEHEWF.SCE.Utilities
{ /// <summary>
  /// Base class for the Web Control
  /// </summary>
    public abstract class ControlBase : WebControl
    {
        /// <summary>
        /// Overrides the OnInit method.
        /// </summary>
        /// <param name="e">EventArgs for the event.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Logging.GetInstance().SetParameterInLoggerContext("Username", Context.User.Identity.Name);
        }

    }

    /// <summary>
    /// Base class for the Web Control
    /// </summary>
    public abstract class WebPartBase : System.Web.UI.WebControls.WebParts.WebPart
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetSessionTimeOut();
        }
        /// <summary>
        /// Overrides the OnInit method.
        /// </summary>
        /// <param name="e">EventArgs for the event.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.ExportMode = System.Web.UI.WebControls.WebParts.WebPartExportMode.None;
            this.ChromeType = System.Web.UI.WebControls.WebParts.PartChromeType.None;
            LCID = System.Threading.Thread.CurrentThread.CurrentUICulture.LCID;
            Logging.GetInstance().SetParameterInLoggerContext("Username", Context.User.Identity.Name);
        }
        /// <summary>
        ///  Set Session TimeOut
        /// </summary>
        public void SetSessionTimeOut()
        {
            try
            {
                Logging.GetInstance().Debug("Entering UserControlBase.SetSessionTimeOut");

                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session.IsNewSession)
                    {
                        if (!string.IsNullOrEmpty(CurrentSessionID))
                        {
                            HttpContext.Current.Response.Redirect("../_layouts/ITWORX.MOEHEWF.SCE/Timeout.aspx", false);
                            HttpContext.Current.Response.End();
                        }
                    }
                    CurrentSessionID = HttpContext.Current.Session.SessionID;
                }
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit UserControlBase.SetSessionTimeOut");
            }
        }
        int lCID = 0;

        public int LCID
        {
            get { return lCID; }
            private set { lCID = value; }
        }
        public string CurrentSessionID
        {
            get
            {
                if (ViewState["CurrentSessionID"] != null && !string.IsNullOrEmpty(ViewState["CurrentSessionID"].ToString()))
                    return ViewState["CurrentSessionID"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["CurrentSessionID"] = value;
            }
        }
    }

    /// <summary>
    /// Base class for the Web Control
    /// </summary>
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
