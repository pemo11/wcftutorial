// File: DALDB.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Xml.Linq;

using WCFDokServiceV1.DAL;

namespace WCFDokServiceV1
{
    [ServiceBehavior(Namespace = "http://ef/documents", Name = "EFDocuments")]
    public class DokumentServiceV1 : IDokumentService
    {
        XDocument xDok = null;
        string infoMessage = "Keine Meldung";

        public DokumentServiceV1()
        {
            // Die XML-Daten einlesen
            using (StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("WCFDokServiceV1.DAL.Dokumente.xml")))
            {
                xDok = XDocument.Load(sr);
            }
        }
        public Dokument GetDokument(int DokId)
        {
            Dokument dokument = null;
            try
            {
                dokument = xDok.Descendants("dokument").Where(d => d.Attribute("id").Value == DokId.ToString()).Select(d =>
                 new Dokument
                 {
                     DokId = Int32.Parse(d.Attribute("id").Value),
                     DokAngelegtAm = DateTime.Parse(d.Attribute("angelegtAm").Value),
                     DokAutor = d.Element("autor").Value,
                     DokTitel = d.Element("titel").Value,
                     Medientyp = (MediaType)Enum.Parse(typeof(MediaType), d.Element("mediaType").Value)
                 }).SingleOrDefault();
                infoMessage = $"*** GetDokument-Abfrage liefert ein Ergebnis: {dokument != null} ***";
                LogHelper.LogInfo(infoMessage);
                return dokument;
            }
            catch (SystemException ex)
            {
                infoMessage = $"Fehler in GetDokument ({ex.Message})";
                LogHelper.LogError(infoMessage, ex);
            }
            return dokument;
        }

        public List<Dokument> GetDokumente()
        {
            List<Dokument> dokumente = new List<Dokument>();
            try
            {
                var doks = xDok.Descendants("dokument").Select(d =>
                    new Dokument
                    {
                        DokId = Int32.Parse(d.Attribute("id").Value),
                        DokAngelegtAm = DateTime.Parse(d.Attribute("angelegtAm").Value),
                        DokAutor = d.Element("autor").Value,
                        DokTitel = d.Element("titel").Value,
                        Medientyp = (MediaType)Enum.Parse(typeof(MediaType), d.Element("mediaType").Value)
                    });
                // Sollte sich in die LINQ-Abfrage einbauen lassen
                foreach (var dok in doks)
                {
                    dokumente.Add(dok);
                }
                infoMessage = $"*** GetDokumente-Abfrage liefert {dokumente.Count} Dokumente ***";
                LogHelper.LogInfo(infoMessage);
            }

            catch (SystemException ex)
            {
                infoMessage = $"Fehler in GetDokumente ({ex.Message})";
                LogHelper.LogError(infoMessage, ex);
            }
            return dokumente;
        }

        public List<Dokument> SearchDokumente(string Filter)
        {
            List<Dokument> dokumente = new List<Dokument>();
            try
            {
                var doks = xDok.Descendants("dokument").Where(d => d.Element("titel").Value.Contains(Filter)).Select(d =>
                    new Dokument
                    {
                        DokId = Int32.Parse(d.Attribute("id").Value),
                        DokAngelegtAm = DateTime.Parse(d.Attribute("angelegtAm").Value),
                        DokAutor = d.Element("autor").Value,
                        DokTitel = d.Element("titel").Value,
                        Medientyp = (MediaType)Enum.Parse(typeof(MediaType), d.Element("mediaType").Value)
                    });
                // Sollte sich in die LINQ-Abfrage einbauen lassen
                foreach (var dok in doks)
                {
                    dokumente.Add(dok);
                }
                infoMessage = $"*** SearchDokumente-Abfrage liefert {dokumente.Count} Dokumente ***";
                LogHelper.LogInfo(infoMessage);

            }
            catch (SystemException ex)
            {
                infoMessage = $"Fehler in SearchDokumente ({ex.Message})";
                LogHelper.LogError(infoMessage, ex);
            }
            return dokumente;
        }
    }
}
