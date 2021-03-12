using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHEWF.PA.Utilities;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA
{
    public partial class RequestsChart : UserControlBase
    {
        #region Public Properties
        public string SPGroupName { get; set; }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
                List<object> chartData = BL.Request.GetRequestTypesCount(LCID,SPGroupName);
                piechartLiteral.Text = Common.BL.RequestsChart.ToPieChartSeries(chartData);
        }
    }
}
