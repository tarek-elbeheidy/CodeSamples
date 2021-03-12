using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using System.Linq;
namespace MOEHE.PSPES.Features.MOEHE.PSPES.Main
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("98fc233e-f237-4cd8-b2df-9e858cb5fe05")]
    public class MOEHEPSPESEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            #region Alhanafi=>30-1-2018 : Check if Supporting documentTypeList and ApplicantAttachedDocuments are existed and  Create the List if not exist  

            string documentTypeList = "documentTypeList";
            string documentTypeListDescription = "This list created to be as lookup for document type";

            string ApplicantAttachedDocuments = "ApplicantAttachedDocuments";
            string ApplicantAttachedDocumentsDescription = "This Document Library created to save applicant requiredDocument";


            string ParentSiteURL = (properties.Feature.Parent as SPSite).Url;
            if (!CheckListLibraryExist(documentTypeList, ParentSiteURL))
            {
                CreateList(documentTypeList,ParentSiteURL,documentTypeListDescription);
            }

            if (!CheckListLibraryExist(ApplicantAttachedDocuments, ParentSiteURL))
            {
                CreateLibrary(ApplicantAttachedDocuments, ParentSiteURL, ApplicantAttachedDocumentsDescription);
            }

            #endregion

        }

        public bool CheckListLibraryExist(string Name,string SiteURL)
        {
            bool checkResult = false;
            using (SPSite site = new SPSite(SiteURL))
            {
                using (SPWeb web = site.RootWeb)
                {
                    SPList ProjectList = web.Lists.TryGetList(Name);
                    if (ProjectList != null)
                    {
                        checkResult = true;
                    }
                }
            }


            return checkResult;
        }


        public void CreateList(string ListName,string SiteURL,string ListDescription)
        {

            using (SPSite site = new SPSite(SiteURL))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    web.AllowUnsafeUpdates = true;

                    SPListCollection lists = web.Lists;

                    // create new Generic list called "My List"

                    lists.Add(ListName, ListDescription, SPListTemplateType.GenericList);

                    SPList newList = web.Lists[ListName];

                    // create Text type new column called "My Column"
                    newList.Fields.Add("ArabicNameDocumentType", SPFieldType.Text, true);
                    newList.Fields.Add("EnglishNameDocumentType", SPFieldType.Text, true);
                    newList.Fields.Add("DocumentTypeStatus", SPFieldType.Boolean, true);




                    // make new column visible in default view
                    SPView view = newList.DefaultView;
                    view.ViewFields.Add("ArabicNameDocumentType");
                    view.ViewFields.Add("EnglishNameDocumentType");
                    view.ViewFields.Add("DocumentTypeStatus");


                    view.Update();
                    web.AllowUnsafeUpdates = false;

                }
            }
        }

        public void CreateLibrary(string LibraryName, string SiteURL, string LibraryDescription)
        {

            using (SPSite site = new SPSite(SiteURL))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    web.AllowUnsafeUpdates = true;


                    Guid guid = web.Lists.Add(LibraryName, LibraryDescription, SPListTemplateType.DocumentLibrary);
                   
                    SPDocumentLibrary library = web.Lists[guid] as SPDocumentLibrary;
                    library.OnQuickLaunch = false;
                    library.Update();
                    web.AllowUnsafeUpdates = false;

                }
            }
        }



        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
