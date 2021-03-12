using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace MOEHE.PSPES.Features.MOEHE.PSPES.LanguageSwitcher
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("5a233308-b89c-4dc6-af5b-8436040eaf11")]
    public class MOEHEPSPESEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

             private const string WebConfigModificationOwner = "MyTestOwner";

        private static readonly SPWebConfigModification[] Modifications = {
            // For not so obvious reasons web.config modifications inside collections 
            // are added based on the value of the key attribute in alphabetic order.
            // Because we need to add the DualLayout module after the 
            // PublishingHttpModule, we prefix the name with 'Q-'.
            new SPWebConfigModification()
                { 
                    // The owner of the web.config modification, useful for removing a 
                    // group of modifications
                    Owner = WebConfigModificationOwner, 
                    // Make sure that the name is a unique XPath selector for the element 
                    // we are adding. This name is used for removing the element
                    Name = "add[@name='HTTPSwitcherModule']",
                    // We are going to add a new XML node to web.config
                    Type = SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode, 
                    // The XPath to the location of the parent node in web.config
                     Path = "configuration/system.webServer/modules",   
                    // Sequence is important if there are multiple equal nodes that 
                    // can't be identified with an XPath expression
                    Sequence = 0,
                    // The XML to insert as child node, make sure that used names match the Name selector
                     Value = "<add name='HTTPSwitcherModule' type='MOEHE.PSPES.LangSwitcherPage.HTTPSwitcherModule, MOEHE.PSPES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=677b80a9c9c0da8c' />"
                }
        };
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
         
         


            SPSite sps = (SPSite)properties.Feature.Parent;
            SPWebApplication webApp = sps.WebApplication;

            if (webApp != null)
            {
               // AddWebConfigModifications(webApp, Modifications);
            }

        }


        private void AddWebConfigModifications(SPWebApplication webApp, IEnumerable<SPWebConfigModification> modifications)
        {
            foreach (SPWebConfigModification modification in modifications)
            {
                webApp.WebConfigModifications.Add(modification);
            }

            // Commit modification additions to the specified web application.
            webApp.Update();
            // Push modifications through the farm.
            webApp.WebService.ApplyWebConfigModifications();
        }

        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPWebApplication webApp = properties.Feature.Parent as SPWebApplication;
            if (webApp != null)
            {
               // RemoveWebConfigModificationsByOwner(webApp, WebConfigModificationOwner);
            }
        }

        private void RemoveWebConfigModificationsByOwner(SPWebApplication webApp, string owner)
        {
            Collection<SPWebConfigModification> modificationCollection = webApp.WebConfigModifications;
            Collection<SPWebConfigModification> removeCollection = new Collection<SPWebConfigModification>();

            int count = modificationCollection.Count;
            for (int i = 0; i < count; i++)
            {
                SPWebConfigModification modification = modificationCollection[i];
                if (modification.Owner == owner)
                {
                    // Collect modifications to delete.
                    removeCollection.Add(modification);
                }
            }

            // Delete the modifications from the web application.
            if (removeCollection.Count > 0)
            {
                foreach (SPWebConfigModification modificationItem in removeCollection)
                {
                    webApp.WebConfigModifications.Remove(modificationItem);
                }

                // Commit modification removals to the specified web application.
                webApp.Update();
                // Push modifications through the farm.
                webApp.WebService.ApplyWebConfigModifications();
            }
        }


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
