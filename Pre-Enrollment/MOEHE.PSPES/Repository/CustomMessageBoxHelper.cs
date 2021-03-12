using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace MOEHE.PSPES.Repository
{
   public static class CustomMessageBoxHelper
    {
        public static void Show(this Page Page, String Message)
        {
            //Page.ClientScript.RegisterStartupScript(
            //   Page.GetType(),
            //   "MessageBox",
            //   "<script language='javascript'>alert('" + Message + "');</script>"
            //);

            #region New Notify Function
            
            string NotificationTitle = "";

            if ((uint)CultureInfo.CurrentUICulture.LCID == 1033)
            {
                NotificationTitle = "Notification";
            }
            else
            {
                NotificationTitle = "إشعار هام";

            }
            Page.ClientScript.RegisterStartupScript(
              Page.GetType(),
              "MessageBox",
              "<script >$.notify({"+
              "title: '"+ NotificationTitle + "',"+
              "message: '"+ Message+ "'"+
              "},{"+
              "type: 'pastel-info',"+
	          "delay: 300000,"+
	          "template: '<div data-notify=\"container\" class=\"col-xs-12 col-sm-12 alert alert-{0}\" role=\"alert\">" +
              "<span data-notify=\"title\" class=\"data-notify-title\">{1}</span>" +
              "<span data-notify=\"message\" class=\"data-notify-message\">{2}</span>" +
              "<button type=\"button\" aria-hidden=\"true\" class=\"close closeNotify\" data-notify=\"dismiss\">×</button>" +
              "</div>'});</script>"
           );
           
            #endregion


        }

        public static void Confirm(this Page Page, String Message,string backURL)
        {
            Page.ClientScript.RegisterStartupScript(
               Page.GetType(),
               "MessageBox",
               "<script language='javascript'>"+

               //"function myFunction() {" +
            //   "var txt;" +
               "var r = confirm('" + Message + "');" +
               "if (r == true) {" +
               "window.location.replace(\"" + backURL+"\");" +
               " } else {" +
               " window.location.replace(\"" + backURL + "\");" +
               "}" +
             //  "}"+
               "</script>"
            );
        }

    }
}
