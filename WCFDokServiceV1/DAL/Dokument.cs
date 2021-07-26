// File: Dokument.cs

using System;

namespace WCFDokServiceV1.DAL
{
    public enum MediaType
    {
        PdfDokument,
        OfficeDokument,
        Sonstiges
    }

    public class Dokument
    {
        public int DokId { get; set; }
        public string DokTitel { get; set; }
        public string DokAutor { get; set; }

        public MediaType Medientyp { get; set; }

        public DateTime DokAngelegtAm { get; set; }

    }
}
