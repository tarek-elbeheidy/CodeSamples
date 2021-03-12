using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Options;
using System.Drawing;
using DotNet.Highcharts.Helpers;

namespace ITWORX.MOEHEWF.Common.BL
{
   public class RequestsChart
    {
        public static string ToPieChartSeries(List<object> data)
        {
            string chartData = string.Empty;
            var returnObject = new List<object>();
            if (data != null)
            {
                foreach (Entities.RequestsChart item in data)
                {
                    returnObject.Add(new object[] { item.RequestType, item.RequestCount });
                }

                DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
           .InitChart(new Chart { PlotShadow = false })
           .SetTitle(new Title { Text=string.Empty})
           .SetCredits( new Credits { Enabled = false })
           .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }" })
           .SetPlotOptions(new PlotOptions
           {
               Pie = new PlotOptionsPie
               {
                   AllowPointSelect = true,
                   ShowInLegend = true,
                   Cursor = Cursors.Pointer,
                   DataLabels = new PlotOptionsPieDataLabels
                   {
                       Enabled = false
                   //Color = ColorTranslator.FromHtml("#000000"),
                   //ConnectorColor = ColorTranslator.FromHtml("#000000"),
                   //Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }"
               }
               }
           })
           .SetSeries(new Series
           {
               Type = ChartTypes.Pie,
               Data = new Data(returnObject.ToArray()),
           });
                chartData = chart.ToHtmlString();
            }
            return chartData;
        }

    }

}
