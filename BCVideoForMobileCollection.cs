using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrightcoveAPI
{
    /// <summary>
    /// Summary description for BCVideoForMobileCollection
    /// </summary>
    /// 
    [Serializable]
    public class BCVideoForMobileCollection : BCObject
    {
        private long total_count;
        private List<BCVideoForMobile> items;
        private int page_number;
        private int page_size;

        // <summary>
        // The total number of videos in the collection.
        // </summary>
        public long TotalCount { get { return total_count; } }

        // <summary>
        // Which page of the results this ItemCollection represents.
        // </summary>
        public int PageNumber { get { return page_number; } }

        // <summary>
        // How many items a page consists of.
        // </summary>
        public int PageSize { get { return page_size; } }

        // <summary>
        // The actual items that this collection contains.
        // </summary>
        public List<BCVideoForMobile> Items { get { return items; } }
    }
}
