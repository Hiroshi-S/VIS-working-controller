using System;
using System.Collections.Generic;
using System.Net;
using System.Text;


namespace BrightcoveAPI
{
    // <summary>
    // The Video object is an aggregation of metadata and asset information associated with a video
    // </summary>
    [Serializable]
    public class BCVideo : BCObject
    {
        private long id;
        private string name;
        private string shortDescription;
        private string longDescription;
        private string creationDate;
        private string publishedDate;
        private string thumbnailURL;
        private string videoStillURL;
        private string linkURL;
        private string linkText;

        
        // <summary>
        // A number that uniquely identifies this Video, assigned by Brightcove when the Video is created.
        // </summary>
        public long ID { get { return id; } }

        // <summary>
        // The title of this Video.
        // </summary> 
        public String VideoName { get { return name; } }

        // <summary>
        // The URL to the thumbnail image associated with this Video. Thumbnails are 120x90 pixels.
        // </summary> 
        public String Thumbnail { get { return thumbnailURL; } }

        // <summary>
        // The URL to the thumbnail image associated with this Video. Thumbnails are 120x90 pixels.
        // </summary> 
        public String VideoStill { get { return videoStillURL; } }

        // <summary>
        // The date this Video was created, represented as the number of milliseconds since the UNIX epoch.
        // </summary> 
        public DateTime CreationDate { get { return  DateFromUnix(creationDate); } }
        public String CreationDateFormatted { get { return DateFromUnix(creationDate).ToString("dd MMM yyyy"); } }

        // <summary>
        // The date this Video was last made active, represented as the number of milliseconds since the UNIX epoch.
        // </summary> 
        public DateTime PublishedDate { get { return DateFromUnix(publishedDate); } }
        public String PublishedDateFormatted { get { return DateFromUnix(publishedDate).ToString("dd MMM yyyy"); } }

        // <summary>
        // A short description describing this Video, limited to 256 characters.
        // </summary> 
        public String ShortDescription { get { return (null == shortDescription) ? "" : shortDescription; } }

        // <summary>
        // A long description describing this Video.
        // </summary> 
        public String LongDescription { get { return (null == longDescription) ? "" : longDescription; } }

        // <summary>
        // An optional URL to a related item, limited to 255 characters.
        // </summary> 
        public String LinkURL { get { return linkURL; } }

        // <summary>
        // The text displayed for the linkURL, limited to 255 characters.
        // </summary> 
        public String LinkText { get { return linkText; } }

       
    }
}