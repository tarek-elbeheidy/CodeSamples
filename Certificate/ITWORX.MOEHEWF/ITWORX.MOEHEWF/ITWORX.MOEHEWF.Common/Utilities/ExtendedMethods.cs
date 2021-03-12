using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITWORX.MOEHEWF.Common.Utilities
{
    public static class ExtendedMethods
    {
        public static SPListItem AddItemWithFolders(this SPList list, string folderUrl = "")
        {
            folderUrl = (string.IsNullOrEmpty(folderUrl) ? DateTime.Now.ToString("yyyy/MM/dd") : folderUrl);

            SPFolder folder = CreateFolderInternal(list, list.RootFolder, folderUrl);
            return list.AddItem(folder.Url, SPFileSystemObjectType.File);
        }


        /// <summary>
        /// Ensure SPFolder
        /// </summary>
        /// <param name="web"></param>
        /// <param name="listTitle"></param>
        /// <param name="folderUrl"></param>
        /// <returns></returns>
        public static SPFolder CreateFolder(this SPWeb web, string listTitle, string folderUrl)
        {
            if (string.IsNullOrEmpty(folderUrl))
                throw new ArgumentNullException("folderUrl");

            var list = web.Lists.TryGetList(listTitle);
            return CreateFolderInternal(list, list.RootFolder, folderUrl);
        }
        private static SPFolder CreateFolderInternal(SPList list, SPFolder parentFolder, string folderUrl)
        {
            var folderNames = folderUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var folderName = folderNames[0];

            var curFolder =
                parentFolder.SubFolders.Cast<SPFolder>()
                            .FirstOrDefault(
                                f =>
                                System.String.Compare(f.Name, folderName, System.StringComparison.OrdinalIgnoreCase) ==
                                0);
            if (curFolder == null)
            {
                var folderItem = list.Items.Add(parentFolder.ServerRelativeUrl, SPFileSystemObjectType.Folder,
                                                folderName);
                folderItem.SystemUpdate();
                curFolder = folderItem.Folder;
            }


            if (folderNames.Length > 1)
            {
                var subFolderUrl = string.Join("/", folderNames, 1, folderNames.Length - 1);
                return CreateFolderInternal(list, curFolder, subFolderUrl);
            }
            return curFolder;
        }
        public static DateTime ToDate(this object obj)
        {
            return Convert.ToDateTime(obj);
        }
        public static string ToMoeHeISODate(this string obj)
        {
            return SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.ParseExact(obj, "MM/dd/yyyy", CultureInfo.CurrentCulture));
        }
        public static int ToInt(this object obj)
        {
            return Convert.ToInt32(obj);
        }
    }
}