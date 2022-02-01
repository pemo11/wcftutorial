// File: Document.cs

using System;

namespace WCFDocServiceV2.DAL
{
    public enum MediaType
    {
        Pdf,
        Office,
        Miscellaneous
    }

    public class Document
    {
        public int DocId { get; set; }
        public string DocTitle { get; set; }
        public string DocAuthor { get; set; }

        public MediaType MediaId { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
