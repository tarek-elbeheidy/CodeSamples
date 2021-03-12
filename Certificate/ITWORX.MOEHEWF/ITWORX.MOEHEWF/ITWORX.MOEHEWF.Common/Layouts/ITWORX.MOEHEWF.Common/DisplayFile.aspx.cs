using System;
using ITWORX.MOEHE.Utilities;
using ITWORX.MOEHE.Utilities.Logging;
using ITWORX.MOEHEWF.Common.Utilities;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace ITWORX.MOEHEWF.Common.Layouts.ITWORX.MOEHEWF.Common
{
    public partial class DisplayFile : UnsecuredLayoutsPageBase
    {

        protected override bool AllowAnonymousAccess { get { return true; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Logging.GetInstance().Debug("Entering DisplayFile.Page_Load");
                SPSecurity.RunWithElevatedPrivileges(
                    () =>
                    {
                        if (Session["DisplayFile"] != null)
                        {
                            DisplayedFile displayFile = Session["DisplayFile"] as DisplayedFile;
                            HelperMethods.DisplayFile(displayFile.DocLibWebUrl, displayFile.ItemID, displayFile.DocumentLibraryName, displayFile.DownloadableName, displayFile.IsDownloadable);
                        }
                    });
            }
            catch (Exception ex)
            {
                Logging.GetInstance().LogException(ex);
            }
            finally
            {
                Logging.GetInstance().Debug("Exit method DisplayFile.Page_Load");
            }
        }

    }
}
