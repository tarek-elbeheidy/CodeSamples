using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ITWORX.MOEHEWF.Common.Utilities
{ 
    public enum AttachmentStatus
    {
        Uploaded=1,
        Deleted=2,
        Saved
    }

    [Serializable]
    public class DisplayedFile
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the document library name.
        /// </summary>
        /// <value>
        /// The document library name.
        /// </value>
        public string DocumentLibraryName { get; set; }

        /// <summary>
        /// Gets or sets the document library web Url.
        /// </summary>
        /// <value>
        /// The document library web Url.
        /// </value>
        public string DocLibWebUrl { get; set; }

        /// <summary>
        /// Gets or sets the document item ID.
        /// </summary>
        /// <value>
        /// The document item ID.
        /// </value>
        public int ItemID { get; set; }

        /// <summary>
        /// Gets or sets the document downloadable name.
        /// </summary>
        /// <value>
        /// The document downloadable name.
        /// </value>
        public string DownloadableName { get; set; }

        // <summary>
        /// Gets or sets the document is downloadable or not.
        /// </summary>
        /// <value>
        /// The document downloadable or display it only.
        /// </value>
        public bool IsDownloadable { get; set; }
        #endregion
    }
    public class FileUploaded
    {
        /// <summary>
        /// Gets or sets the file identifier.
        /// </summary>
        /// <value>
        /// The file identifier.
        /// </value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the request ID of the file.
        /// </summary>
        /// <value>
        /// The file request ID.
        /// </value>
        public int RequestID { get; set; }

        /// <summary>
        /// Gets or sets the group name of the file.
        /// </summary>
        /// <value>
        /// The file group name.
        /// </value>
        public string Group { get; set; }
        /// <summary>
        /// Gets or sets the title of the file.
        /// </summary>
        /// <value>
        /// The title of the file.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the type of the MIME.
        /// </summary>
        /// <value>
        /// The type of the MIME.
        /// </value>
        public string MIMEType { get; set; }

        /// <summary>
        /// Gets or sets the size of the file.
        /// </summary>
        /// <value>
        /// The type of the MIME.
        /// </value>
        public long Size { get; set; }

        /// <summary>
        /// Gets or sets the file Url.
        /// </summary>
        /// <value>
        /// The file Url.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets file modified date and time.
        /// </summary>
        /// <value>
        /// The file modified date and time.
        /// </value>    
        public DateTime Modified
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets file modified by who.
        /// </summary>
        /// <value>
        /// The file modified by who.
        /// </value>    
        public string ModifiedBy
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets file created date and time.
        /// </summary>
        /// <value>
        /// The file created date and time.
        /// </value>    
        public DateTime Created
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets file created by who.
        /// </summary>
        /// <value>
        /// The file created by who.
        /// </value>    
        public string CreatedBy
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets file status.
        /// </summary>
        /// <value>
        /// The file status(Uploaded, Deleted, or Saved).
        /// </value>    
        public AttachmentStatus FileStatus
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets replace existing file.
        /// </summary>
        /// <value>
        /// replace existing file.
        /// </value>    
        public bool ReplaceExistingFile
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the file bytes.
        /// </summary>
        /// <value>
        /// The file bytes.
        /// </value>    
        public byte[] FileBytes { get; set; }

        /// <summary>
        /// Gets or sets the file name as guid to be unique.
        /// </summary>
        /// <value>
        /// The file name as guid.
        /// </value>    
        public string NameGuid { get; set; }

        /// <summary>
        /// Gets or sets the file template ID if exists as it is a lookup field from the template library.
        /// </summary>
        /// <value>
        /// The file name as guid.
        /// </value>    
        public int TemplateID { get; set; }
    }
}
